using PhotoSharingApplication.Core.Interfaces;
using PhotoSharingApplication.Core.Services;
using PhotoSharingApplication.Infrastructure.Repositories;
using PhotoSharingApplication.Core.Validators;
using FluentValidation.AspNetCore;
using PhotoSharingApplication.Core.Interfaces.Client;
using PhotoSharingApplication.Core.Services.Client;
using PhotoSharingApplication.Infrastructure.Repositories.Client;

namespace PhotoSharingApplication.Web;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddPhotoSharingServices(this IServiceCollection services) {
        //services for Validation
        services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PhotoValidator>());
        
        services.AddScoped<IPhotosRepository, PhotosRepositoryEF>();
        services.AddScoped<IPhotosService, PhotosService>();

        services.AddSingleton<Core.Interfaces.Client.ICommentsRepository, Infrastructure.Repositories.Client.CommentsRepositoryList>();
        services.AddScoped<Core.Interfaces.Client.ICommentsService, Core.Services.Client.CommentsService>();

        return services;
    }
}
