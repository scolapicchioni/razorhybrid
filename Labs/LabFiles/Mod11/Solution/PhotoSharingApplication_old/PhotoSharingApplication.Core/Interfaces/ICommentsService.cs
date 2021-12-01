using PhotoSharingApplication.Core.Entities;

namespace PhotoSharingApplication.Core.Interfaces;

public interface ICommentsService {
    Task<IEnumerable<Comment>> GetCommentsForPhotoAsync(int photoId);
    Task<Comment?> GetCommentByIdAsync(int id);
    Task<Comment> AddCommentAsync(Comment comment);
}
