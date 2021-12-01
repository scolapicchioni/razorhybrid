# Unit Testing

## Goals

Write some unit tests for your razor pages
- IndexTests
    - OnGet_Sets_Photos
- UploadTests
    - OnPost_ReturnsPageResult_WhenModelStateIsInvalid
    - OnPostAsync_ReturnsARedirectToPageResult_WhenModelStateIsValid

Write some unit tests for your API Controller
- GetCommentsForPhoto_ShouldReturnComments
- GetCommentById_ShouldReturnNotFound_WhenCommentIdDoesNotExist

Write some unit test for your Minimal API
- GetPhotoImage_ShouldReturnFile_WhenPhotoExists
- GetPhotoImage_ShouldReturnNotFound_WhenPhotoDoesNotExist

## Resources
- https://docs.microsoft.com/en-us/aspnet/core/test/razor-pages-tests?view=aspnetcore-6.0
- https://github.com/dotnet/AspNetCore.Docs/tree/main/aspnetcore/test/razor-pages-tests/samples
- https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-6.0#test-actionresultt
- https://www.hanselman.com/blog/minimal-apis-in-net-6-but-where-are-the-unit-tests
- https://github.com/DamianEdwards/MinimalApiPlayground/blob/main/tests/MinimalApiPlayground.Tests/Examples.cs