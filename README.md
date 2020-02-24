# Wolverine
Card Sorting Tool

### Getting Started
To be able to build and run the code, you will need the following. 
1. Visual Studio 2019 with the workloads: ASP .NET & Web Development and Windows .NET Desktop Development. 
2. Visual Studio Code. 
3. Internet connectivity. 

Once the clone is created, open the Visual Studio Solution file from *Wolverine\Wolverine.sln* and **build** the solution. 
After the build succeeds, run the following command on powershell. 

` dotnet ef database update `

Running the about command will create the Wolverine.db (sqlite) under *Wolverine\Wolverine.Service*. 
Once the database is created, set the startup as *Wolverine.Service* and **run** it. This will start IIS Express and the service apis can be accessed via http calls. 

Open the folder *Wolverine\Wolverine.Web\Wolverine-Angular* in Visual Studio Code and install all the node packages by running the command `npm install`. Once all the dependencies are installed, you can run the angular application by executing `ng serve`. 

### ASP.NET MVC Service
A snapshot of the service is running in Azure at https://wolverineservice.azurewebsites.net/api/projects/help. 
Since this is hosted in my personal profile, use it at your own risk! 😈

Since the service code is co-located in the same repository, you may host it whereever you want. Make sure that the **Wolverine.Web\Wolverine-Angular\src\app\Services\project.service.ts** file is updated to consider the custom api path before using the application. 

### Sneakpeek at the source
The following are the projects that make up solution. 
#### Wolverine.Service
This is an asp .net core service providing some REST apis to interface with the database. 
The service uses Entity Framework Core to map objects to tables, which in-turn, are stored in an SQLite db. There are totally 4 tables that make up the database. 
- **SortSessions:** contains an instance of a project, as sorted by a participant. 
- **Projects:** contains a set of groups; the _IsSessionZero_ field denotes whether this project is a root project created by an author, or a sorted project manipulated by a participant. 
- **Groups:** contains one or more cards; the _IsUnsorted_ field denotes whether this group contains cards which are yet to be sorted. Ideally, this will be full in projects created by an author and will be empty in projects created by a participant. 
- **Cards:** denotes a funtion that needs to be organized.
##### Entity Relationship
One SortSession references One Project
One Project contains Many SortSession
One Project contains Many Groups
One Group contains Many Cards

Refer to _**ProjectTableContext**.OnModelCreating()_ to understand the model in detail. 
![entity relationship diagram](https://raw.githubusercontent.com/sudarsanyes/wolverine/master/Entities.png?token=AAAGHXQD2JMBKUI75I474XS46YOS4)
#### Wolverine.Web/Wolverine-Angular
This is an angular application that contains all the views which make up the application. 
The application uses Angular 7 with the following dependencies. 
- **cdk:** for drag and drop support. 
- **bootstrap (via cdn):** to enable bootstrap classes for theming. 
- **ng-boostrap:** to enable boostrap components inside templates. 

The application is written as a single module and contains the following components. 
- **AnalyzeComponent:** renders the analysis (or report) part. 
- **CreateComponent:** contains ui and logic involved in creating a new project. 
- **HomeComponent:** the lading page. 
- **SortComponent:** contains ui and logic used to sort cards. 

In addition to these components, we have, 
- **ProjectService:** a service interface between this angular application and the asp .net core service. 
- **ConfirmationGuard and SortConfirmationGuard:** a couple of guards to validate before navigating to and from **CreateComponent** and **SortComponent**. 
- **app-routing.module.ts:** containing the component route map for the angular application. 

### Deployment
As mentioned before, a sapshot of the API service is deployed in my [Azure profile](https://portal.azure.com/#@sudarsanyeshotmail.onmicrosoft.com/resource/subscriptions/159d5d7e-43aa-4f17-8c6d-0b7d6c22e9c5/resourceGroups/Default-ApplicationInsights-EastUS/providers/Microsoft.Web/sites/WolverineService/appServices"). 
And the web app is deployed in [github](https://sudarsanyes.github.io/wolverine/ "github") under a branch called [gh-pages](https://github.com/sudarsanyes/wolverine/tree/gh-pages "gh-pages"). 
Whenever a change is made to the web application, the changes can be deployed by running the following commands. 
- Make sure that you have installed gh-pages by running 
```bash
npm install -g angular-cli-ghpages 
``` 
- Build the web app and make sure that the base href is updated by running
```bash
ng build --prod --base-href https://sudarsanyes.github.io/wolverine/
```
- Copy the content of *Wolverine\Wolverine.Web\Wolverine-Angular\dist\Wolverine-Angular* to **Wolverine\Wolverine.Web\Wolverine-Angular\dist\**
- Deploy the distrbutables to github by running 
```bash
ngh
```
