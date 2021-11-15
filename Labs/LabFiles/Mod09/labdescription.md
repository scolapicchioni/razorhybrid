# Blazor Integration

## Goal

Build the client side of the Comments section integrating Blazor Components into the Photo/Details Razor Page.
- One Blazor component
    - CommentsForPhotoComponent  
Note: we could create three different components and let them communicate with each other, but it's out of scope for this course.  
For now we use a fake service to read / write the comments. We'll build a real service later.

At first, we just try to add the Counter to the Details Razor Page, to see if the integration works. Then, we build the CommentsForPhotoComponent.  
We concentrate on reading the existing comments first, writing a new comment at a later step.

## Steps

- Add a new project of type **Blazor WebAssembly**. Name the App PhotoSharingApplication.Blazor.
- In the Additional information dialog, select the **ASP.NET Core hosted** checkbox when creating the Blazor WebAssembly app.

We have 3 projects added to the solution: 
- PhotoSharingApplication.Blazor.Client
- PhotoSharingApplication.Blazor.Server
- PhotoSharingApplication.Blazor.Shared.

We will remove the Server and the Shared in the future, but we can use them to steal some code from them and configure our Web app.


### Blazor.Client

- Delete the `wwwroot/index.html` and `wwwroot.favicon.ico` files.
- Delete the following lines in Program.cs:

```diff
- builder.RootComponents.Add<App>("#app");
- builder.RootComponents.Add<HeadOutlet>("head::after");
```

### PhotoSharingApplication.Web

- Add the Package `"Microsoft.AspNetCore.Components.WebAssembly.Server`
- Add a Project Reference to th `PhotoSharingApplication.Blazor.Client` project.
- In `program.cs`, between `app.UseHttpsRedirection();` and `app.UseStaticFiles();` add the following:

```cs
app.UseBlazorFrameworkFiles();
```

In the `Pages/Shared/_Layout.cshtml` file, add the following:

```html
<base href="~/" />
<link rel="stylesheet" href="~/css/app.css" />
<link rel="stylesheet" href="~/PhotoSharingApplication.Blazor.Client.styles.css" asp-append-version="true" />
```

In the `Pages/Photos/Details.cshtml` file, add the following:

```cs
@using PhotoSharingApplication.Blazor.Client.Pages
```

```html
<component type="typeof(Counter)" render-mode="WebAssemblyPrerendered"/>

@section Scripts {
	<script src="_framework/blazor.webassembly.js"></script>
}
```

Run the application, navigate to the details of a photo and verify that the counter is rendered and that it is interactive.

We successfully integrated the Blazor Client into the Photo/Details Razor Page. Now it's time to build the actual Blazor Component.  
We want the component to be agnostic of technologies, so we'll follow the same pattern of service and repository as we did for the server.

### PhotoSharingApplication.Core

- In the `Entities` folder, add a new file called `Comment.cs` with the Comment class containing properties for Id, Title and Body.
- In the `Interfaces` folder, add a `Client` folder.
- In the `Interfaces/Client` folder, add a new file called `ICommentsService.cs` with the `ICommentsService` interface. Make sure that the `AddCommentAsync` returns a `Task<Comment>`.
- In the `Interfaces/Client` folder, add a new file called `ICommentsRepository.cs` with the `ICommentsRepository` interface. Make sure that the `AddComment` returns a `Task<Comment>`.
- In the `Validators` folder, add a new file called `CommentValidator.cs` with the `CommentValidator` class. Add rules to ensure that the Title and Body are not empty and that they are not longer than 100 and 250 charachters respectively.
- In the `Service` folder, add a new `Client` folder
- In the `Services/Client` folder, add a new file called `CommentsService.cs` with the `CommentsService` class. Implement the `ICommentsService` interface by using the validator and the repository. Make sure that the `AddCommentAsync` returns the newly created comment.

### PhotoSharingApplication.Infrastructure

- In the `Repository` folder, add a new `Client` folder.
- In the `Repository/Client` folder, add a new file called `CommentsRepositoryList.cs` with the `CommentsRepositoryList` class. Implement the `ICommentsRepository` interface by using a List<Comment>`. Add a couple of comments for the first couple of photos, so that we can test the CommentsForPhotoComponent later. Make sure that the `AddCommentAsync` returns the newly created comment.

### PhotosSharingApplication.Web

- Register the Service and the Repository in the ServiceCollectionExtensions.cs file.

### PhotoSharing.Blazor.Client

With the logic and data access in place, we can now build the Blazor Component.  
The first step is to create a new folder in the `PhotoSharingApplication.Blazor.Client` project called `Components`.  
We'll add a new **Razor Component** called `CommentsForPhotoComponent` in the `Components` folder.  
We can peak at the `FetchData` page to see how the component has to be built. 
- Instead of asking for an `HttpClient`, we'll use the `ICommentsService`. 
- Instead of an array of `WeatherForecast`, we'll have a `List<Comment>`.
- In the table, we'll render the `Title` and the `Body` of each `comment` contained in our model.
- We also need a `[Parameter]` to pass the `PhotoId` to the component.
- During the `OnInitializedAsync` method, we'll call the `ICommentsService` to get the comments for the photo.

### PhotoSharingApplication.Web

In the`Pages/Photos/Details.cshtml` file, add the following:

```cs
@using PhotoSharingApplication.Blazor.Client.Components
```

```html
<component type="typeof(CommentsForPhotoComponent)" render-mode="WebAssemblyPrerendered" param-PhotoId="@Model.Photo.Id"/>

@section Scripts {
	<script src="_framework/blazor.webassembly.js"></script>
}
```

If you run the application and navigate to the details of a photo, you should be able to see the comments for that photo. 

### PhotoSharing.Blazor.Client
Now it's time to add a comment. Let's go back to our `CommentsForPhotoComponent`.  
We need a new variable of type `Comment` to hold the new comment. We will initialize it during the `OnInitializedAsync` with a new `Comment` object with a PhotoId equal to the component parameter with the same name.  
In the html section, we'll add an `EditForm`, setting the `Model` to the comment variable. We'll have an `InputText` with a `bind-Value` set to `comment.Title` and a `InputTextArea` with a `bind-Value` set to `comment.Body`.  
In order to use `FluentValidation`, let's add the `Blazored.FluentValidation` package and the `@using Blazored.FluentValidation` in the `_Imports.razor` file.    
In the `EditForm`, instead of using the `<DataAnnotationValidator />`, let's use a `<FluentValidationValidator />`, together with a `<ValidationSummary />`.  
Let's handle the `OnValidubmit` of the `EditForm` by calling a `HandleValidSubmit` method, where we await the service.AddCommentAsync(comment) and then add the result to the comments list.    
Since the service, the repository and the validators will be used client side, we need to register them on the Blazor Client project as well.
In the `program.cs` file, add the following:

```cs
builder.Services.AddSingleton<PhotoSharingApplication.Core.Interfaces.Client.ICommentsRepository, PhotoSharingApplication.Infrastructure.Repositories.Client.CommentsRepositoryList>();
builder.Services.AddScoped<PhotoSharingApplication.Core.Interfaces.Client.ICommentsService, PhotoSharingApplication.Core.Services.Client.CommentsService>();
builder.Services.AddSingleton<CommentValidator>();
```

If you run the application now, you should be able to add a new comment for an existing photo.

In the next lab, we're going to create a WebApi to read and write the comments, then we're going to connect the BlazorComponent to the WebApi by using an HttpClient object.

## Resources

- https://docs.microsoft.com/en-us/aspnet/core/blazor/components/prerendering-and-integration?view=aspnetcore-6.0&pivots=webassembly
- https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/built-in/component-tag-helper?view=aspnetcore-6.0
- https://docs.microsoft.com/en-us/aspnet/core/blazor/forms-validation?view=aspnetcore-6.0#example-form
- https://github.com/Blazored/FluentValidation
