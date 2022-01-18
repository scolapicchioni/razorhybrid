using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoSharingApplication.Shared.Entities;
using PhotoSharingApplication.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace PhotoSharingApplication.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase {
    private readonly ICommentsService commentsService;
    private readonly IAuthorizationService authorizationService;
    public CommentsController(ICommentsService commentsService, IAuthorizationService authorizationService) => (this.commentsService, this.authorizationService) =(commentsService, authorizationService);
    
    [HttpGet("/api/Photos/{photoId}/Comments")]
    public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsForPhoto(int photoId) => (await commentsService.GetCommentsForPhotoAsync(photoId)).ToList();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Comment>> GetCommentById(int id) {
        Comment? comment = await commentsService.GetCommentByIdAsync(id);
        if (comment is null) return NotFound();
        return comment;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Comment>> AddComment(Comment comment) {
        comment.SubmittedBy = User?.Identity?.Name;
        await commentsService.AddCommentAsync(comment);
        return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Comment?>> DeleteComment(int id) {
        Comment? comment = await commentsService.GetCommentByIdAsync(id);
        var result = await authorizationService.AuthorizeAsync(User, comment, "CommentDeletionPolicy");
        if (result.Succeeded) {
            return await commentsService.DeleteCommentAsync(id);
        } else {
            return Forbid();
        }
    }
}
