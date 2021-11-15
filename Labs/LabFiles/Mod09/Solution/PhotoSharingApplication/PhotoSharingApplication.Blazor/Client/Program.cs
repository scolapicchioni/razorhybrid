using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PhotoSharingApplication.Core.Validators;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddSingleton<PhotoSharingApplication.Core.Interfaces.Client.ICommentsRepository, PhotoSharingApplication.Infrastructure.Repositories.Client.CommentsRepositoryList>();
builder.Services.AddScoped<PhotoSharingApplication.Core.Interfaces.Client.ICommentsService, PhotoSharingApplication.Core.Services.Client.CommentsService>();
builder.Services.AddSingleton<CommentValidator>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
