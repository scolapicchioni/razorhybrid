﻿using PhotoSharingApplication.Core.Entities;
using PhotoSharingApplication.Core.Interfaces;

namespace PhotoSharingApplication.Core.Services;

public class PhotosService : IPhotosService {
    private readonly IPhotosRepository photosRepository;

    public PhotosService(IPhotosRepository photosRepository) {
        this.photosRepository = photosRepository;
    }
    public Task AddPhotoAsync(Photo photo) {
        return photosRepository.AddPhotoAsync(photo);
    }

    public Task<IEnumerable<Photo>> GetAllPhotosAsync() {
        return photosRepository.GetAllPhotosAsync();
    }

    public Task<Photo?> GetPhotoByIdAsync(int id) {
        return photosRepository.GetPhotoByIdAsync(id);
    }
}
