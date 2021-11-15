using PhotoSharingApplication.Core.Entities;

namespace PhotoSharingApplication.Core.Interfaces;

public interface IPhotosRepository {
    Task<IEnumerable<Photo>> GetAllPhotosAsync();
    Task<IEnumerable<Photo>> GetSetOfPhotosAsync(IEnumerable<int> ids);
    Task<Photo?> GetPhotoByIdAsync(int id);
    Task AddPhotoAsync(Photo photo);
    Task<Photo?> DeletePhotoAsync(int id);
}
