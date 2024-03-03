using AsyncCourse.Auth.Api.Models;
using AsyncCourse.Auth.Api.Models.Accounts;

namespace AsyncCourse.Auth.Api.Mappers;

public static class AccountMapper
{
    public static AccountModel MapFrom(AuthAccount account)
    {
        return new AccountModel
        {
            Id = account.Id,
            Email = account.Email,
            FullName = GetFullName(account.Surname, account.Name),
            Role = MapFrom(account.Role)
        };
    }

    public static AuthAccount MapFromSignUpModel(SignupModel signupModel)
    {
        return new AuthAccount
        {
            Id = Guid.NewGuid(),
            Email = signupModel.Email,
            Password = signupModel.Password,
            Name = signupModel.Name,
            Surname = signupModel.Surname,
            Role = AuthAccountRole.Employee
        };
    }
    
    public static EditAuthAccount MapFromEditAccountModel(EditAccountModel accountModel)
    {
        return new EditAuthAccount
        {
            Id = accountModel.Id == Guid.Empty ? Guid.NewGuid() : accountModel.Id,
            Role = MapFromModel(accountModel.Role)
        };
    }

    private static string GetFullName(string surname, string name)
    {
        if (!string.IsNullOrWhiteSpace(surname) && !string.IsNullOrWhiteSpace(name))
        {
            return $"{surname} {name}";
        }

        if (!string.IsNullOrWhiteSpace(surname))
        {
            return $"{surname}";
        }
            
        if (!string.IsNullOrWhiteSpace(name))
        {
            return $"{name}";
        }

        return "Full name is absent";
    }

    private static AuthAccountRoleModel MapFrom(AuthAccountRole role)
    {
        return role switch
        {
            AuthAccountRole.Unknown => AuthAccountRoleModel.Unknown,
            AuthAccountRole.Employee => AuthAccountRoleModel.Employee,
            AuthAccountRole.Administrator => AuthAccountRoleModel.Administrator,
            AuthAccountRole.Manager => AuthAccountRoleModel.Manager,
            AuthAccountRole.Accountant => AuthAccountRoleModel.Accountant,
            _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
        };
    }

    private static AuthAccountRole MapFromModel(AuthAccountRoleModel role)
    {
        return role switch
        {
            AuthAccountRoleModel.Unknown => AuthAccountRole.Unknown,
            AuthAccountRoleModel.Employee => AuthAccountRole.Employee,
            AuthAccountRoleModel.Administrator => AuthAccountRole.Administrator,
            AuthAccountRoleModel.Manager => AuthAccountRole.Manager,
            AuthAccountRoleModel.Accountant => AuthAccountRole.Accountant,
            _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
        };
    }
}