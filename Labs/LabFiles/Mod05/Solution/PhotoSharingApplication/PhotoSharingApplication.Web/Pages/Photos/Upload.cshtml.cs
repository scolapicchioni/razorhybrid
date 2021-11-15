using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotoSharingApplication.Core.Entities;
using PhotoSharingApplication.Core.Interfaces;

namespace PhotoSharingApplication.Web.Pages.Photos;

public class UploadModel : PageModel {
    private readonly IPhotosService photosService;

    [BindProperty]
    public Photo Photo { get; set; }

    [BindProperty]
    public IFormFile FormFile { get; set; }

    public UploadModel(IPhotosService photosService) {
        this.photosService = photosService;
    }
    public void OnGet() {
    }
    public async Task<IActionResult> OnPostAsync() {
        using (var memoryStream = new MemoryStream()) {
            await FormFile.CopyToAsync(memoryStream);
            Photo.PhotoFile = memoryStream.ToArray();
            Photo.ContentType = FormFile.ContentType;
        }

        await photosService.AddPhotoAsync(Photo);
        return RedirectToPage("./Index");
    }
}
