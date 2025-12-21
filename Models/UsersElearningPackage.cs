using System;

namespace Models;

public class UsersELearningPackage
{
    public string ID { get; set; }
    public string UserID { get; set; }
    public string CourseName { get; set; }
    public DateTime DateStamp { get; set; }
    public DateTime DateFirstViewed { get; set; }
    public CompletionStatus CompletionStatus { get; set; } = CompletionStatus.Complete;
    public SuccessStatus SuccessStatus { get; set; } = SuccessStatus.Failed;

    public string CandidateDirectELearningUrl { get; set; }

    public string CompletionStatusText => CompletionStatus.ToString();
    public string SuccessStatusText => SuccessStatus.ToString();

    public string DateFirstViewedDisplay
        => DateFirstViewed == DateTime.MinValue ? "Not Started" : DateFirstViewed.ToString("g");
}

public enum SuccessStatus
{
	Unknown = 1,
	Failed = 2,
	Passed = 4,
}

/// <summary>
/// Represents the completion status of a UserELearningPackage.
/// </summary>
public enum CompletionStatus
{
	Unknown = 1,
	Complete = 2,
	Incomplete = 4,
}