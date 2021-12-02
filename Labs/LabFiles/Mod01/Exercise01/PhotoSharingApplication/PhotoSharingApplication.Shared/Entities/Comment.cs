namespace PhotoSharingApplication.Shared.Entities;

public class Comment {
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string SubmittedBy { get; set; } = string.Empty;
    public DateTime SubmittedOn { get; set; }
    public int PhotoId { get; set; }

    
}
