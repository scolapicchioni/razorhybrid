using PhotoSharingApplication.Core.Interfaces;
using PhotoSharingApplication.Core.Services;
using PhotoSharingApplication.Infrastructure.Repositories;
using PhotoSharingApplication.Core.Validators;
using FluentValidation.AspNetCore;

namespace PhotoSharingApplication.Web;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddPhotoSharingServices(this IServiceCollection services) {
        //services for Validation
        services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PhotoValidator>());

        services.AddSingleton<IPhotosRepository, PhotosRepositoryList>();
        services.AddScoped<IPhotosService, PhotosService>();
        return services;
    }
}
