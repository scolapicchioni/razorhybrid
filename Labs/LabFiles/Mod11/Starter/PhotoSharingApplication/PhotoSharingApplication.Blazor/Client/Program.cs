using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PhotoSharingApplication.Blazor.Core.Interfaces;
using PhotoSharingApplication.Blazor.Infrastructure.Repositories;
using PhotoSharingApplication.Blazor.Core.Services;
using PhotoSharingApplication.Shared.Validators;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<ICommentsRepository, CommentsRepositoryHttp>();
builder.Services.AddScoped<ICommentsService, CommentsService>();
builder.Services.AddSingleton<CommentValidator>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
