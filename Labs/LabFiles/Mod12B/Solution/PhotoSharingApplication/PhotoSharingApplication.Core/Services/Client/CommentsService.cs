using FluentValidation;
using PhotoSharingApplication.Shared.Entities;
using PhotoSharingApplication.Shared.Validators;
using PhotoSharingApplication.Blazor.Core.Interfaces;

namespace PhotoSharingApplication.Core.Services.Client;

public class CommentsService : ICommentsService {
    private readonly ICommentsRepository repository;
    private readonly CommentValidator validator;

    public CommentsService(ICommentsRepository repository, CommentValidator validator) {
        this.repository = repository;
        this.validator = validator;
    }
    public Task<Comment> AddCommentAsync(Comment comment) {
        validator.ValidateAndThrow(comment);
        return repository.AddCommentAsync(comment);
    }

    public Task<Comment?> DeleteCommentAsync(int id) => repository.DeleteCommentAsync(id);

    public Task<Comment?> GetCommentByIdAsync(int id) => repository.GetCommentByIdAsync(id);

    public Task<IEnumerable<Comment>> GetCommentsForPhotoAsync(int photoId) => repository.GetCommentsForPhotoAsync(photoId);
}
