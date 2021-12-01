namespace PhotoSharingApplication.Shared.Entities;

public class Comment {
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public int PhotoId { get; set; }

    public int Max(Func<object, object> p) {
        throw new NotImplementedException();
    }
}
