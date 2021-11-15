using FluentValidation;
using PhotoSharingApplication.Core.Entities;
using PhotoSharingApplication.Core.Interfaces;
using PhotoSharingApplication.Core.Validators;

namespace PhotoSharingApplication.Core.Services;

public class PhotosService : IPhotosService {
    private readonly IPhotosRepository photosRepository;
    private readonly PhotoValidator validator;

    public PhotosService(IPhotosRepository photosRepository, PhotoValidator validator) {
        this.photosRepository = photosRepository;
        this.validator = validator;
    }
    public Task AddPhotoAsync(Photo photo) {
        photo.SubmittedOn = DateTime.Now;
        validator.ValidateAndThrow(photo);
        return photosRepository.AddPhotoAsync(photo);
    }

    public Task<Photo?> DeletePhotoAsync(int id) => photosRepository.DeletePhotoAsync(id);

    public Task<IEnumerable<Photo>> GetAllPhotosAsync() {
        return photosRepository.GetAllPhotosAsync();
    }

    public Task<Photo?> GetPhotoByIdAsync(int id) {
        return photosRepository.GetPhotoByIdAsync(id);
    }
}
