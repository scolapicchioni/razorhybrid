using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotoSharingApplication.Core.Entities;
using PhotoSharingApplication.Core.Interfaces;

namespace PhotoSharingApplication.Web.Pages.Photos;

public class UploadModel : PageModel {
    private readonly IPhotosService photosService;

    [BindProperty]
    public Photo Photo { get; set; }

    public UploadModel(IPhotosService photosService) {
        this.photosService = photosService;
    }
    public void OnGet() {
    }
    public async Task<IActionResult> OnPostAsync() {
        await photosService.AddPhotoAsync(Photo);
        return RedirectToPage("./Index");
    }
}
