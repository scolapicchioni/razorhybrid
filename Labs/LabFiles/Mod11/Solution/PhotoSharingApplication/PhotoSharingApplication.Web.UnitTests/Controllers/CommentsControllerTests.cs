using Microsoft.AspNetCore.Mvc;
using Moq;
using PhotoSharingApplication.Core.Interfaces;
using PhotoSharingApplication.Shared.Entities;
using PhotoSharingApplication.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PhotoSharingApplication.Web.UnitTests.Controllers;

public class CommentsControllerTests {
    private Mock<ICommentsService> commentsServiceMock;
    private CommentsController sut;
    public CommentsControllerTests() {
        commentsServiceMock = new Mock<ICommentsService>();
        sut = new CommentsController(commentsServiceMock.Object);
    }
    [Fact]
    public async Task GetCommentsForPhoto_ShouldReturnComments() {
        //Arrange        
        List<Comment> expected = new List<Comment>() {
            new Comment(){Id = 1, Title = "Title1", Body = "Body1" },
            new Comment(){Id = 2, Title = "Title2", Body = "Body2" },
            new Comment(){Id = 3, Title = "Title3", Body = "Body3" },
        };
        commentsServiceMock.Setup(ps => ps.GetCommentsForPhotoAsync(1)).ReturnsAsync(expected);

        //Act
        var result = await sut.GetCommentsForPhoto(1);

        // Assert
        ActionResult<IEnumerable<Comment>> actionResult =
            Assert.IsType<ActionResult<IEnumerable<Comment>>>(result);
        List<Comment> returnValue = Assert.IsType<List<Comment>>(actionResult.Value);
        Assert.True(returnValue.SequenceEqual(expected));
    }

    [Fact]
    public async Task GetCommentById_ShouldReturnNotFound_WhenCommentIdDoesNotExist() {
        //Arrange        
        commentsServiceMock.Setup(ps => ps.GetCommentByIdAsync(1)).ReturnsAsync((Comment)null);

        //Act
        var result = await sut.GetCommentById(1);

        // Assert
        ActionResult<Comment> actionResult = Assert.IsType<ActionResult<Comment>>(result);
        Assert.IsType<NotFoundResult>(actionResult.Result);
    }
}
