using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotoSharingApplication.Core.Interfaces;
using PhotoSharingApplication.Shared.Entities;
using PhotoSharingApplication.Web.Sessions;

namespace PhotoSharingApplication.Web.Pages.Photos;

public class FavoritesModel : PageModel {
    private readonly IPhotosService photosService;

    public IEnumerable<Photo> Photos { get; set; }
    public FavoritesModel(IPhotosService photosService) {
        this.photosService = photosService;
    }
    public async Task OnGetAsync() {
        HashSet<int> favorites = HttpContext.Session.Get<HashSet<int>>("favoritePhotos") ?? new();
        Photos = await photosService.GetSetOfPhotosAsync(favorites);
    }
}
