namespace PhotoSharingApplication.Shared.Entities;

public class Photo {
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public byte[] PhotoFile { get; set; }
    public string ContentType { get; set; } = string.Empty;
    public string SubmittedBy { get; set; } = string.Empty;
    public DateTime SubmittedOn { get; set; }
    public List<Comment>? Comments { get; set; }
}
