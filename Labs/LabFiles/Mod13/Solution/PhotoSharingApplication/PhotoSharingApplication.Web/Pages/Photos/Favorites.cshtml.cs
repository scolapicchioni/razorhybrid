using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotoSharingApplication.Core.Entities;
using PhotoSharingApplication.Core.Interfaces;
using PhotoSharingApplication.Web.Sessions;

namespace PhotoSharingApplication.Web.Pages.Photos
{
    public class FavoritesModel : PageModel
    {
        private readonly IPhotosService photosService;

        public IEnumerable<Photo> Photos { get; set; }
        public FavoritesModel(IPhotosService photosService) {
            this.photosService = photosService;
        }
        public async Task OnGetAsync() {
            SortedSet<int> favorites = HttpContext.Session.Get<SortedSet<int>>("favoritePhotos");
            if (favorites == default) {
                favorites = new();
            }
            Photos = await photosService.GetSetOfPhotosAsync(favorites);
        }
    }
}
