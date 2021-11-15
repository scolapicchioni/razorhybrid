using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotoSharingApplication.Core.Entities;
using PhotoSharingApplication.Core.Interfaces;

namespace PhotoSharingApplication.Web.Pages.Photos;

public class DetailsModel : PageModel {
    private readonly IPhotosService photosService;

    public DetailsModel(IPhotosService photosService) {
        this.photosService = photosService;
    }
    public Photo? Photo { get; set; }
    public async Task<IActionResult> OnGet(int id) {
        Photo = await photosService.GetPhotoByIdAsync(id);
        if (Photo is null) {
            return NotFound();
        }
        return Page();
    }
}
