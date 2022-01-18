using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using PhotoSharingApplication.Core.Interfaces;
using PhotoSharingApplication.Web.Controllers;

namespace PhotoSharingApplication.Web.UnitTests.MinimalApi;

class PhotoSharingApplicationApp : WebApplicationFactory<CommentsController> {
    private readonly Mock<IPhotosService> mock;

    public PhotoSharingApplicationApp(Mock<IPhotosService> mock) {
        this.mock = mock;
    }
    protected override IHost CreateHost(IHostBuilder builder) {
        //var root = new InMemoryDatabaseRoot();

        builder.ConfigureServices(services => {
            //    services.AddScoped(sp => {
            //        // Replace SQLite with the in memory provider for tests
            //        return new DbContextOptionsBuilder<PhotoSharingDbContext>()
            //                    .UseInMemoryDatabase("Tests", root)
            //                    .UseApplicationServiceProvider(sp)
            //                    .Options;


            //    });
            services.AddScoped<IPhotosService>(s => mock.Object);
        });

        return base.CreateHost(builder);
    }
}
