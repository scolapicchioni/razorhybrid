using PhotoSharingApplication.Shared.Entities;

namespace PhotoSharingApplication.Core.Interfaces;

public interface IPhotosService {
    Task<IEnumerable<Photo>> GetAllPhotosAsync();
    Task<Photo?> GetPhotoByIdAsync(int id);
    Task AddPhotoAsync(Photo photo);
    Task<Photo?> DeletePhotoAsync(int id);
}
