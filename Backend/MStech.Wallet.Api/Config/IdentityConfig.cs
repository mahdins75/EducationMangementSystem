using Microsoft.AspNetCore.Identity;

public class CustomIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError PasswordTooShort(int length)
    {
        return new IdentityError
        {
            Code = "PasswordTooShort",
            Description = $"رمز عبور باید حداقل ۶ کاراکتر باشد."
        };
    }
    
    public override IdentityError PasswordRequiresNonAlphanumeric()
    {
        return new IdentityError
        {
            Code = "PasswordRequiresNonAlphanumeric",
            Description = $"رمز عبور باید شامل یک از حروف @و#و$و ... باشد."
        };
    }

    public override IdentityError PasswordRequiresLower()
    {
        return new IdentityError
        {
            Code = "PasswordRequiresLower",
            Description = $"رمز عبور باید شامل یک حرف کوچک الفبای انگلیسی باشد."
        };
    }
    public override IdentityError PasswordRequiresUpper()
    {
        return new IdentityError
        {
            Code = "PasswordRequiresUpper",
            Description = $"رمز عبور باید شامل یک حرف بزرگ الفبای انگلیسی باشد."

        };
    }

    public override IdentityError PasswordMismatch()
    {
        return new IdentityError
        {
            Code = "PasswordMismatch",
            Description = $"کلمه عبور فعلی نادرست وارد شده است.."

        };
    }

}