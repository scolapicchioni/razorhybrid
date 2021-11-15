using FluentValidation;
using PhotoSharingApplication.Core.Entities;
using PhotoSharingApplication.Core.Interfaces.Client;
using PhotoSharingApplication.Core.Validators;

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

    public Task<Comment?> GetCommentByIdAsync(int id) => repository.GetCommentByIdAsync(id);

    public Task<IEnumerable<Comment>> GetCommentsForPhotoAsync(int photoId) => repository.GetCommentsForPhotoAsync(photoId);
}
