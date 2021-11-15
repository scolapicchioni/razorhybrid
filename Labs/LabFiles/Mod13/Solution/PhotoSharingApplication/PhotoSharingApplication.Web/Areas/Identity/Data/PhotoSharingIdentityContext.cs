using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace PhotoSharingApplication.Web.Data;

public class PhotoSharingIdentityContext : ApiAuthorizationDbContext<IdentityUser> /* IdentityDbContext<IdentityUser>*/
{
    public PhotoSharingIdentityContext(DbContextOptions<PhotoSharingIdentityContext> options, IOptions<OperationalStoreOptions> operationalStoreOptions) 
        : base(options, operationalStoreOptions) {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}


//before Blazor
//public class PhotoSharingIdentityContext : ApiAuthorizationDbContext<IdentityUser> 
//{
//    public PhotoSharingIdentityContext(DbContextOptions<PhotoSharingIdentityContext> options)
//        : base(options) {
//    }

//    protected override void OnModelCreating(ModelBuilder builder) {
//        base.OnModelCreating(builder);
//        // Customize the ASP.NET Identity model and override the defaults if needed.
//        // For example, you can rename the ASP.NET Identity table names and more.
//        // Add your customizations after calling base.OnModelCreating(builder);
//    }
//}
