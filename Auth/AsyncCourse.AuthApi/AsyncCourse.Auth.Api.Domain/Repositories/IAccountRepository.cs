using AsyncCourse.Auth.Api.Models.Accounts;

namespace AsyncCourse.Auth.Api.Domain.Repositories;

public interface IAccountRepository
{
    Task<List<AuthAccount>> GetListAsync();
    
    Task<AuthAccount> GetByLoginAndPasswordAsync(string email, string password);

    Task<AuthAccount> GetByIdAsync(Guid id);

    Task AddAsync(AuthAccount account);

    Task<AuthAccount> EditAsync(EditAuthAccount account);

    Task DeleteAsync(Guid id);
}