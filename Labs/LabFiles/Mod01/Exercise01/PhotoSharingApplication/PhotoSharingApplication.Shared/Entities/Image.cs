namespace PhotoSharingApplication.Shared.Entities;

public class Image {
    public int Id { get; set; }
    public byte[] PhotoFile { get; set; }
    public string ContentType { get; set; } = string.Empty;
}
