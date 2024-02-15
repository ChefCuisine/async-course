using AsyncCourse.Client;

namespace AsyncCourse.Template.Api.Client;

public interface ITemplateApiClient
{
    Task<OperationResult<int>> Test();
}