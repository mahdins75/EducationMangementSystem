using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Implementation.AccessedLinkService;
using Implementation.RoleUserService;
using Implementation.TokenManagement;
using Implementation.UserService;
using Mapster;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Mstech.Entity.Etity;
using Mstech.ViewModel.DTO;
using DNTCaptcha.Core;
using Microsoft.Extensions.Options;
[Route("Membership/Account")]
[Area("Membership")]
[Authorize()]

public class AccountController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly RoleUserService _roleUserService;
    private readonly TokenService _tokenService;
    public readonly RoleAccessedLinkService roleAccessedLinkService;
    private readonly UserService _userService;
    private readonly DNTCaptchaOptions _captchaOptions;
    private readonly IDNTCaptchaValidatorService _validatorService;


    public AccountController(IDNTCaptchaValidatorService validatorService,
        IOptions<DNTCaptchaOptions> options,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        RoleUserService roleUserService,
        TokenService tokenService,
        RoleAccessedLinkService roleAccessedLinkService,
        UserService userService
      )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleUserService = roleUserService;
        _tokenService = tokenService;
        this.roleAccessedLinkService = roleAccessedLinkService;
        _userService = userService;
        _validatorService = validatorService;
        _captchaOptions = options == null ? throw new ArgumentNullException(nameof(options)) : options.Value;
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("Login")]
    public async Task<IActionResult> Login([FromQuery] string? returnUrl, bool? mustRefresh = false)
    {
        var Token = HttpContext.Request.Cookies["token"];
        var dontRefresh = HttpContext.Request.Cookies["DontRefresh"];
        var RefreshToken = HttpContext.Request.Cookies["RefreshToken"];
        if (!string.IsNullOrEmpty(Token) && !string.IsNullOrEmpty(dontRefresh) && !string.IsNullOrEmpty(RefreshToken))
        {
            if (!JwtHelper.IsTokenExpired(Token))
            {
                returnUrl = !string.IsNullOrEmpty(returnUrl) ? returnUrl : "/";
                return Redirect(returnUrl);
            }
        }
        else if (!string.IsNullOrEmpty(Token) || !string.IsNullOrEmpty(dontRefresh) || !string.IsNullOrEmpty(RefreshToken))
        {
            Response.Cookies.Append("token", "", new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true, // Use HTTPS
                               // Set the cookie expiration as needed
            });
            Response.Cookies.Append("DontRefresh", "", new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true, // Use HTTPS
                               // Set the cookie expiration as needed
            });
            Response.Cookies.Append("RefreshToken", "", new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true, // Use HTTPS
                               // Set the cookie expiration as needed
            });
            returnUrl = !string.IsNullOrEmpty(returnUrl) ? returnUrl : "/";
            return Redirect(returnUrl);

        }
        var model = new LoginViewModel();
        model.ReturnUrl = !string.IsNullOrEmpty(returnUrl) ? returnUrl : "";
        ViewBag.loginError = string.Empty;
        return View(model);
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("AccessDenied")]
    public async Task<IActionResult> AccessDenied([FromQuery] string? returnUrl)
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    [ValidateDNTCaptcha(ErrorMessage = "کد امنیتی به درستی وارد نشده است")]
    [Route("Login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {

        if (!ModelState.IsValid)
        {
            model.ReturnUrl = !string.IsNullOrEmpty(model.ReturnUrl) ? model.ReturnUrl : "";

            return View(model);
        }

        var user = await _userManager.FindByNameAsync(model.UserName);

        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var roles = await _roleUserService.GetAll().AsQueryable().Include(m => m.Role).Where(m => m.UserId == user.Id).ToListAsync();
            // Create claims for the user
            var tokenHandler = new JwtSecurityTokenHandler();
            var refreshClaim = User.Claims.FirstOrDefault(c => c.Type == "RefreshToken");
            var refreshToken = refreshClaim?.Value ?? Guid.NewGuid().ToString(); // Generate a new refresh token if it doesn't exist


            try
            {
                var tokenRemoveResult = await _tokenService.RemoveRefreshToken(user.Adapt<UserViewModel>());
                var tokenResult = await _tokenService.AddRefreshToken(refreshToken, user.Adapt<UserViewModel>());
            }
            catch
            {
            }
            var rolenameList = await _roleUserService.GetAllUserRoles(user.Id);
            var claimList = new List<Claim>(){
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("username", user.UserName),
                    new Claim("name", user.Name),
                    new Claim("lastname", user.LastName),
                    };
            foreach (var item in rolenameList.Entity)
            {
                claimList.Add(new Claim(ClaimTypes.Role, item));
            }
            claimList.Add(new Claim("RefreshToken", refreshToken));
            var claims = claimList.ToArray();
            // Create a JWT token
            var jwtSecret = "bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34"; // Replace with your secret key

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                issuer: "your-issuer", // Replace with your issuer
                audience: "your-audience", // Replace with your audience
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // Set the token expiration as needed
                signingCredentials: creds
            );
            Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true, // Use HTTPS
                               // Set the cookie expiration as needed
            });


            var tokenString = tokenHandler.WriteToken(token);

            // Sign in the user using cookies
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)),
                new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1) // Adjust expiration as needed
                });

            // You can return the token as a JSON response
            return RedirectToAction("LoginSuccess", "Account", new { area = "Membership", ReturnUrl = model.ReturnUrl, userName = user.UserName, role = roles.Select(m => m.Role.Name).ToList() });
        }
        else
        {
            ViewBag.loginError = "ایمیل یا رمز عبور اشتباه وارد شده است.";
            ViewBag.ReturnUrl = !string.IsNullOrEmpty(model.ReturnUrl) ? model.ReturnUrl : "";

            return View(model);
        }
    }
    [Route("LoginSuccess")]
    [AllowAnonymous]
    [HttpGet]

    public async Task<IActionResult> LoginSuccess([FromQuery] string? ReturnUrl, [FromQuery] string userName, [FromQuery] List<string> role)
    {
        var navItems = await this.roleAccessedLinkService.GetMenuItemsInHTML(userName, role);
        ViewBag.NavItems = navItems;
        ViewBag.ReturnUrl = ReturnUrl == null ? "/Admin/complex/index" : ReturnUrl;
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("Logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        Response.Cookies.Append("RefreshToken", "", new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None,
            Secure = true, // Use HTTPS
                           // Set the cookie expiration as needed
        });
        Response.Cookies.Append("DontRefresh", "0", new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None,
            Secure = true, // Use HTTPS
                           // Set the cookie expiration as needed
        });

        return RedirectToAction("Login", "Account", new { area = "Membership", returnUrl = "/" });
    }
    [Route("RefreshToken")]
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> RefreshToken([FromQuery] string? ReturnUrl)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = Request.Cookies["Token"];

        var refreshTokenList = Request.Cookies["RefreshToken"];

        if (string.IsNullOrEmpty(refreshTokenList))
        {
            return RedirectToAction("login", "Account", new { area = "Membership", ReturnUrl = ReturnUrl });
        }
        var user = await _tokenService.GetUserFromRefreshToken(refreshTokenList);
        if (user == null)
        {
            Response.Cookies.Append("RefreshToken", "", new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true, // Use HTTPS
                               // Set the cookie expiration as needed
            });
            return RedirectToAction("Login", "Account", new { area = "Membership", ReturnUrl = ReturnUrl });

        }
        var roles = await _roleUserService.GetAll().AsQueryable().Include(m => m.Role).Where(m => m.UserId == user.Id).ToListAsync();

        if (user == null)
        {
            return Unauthorized("Invalid refresh token.");
        }
        var claimList = new List<Claim>
                                     {
                                          new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("name", user.Name),
                    new Claim("lastname", user.LastName),
                                     };
        var refreshClaim = User.Claims.FirstOrDefault(c => c.Type == "RefreshToken");
        var refreshToken = refreshClaim?.Value ?? Guid.NewGuid().ToString();
        claimList.Add(new Claim("RefreshToken", refreshToken));
        foreach (var item in roles)
        {
            claimList.Add(new Claim(ClaimTypes.Role, item.Role.Name));
        }
        claimList.Add(new Claim(ClaimTypes.Role, "all"));
        var newclaims = claimList.ToArray();

        // Create and issue a new JWT token with an extended expiration
        var jwtSecret = "bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34bf4e7749-5d6b-4e2b-91cd-be7e15466f34"; // Replace with your secret key

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        var token = new JwtSecurityToken(
            issuer: "your-issuer", // Replace with your issuer
            audience: "your-audience", // Replace with your audience
            claims: claimList,
            expires: DateTime.UtcNow.AddHours(1), // Set the token expiration as needed
            signingCredentials: creds
        );
        Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None,
            Secure = true, // Use HTTPS
                           // Set the cookie expiration as needed
        });
        Response.Cookies.Append("DontRefresh", "1", new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None,
            Secure = true, // Use HTTPS
                           // Set the cookie expiration as needed
        });
        try
        {
            var tokenRemoveResult = await _tokenService.RemoveRefreshToken(user);
            var tokenResult = await _tokenService.AddRefreshToken(refreshToken, user);
        }
        catch
        {
        }
        var tokenString = tokenHandler.WriteToken(token);

        // Sign in the user using cookies
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(new ClaimsIdentity(claimList, CookieAuthenticationDefaults.AuthenticationScheme)),
            new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1) // Adjust expiration as needed
            });

        // You can return the token as a JSON response
        return RedirectToAction("LoginSuccess", "Account", new { area = "Membership", ReturnUrl = ReturnUrl, userName = user.UserName, role = roles.Select(m => m.Role.Name).ToList() });


    }
    [HttpGet]
    [AllowAnonymous]
    [Route("Register")]
    public async Task<IActionResult> Register()
    {
        ViewBag.registerError = string.Empty;
        return View(new RegisterViewModel());
    }
    [HttpPost]
    [AllowAnonymous]
    [Route("Register")]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        //Check Email and Phone 
        if (await _userService.IsEmailExist(model.Email))
        {
            ViewBag.registerError = "ایمیل وارد شده قبلا استفاده شده است.";
            return View(model);
        }

        if (await _userService.IsPhoneNumberExist(model.PhoneNumber))
        {
            ViewBag.registerError = "شماره همراه وارد شده قبلا استفاده شده است.";
            return View(model);
        }


        var user = new User
        {
            UserName = model.Email,
            Email = model.Email,
            EmailConfirmed = true,
            PhoneNumber = model.PhoneNumber,
            Name = model.Name,
            LastName = model.LastName,
            FullName = model.Name + " " + model.LastName,
            IsDeleted = false,
            IsActive = true
        };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            // Generate an email confirmation token
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            model.IsLoginSuccess = true;
            return View(model);
        }
        else
        {
            var errorMessage = string.Empty;
            foreach (var item in result.Errors)
            {
                errorMessage += item.Description + "<br>";
            }
            ViewBag.registerError = string.IsNullOrEmpty(errorMessage) ? "ثبت نام شما با خطا مواجه شده، لطفا مجدد سعی کنید." : errorMessage;
            return View(model);
        }
    }
    [HttpGet]
    [AllowAnonymous]
    [Route("RegistrationConfirmation")]
    public async Task<IActionResult> RegistrationConfirmation([FromForm] ConfirmEmailNumberViewModel model)
    {
        if (model == null || string.IsNullOrWhiteSpace(model.UserId) || string.IsNullOrWhiteSpace(model.Token))
        {
            return RedirectToAction("Error");
        }

        var user = await _userManager.FindByIdAsync(model.UserId);

        if (user == null)
        {
            return RedirectToAction("Error");
        }

        var result = await _userManager.ConfirmEmailAsync(user, model.Token);

        if (result.Succeeded)
        {
            return View("EmailConfirmationSuccess");
        }
        else
        {
            // Handle the error, possibly redirect to an error page
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View("EmailConfirmationError");
        }
    }
    [HttpGet]
    [AllowAnonymous]
    [Route("ChangePassword")]
    public IActionResult ChangePassword()
    {
        return View();
    }
    [HttpPost]
    [AllowAnonymous]
    [Route("ChangePassword")]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(User); // Get the currently authenticated user

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                // Password changed successfully
                return RedirectToAction("ChangePasswordConfirmation", "Account", new { area = "MemberShip" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }
    [HttpGet]
    [AllowAnonymous]
    [Route("ChangePasswordConfirmation")]
    public IActionResult ChangePasswordConfirmation()
    {
        return View();
    }
    [HttpGet]
    [AllowAnonymous]
    [Route("GeneratePasswordResetToken")]
    public IActionResult GeneratePasswordResetToken()
    {
        return View();
    }
    [HttpPost]
    [AllowAnonymous]
    [Route("GeneratePasswordResetToken")]
    public async Task<IActionResult> GeneratePasswordResetToken(GeneratePasswordResetTokenViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // Handle the case when the user doesn't exist or their email is not confirmed
                ModelState.AddModelError(string.Empty, "User does not exist or email is not confirmed.");
                return View(model);
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            if (!string.IsNullOrEmpty(token))
            {
                // Send the password reset token via email to the user
                var callbackUrl = Url.Action("ResetPassword", "Account", new { area = "MemberShip", userId = user.Id, token = token }, protocol: HttpContext.Request.Scheme);

                // Send the email with the callback URL
                // You can use your email service or library to send the email
                // Example: await _emailService.SendPasswordResetEmailAsync(user.Email, callbackUrl);

                return View("PasswordResetTokenSent");
            }

            // Handle the case when token generation fails
            ModelState.AddModelError(string.Empty, "Token generation failed. Please try again.");
        }

        return View(model);
    }
    [HttpGet]
    [AllowAnonymous]
    [Route("ForgotPassword")]
    public IActionResult ForgotPassword()
    {

        var model = new GetResetPasswordCodeViewModel
        {
        };
        return View(model);
    }
    [HttpPost]
    [AllowAnonymous]
    [Route("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword([FromForm] GetResetPasswordCodeViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                ViewBag.errorMessage = "کاربری با نام کاربری وارد شده یافت نشد";
                return View(model);
            }

            var result = await _userManager.GeneratePasswordResetTokenAsync(user);

            if (!string.IsNullOrEmpty(result))
            {
                var callbackUrl = Url.Action(
                             "ResetPassword",
                             "Account",
                             new { userId = user.Id, code = result },
                             protocol: HttpContext.Request.Scheme);
                // Log the user in with the new password (if desired)
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("ResetPasswordConfirmation", "Account", new { area = "MemberShip" });
            }

        }

        return View(model);
    }
    [HttpGet]
    [AllowAnonymous]
    [Route("UpdateUserInfo")]
    public async Task<IActionResult> UpdateUserInfo()
    {
        var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

        if (user == null)
        {
            return RedirectToAction("Error");
        }

        var userViewModel = new UserViewModel
        {
            Phone = user.PhoneNumber,
            Address = user.Address,
            Avatar = user.Avatar,
            Description = user.Description
        };

        // Retrieve user's agencies and populate the UserViewModel's Agencies property as needed
        // userViewModel.Agencies = ...;

        return View(userViewModel);
    }
    [HttpPost]
    [AllowAnonymous]
    [Route("UpdateUserInfo")]
    public async Task<IActionResult> UpdateUserInfo(UserViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user == null)
            {
                return RedirectToAction("Error");
            }

            // Update the user's information
            user.PhoneNumber = model.Phone;
            user.Address = model.Address;
            user.Avatar = model.Avatar;
            user.Description = model.Description;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                // Handle successful update (e.g., redirect to a user profile page)
                return RedirectToAction("UserProfile");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }

}
