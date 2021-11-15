using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotoSharingApplication.Core.Entities;
using PhotoSharingApplication.Core.Interfaces;

namespace PhotoSharingApplication.Web.Pages.Photos
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly IPhotosService photosService;
        private readonly IAuthorizationService authorizationService;

        public Photo Photo { get; set; }
        public DeleteModel(IPhotosService service, IAuthorizationService authorizationService) {
            this.photosService = service;
            this.authorizationService = authorizationService;
        }
        public async Task<IActionResult> OnGet(int id)
        {
            Photo = await photosService.GetPhotoByIdAsync(id);
            if (Photo is null) {
                return NotFound();
            }
            var authorizationResult = await authorizationService.AuthorizeAsync(User, Photo, "PhotoDeletionPolicy");

            if (authorizationResult.Succeeded) {
                return Page();
            } else { 
                return new ForbidResult();
            }
        }
        public async Task<IActionResult> OnPost(int id) {
            Photo = await photosService.GetPhotoByIdAsync(id);
            
            var authorizationResult = await authorizationService.AuthorizeAsync(User, Photo, "PhotoDeletionPolicy");

            if (authorizationResult.Succeeded) {
                await photosService.DeletePhotoAsync(id);
                return RedirectToPage("./Index");
            } else {
                return new ForbidResult();
            }
        }
    }
}
