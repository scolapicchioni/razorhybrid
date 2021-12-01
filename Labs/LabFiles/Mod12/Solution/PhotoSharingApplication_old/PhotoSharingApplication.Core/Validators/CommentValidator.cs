using FluentValidation;
using PhotoSharingApplication.Core.Entities;

namespace PhotoSharingApplication.Core.Validators;

public class CommentValidator : AbstractValidator<Comment> {
    public CommentValidator() {
        RuleFor(comment => comment.Title).NotEmpty().MaximumLength(100);
        RuleFor(comment => comment.Body).NotEmpty().MaximumLength(250);
    }
}
