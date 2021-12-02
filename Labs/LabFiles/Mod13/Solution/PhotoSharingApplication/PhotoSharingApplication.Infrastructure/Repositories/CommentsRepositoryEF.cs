using Microsoft.EntityFrameworkCore;
using PhotoSharingApplication.Shared.Entities;
using PhotoSharingApplication.Core.Interfaces;
using PhotoSharingApplication.Infrastructure.Data;

namespace PhotoSharingApplication.Infrastructure.Repositories;

public class CommentsRepositoryEF : ICommentsRepository {
    private readonly PhotoSharingDbContext dbContext;

    public CommentsRepositoryEF(PhotoSharingDbContext dbContext) {
        this.dbContext = dbContext;
    }
    public async Task<Comment> AddCommentAsync(Comment comment) {
        dbContext.Comments.Add(comment);
        await dbContext.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> GetCommentByIdAsync(int id) => await dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);

    public async Task<IEnumerable<Comment>> GetCommentsForPhotoAsync(int photoId) => await dbContext.Comments.Where(c => c.PhotoId == photoId).ToListAsync();
}
