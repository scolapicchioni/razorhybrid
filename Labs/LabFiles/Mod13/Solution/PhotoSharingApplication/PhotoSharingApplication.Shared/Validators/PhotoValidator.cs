using FluentValidation;
using PhotoSharingApplication.Shared.Entities;

namespace PhotoSharingApplication.Shared.Validators;

public class PhotoValidator : AbstractValidator<Photo> {
    public PhotoValidator() {
        RuleFor(photo => photo.Title).NotEmpty().MaximumLength(100);
        RuleFor(photo => photo.Description).NotEmpty().MaximumLength(250);
        RuleFor(photo => photo.Image).NotEmpty().SetValidator(new ImageValidator());
    }
}
