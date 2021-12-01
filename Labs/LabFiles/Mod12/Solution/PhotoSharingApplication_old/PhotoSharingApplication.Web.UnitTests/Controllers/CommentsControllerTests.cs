using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PhotoSharingApplication.Core.Entities;
using PhotoSharingApplication.Core.Interfaces;
using PhotoSharingApplication.Web.Controllers;
using Xunit;

namespace PhotoSharingApplication.Web.UnitTests.Controllers;

public class CommentsControllerTests {
    [Fact]
    public async Task GetCommentsForPhoto_ShouldReturnComments() {
        Mock<ICommentsService> commentsServiceMock = new Mock<ICommentsService>();
        List<Comment> expected = new List<Comment>() {
            new Comment(){Id = 1, Title = "Title1", Body = "Body1" },
            new Comment(){Id = 2, Title = "Title2", Body = "Body2" },
            new Comment(){Id = 3, Title = "Title3", Body = "Body3" },
        };
        commentsServiceMock.Setup(ps => ps.GetCommentsForPhotoAsync(1)).ReturnsAsync(expected);

        CommentsController sut = new CommentsController(commentsServiceMock.Object);

        var result = await sut.GetCommentsForPhoto(1);

        // Assert
        ActionResult<IEnumerable<Comment>> actionResult = 
            Assert.IsType<ActionResult<IEnumerable<Comment>>>(result);
        List<Comment> returnValue = Assert.IsType<List<Comment>>(actionResult.Value);
        Assert.True(returnValue.SequenceEqual(expected));
    }

    [Fact]
    public async Task GetCommentById_ShouldReturnNotFound_WhenCommentIdDoesNotExist() {
        Mock<ICommentsService> commentsServiceMock = new Mock<ICommentsService>();
        commentsServiceMock.Setup(ps => ps.GetCommentByIdAsync(1)).ReturnsAsync((Comment)null);

        CommentsController sut = new CommentsController(commentsServiceMock.Object);

        var result = await sut.GetCommentById(1);

        // Assert
        ActionResult<Comment> actionResult = Assert.IsType<ActionResult<Comment>>(result);
        Assert.IsType<NotFoundResult>(actionResult.Result);
    }
}
