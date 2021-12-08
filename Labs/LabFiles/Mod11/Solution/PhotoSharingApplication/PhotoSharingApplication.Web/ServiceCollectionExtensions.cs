using FluentValidation.AspNetCore;
using PhotoSharingApplication.Core.Interfaces;
using PhotoSharingApplication.Core.Services;
using PhotoSharingApplication.Shared.Validators;
using PhotoSharingApplication.Web.Controllers;

namespace PhotoSharingApplication.Web;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddPhotoSharingServices(this IServiceCollection services) {
        //services for Validation
        services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PhotoValidator>());

        services.AddScoped<IPhotosService, PhotosService>();

        services.AddServicesForBlazorPrerendering();

        services.AddServicesForCommentsWebApi();

        services.AddOpenApiSupport();

        return services;
    }

    private static IServiceCollection AddOpenApiSupport(this IServiceCollection services) {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }

    private static IServiceCollection AddServicesForCommentsWebApi(this IServiceCollection services) {
        services.AddScoped<Core.Interfaces.ICommentsRepository, Infrastructure.Repositories.CommentsRepositoryEF>();
        services.AddScoped<Core.Interfaces.ICommentsService, Core.Services.CommentsService>();
        return services;
    }

    private static IServiceCollection AddServicesForBlazorPrerendering(this IServiceCollection services) {
        services.AddScoped<CommentsController>();
        services.AddScoped<PhotoSharingApplication.Blazor.Core.Interfaces.ICommentsRepository, PhotoSharingApplication.Web.Controllers.CommentsRepositoryApi>();
        services.AddScoped<PhotoSharingApplication.Blazor.Core.Interfaces.ICommentsService, PhotoSharingApplication.Core.Services.Client.CommentsService>();
        return services;
    }
}
