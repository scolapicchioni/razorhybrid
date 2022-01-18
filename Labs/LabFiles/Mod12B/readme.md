# Authentication and Authorization

# Goals

- Any user may see the comments
- Only an authenticated user may add a new comment
- Only the owner of a comment may delete it

## Authorization

### Allow only authenticated users to add comments.  

First, we need to add the concept of ownership to our `Comment` model, by adding a `SubmittedBy` property of type string to the Comment model.   

```cs
namespace PhotoSharingApplication.Shared.Entities;

public class Comment {
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string SubmittedBy { get; set; } = string.Empty;
    public DateTime SubmittedOn { get; set; }
    public int PhotoId { get; set; }
}
```
Then, we need to save the User Name into our new `SubmittedBy` property during upload.  

Authorization in ASP.NET Core is controlled with `AuthorizeAttribute` and its various parameters. In its most basic form, applying the `[Authorize]` attribute to a controller, action, or Razor Page, limits access to that component authenticated users.  
Add the `[Authorize]` attribute to the `AddComment` action of the `CommentsController`, located under the `Controllers` folder of the `PhotoSharingApplication.Web` project.  
Add the following code
```cs
comment.SubmittedBy = User?.Identity?.Name;
```
Modify the `CommentsForPhotoComponent` Blazor Component to include the name of the user who submitted the comment.  
At this point you should be able to register a user, log on, add a comment to a photo, and see the name of the user who submitted the comment.  
When trying to add a comment without logging on first, you should (see the login page).

## Delete

We haven't implemented the deletion of a comment yet, so let's start by implementing this feature without any security involved, just to see it working first.  

- Add a `DeleteCommentAsync` method to the `PhotoSharingApplication.Core.Interfaces.ICommentsService` interface.

```cs
Task<Comment?> DeleteCommentAsync(int id);
```

- Add a `DeleteCommentAsync` method to the `PhotoSharingApplication.Core.Interfaces.ICommentsRepository` interface.

```cs
Task<Comment?> DeleteCommentAsync(int id);
```

- Implement the `DeleteCommentAsync` method in the `PhotoSharingApplication.Core.Services.CommentsService` class.

```cs
public Task<Comment?> DeleteCommentAsync(int id) => repository.DeleteCommentAsync(id);
```

- Implement the `DeleteCommentAsync` method in the `PhotoSharingApplication.Infrastructure.Repositories.CommentsRepositoryEF` class.

```cs
public async Task<Comment?> DeleteCommentAsync(int id) {
    Comment? comment = await GetCommentByIdAsync(id);
    if (comment is not null) {
        dbContext.Comments.Remove(comment);
        await dbContext.SaveChangesAsync();
    }
    return comment;
}
```

- Implement the `DeleteComment` method on the `CommentsController`
```cs
[HttpDelete("{id:int}")]
public async Task<ActionResult<Comment?>> DeleteComment(int id) {
    return await commentsService.DeleteCommentAsync(id);
}
```

- Add a `DeleteCommentAsync` method to the `PhotoSharingApplication.Blazor.Core.Interfaces.ICommentsService` interface.

```cs
Task<Comment?> DeleteCommentAsync(int id);
```

- Add a `DeleteCommentAsync` method to the `PhotoSharingApplication.Blazor.Core.Interfaces.ICommentsRepository` interface.

```cs
Task<Comment?> DeleteCommentAsync(int id);
```

- Implement the `DeleteCommentAsync` method in the `PhotoSharingApplication.Blazor.Core.Services.CommentsService` class.

```cs
public Task<Comment?> DeleteCommentAsync(int id) => repository.DeleteCommentAsync(id);
```

- Implement the `DeleteCommentAsync` method in the `PhotoSharingApplication.Blazor.Infrastructure.Repositories.CommentsRepositoryHttp` class.

```cs
public async Task<Comment?> DeleteCommentAsync(int id) {
    using var httpResponseMessage = await httpClient.DeleteAsync($"/api/Comments/{id}");
    return await httpResponseMessage.Content.ReadFromJsonAsync<Comment>();
}
```

- Implement the `DeleteCommentAsync` method in the `PhotoSharingApplication.Core.Services.Client.CommentsService` class

```cs
public Task<Comment?> DeleteCommentAsync(int id) => repository.DeleteCommentAsync(id);
```

- Implement the `DeleteCommentAsync` in the `PhotoSharingApplication.Web.Controllers.CommentsRepositoryApi` class.

```cs
    public async Task<Comment?> DeleteCommentAsync(int id) => (await controller.DeleteComment(id)).Value;
```

- Add a button to the `CommentsForPhotoComponent` Blazor Component to delete a comment.

```html
@foreach (var comment in comments)
{
    <tr>
        <td>@comment.Title</td>
        <td>@comment.Body</td>
        <td>@comment.SubmittedBy</td>
        <td>@comment.SubmittedOn.ToShortDateString() @comment.SubmittedOn.ToShortTimeString()</td>
        <td><button @onclick="@(()=>DeleteComment(comment.Id))">Delete</button></td>
    </tr>
}
```

Add a method to delete the comment and update the UI.
```cs
private async Task DeleteComment(int id) {
    Comment removedComment = await service.DeleteCommentAsync(id);
    comments.Remove(comments.First(c=>c.Id == removedComment.Id));
}
```

By this point, you should be able to delete a comment, even if you're not logged on or if you're not the owner of the comment.


### Allow only the owner of a comment to delete it.

Authorization strategy depends upon the resource being accessed. Consequently, the comment must be retrieved from the data store before authorization evaluation can occur.

Attribute evaluation occurs before data binding and before execution of the page handler or action that loads the document. For these reasons, declarative authorization with an `[Authorize]` attribute doesn't suffice. Instead, you can invoke a custom authorization method — a style known as *imperative authorization*.

Authorization is implemented as an `IAuthorizationService` service and is registered in the service collection. The service is made available via dependency injection to page handlers or actions.
In the constructor of the `PhotoSharingApplication.Web.Controllers.CommentsController` controller, add an `IAuthorizationService authorizationService` parameter and save it into a private readonly field.

`IAuthorizationService` has two `AuthorizeAsync` method overloads: one accepting the resource and the policy name and the other accepting the resource and a list of requirements to evaluate.  

In the `DeleteComment` method, retrieve the `Comment`, then call the `AuthorizeAsync` method with the `Comment` and the `CommentDeletion` policy.  Return a `ForbidResult` if the authorization fails. Continue with the normal operations if the authorization succeeds.  

Writing a handler for resource-based authorization isn't much different than writing a plain requirements handler. Create a custom requirement class, and implement a requirement handler class. 

The handler class specifies both the requirement and resource type. 

```cs
using Microsoft.AspNetCore.Authorization;
using PhotoSharingApplication.Shared.Entities;

namespace PhotoSharingApplication.Web.AuthorizationHandlers;

public class CommentOwnerAuthorizationHandler : AuthorizationHandler<CommentOwnerRequirement, Comment> {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   CommentOwnerRequirement requirement,
                                                   Comment comment) {
        if (context.User.Identity?.Name == comment.SubmittedBy) {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

public class CommentOwnerRequirement : IAuthorizationRequirement { }
```

Register the requirement and handler in `Program.cs`:

```cs
builder.Services.AddAuthorization(options => {
    options.AddPolicy("CommentDeletionPolicy", policy => {
        policy.RequireAuthenticatedUser();
        policy.Requirements.Add(new CommentOwnerRequirement());
    });
});

builder.Services.AddSingleton<IAuthorizationHandler, CommentOwnerAuthorizationHandler>();
```

At this point, you should be able to delete a comment only when logged on with the user that submitted the comment in the first place. When trying to delete a comment without logging on first, you should see the login page. When trying to delete a comment that you don't own, you should see an `Access Denied`.

### Modify the UI based on the current user identity

To inject the authorization service into a Razor view, use the @inject directive:

```cs
@using Microsoft.AspNetCore.Authorization
@inject IAuthorizationService AuthorizationService
```

Use the injected authorization service to invoke AuthorizeAsync in exactly the same way you would check during resource-based authorization:

```cs
bool userIsAuthorized = (await AuthorizationService.AuthorizeAsync(User, Model, "PhotoDeletionPolicy")).Succeeded;
```

## Resources

- https://stackoverflow.com/questions/63831943/httpclient-doesnt-include-cookies-with-requests-in-blazor-webassembly-app
- https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/signalr?view=aspnetcore-6.0#signalr-cross-origin-negotiation-for-authentication-blazor-webassembly
- https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests#how-to-use-typed-clients-with-ihttpclientfactory
