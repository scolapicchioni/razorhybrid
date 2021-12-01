using Moq;
using PhotoSharingApplication.Core.Entities;
using PhotoSharingApplication.Core.Interfaces;
using PhotoSharingApplication.Web.Pages.Photos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PhotoSharingApplication.Web.UnitTests.Pages.Photos;

public class IndexTests {
    [Fact]
    public async Task OnGet_Sets_Photos() {
        Mock<IPhotosService> photosServiceMock = new Mock<IPhotosService>();
        List<Photo> expected = new List<Photo>() { 
            new Photo(){Id = 1, Title = "Title1", Description = "Description1" },
            new Photo(){Id = 2, Title = "Title2", Description = "Description2" },
            new Photo(){Id = 3, Title = "Title3", Description = "Description3" },
        };
        photosServiceMock.Setup(ps => ps.GetAllPhotosAsync()).ReturnsAsync(expected);

        IndexModel index = new IndexModel(photosServiceMock.Object);

        await index.OnGetAsync();

        var actual = Assert.IsAssignableFrom<List<Photo>>(index.Photos);
        
        Assert.True(expected.SequenceEqual(actual));
    }
}
