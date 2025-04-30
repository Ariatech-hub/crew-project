

namespace FarmToFork.Business.DTOs
{
    internal class AuthRelated
    {
    }

    public class LoginModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public class RegisterModel
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public string Role { get; set; } = null!;
    }

    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            _ = RuleFor(v => v.Username).NotNull().NotEmpty().WithMessage("Username is required!");
            _ = RuleFor(v => v.Password).NotNull().NotEmpty().WithMessage("Password is required!");
            _ = RuleFor(v => v.ConfirmPassword).NotNull().NotEmpty().Equal(v => v.Password).WithMessage("Password and Confirm Password do not match.");
            _ = RuleFor(v => v.Role).NotNull().NotEmpty().WithMessage("Role is required.");
        }
    }

    public class PasswordResetVm
    {
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public string Username { get; set; } = null!;
    }

    public class PasswordResetVmValidator : AbstractValidator<PasswordResetVm>
    {
        public PasswordResetVmValidator()
        {
            _ = RuleFor(v => v.Username).NotNull().NotEmpty().WithMessage("Username is required!");
            _ = RuleFor(v => v.Password).NotNull().NotEmpty().WithMessage("Password is required!");
            _ = RuleFor(v => v.ConfirmPassword).NotNull().NotEmpty().WithMessage("Confirm Password is required!");
            _ = RuleFor(v => v.ConfirmPassword).NotNull().NotEmpty().Equal(v => v.Password).WithMessage("Password and Confirm Password do not match.");
        }
    }

    public class UnRegisterVm
    {
        public string UserName { get; set; } = null!;
    }

    public class UnRegisterValidator : AbstractValidator<UnRegisterVm>
    {
        public UnRegisterValidator()
        {
            _ = RuleFor(v => v.UserName).NotNull().NotEmpty().WithMessage("Username is required!");
            
        }
    }

    public class ChangePasswordVm
    {
        public string Username { get; set; } = null!;

        public string OldPassword { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;

    }

    public class PasswordChangeValidator : AbstractValidator<ChangePasswordVm>
    {
        public PasswordChangeValidator()
        {
            _ = RuleFor(v => v.Username).NotNull().NotEmpty().WithMessage("Username is required!");
            _ = RuleFor(v => v.Password).NotNull().NotEmpty().WithMessage("Password is required!");
            _ = RuleFor(v => v.OldPassword).NotNull().NotEmpty().WithMessage("Old Password is required!");
            _ = RuleFor(v => v.ConfirmPassword).NotNull().NotEmpty().WithMessage("Confirm Password is required!");
            _ = RuleFor(v => v.ConfirmPassword).NotNull().NotEmpty().Equal(v => v.Password).WithMessage("Password and Confirm Password do not match.");
        }
    }

    public class ApplicationUserWithRole
    {
        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? Role { get; set; } = null!;
    }

    public class UnRegisterUser
    {
        public string Username { get; set; } = null!;
    }


    public class UnRegisterUserValidator : AbstractValidator<UnRegisterUser>
    {
        public UnRegisterUserValidator()
        {
            _ = RuleFor(v => v.Username).NotNull().NotEmpty().WithMessage("Username is required!");
        }
    }

    public class Role
    {
        public string RoleName { get; set; } = null!;
    }




}
