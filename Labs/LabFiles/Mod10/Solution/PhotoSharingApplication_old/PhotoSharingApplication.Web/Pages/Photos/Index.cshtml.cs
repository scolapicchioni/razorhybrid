using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotoSharingApplication.Core.Entities;
using PhotoSharingApplication.Core.Interfaces;

namespace PhotoSharingApplication.Web.Pages.Photos;

public class IndexModel : PageModel {
    private readonly IPhotosService photosService;

    public IEnumerable<Photo> Photos { get; set; }
    public IndexModel(IPhotosService photosService) {
        this.photosService = photosService;
    }
    public async Task OnGetAsync() {
        Photos = await photosService.GetAllPhotosAsync();
    }
}
