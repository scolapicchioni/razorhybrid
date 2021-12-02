# Lab: Exploring ASP.NET Core Razor Pages

## Scenario 
You are working as a junior developer at Adventure Works. You have been asked by a senior developer to investigate the possibility of creating a web-based photo sharing application for your organization’s customers, similar to one that the senior developer has seen on the Internet. Such an application will promote a community of cyclists who use Adventure Works equipment, and the community members will be able to share their experiences. This initiative is intended to increase the popularity of Adventure Works Cycles, and thereby to increase sales. You have been asked to begin the planning of the application by examining an existing photo sharing application and evaluating its functionality. You have also been asked to examine programming models available to ASP.NET Core developers. To do this, you need to create basic web applications written with ASP.NET Core with Razor Pages. Your manager has asked you to report on the following specific questions: 
- How does the developer set a connection string and data provider? 
- How does the developer impose a consistent layout, with Adventure Works branding and menus, on all pages in the web application? 
- How does the developer set a cascading style sheet with a consistent set of color, fonts, borders, and other styles? 
- How does the developer add a new page to the application and apply the layout and styles to it? 
## Objectives 
After completing this lab, you will be able to: 
- Describe the Razor Pages programming model available in ASP.NET Core. 
- Describe the structure of a web application developed in the Razor Pages programming model. 

# Exercise 1: Exploring a Photo Sharing Application 
## Scenario 
In this exercise, you will begin by examining the photo sharing application.  
The main tasks for this exercise are as follows:  
1. Register a user account. 
2. Upload and explore photos. 
3. Explore the Favorites feature.
4. Chat with other users about a photo
5. Test the authorization. 
## Task 1: Register a user account. 
1.	On the Start screen, open Visual Studio. 
2.	Navigate to the following location to open the `PhotoSharingApplication.sln` file: 
    - `Labfiles\Mod01\Exercise01\PhotoSharingApplication` 
3.	Run the web application in non-debugging mode. 
4.	Create a new user account with the following credentials: 
    - User name: `A user name of your choice`
    - Password: `A password of your choice`
## Task 2: Upload and explore photos. 
1. Add a new photo to the application by using the following information: 
    - Title of the photo: `Kitten`
    - Navigation path to upload the photo: `Labfiles\Mod01\Exercise01\OneKitten.jpg` 
    - Description: `One sleepy kitten` 
2. Verify the description details of the newly added photo. 
3. Add a new photo to the application by using the following information: 
    - Title of the photo: `Kittens`
    - Navigation path to upload the photo: `Labfiles\Mod01\Exercise01\Kittens.jpg` 
    - Description: `Multiple kittens in a grass field` 
4. Add a new photo to the application by using the following information: 
    - Title of the photo: `Cat`
    - Navigation path to upload the photo: `Labfiles\Mod01\Exercise01\Cat.jpg` 
    - Description: `One cat in black and white` 
5. Verify the description details of the newly added photo. 
6. Add the following comment to the `Kitten` image: 
    -	Subject: `Just a Test Comment` 
    -	Comment: `This is a Sample` 
## Task 3: Explore the Favorites feature. 
1.	Add the following images to your list of favorite photos: 
    - Kitten 
    - Cat 
2.	View the page of the images selected as favorites. 
## Task 4: Chat with other users about a photo
1. Navigate to the Details of the `Kittens` photo
2. Open multiple tabs in the browser and chat with other users about the `Kittens` photo
3. Verify that the chat is working
## Task 4: Test the authorization. 
1. Register and log on with a different user
2. Attempt to delete the `Kitten` photo added with the first user
3. Log off from the application
4. Attempt to add a new photo to the Photo Index page. 
3.	Close the browser window and the Visual Studio application. 


Results: At the end of this exercise, you will be able to understand the functionality of a photo sharing application, and implement the required application structure in the Adventure Works photo sharing application. 

# Exercise 2: Exploring a Razor Pages Application 
## Scenario 
In this exercise, you will create a simple Razor Pages application and explore its structure. 
The main tasks for this exercise are as follows: 
1.	Create a ASP.NET Core Razor Pages application. 
2.	Explore the application structure. 
3.	Add simple functionality. 
4.	Apply the site layout. 
## Task 1: Create a ASP.NET Core Razor Pages Web Application. 
1. Start Visual Studio and create a new Razor Pages project by using the ASP.NET Core Web App  template. Choose the Razor Pages template with No Authentication, No Docker Support, HTTPS and .NET 6.0 LTS. 
2. Run the new Razor Pages Application in a browser and explore the Home and Privacy pages. 
3. Stop debugging by closing the browser. 
## Task 2: Explore the application structure. 
1. Open the `appsettings.json` file and verify whether there is a database connection string. 
2. Verify that the `~/Pages/Shared/_Layout.cshtml` file contains a common layout for all pages on the website, and how pages link to the layout. 
3. Verify that the `site.css` file is used to apply styles to all pages on the website, and note how the pages link to the style sheet. 
## Task 3: Add simple functionality. 
1. Add a new Razor Page to the application by using the following information: 
    - Parent folder: `/Pages` 
    - Name of the view: `TestPage.cshtml` 
2. Add an `H1` element to the `TestPage.cshtml` view by using the following information: 
    - Content: `This is a Test Page` 
3. Configure the route of the page
    - At the top of the page replace `@page` with `@page "/testpage"`
4. Add a link to the `Index.cshtml` page by using the following information: 
    - Start tag: `<a>` 
    - Attribute: `href="~/TestPage"` 
    - Content: `Test Page`
    - End tag: `</a>` 
5.	Save all the changes. 
6.	Run the website and view the page you added. 
7.	Stop debugging by closing the browser. 

Results: At the end of this exercise, you will be able to build a simple ASP.NET Web Application with Razor Pages in Visual Studio.  
