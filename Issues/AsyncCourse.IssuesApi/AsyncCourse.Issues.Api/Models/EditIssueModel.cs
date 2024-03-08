﻿namespace AsyncCourse.Issues.Api.Models;

public class EditIssueModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }
    
    public string Description { get; set; }

    public IssueStatusModel Status { get; set; }
    
    public Guid? AssignedToAccountId { get; set; }
}