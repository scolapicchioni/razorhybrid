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

## Resources
- https://docs.microsoft.com/en-us/aspnet/core/test/razor-pages-tests?view=aspnetcore-6.0
- https://github.com/dotnet/AspNetCore.Docs/tree/main/aspnetcore/test/razor-pages-tests/samples
- https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-6.0#test-actionresultt