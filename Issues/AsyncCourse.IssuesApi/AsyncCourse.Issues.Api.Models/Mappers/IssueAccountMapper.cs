namespace AsyncCourse.Issues.Api.Models.Mappers;

public static class IssueAccountMapper
{
    public static string GetFullName(string surname, string name)
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
}