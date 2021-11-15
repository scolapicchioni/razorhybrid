using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PhotoSharingApplication.Core.Validators;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<PhotoSharingApplication.Core.Interfaces.Client.ICommentsRepository, PhotoSharingApplication.Infrastructure.Repositories.Client.CommentsRepositoryHttp>();
builder.Services.AddScoped<PhotoSharingApplication.Core.Interfaces.Client.ICommentsService, PhotoSharingApplication.Core.Services.Client.CommentsService>();
builder.Services.AddSingleton<CommentValidator>();

//before adding auth
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//end

//after adding auth
builder.Services.AddHttpClient("BlazorAppIdentity.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorAppIdentity.ServerAPI"));

builder.Services.AddApiAuthorization();
//end

await builder.Build().RunAsync();
