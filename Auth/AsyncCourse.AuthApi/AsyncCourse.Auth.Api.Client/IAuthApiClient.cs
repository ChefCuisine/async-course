using AsyncCourse.Client;

namespace AsyncCourse.Auth.Api.Client;

public interface IAuthApiClient
{
    Task<OperationResult<bool>> Authenticated(string cookie);
}