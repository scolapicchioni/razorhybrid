using PhotoSharingApplication.Core.Interfaces;
using PhotoSharingApplication.Core.Services;
using PhotoSharingApplication.Infrastructure.Repositories;
using FluentValidation.AspNetCore;
using PhotoSharingApplication.Shared.Validators;
using PhotoSharingApplication.Web.Controllers;
using PhotoSharingApplication.Web.Services;

namespace PhotoSharingApplication.Web;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddPhotoSharingServices(this IServiceCollection services) {
        //services for Validation
        services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PhotoValidator>());
        
        services.AddScoped<IPhotosRepository, PhotosRepositoryEF>();
        services.AddScoped<IPhotosService, PhotosService>();

        services.AddScoped<IPhotosServiceCache, PhotosServiceCache>();

        //Blazor Client, but server side (for prerendering)
        services.AddScoped<CommentsController>();
        services.AddScoped<PhotoSharingApplication.Blazor.Core.Interfaces.ICommentsRepository, PhotoSharingApplication.Web.Controllers.CommentsRepositoryApi>();
        services.AddScoped<PhotoSharingApplication.Blazor.Core.Interfaces.ICommentsService, PhotoSharingApplication.Core.Services.Client.CommentsService>();

        //Web Api
        services.AddScoped<Core.Interfaces.ICommentsRepository, Infrastructure.Repositories.CommentsRepositoryEF>();
        services.AddScoped<Core.Interfaces.ICommentsService, Core.Services.CommentsService>();

        return services;
    }
}
