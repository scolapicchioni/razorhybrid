using PhotoSharingApplication.Core.Interfaces;
using PhotoSharingApplication.Core.Services;
using PhotoSharingApplication.Infrastructure.Repositories;

namespace PhotoSharingApplication.Web;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddPhotoSharingServices(this IServiceCollection services) {
        services.AddSingleton<IPhotosRepository, PhotosRepositoryList>();
        services.AddScoped<IPhotosService, PhotosService>();
        return services;
    }
}
