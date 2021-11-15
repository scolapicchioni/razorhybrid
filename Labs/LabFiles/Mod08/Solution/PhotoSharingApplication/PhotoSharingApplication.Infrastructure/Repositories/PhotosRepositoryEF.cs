using Microsoft.EntityFrameworkCore;
using PhotoSharingApplication.Core.Entities;
using PhotoSharingApplication.Core.Interfaces;
using PhotoSharingApplication.Infrastructure.Data;

namespace PhotoSharingApplication.Infrastructure.Repositories;

public class PhotosRepositoryEF : IPhotosRepository {
    private readonly PhotoSharingDbContext dbContext;

    public PhotosRepositoryEF(PhotoSharingDbContext dbContext) {
        this.dbContext = dbContext;
    }
    public async Task AddPhotoAsync(Photo photo) {
        dbContext.Photos.Add(photo);
        await dbContext.SaveChangesAsync();
    }

    async Task<IEnumerable<Photo>> IPhotosRepository.GetAllPhotosAsync() => await dbContext.Photos.ToListAsync();

    public async Task<Photo?> GetPhotoByIdAsync(int id) => await dbContext.Photos.FirstOrDefaultAsync(p => p.Id == id);
}
