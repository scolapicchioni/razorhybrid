using FluentValidation;
using PhotoSharingApplication.Shared.Entities;
using PhotoSharingApplication.Core.Interfaces;
using PhotoSharingApplication.Shared.Validators;

namespace PhotoSharingApplication.Core.Services;

public class PhotosService : IPhotosService {
    private readonly IPhotosRepository photosRepository;
    private readonly PhotoValidator validator;

    public PhotosService(IPhotosRepository photosRepository, PhotoValidator validator) => 
        (this.photosRepository, this.validator) = (photosRepository, validator);

    public Task AddPhotoAsync(Photo photo) {
        photo.SubmittedOn = DateTime.Now;
        validator.ValidateAndThrow(photo);
        return photosRepository.AddPhotoAsync(photo);
    }

    public Task<IEnumerable<Photo>> GetAllPhotosAsync() => photosRepository.GetAllPhotosAsync();
    public Task<Image?> GetImageByIdAsync(int id)=> photosRepository.GetImageByIdAsync(id);
    public Task<Photo?> GetPhotoByIdAsync(int id) => photosRepository.GetPhotoByIdAsync(id);
    public Task<Photo?> DeletePhotoAsync(int id) => photosRepository.DeletePhotoAsync(id);
    public Task<IEnumerable<Photo>> GetSetOfPhotosAsync(IEnumerable<int> ids) => photosRepository.GetSetOfPhotosAsync(ids);
}
