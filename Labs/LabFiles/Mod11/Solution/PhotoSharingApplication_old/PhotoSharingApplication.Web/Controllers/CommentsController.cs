using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoSharingApplication.Core.Entities;
using PhotoSharingApplication.Core.Interfaces;

namespace PhotoSharingApplication.Web.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase {
        private readonly ICommentsService service;

        public CommentsController(ICommentsService service) {
            this.service = service;
        }
        [HttpGet("/api/Photos/{photoId}/Comments")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsForPhoto(int photoId) { 
            return (await service.GetCommentsForPhotoAsync(photoId)).ToList();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Comment>> GetCommentById(int id) {
            Comment? comment = await service.GetCommentByIdAsync(id);
            if (comment is null) return NotFound();
            return comment;
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> AddComment(Comment comment) {
            await service.AddCommentAsync(comment);
            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment);
        }
    }
}
