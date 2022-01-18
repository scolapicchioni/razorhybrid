using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PhotoSharingApplication.Core.Interfaces;
using PhotoSharingApplication.Infrastructure.Data;
using PhotoSharingApplication.Infrastructure.Repositories;

namespace PhotoSharingApplication.Infrastructure;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddPhotoSharingDb(this IServiceCollection services, string connectionString) {
        services.AddDbContext<PhotoSharingDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IPhotosRepository, PhotosRepositoryEF>();
        return services;
    }
}
