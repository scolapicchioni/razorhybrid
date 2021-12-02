using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotoSharingApplication.Core.Interfaces;
using PhotoSharingApplication.Shared.Entities;
using PhotoSharingApplication.Web.Sessions;

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

    public async Task<IActionResult> OnPostAddToFavorites(int id) {
        string key = "favoritePhotos";
        HashSet<int> favorites = HttpContext.Session.Get<HashSet<int>>(key) ?? new();
        favorites.Add(id);
        HttpContext.Session.Set(key, favorites);
        Photo = await photosService.GetPhotoByIdAsync(id);
        return Page();
    }
}
