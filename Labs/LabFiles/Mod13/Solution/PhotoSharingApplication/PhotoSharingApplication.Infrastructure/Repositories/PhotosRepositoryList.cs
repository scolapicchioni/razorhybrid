using PhotoSharingApplication.Shared.Entities;
using PhotoSharingApplication.Core.Interfaces;

namespace PhotoSharingApplication.Infrastructure.Repositories;

public class PhotosRepositoryList : IPhotosRepository {
    private static List<Photo> photos;
    public PhotosRepositoryList() {
        photos = new() {
            new() { Id = 1, Title = "One Photo", Description = "The first photo" },
            new() { Id = 2, Title = "Another Photo", Description = "The second photo" }
        };
    }
    public Task AddPhotoAsync(Photo photo) {
        photo.Id = photos.Max(p => p.Id) + 1;
        photos.Add(photo);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Photo>> GetAllPhotosAsync() => Task.FromResult((IEnumerable<Photo>)photos);
    public Task<IEnumerable<Photo>> GetSetOfPhotosAsync(IEnumerable<int> ids) => Task.FromResult(photos.Where(p => ids.Contains(p.Id)));
    public Task<Image?> GetImageByIdAsync(int id) => Task.FromResult<Image?>(photos.FirstOrDefault(p => p.Id == id)?.Image);
    public Task<Photo?> GetPhotoByIdAsync(int id) => Task.FromResult(photos.FirstOrDefault(p => p.Id == id));
    public Task<Photo?> DeletePhotoAsync(int id) {
        Photo? photo = photos.FirstOrDefault(p => p.Id == id);
        if (photo is not null) {
            photos.Remove(photo);
        }
        return Task.FromResult(photo);
    }
}
