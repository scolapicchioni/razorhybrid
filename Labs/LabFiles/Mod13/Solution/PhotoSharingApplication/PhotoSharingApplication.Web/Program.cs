using Microsoft.EntityFrameworkCore;
using PhotoSharingApplication.Core.Entities;
using PhotoSharingApplication.Core.Interfaces;
using PhotoSharingApplication.Infrastructure.Data;
using PhotoSharingApplication.Web;
using Microsoft.AspNetCore.Identity;
using PhotoSharingApplication.Web.Data;
using PhotoSharingApplication.Web.AuthorizationHandlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.ResponseCompression;
using PhotoSharingApplication.Web.Chat;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//Our own extension method, contained in the ServiceCollectionExtensions class
builder.Services.AddPhotoSharingServices();

builder.Services.AddDbContext<PhotoSharingDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddDbContext<PhotoSharingIdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PhotoSharingIdentityContextConnection")));

builder.Services
    .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<PhotoSharingIdentityContext>();

//for blazor identity
//builder.Services.AddIdentityServer()
//    .AddApiAuthorization<IdentityUser, PhotoSharingIdentityContext>();

//builder.Services.AddAuthentication() //"Identity.Application"
//    .AddIdentityServerJwt();
//end blazor 

builder.Services.AddAuthorization(options => {
    options.AddPolicy("PhotoDeletionPolicy", policy => {
        //after Blazor auth
        policy.AuthenticationSchemes.Add("Identity.Application");
        policy.RequireAuthenticatedUser();
        //end
        policy.Requirements.Add(new PhotoOwnerRequirement());
    });
});

builder.Services.AddSingleton<IAuthorizationHandler, PhotoOwnerAuthorizationHandler>();


//session
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
//end session


//SignalR
builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opts => {
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});
//end SignalR

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();

app.UseStaticFiles();

app.UseRouting();

//blazor
//app.UseIdentityServer();
//end blazor

app.UseAuthentication();
app.UseAuthorization();

//session
app.UseSession();
//end session

//minimal API
app.MapGet("/photos/image/{id:int}", async (int id, IPhotosService photosService, IMemoryCache cache) => {
    string key = $"photo-{id}";
    Photo? photo;
    if(!cache.TryGetValue(key,out photo)) {
        photo = await photosService.GetPhotoByIdAsync(id);
        // Set cache options.
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            // Keep in cache for this time, reset time if accessed.
            .SetSlidingExpiration(TimeSpan.FromMinutes(10));

        // Save data in cache.
        cache.Set(key, photo, cacheEntryOptions);
    }
    
    if (photo is null || photo.PhotoFile is null) {
        return Results.NotFound();
    }
    return Results.File(photo.PhotoFile, photo.ContentType);
});
app.MapControllers();
app.MapRazorPages();

//SignalR
app.MapHub<ChatHub>("/chathub");
//end SignalR

app.Run();
