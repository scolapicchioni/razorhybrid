using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Moq;
using PhotoSharingApplication.Core.Interfaces;
using PhotoSharingApplication.Web.Pages.Photos;
using System.Threading.Tasks;
using Xunit;

namespace PhotoSharingApplication.Web.UnitTests.Pages.Photos;

public class UploadTests {
    private Mock<IPhotosService> photosServiceMock;
    private DefaultHttpContext? httpContext;
    private ModelStateDictionary modelState;
    private ActionContext actionContext;
    private EmptyModelMetadataProvider modelMetadataProvider;
    private ViewDataDictionary viewData;
    private TempDataDictionary tempData;
    private PageContext pageContext;
    private UploadModel pageModel;
    public UploadTests() {
        photosServiceMock = new Mock<IPhotosService>();
        httpContext = new DefaultHttpContext();
        modelState = new ModelStateDictionary();
        actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
        modelMetadataProvider = new EmptyModelMetadataProvider();
        viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
        tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
        pageContext = new PageContext(actionContext) {
            ViewData = viewData
        };
        pageModel = new UploadModel(photosServiceMock.Object) {
            PageContext = pageContext,
            TempData = tempData,
            Url = new UrlHelper(actionContext)
        };
    }
    [Fact]
    public async Task OnPost_ReturnsPageResult_WhenModelStateIsInvalid() {
        // Arrange
        pageModel.ModelState.AddModelError("Photo.Title", "The Title field is required.");

        // Act
        var result = await pageModel.OnPostAsync();

        // Assert
        Assert.IsType<PageResult>(result);
    }

    [Fact]
    public async Task OnPostAsync_ReturnsARedirectToPageResult_WhenModelStateIsValid() {
        // Arrange
        pageModel.Photo = new Shared.Entities.Photo();
        pageModel.FormFile = new Mock<IFormFile>().Object;

        // Act
        // A new ModelStateDictionary is valid by default.
        var result = await pageModel.OnPostAsync();

        // Assert
        Assert.IsType<RedirectToPageResult>(result);
    }
}
