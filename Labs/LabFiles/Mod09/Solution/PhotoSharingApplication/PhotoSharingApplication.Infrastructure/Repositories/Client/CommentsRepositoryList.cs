using PhotoSharingApplication.Core.Entities;
using PhotoSharingApplication.Core.Interfaces.Client;

namespace PhotoSharingApplication.Infrastructure.Repositories.Client;

public class CommentsRepositoryList : ICommentsRepository {
    private readonly List<Comment> comments;
    public CommentsRepositoryList() {
        comments = new List<Comment>() {
                new() { Id = 1, PhotoId = 1, Title = "One Comment for Photo 1", Body = "Lorem ipsum" },
                new() { Id = 2, PhotoId = 1, Title = "Anoter Comment for Photo 1", Body = "Bacon ipsum" },
                new() { Id = 3, PhotoId = 2, Title = "One Comment for Photo 2", Body = "Yadam ipsum" }
            };
    }
    public Task<Comment> AddCommentAsync(Comment comment) {
        comment.Id = comments.Max(c => c.Id) + 1;
        comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task<Comment?> GetCommentByIdAsync(int id) {
        return Task.FromResult(comments.FirstOrDefault(p => p.Id == id));
    }

    public Task<IEnumerable<Comment>> GetCommentsForPhotoAsync(int photoId) {
        return Task.FromResult(comments.Where(c => c.PhotoId == photoId));
    }
}
