using PhotoSharingApplication.Core.Interfaces;
using PhotoSharingApplication.Core.Services;
using PhotoSharingApplication.Infrastructure.Repositories;
using PhotoSharingApplication.Core.Validators;
using FluentValidation.AspNetCore;
using PhotoSharingApplication.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace PhotoSharingApplication.Web;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddPhotoSharingServices(this IServiceCollection services) {
        //services for Validation
        services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PhotoValidator>());

        services.AddScoped<IPhotosRepository, PhotosRepositoryEF>();
        services.AddScoped<IPhotosService, PhotosService>();
        return services;
    }
}
