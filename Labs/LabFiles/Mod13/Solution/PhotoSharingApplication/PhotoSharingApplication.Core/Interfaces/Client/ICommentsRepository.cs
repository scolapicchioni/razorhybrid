using PhotoSharingApplication.Core.Entities;

namespace PhotoSharingApplication.Core.Interfaces.Client;

public interface ICommentsRepository {
    Task<IEnumerable<Comment>> GetCommentsForPhotoAsync(int photoId);
    Task<Comment?> GetCommentByIdAsync(int id);
    Task<Comment> AddCommentAsync(Comment comment);
}
