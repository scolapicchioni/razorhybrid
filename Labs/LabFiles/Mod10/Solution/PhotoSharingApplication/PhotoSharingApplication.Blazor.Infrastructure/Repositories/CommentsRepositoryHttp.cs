using PhotoSharingApplication.Blazor.Core.Interfaces;
using PhotoSharingApplication.Shared.Entities;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace PhotoSharingApplication.Blazor.Infrastructure.Repositories;

public class CommentsRepositoryHttp : ICommentsRepository {
    private readonly HttpClient httpClient;

    public CommentsRepositoryHttp(HttpClient httpClient) {
        this.httpClient = httpClient;
    }
    public async Task<Comment> AddCommentAsync(Comment comment) {
        var commentJson = new StringContent(JsonSerializer.Serialize(comment), Encoding.UTF8, Application.Json);

        using var httpResponseMessage = await httpClient.PostAsync("/api/Comments", commentJson);

        return await httpResponseMessage.Content.ReadFromJsonAsync<Comment>();
    }

    public async Task<Comment?> GetCommentByIdAsync(int id) => await httpClient.GetFromJsonAsync<Comment>($"/api/Comments/{id}");

    public async Task<IEnumerable<Comment>> GetCommentsForPhotoAsync(int photoId) => await httpClient.GetFromJsonAsync<IEnumerable<Comment>>($"/api/Photos/{photoId}/Comments");
}
