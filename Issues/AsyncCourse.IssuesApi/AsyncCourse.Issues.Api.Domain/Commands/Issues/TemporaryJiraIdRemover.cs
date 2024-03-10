namespace AsyncCourse.Issues.Api.Domain.Commands.Issues;

public static class TemporaryJiraIdRemover
{
    public static (string, string) SeparateTitle(string title)
    {
        if (!title.Contains('[') && !title.Contains(']'))
        {
            return (title, string.Empty);
        }

        return (title, string.Empty); // но уже без содержимого квадратных скобок
    }
}