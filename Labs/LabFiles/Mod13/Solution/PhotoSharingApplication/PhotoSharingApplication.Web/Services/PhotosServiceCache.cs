using Microsoft.Extensions.Caching.Memory;
using PhotoSharingApplication.Core.Interfaces;
using PhotoSharingApplication.Core.Services;
using PhotoSharingApplication.Shared.Entities;
using PhotoSharingApplication.Shared.Validators;

namespace PhotoSharingApplication.Web.Services {
    public class PhotosServiceCache : PhotosService, IPhotosServiceCache {
        private readonly IMemoryCache cache;

        public PhotosServiceCache(IPhotosRepository repository, PhotoValidator validator, IMemoryCache cache) : base(repository, validator) => this.cache = cache;
        public async Task<Image?> GetImageByIdAsync(int id) {
            string key = $"image-{id}";
            Image? image;
            if (!cache.TryGetValue(key, out image)) {
                image = await base.GetImageByIdAsync(id);
                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10));

                // Save data in cache.
                cache.Set(key, image, cacheEntryOptions);
            }
            return image;
        }
    }
}
