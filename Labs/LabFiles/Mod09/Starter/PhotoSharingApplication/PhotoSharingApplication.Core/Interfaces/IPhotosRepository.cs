using PhotoSharingApplication.Core.Entities;

namespace PhotoSharingApplication.Core.Interfaces;

public interface IPhotosRepository {
    Task<IEnumerable<Photo>> GetAllPhotosAsync();
    Task<Photo?> GetPhotoByIdAsync(int id);
    Task AddPhotoAsync(Photo photo);
}
