using Authorization.Custom;
using Implementation.AccessLinkeRoles;
using Implementation.ConstantService;
using Implementation.FileService;
using Implementation.RoleUserService;
using Implementation.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mstech.Entity.Etity;
using Mstech.ViewModel.DTO;
using System.Net;

[Area("api/Admin")]
[Route("[area]/User")]
public class UserController : SecureBaseController
{
    private readonly UserService _userService;
    private readonly FileService _fileService;
    private readonly ConstantService _constantService;
    private readonly UserManager<User> _userManager;
    public readonly RoleUserService _roleUserService;
    public readonly AccessLinkRolesService _accessLinkeRolesService;

    public UserController(
        UserService userService,
        FileService fileService,
        UserManager<User> userManager,
        ConstantService constantService,
        RoleUserService roleUserService,
        AccessLinkRolesService accessLinkeRolesService)
    {
        _userService = userService;
        _userManager = userManager;
        _constantService = constantService;
        _roleUserService = roleUserService;
        _accessLinkeRolesService = accessLinkeRolesService;
        _fileService = fileService;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] UserViewModel model)
    {
        var result = await _userService.GetAll(model);
        return Json(new ResponseViewModel<List<UserViewModel>>() { IsSuccess = true, Entity = result.Entity, Message = result.Message, QueryCount = result.Count });
    }


    [HttpGet]
    [Route("GetAllUsersForMyClient")]
    public async Task<JsonResult> GetAllUsersForMyClient([FromQuery] UserViewModel model)
    {
        var result = await _userService.GetAllOfClientsUser(model);

        return Json(new ResponseViewModel<List<UserViewModel>>() { Entity = result.Entity, Message = "Success", IsSuccess = result.Success, QueryCount = result.Count });
    }

    [HttpGet]
    [Route("GetUser")]
    public async Task<IActionResult> GetUser(string id)
    {
        var user = await _userService.FindAsync(id);
        var model = new EditUserViewModel()
        {
            Id = user.Id,
            Name = user.Name ?? "",
            EditIsHRMember = user.IsHRMember,
            Email = user.Email ?? "",
            Phone = user.PhoneNumber ?? "",
            PositionId = user.PositionId,
            LastName = user.LastName ?? "",
        };

        return Json(new { success = true, data = model });
    }

    [HttpGet]
    [Route("GetUserInfo")]
    [AllowAnonymous]
    public async Task<IActionResult> GetUserInfo()
    {
        var userName = User.Identity.Name;

        if (string.IsNullOrEmpty(userName))
        {
            return Json(new ResponseViewModel<UserViewModel>
            {
                IsSuccess = false,
                Message = "شما وارد سیستم نشده اید"
            });
        }

        var user = await _userService.FindByUserNameAsync(userName);

        if (user == null)
        {
            return Json(new ResponseViewModel<UserViewModel>
            {
                IsSuccess = false,
                Message = "شما وارد سیستم نشده اید"
            });
        }

        var model = new UserViewModel()
        {
            Id = user.Id,
            Name = user.Name ?? "",
            Email = user.Email ?? "",
            UserName = user.UserName ?? "",
            Phone = user.PhoneNumber ?? "",
            PositionId = user.PositionId,
            LastName = user.LastName ?? "",
        };

        return Json(new ResponseViewModel<UserViewModel>
        {
            IsSuccess = true,
            Entity = model,
            Message = ""
        });
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create([FromBody] CreateUserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errorProps = new List<AjaxModalValidation>();
            foreach (var key in ModelState.Keys)
            {
                var modelStateEntry = ModelState[key];
                if (modelStateEntry != null && modelStateEntry.ValidationState == ModelValidationState.Invalid)
                {
                    errorProps.Add(new AjaxModalValidation()
                    {
                        Property = key,
                        Errors = modelStateEntry.Errors.Select(e => e.ErrorMessage).FirstOrDefault()
                    });
                }
            }

            string validationMessages = string.Join(" | ", errorProps.Select(e => $"{e.Property}: {e.Errors}"));

            return Json(new ResponseViewModel<CreateUserViewModel>
            {
                IsSuccess = false,
                Entity = model,
                Message = $"شماره همراه وارد شده قبلا استفاده شده است. Additional Errors: {validationMessages}"
            });
        }

        //Check Email and Phone 
        if (await _userService.IsEmailExist(model.UserName))
        {
            return Json(new ResponseViewModel<CreateUserViewModel> { IsSuccess = false, Entity = model, Message = "ایمیل وارد شده قبلا استفاده شده است." });

        }

        if (await _userService.IsPhoneNumberExist(model.PhoneNumber))
        {
            return Json(new ResponseViewModel<CreateUserViewModel> { IsSuccess = false, Entity = model, Message = "شماره همراه وارد شده قبلا استفاده شده است." });

        }
        var result = await _userService.CreateUserAsync(model);

        if (!result.Success)
            return Json(new ResponseViewModel<CreateUserViewModel> { IsSuccess = false, Entity = model, Message = result.Message });

        return Json(new ResponseViewModel<CreateUserViewModel> { IsSuccess = true });

    }

    [HttpPost]
    [Route("Edit")]
    public async Task<IActionResult> Edit([FromBody] EditUserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errorProps = new List<AjaxModalValidation>();
            foreach (var key in ModelState.Keys)
            {
                var modelStateEntry = ModelState[key];
                if (modelStateEntry != null && modelStateEntry.ValidationState == ModelValidationState.Invalid)
                {
                    errorProps.Add(new AjaxModalValidation()
                    {
                        Property = key,
                        Errors = modelStateEntry.Errors.Select(e => e.ErrorMessage).FirstOrDefault()
                    });
                }
            }

            string validationMessages = string.Join(" | ", errorProps.Select(e => $"{e.Property}: {e.Errors}"));

            return Json(new ResponseViewModel<EditUserViewModel>
            {
                IsSuccess = false,
                Entity = model,
                Message = $"شماره همراه وارد شده قبلا استفاده شده است. Additional Errors: {validationMessages}"
            });
        }





        if (!model.OnlyCurrentUser)
        {
            var result = await _userService.EditUserAsync(model);
            if (!result.Success)
                return Json(new ResponseViewModel<EditUserViewModel> { IsSuccess = false, Entity = model, Message = result.Message });

        }
        else
        {
            var currentUserName = User.Identity.Name;
            if (string.IsNullOrEmpty(currentUserName))
            {
                return Json(new ResponseViewModel<EditUserViewModel> { IsSuccess = false, Entity = model, Message = "کاربری یافت نشد" });
            }
            if (model.UserName == currentUserName)
            {
                var result = await _userService.EditUserProfileAsync(model);
                if (!result.Success)
                    return Json(new ResponseViewModel<EditUserViewModel> { IsSuccess = result.Success, Entity = model, Message = result.Message });
            }
            else
            {
                return Json(new ResponseViewModel<EditUserViewModel> { IsSuccess = false, Entity = model, Message = "شما اجازه تغییر رمز این کاربر را ندارید" });
            }


        }


        return Json(new ResponseViewModel<EditUserViewModel> { IsSuccess = true });
    }

    [HttpPost]
    [Route("Delete")]
    [AllowAnonymous]
    [SessionRequirement]
    public async Task<IActionResult> Delete([FromBody] UserViewModel model)
    {
        var result = await _userService.DeleteUserAsync(model.Id);

        return Json(new ResponseViewModel<EditUserViewModel> { IsSuccess = true });
    }

    [HttpPost]
    [Route("ChangePassword")]
    public async Task<IActionResult> ChangePassword(ChangePasswordUserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errorProps = new List<AjaxModalValidation>();
            foreach (var key in ModelState.Keys)
            {
                var modelStateEntry = ModelState[key];
                if (modelStateEntry != null && modelStateEntry.ValidationState == ModelValidationState.Invalid)
                {
                    errorProps.Add(new AjaxModalValidation()
                    {
                        Property = key,
                        Errors = modelStateEntry.Errors.Select(e => e.ErrorMessage).FirstOrDefault()
                    });
                }
            }

            return Json(new { success = false, isValid = false, errors = errorProps });
        }

        var user = await _userService.FindAsync(model.Id);
        if (user == null)
        {
            return Json(new { success = false, isValid = true, message = "کاربری یافت نشد." });
        }

        var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);

        if (!result.Succeeded)
        {
            var errorMessage = string.Empty;
            foreach (var error in result.Errors)
            {
                errorMessage += error.Description + "<br>";
            }

            return Json(new { success = false, isValid = true, message = errorMessage });
        }

        return Json(new { success = true });
    }

    [HttpPost]
    [Route("AddRoleToUser")]
    public async Task<IActionResult> AddRoleToUser(UserRoleViewModel model)
    {
        if (model.RoleIds.Count() != 1)
        {
            return Ok(new { status = false, message = "فقط یک نقش می توانید انتخاب کنید." });
        }

        var user = await _userManager.FindByIdAsync(model.UserId);

        if (user == null)
        {
            return Ok(new { status = false, message = "کاربر انتخابی معتبر نیست." });
        }

        var result = await _roleUserService.InsertRoleToUser(model);

        if (result)
            return Ok(new { status = true });
        else
            return Ok(new { status = false, message = "خطایی به وجود آمده است! لطفا مجدد سعی نمایید." });
    }

    [Route("EditProfile")]
    public async Task<IActionResult> EditProfile()
    {
        string? currentUserName = User.Identity?.Name;
        if (string.IsNullOrEmpty(currentUserName))
            return RedirectToAction("Login", "Account", new { area = "Membership", ReturnUrl = "/Admin/User/EditProfile" });

        var user = await _userManager.FindByNameAsync(currentUserName);

        var model = new EditProfileViewModel()
        {
            Id = user.Id,
            Email = user.Email,
            LastName = user.LastName,
            Name = user.Name,
            Phone = user.Phone
        };

        var profileFile = await _fileService.GetFileAsync(user.Id, typeof(User).Name, "AvatarFile");
        if (profileFile != null)
        {
            model.AvatarSrc = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(profileFile.FileData));
        }

        ViewBag.errorMessage = "";
        return View(model);
    }

    [HttpPost]
    [Route("EditProfile")]
    public async Task<IActionResult> EditProfile(EditProfileViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.errorMessage = "داده های ورودی دچار مشکل هستند.";
            return View(model);
        }

        if (model.AvatarFile != null && model.AvatarFile.Length > 1)
        {
            //todo validation file

            byte[] fileData;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                model.AvatarFile.CopyTo(memoryStream);
                fileData = memoryStream.ToArray();
            }

            var fileResult = await _fileService.AddAsync(new CreateFileViewModel()
            {
                EntityId = model.Id,
                EntityName = typeof(User).Name,
                FileName = model.AvatarFile.FileName,
                PropertyName = "AvatarFile",
                FileData = fileData,
            });

            if (fileResult < 1)
            {
                ViewBag.errorMessage = "افزودن عکس پروفایل با خطا مواجه شد!";
                return View(model);
            }
        }

        var result = await _userService.EditProfileAsync(model);
        if (result < 1)
        {
            ViewBag.errorMessage = "ویرایش پروفایل با خطا مواجه شد!";
            return View(model);
        }

        return RedirectToAction("EditProfile");
    }

    [HttpPost]
    [Route("ChangePasswordProfile")]
    public async Task<IActionResult> ChangePasswordProfile(ChangePasswordProfileViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errorProps = new List<AjaxModalValidation>();
            foreach (var key in ModelState.Keys)
            {
                var modelStateEntry = ModelState[key];
                if (modelStateEntry != null && modelStateEntry.ValidationState == ModelValidationState.Invalid)
                {
                    errorProps.Add(new AjaxModalValidation()
                    {
                        Property = key,
                        Errors = modelStateEntry.Errors.Select(e => e.ErrorMessage).FirstOrDefault()
                    });
                }
            }

            return Json(new { success = false, isValid = false, errors = errorProps });
        }

        var user = await _userService.FindAsync(model.Id);
        if (user == null)
        {
            return Json(new { success = false, isValid = true, message = "کاربری یافت نشد." });
        }

        var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);

        if (!result.Succeeded)
        {
            var errorMessage = string.Empty;
            foreach (var error in result.Errors)
            {
                errorMessage += error.Description + "<br>";
            }

            return Json(new { success = false, isValid = true, message = errorMessage });
        }

        return Json(new { success = true });
    }

    [HttpPost]
    [Route("ConfirmPhoneNumber")]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmPhoneNumber([FromBody] ConfirmPhoneNumberViewModel model)
    {

        var user = _userService.GetAll().Where(m => m.PhoneNumber == model.UserName).FirstOrDefault();
        if (user == null)
        {
            return Json(new { success = false, isValid = true, message = "کاربری یافت نشد." });
        }

        if (user.CurrentConfirmationCode == model.Code)
        {
            user.CurrentConfirmationCode = null;
            user.IsActiveOutsideWallet = false;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            model.ResetPasswordToken = WebUtility.UrlEncode(token);
            _userService.Update(user);

            return Json(new ResponseViewModel<ConfirmPhoneNumberViewModel> { IsSuccess = true, Message = "شماره شما تایید شد لطفا در مرحله بعدی رمز عبور جدید خود را وارد کنید", Entity = model });

        }
        else
        {

            return Json(new ResponseViewModel<ConfirmPhoneNumberViewModel> { IsSuccess = false, Message = "کد وارد شده اشتباه است" });
        }


    }


}
