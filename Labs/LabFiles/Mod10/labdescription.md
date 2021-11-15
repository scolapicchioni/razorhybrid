# WebApi

## Goals

- Use an actual table of our database to store and retrieve the comments. 
- Expose the data through a Web Service by implementing a Web Api controller. 
- Call the API from the Blazor Component. 

## Steps

- Change the DB
    - Add the Comment Entity  
    - Add a property to the Photo Entity to establish the one to many relationship between the two entities.
    - Add the DbSSet of Comment on the DbContext
    - Configure the Comment Entity of the DbContext to ensure that the Title has a maximum length of 100 characters and the Body has a maximum length of 250 characters.
    - Add the migration to the database
    - Update the DB
- Create Service and Repository for the Serve Side
    - Add the interfaces and classes for the CommentsService and CommentsRepository. Let the CommentsRepository work with the DbContext.
    - Register them in the ServiceCollectionExtensions.cs file.
- Expose the DB through a WebApi
    - Add a CommentsController to the Web project.
    - Create actions to retrieve and add comments.
    - Map the Controller routes in the program.cs file.
    - Optional: add some comments on the DB and test if your controller works by navigating to the route to get the comments for one photo.
- Connect the BlazorComponent with the API
    - In the `Infrastructure/Repositories/Client` folder, create a `CommentsRepositoryHttp.cs` file with a `CommentsRepositoryHttp class that implements the `PhotoSharingApplication.Infrastructure.Repositories.Client.ICommentsRepository` interface.  
    Accept an `HttpClient` parameter in the constructor and use it in each method. 
    - Register the `CommentsRepositoryHttp` class as a Scoped service in the `Program.cs` file of the Blazor client application.
    - Remove the previously registered `CommentsRepositoryList` class from the collection of services.


## Resources

- https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-6.0&tabs=visual-studio
- https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-6.0
