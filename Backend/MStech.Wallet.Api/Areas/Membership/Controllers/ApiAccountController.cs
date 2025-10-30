using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Implementation.AccessedLinkService;
using Implementation.RoleUserService;
using Implementation.TokenManagement;
using Implementation.UserService;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Mstech.Entity.Etity;
using Mstech.ViewModel.DTO;
using DNTCaptcha.Core;
using Microsoft.Extensions.Options;
using Implementation.RoleService;
using Implementation.WalletServiceArea;
using Authorization.Custom;
using Implementation.WalletClientArea;
using Implementation.Notification;
using System.Net;

[Area("api/Admin")]
[Route("[area]/Membership")]
public class ApiAccountController : BaseController
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly RoleUserService _roleUserService;
    private readonly RoleService _roleService;
    private readonly TokenService _tokenService;
    public readonly RoleAccessedLinkService roleAccessedLinkService;
    private readonly UserService _userService;
    private readonly DNTCaptchaOptions _captchaOptions;
    private readonly IDNTCaptchaValidatorService _validatorService;
    private readonly WalletService walletService;
    private readonly WalletClientService walletClientService;
    private readonly EmailService emailService;
    private readonly ModirPayamakService modirPayamakService;
    public ApiAccountController(IDNTCaptchaValidatorService validatorService,
        IOptions<DNTCaptchaOptions> options,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        RoleUserService roleUserService,
        TokenService tokenService,
        RoleAccessedLinkService roleAccessedLinkService,
        UserService userService,
        RoleService roleService,
        WalletService walletService,
        WalletClientService walletClientService,
        EmailService emailService,
        ModirPayamakService modirPayamakService
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleUserService = roleUserService;
        _tokenService = tokenService;
        this.roleAccessedLinkService = roleAccessedLinkService;
        _userService = userService;
        _validatorService = validatorService;
        _captchaOptions = options?.Value ?? throw new ArgumentNullException(nameof(options));
        this.walletService = walletService;
        this._roleService = roleService;
        this.walletClientService = walletClientService;
        this.emailService = emailService;
        this.modirPayamakService = modirPayamakService;
    }

    [HttpPost]
    [Route("Login")]
    [AllowAnonymous]
    public async Task<JsonResult> Login([FromBody] LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return Json(new ResponseViewModel<LoginViewModel>
            {
                Message = "خطای ورود اطلاعات",
                IsSuccess = false
            });
        }

        var user = await _userManager.FindByNameAsync(model.UserName);

        if (user != null && user.IsActiveOutsideWallet)
        {
            return Json(new ResponseViewModel<LoginViewModel>
            {
                Message = "",
                IsSuccess = false,
                Entity = new LoginViewModel
                {
                    NeedsToChangePassowrd = true
                }
            });

        }

        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {

            var roles = await _roleUserService.GetAllAsIqueriable()
                .Include(m => m.Role)
                .Where(m => m.UserId == user.Id)
                .ToListAsync();

            var claimList = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("username", user.UserName),
                new Claim("name", user.Name),
                new Claim("lastname", user.LastName)
            };

            var rolenameList = await _roleUserService.GetAllUserRoles(user.Id);
            foreach (var item in rolenameList.Entity)
            {
                claimList.Add(new Claim(ClaimTypes.Role, item));
            }

            string jwtSecret = "bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b"; // At least 16 characters
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha512Signature);

            // Create token
            var sectoke = new JwtSecurityToken(

               issuer: "https://localhost:7081",
               audience: "api",
                claims: claimList,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(sectoke);
            var refreshToken = Guid.NewGuid().ToString();



            await _tokenService.AddRefreshToken(refreshToken, user.Adapt<UserViewModel>());

            return Json(new ResponseViewModel<LoginViewModel>
            {
                Message = "",
                IsSuccess = true,
                Entity = new LoginViewModel
                {
                    Token = tokenString,
                    UserName = user.UserName,
                    RefreshToken = refreshToken
                }
            });
        }
        else
        {
            return Json(new ResponseViewModel<LoginViewModel>
            {
                Message = "نام کاربری یا رمز عبور اشتباه است",
                IsSuccess = false
            });
        }
    }

    [HttpPost]
    [Route("Token")]
    [AllowAnonymous]
    public async Task<JsonResult> Token([FromBody] TokenViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return Json(new ResponseViewModel<LoginViewModel>
            {
                Message = "خطای ورود اطلاعات",
                IsSuccess = false
            });
        }
        if (model.client_id != "mvc" || model.client_secret != "mvc-client-secret")
        {
            Request.HttpContext.Response.StatusCode = 400;
            await Request.HttpContext.Response.WriteAsJsonAsync(new { error = "invalid_client" });
            return Json(new { error = "invalid_client" });
        }

        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var roles = await _roleUserService.GetAllAsIqueriable()
                .Include(m => m.Role)
                .Where(m => m.UserId == user.Id)
                .ToListAsync();

            var claimList = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("username", user.UserName),
                new Claim("name", user.Name),
                new Claim("lastname", user.LastName)
            };

            var rolenameList = await _roleUserService.GetAllUserRoles(user.Id);
            foreach (var item in rolenameList.Entity)
            {
                claimList.Add(new Claim(ClaimTypes.Role, item));
            }

            string jwtSecret = "bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b"; // At least 16 characters
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha512Signature);

            // Create token
            var sectoke = new JwtSecurityToken(

               issuer: "https://localhost:7081",
               audience: "api",
                claims: claimList,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(sectoke);
            var refreshToken = Guid.NewGuid().ToString();



            await _tokenService.AddRefreshToken(refreshToken, user.Adapt<UserViewModel>());

            return Json(new ResponseViewModel<LoginViewModel>
            {
                Message = "",
                IsSuccess = true,
                Entity = new LoginViewModel
                {
                    Token = tokenString,
                    UserName = user.UserName,
                    RefreshToken = refreshToken
                }
            });
        }
        else
        {
            return Json(new ResponseViewModel<LoginViewModel>
            {
                Message = "نام کاربری یا رمز عبور اشتباه است",
                IsSuccess = false
            });
        }
    }

    [Route("RefreshToken")]
    [AllowAnonymous]
    [HttpGet]
    public async Task<JsonResult> RefreshToken([FromHeader] string? Authorization, [FromHeader] string? xRefreshToken)
    {
        if (string.IsNullOrEmpty(Authorization) || !Authorization.StartsWith("Bearer "))
        {
            return Json(new ResponseViewModel<LoginViewModel>
            {
                Message = "Access token not found or improperly formatted.",
                IsSuccess = false
            });
        }

        var jwtToken = Authorization.Substring("Bearer ".Length).Trim();

        if (string.IsNullOrEmpty(xRefreshToken))
        {
            return Json(new ResponseViewModel<LoginViewModel> { Message = "Refresh token not found.", IsSuccess = false });
        }

        var user = await _tokenService.GetUserFromRefreshToken(xRefreshToken);
        if (user == null)
        {
            return Json(new ResponseViewModel<LoginViewModel> { Message = "Invalid refresh token.", IsSuccess = false });
        }

        var roles = await _roleUserService.GetAllAsIqueriable()
            .Include(m => m.Role)
            .Where(m => m.UserId == user.Id)
            .ToListAsync();

        var claimList = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("name", user.Name),
            new Claim("lastname", user.LastName),
        };

        var refreshToken = Guid.NewGuid().ToString();
        claimList.Add(new Claim("RefreshToken", refreshToken));

        foreach (var item in roles)
        {
            claimList.Add(new Claim(ClaimTypes.Role, item.Role.Name));
        }

        var jwtSecret = "bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34"; // Replace with your secure key
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var newToken = new JwtSecurityToken(
            issuer: "your-issuer",
            audience: "your-audience",
            claims: claimList,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(newToken);
        Response.Headers["X-Refresh-Token"] = refreshToken;

        await _tokenService.RemoveRefreshToken(user);
        await _tokenService.AddRefreshToken(refreshToken, user);

        return Json(new ResponseViewModel<LoginViewModel>
        {
            Message = "Refresh token successful",
            IsSuccess = true,
            Entity = new LoginViewModel
            {
                Token = tokenString,
                RefreshToken = refreshToken
            }
        });
    }

    [HttpPost]
    [AllowAnonymous]
    [SessionRequirement]
    [Route("Register")]
    public async Task<JsonResult> Register([FromBody] RegisterViewModel model)
    {
        model.UserName = model.Email;

        var client = new WalletClientViewModel();

        if (Request.Headers.TryGetValue("clientidforapis", out var clientId))
        {
        }
        else
        {
            return Json(new ResponseViewModel<RegisterViewModel>
            {
                Message = "کلاینت مجاز نمیباشد",
                IsSuccess = false
            });
        }
        client = walletClientService.GetAll(new WalletClientViewModel() { ClientIdForApi = clientId }).Result.Entity.FirstOrDefault();

        if (!ModelState.IsValid)
        {
            return Json(new ResponseViewModel<RegisterViewModel>
            {
                Message = "خطای ورود اطلاعات",
                IsSuccess = false
            });
        }

        if (!string.IsNullOrEmpty(model.Email) && await _userService.IsEmailExist(model.Email))
        {
            return Json(new ResponseViewModel<RegisterViewModel> { Message = "این نام کاربری قبلا ثبت شده است", IsSuccess = false });
        }

        if (await _userService.IsPhoneNumberExist(model.PhoneNumber))
        {
            return Json(new ResponseViewModel<RegisterViewModel> { Message = "نام کاربری موجود است", IsSuccess = false });
        }

        var user = new User
        {
            UserName = model.PhoneNumber,
            Email = model.Email,
            EmailConfirmed = true,
            PhoneNumber = model.PhoneNumber,
            Name = model.Name,
            LastName = model.LastName,
            FullName = $"{model.Name} {model.LastName}",
            IsDeleted = false,
            IsActive = true,

        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            var confimationCode = GeneratePhoneNumberConfirmation();

            user.CurrentConfirmationCode = confimationCode;

            user.ConfirmationCodeExpireDate = DateTime.Now.AddMinutes(3);

            var role = _roleService.GetAllAsIqueriable().Where(m => m.Name == "User");
            if (role is not null)
            {
                var addRoleResult = _roleUserService.Insert(new RoleUser() { UserId = user.Id, RoleId = role.First().Id });
            }
            var walletGenerationResult = await walletService.CreateNewWalletForNewUser(user.Id, clientId);
            var token = confimationCode;


            var emailMessage = $"کد تایید شما  میباشد{token}";

            modirPayamakService.SendRegisterConfirmation(toNumber: user.PhoneNumber, code: token.ToString());

            return Json(new ResponseViewModel<object>
            {
                Message = " ساخت حساب کاربری با موفیت انجام کد تایید را وارد کنید",
                IsSuccess = true,
            });
        }

        var tempUser = await _userManager.FindByNameAsync(model.UserName);

        if (tempUser != null)
        {
            var userHasWalletForThisCLient = await walletService.GetAll(new WalletViewModel() { Client = new WalletClientViewModel() { ClientIdForApi = clientId }, UserId = tempUser.Id });

            if (userHasWalletForThisCLient.Success && (userHasWalletForThisCLient.Entity == null || !userHasWalletForThisCLient.Entity.Any()))
            {
                var walletGenerationResult = await walletService.CreateNewWalletForNewUser(user.Id, clientId);
            }
        }

        var errorMessage = string.Join("<br>", result.Errors.Select(e => e.Description));

        return Json(new ResponseViewModel<RegisterViewModel>
        {
            Message = string.IsNullOrEmpty(errorMessage) ? "ساخت حساب کاربری با مشکل مواجه شد لطفا بعدامتحان کنید" : errorMessage,
            IsSuccess = false
        });

    }

    [HttpPost]
    [AllowAnonymous]
    [SessionRequirement]
    [Route("RegisterFromClient")]
    public async Task<JsonResult> RegisterFromClient([FromBody] RegisterFromClientViewModel model)
    {
        model.UserName = model.UserName;
        model.LastName = model.Name;
        model.Name = "";
        model.Password = "@Trabase_123123";
        model.ConfirmPassword = "@Trabase_123123";

        var client = new WalletClientViewModel();

        if (Request.Headers.TryGetValue("clientidforapis", out var clientId))
        {
        }
        else
        {
            return Json(new ResponseViewModel<RegisterViewModel>
            {
                Message = "کلاینت مجاز نمیباشد",
                IsSuccess = false
            });
        }

        client = walletClientService.GetAll(new WalletClientViewModel() { ClientIdForApi = clientId, UserId = "" }).Result.Entity.FirstOrDefault();


        if (!string.IsNullOrEmpty(model.Email) && await _userService.IsEmailExist(model.Email))
        {
            return Json(new ResponseViewModel<RegisterViewModel> { Message = "این نام کاربری قبلا ثبت شده است", IsSuccess = false });
        }

        if (await _userService.IsPhoneNumberExist(model.PhoneNumber))
        {
            return Json(new ResponseViewModel<RegisterViewModel> { Message = "نام کاربری موجود است", IsSuccess = false });
        }

        var tempUser = await _userManager.FindByNameAsync(model.PhoneNumber);

        if (tempUser != null)
        {
            var walletGenerationResult = await walletService.CreateNewWalletForNewUser(tempUser.Id, clientId);

            return Json(new ResponseViewModel<object>
            {
                Message = " حساب کاربری کاربر شما با موفقیت ساخته شد",
                IsSuccess = true,
            });
        }

        if (tempUser == null)
        {
            var user = new User
            {
                UserName = model.PhoneNumber,
                Email = model.Email,
                EmailConfirmed = true,
                PhoneNumber = model.PhoneNumber,
                Name = model.Name,
                LastName = model.LastName,
                FullName = $"{model.Name} {model.LastName}",
                IsDeleted = false,
                IsActive = model.IsActive,
                IsActiveOutsideWallet = true

            };

            var result = await _userManager.CreateAsync(user, "WalletTra@123123123");

            if (result.Succeeded)
            {

                var role = _roleService.GetAllAsIqueriable().Where(m => m.Name == "User");

                if (role is not null)
                {
                    var addRoleResult = _roleUserService.Insert(new RoleUser() { UserId = user.Id, RoleId = role.First().Id });
                }
                tempUser = await _userManager.FindByNameAsync(model.PhoneNumber);

                if (tempUser != null)
                {
                    var userHasWalletForThisCLient = await walletService.GetAll(new WalletViewModel() { Client = new WalletClientViewModel() { ClientIdForApi = clientId }, UserId = tempUser.Id });

                    if (userHasWalletForThisCLient.Success && (userHasWalletForThisCLient.Entity == null || !userHasWalletForThisCLient.Entity.Any()))
                    {
                        var walletGenerationResult = await walletService.CreateNewWalletForNewUser(user.Id, clientId);
                    }
                }

            }


            var errorMessage = string.Join("<br>", result.Errors.Select(e => e.Description));

            return Json(new ResponseViewModel<RegisterViewModel>
            {
                Message = string.IsNullOrEmpty(errorMessage) ? "ساخت حساب کاربری با مشکل مواجه شد لطفا بعدامتحان کنید" : errorMessage,
                IsSuccess = false
            });
        }
        return Json(new ResponseViewModel<RegisterViewModel>
        {
            Message = "",
            IsSuccess = false
        });
    }
    [HttpPost]
    [AllowAnonymous]
    [SessionRequirement]
    [Route("RegisterRangeFromClient")]
    public async Task<JsonResult> RegisterRangeFromClient([FromBody] List<RegisterFromClientViewModel> models)
    {
        var counter = 0;

        foreach (var model in models)
        {
            model.UserName = model.UserName;
            model.LastName = model.Name;
            model.Name = "";
            model.Password = "@Trabase_123123";
            model.ConfirmPassword = "@Trabase_123123";

            var client = new WalletClientViewModel();

            if (Request.Headers.TryGetValue("clientidforapis", out var clientId))
            {
            }
            else
            {
                return Json(new ResponseViewModel<RegisterViewModel>
                {
                    Message = "کلاینت مجاز نمیباشد",
                    IsSuccess = false
                });
            }

            client = walletClientService.GetAll(new WalletClientViewModel() { ClientIdForApi = clientId, UserId = "" }).Result.Entity.FirstOrDefault();

            if (!string.IsNullOrEmpty(model.Email) && await _userService.IsEmailExist(model.Email))
            {
            }

            if (await _userService.IsPhoneNumberExist(model.PhoneNumber))
            {
            }

            var tempUser = await _userManager.FindByNameAsync(model.PhoneNumber);

            if (tempUser != null)
            {
                var walletGenerationResult = await walletService.CreateNewWalletForNewUser(tempUser.Id, clientId);

            }

            if (tempUser == null)
            {
                var user = new User
                {
                    UserName = model.PhoneNumber,
                    Email = model.Email,
                    EmailConfirmed = true,
                    PhoneNumber = model.PhoneNumber,
                    Name = model.Name,
                    LastName = model.LastName,
                    FullName = $"{model.Name} {model.LastName}",
                    IsDeleted = false,
                    IsActive = model.IsActive,
                    IsActiveOutsideWallet = true

                };

                var result = await _userManager.CreateAsync(user, "WalletTra@123123123");

                if (result.Succeeded)
                {

                    var role = _roleService.GetAllAsIqueriable().Where(m => m.Name == "User");

                    if (role is not null)
                    {
                        var addRoleResult = _roleUserService.Insert(new RoleUser() { UserId = user.Id, RoleId = role.First().Id });
                    }
                    tempUser = await _userManager.FindByNameAsync(model.PhoneNumber);

                    if (tempUser != null)
                    {
                        var userHasWalletForThisCLient = await walletService.GetAll(new WalletViewModel() { Client = new WalletClientViewModel() { ClientIdForApi = clientId }, UserId = tempUser.Id });

                        if (userHasWalletForThisCLient.Success && (userHasWalletForThisCLient.Entity == null || !userHasWalletForThisCLient.Entity.Any()))
                        {
                            var walletGenerationResult = await walletService.CreateNewWalletForNewUser(user.Id, clientId);
                        }
                    }
                    counter++;
                }
                else
                {
                }
            }
        }

        return Json(new ResponseViewModel<int>
        {
            Message = "",
            IsSuccess = true,
            Entity = counter
        });
    }

    public string GeneratePhoneNumberConfirmation()
    {
        var randomizer = new Random();

        return randomizer.Next(1100, 99999).ToString();
    }
    [HttpPost]
    [AllowAnonymous]
    [Route("ChangePassword")]
    public async Task<JsonResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return Json(new ResponseViewModel<object> { Message = "خطای ورود اطلاعات", IsSuccess = false });
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Json(new ResponseViewModel<LoginViewModel> { Message = "کاربر یافت نشد", IsSuccess = false });
        }

        var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        if (result.Succeeded)
        {
            return Json(new ResponseViewModel<LoginViewModel> { Message = "رمز عبور با موفقیت تغییر یافت", IsSuccess = true });
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return Json(new ResponseViewModel<LoginViewModel>
        {
            Message = "تغییر رمز عبود با خطا مواجه شد",
            IsSuccess = false
        });
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("ResetPassword")]
    public async Task<JsonResult> ResetPassword([FromBody] ResetPasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return Json(new ResponseViewModel<object> { Message = "خطای ورود اطلاعات", IsSuccess = false });
        }

        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user == null)
        {
            return Json(new ResponseViewModel<LoginViewModel> { Message = "کاربر یافت نشد", IsSuccess = false });
        }

        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
        if (result.Succeeded)
        {
            return Json(new ResponseViewModel<LoginViewModel> { Message = "رمز عبور با موفقیت تغییر یافت", IsSuccess = true });
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return Json(new ResponseViewModel<LoginViewModel>
        {
            Message = "تغییر رمز عبود با خطا مواجه شد",
            IsSuccess = false
        });
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("GeneratePasswordResetToken")]
    public async Task<JsonResult> GeneratePasswordResetToken(GeneratePasswordResetTokenViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return Json(new ResponseViewModel<LoginViewModel> { Message = "خطای ورود اطلاعات", IsSuccess = false });
        }

        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
        {
            return Json(new ResponseViewModel<LoginViewModel> { Message = "کار بر وجود ندارد یا غیر فعال است", IsSuccess = false });
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        ////send token 
        ///////
        if (!string.IsNullOrEmpty(token))
        {
            var callbackUrl = Url.Action("ResetPassword", "Account", new { area = "MemberShip", userId = user.Id, token = token }, protocol: HttpContext.Request.Scheme);
            return Json(new ResponseViewModel<LoginViewModel>
            {
                Message = "لینک تغییر رمز عبور ارسال شد",
                IsSuccess = true,
            });
        }

        return Json(new ResponseViewModel<LoginViewModel>
        {
            Message = "ورود به سیستم با خطا مواجه شد",
            IsSuccess = false
        });
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("ForgotPassword")]
    public async Task<JsonResult> ForgotPassword([FromForm] GetResetPasswordCodeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return Json(new ResponseViewModel<LoginViewModel> { Message = "کاربر یافت نشد", IsSuccess = false });
        }

        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user == null)
        {
            return Json(new ResponseViewModel<LoginViewModel> { Message = "کاربر یافت نشد", IsSuccess = false });
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        if (!string.IsNullOrEmpty(token))
        {
            var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = token }, protocol: HttpContext.Request.Scheme);
            await _signInManager.SignInAsync(user, isPersistent: false);
            return Json(new ResponseViewModel<LoginViewModel>
            {
                Message = "کد تغییر نام کاربری با موفیت ساخته شد",
                IsSuccess = true
            });
        }

        return Json(new ResponseViewModel<LoginViewModel> { Message = "عمبات ورد به سیستم با خطا مواجه شد", IsSuccess = false });
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("UpdateUserInfo")]
    public async Task<JsonResult> UpdateUserInfo()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return Json(new ResponseViewModel<UserViewModel> { Message = "کاربر یافت نشد", IsSuccess = false });
        }

        var userViewModel = new UserViewModel
        {
            Phone = user.PhoneNumber,
            Address = user.Address,
            Avatar = user.Avatar,
            Description = user.Description
        };

        return Json(new ResponseViewModel<UserViewModel> { Entity = userViewModel, IsSuccess = true });
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("UpdateUserInfo")]
    public async Task<JsonResult> UpdateUserInfo(UserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return Json(new ResponseViewModel<LoginViewModel> { Message = "اطلاعات ورودی را بررسی کنید", IsSuccess = false });
        }

        var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (user == null)
        {
            return Json(new ResponseViewModel<LoginViewModel> { Message = "کاربر یافت نشد", IsSuccess = false });
        }

        user.PhoneNumber = model.Phone;
        user.Address = model.Address;
        user.Avatar = model.Avatar;
        user.Description = model.Description;

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            return Json(new ResponseViewModel<LoginViewModel> { Message = "ویرایش اطلاعات کاربر بام وفقیت انجام شد", IsSuccess = true });
        }

        var errorMessages = result.Errors.Select(error => error.Description).ToArray();
        return Json(new ResponseViewModel<LoginViewModel>
        {
            Message = "عملیات ویرایش مشخصات کاربر با مشکل مواجه شد",
            IsSuccess = false,
            ErrorMessageList = errorMessages

        });
    }

    [HttpPost]
    [AllowAnonymous]
    [SessionRequirement]
    [Route("RegistrationConfirmation")]
    public async Task<IActionResult> RegistrationConfirmation([FromBody] ConfirmEmailNumberViewModel model)
    {
        if (model == null || string.IsNullOrWhiteSpace(model.UserId) || string.IsNullOrWhiteSpace(model.Token))
        {
            return Json(new ResponseViewModel<RegisterViewModel> { Message = "اطلاعات وارد شده صحیح نمیباشد", IsSuccess = false });

        }

        var user = await _userManager.FindByIdAsync(model.UserId);

        if (user == null)
        {
            return Json(new ResponseViewModel<RegisterViewModel> { Message = "کاربر وارد شده وجود ندارد", IsSuccess = false });

        }

        var result = await _userManager.ConfirmEmailAsync(user, model.Token);

        if (result.Succeeded)
        {
            return Json(new ResponseViewModel<RegisterViewModel> { Message = "تایید نام کاربری با موفقیت انجام شد", IsSuccess = true });

        }
        else
        {
            // Handle the error, possibly redirect to an error page
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            var message = string.Join(" ", result.Errors.Select(e => e.Description)); ;
            return Json(new ResponseViewModel<RegisterViewModel> { Message = message, IsSuccess = true });

        }
    }

    [HttpPost]
    [AllowAnonymous]
    [SessionRequirement]
    [Route("CreateConfirmationCodeForPhoneNumber")]
    public async Task<IActionResult> CreateConfirmationCodeForPhoneNumber([FromBody] PhoneNumberConfirmationViewModel model)
    {

        var user = await _userManager.FindByNameAsync(model.PhoneNumber);

        if (user == null)
        {
            return Json(new ResponseViewModel<PhoneNumberConfirmationViewModel> { Message = "کاربر وارد شده وجود ندارد", IsSuccess = false });

        }

        if (user.CurrentConfirmationCode == model.Code && user.ConfirmationCodeExpireDate != null & DateTime.Now <= user.ConfirmationCodeExpireDate)
        {
            user.CurrentConfirmationCode = null;

            user.ConfirmationCodeExpireDate = null;

            await _userManager.UpdateAsync(user);

            return Json(new ResponseViewModel<PhoneNumberConfirmationViewModel> { Message = "تایید نام کاربری با موفقیت انجام شد", IsSuccess = true });
        }
        var confirmationCode = GeneratePhoneNumberConfirmation();

        user.CurrentConfirmationCode = confirmationCode;
        _userManager.UpdateAsync(user);
        modirPayamakService.SendRegisterConfirmation(user.UserName, confirmationCode);

        return Json(new ResponseViewModel<PhoneNumberConfirmationViewModel> { Message = "کد تایید برای شماره شما ارسال شد", IsSuccess = true });

    }
}
