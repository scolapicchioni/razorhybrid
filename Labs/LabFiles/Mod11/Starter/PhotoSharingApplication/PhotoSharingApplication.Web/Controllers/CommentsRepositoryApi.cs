using PhotoSharingApplication.Blazor.Core.Interfaces;
using PhotoSharingApplication.Shared.Entities;

namespace PhotoSharingApplication.Web.Controllers;

public class CommentsRepositoryApi : ICommentsRepository {
    private readonly CommentsController controller;

    public CommentsRepositoryApi(CommentsController controller) {
        this.controller = controller;
    }
    public async Task<Comment> AddCommentAsync(Comment comment) => (await controller.AddComment(comment)).Value;

    public async Task<Comment?> GetCommentByIdAsync(int id) => (await controller.GetCommentById(id)).Value;

    public async Task<IEnumerable<Comment>> GetCommentsForPhotoAsync(int photoId) => (await controller.GetCommentsForPhoto(photoId)).Value;
}
