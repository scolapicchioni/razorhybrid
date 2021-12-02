using PhotoSharingApplication.Shared.Entities;

namespace PhotoSharingApplication.Core.Interfaces;

public interface IPhotosService {
    Task<IEnumerable<Photo>> GetAllPhotosAsync();
    Task<Photo?> GetPhotoByIdAsync(int id);
    Task<Image?> GetImageByIdAsync(int id);
    Task AddPhotoAsync(Photo photo);
    Task<Photo?> DeletePhotoAsync(int id);
    Task<IEnumerable<Photo>> GetSetOfPhotosAsync(IEnumerable<int> ids);
}
