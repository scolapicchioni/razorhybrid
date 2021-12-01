using Moq;
using PhotoSharingApplication.Core.Interfaces;
using PhotoSharingApplication.Shared.Entities;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace PhotoSharingApplication.Web.UnitTests.MinimalApi;

public class MinimalApiTests {
    [Fact]
    public async Task GetPhotoImage_ShouldReturnFile_WhenPhotoExists() {
        //Arrange
        Mock<IPhotosService> mock = new();
        string expectedContentType = "jpg";
        byte[] expectedContent = new byte[] { 1, 2, 3, 4 };
        int id = 1;
        Photo photo = new() { ContentType = expectedContentType, PhotoFile = expectedContent };
        mock.Setup(s => s.GetPhotoByIdAsync(id)).ReturnsAsync(photo);
        await using var application = new PhotoSharingApplicationApp(mock);

        var client = application.CreateClient();

        //Act
        using HttpResponseMessage res = await client.GetAsync($"/photos/image/{id}");

        //Assert
        string? actualContentType = res.Content.Headers.First(kv => kv.Key == "Content-Type").Value.FirstOrDefault(v => v == expectedContentType);
        Assert.NotNull(actualContentType);
        byte[] actualContent = await res.Content.ReadAsByteArrayAsync();
        Assert.Equal(expectedContent, actualContent);

        //FileContentResult file = new FileContentResult(await res.Content.ReadAsByteArrayAsync(), res.Content.Headers.ContentType.MediaType);
        //Assert.Equal(expectedContentType, file.ContentType);
        //Assert.Equal(expectedContent, file.FileContents);

    }

    [Fact]
    public async Task GetPhotoImage_ShouldReturnNotFound_WhenPhotoDoesNotExist() {
        //Arrange
        Mock<IPhotosService> mock = new();
        int id = 1;
        mock.Setup(s => s.GetPhotoByIdAsync(id)).ReturnsAsync(default(Photo));
        await using var application = new PhotoSharingApplicationApp(mock);

        var client = application.CreateClient();

        //Act
        using HttpResponseMessage res = await client.GetAsync($"/photos/image/{id}");

        //Assert
        Assert.Equal(HttpStatusCode.NotFound, res.StatusCode);

    }
}
