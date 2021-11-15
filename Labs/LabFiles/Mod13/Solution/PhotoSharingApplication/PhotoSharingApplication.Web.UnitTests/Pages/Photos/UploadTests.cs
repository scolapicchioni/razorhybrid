using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using Xunit;

namespace PhotoSharingApplication.Web.UnitTests.Pages.Photos;

public class UploadTests {
    [Fact]
    public async Task OnPost_ReturnsPageResult_WhenModelStateIsInvalid() {
        // Arrange

        Mock<IPhotosService> photosServiceMock = new Mock<IPhotosService>();

        var httpContext = new DefaultHttpContext();
        var modelState = new ModelStateDictionary();
        var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
        var modelMetadataProvider = new EmptyModelMetadataProvider();
        var viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
        var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
        var pageContext = new PageContext(actionContext) {
            ViewData = viewData
        };
        var pageModel = new UploadModel(photosServiceMock.Object) {
            PageContext = pageContext,
            TempData = tempData,
            Url = new UrlHelper(actionContext)
        };
        pageModel.ModelState.AddModelError("Photo.Title", "The Title field is required.");

        // Act
        var result = await pageModel.OnPostAsync();

        // Assert
        Assert.IsType<PageResult>(result);
    }

    [Fact]
    public async Task OnPostAsync_ReturnsARedirectToPageResult_WhenModelStateIsValid() {
        // Arrange
        Mock<IPhotosService> photosServiceMock = new Mock<IPhotosService>();
        
        var httpContext = new DefaultHttpContext();
        var modelState = new ModelStateDictionary();
        var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
        var modelMetadataProvider = new EmptyModelMetadataProvider();
        var viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
        var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
        var pageContext = new PageContext(actionContext) {
            ViewData = viewData
        };
        var pageModel = new UploadModel(photosServiceMock.Object) {
            PageContext = pageContext,
            TempData = tempData,
            Url = new UrlHelper(actionContext),
            Photo = new Core.Entities.Photo(),
            FormFile = new Mock<IFormFile>().Object
        };

        // Act
        // A new ModelStateDictionary is valid by default.
        var result = await pageModel.OnPostAsync();

        // Assert
        Assert.IsType<RedirectToPageResult>(result);
    }
}
