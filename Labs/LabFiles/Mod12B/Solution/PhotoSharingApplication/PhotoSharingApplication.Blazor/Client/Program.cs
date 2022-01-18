using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PhotoSharingApplication.Blazor.Core.Interfaces;
using PhotoSharingApplication.Blazor.Infrastructure.Repositories;
using PhotoSharingApplication.Blazor.Core.Services;
using PhotoSharingApplication.Shared.Validators;
using PhotoSharingApplication.Blazor.Client.RequestHandlers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<ICommentsRepository, CommentsRepositoryHttp>();
builder.Services.AddScoped<ICommentsService, CommentsService>();
builder.Services.AddSingleton<CommentValidator>();
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


builder.Services.AddTransient<IncludeRequestCredentialsMessageHandler>();

builder.Services.AddHttpClient<ICommentsRepository, CommentsRepositoryHttp>(httpClient => httpClient.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<IncludeRequestCredentialsMessageHandler>();
                //.ConfigurePrimaryHttpMessageHandler(x=> {
                //    HttpClientHandler handler = new HttpClientHandler();
                //    handler.AllowAutoRedirect = false;
                //    return handler;
                //});

await builder.Build().RunAsync();
