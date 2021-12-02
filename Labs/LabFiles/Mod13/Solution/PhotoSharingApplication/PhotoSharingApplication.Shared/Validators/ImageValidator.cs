using FluentValidation;
using PhotoSharingApplication.Shared.Entities;

namespace PhotoSharingApplication.Shared.Validators;

public class ImageValidator : AbstractValidator<Image> {
    public ImageValidator() {
        RuleFor(image => image.ContentType).NotEmpty();
    }
}
