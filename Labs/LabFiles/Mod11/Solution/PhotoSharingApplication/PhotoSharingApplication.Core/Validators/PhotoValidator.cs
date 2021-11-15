using FluentValidation;
using PhotoSharingApplication.Core.Entities;

namespace PhotoSharingApplication.Core.Validators;

public class PhotoValidator : AbstractValidator<Photo> {
    public PhotoValidator() {
        RuleFor(photo => photo.Title).NotEmpty().MaximumLength(100);
        RuleFor(photo => photo.Description).NotEmpty().MaximumLength(250);
        RuleFor(photo => photo.PhotoFile).NotEmpty();
    }
}
