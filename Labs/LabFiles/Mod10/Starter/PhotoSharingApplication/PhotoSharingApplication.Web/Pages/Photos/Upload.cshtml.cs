using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotoSharingApplication.Core.Entities;
using PhotoSharingApplication.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PhotoSharingApplication.Web.Pages.Photos;

public class UploadModel : PageModel {
    private readonly IPhotosService photosService;

    [BindProperty]
    public Photo Photo { get; set; }

    [BindProperty]
    [Display(Name ="Image")]
    public IFormFile FormFile { get; set; }

    public UploadModel(IPhotosService photosService) {
        this.photosService = photosService;
    }
    public void OnGet() {
    }
    public async Task<IActionResult> OnPostAsync() {
        //removing error for empty photo file, since we have to fill it manually
        ModelState.Remove("Photo.PhotoFile");

        if (!ModelState.IsValid) {
            return Page();
        }
        using (var memoryStream = new MemoryStream()) {
            await FormFile.CopyToAsync(memoryStream);
            Photo.PhotoFile = memoryStream.ToArray();
            Photo.ContentType = FormFile.ContentType;
        }

        try {
            await photosService.AddPhotoAsync(Photo);
        } catch (FluentValidation.ValidationException) {
            return Page();
        }
        return RedirectToPage("./Index");
    }
}
