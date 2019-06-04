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

### Sneakpeek at the source
The following are the projects that make up solution. 
#### Wolverine.Service
This is an asp .net core service providing some REST apis to interface with the database. 
The service uses Entity Framework Core to map objects to tables, which in-turn, are stored in an SQLite db. There are totally 4 tables that make up the database. 
- SortSessions: contains an instance of a project, as sorted by a participant. 
- Projects: contains a set of groups; the IsSessionZero field denotes whether this project is a root project created by an author, or a sorted project manipulated by a participant. 
- Groups: contains one or more cards; the IsUnsorted field denotes whether this group contains cards which are yet to be sorted. Ideally, this will be full in projects created by an author and will be empty in projects created by a participant. 
- Cards: denotes a funtion that needs to be organized. 
##### Entity Relationship
One SortSession references One Project
One Project contains Many SortSession
One Project contains Many Groups
One Group contains Many Cards

Refer to *ProjecttableContext.OnModelCreating()* to understand the model in detail. 

#### Wolverine.Web/Wolverine-Angular
This is an angular application that contains all the views which make up the application. 
The application uses Angular 7 with the following dependencies. 
- cdk: for drag and drop support. 
- bootstrap (via cdn): to enable bootstrap classes for theming. 
- ng-boostrap: to enable boostrap components inside templates. 

The application is written as a single module and contains the following components. 
- AnalyzeComponent: renders the analysis (or report) part. 
- CreateComponent: contains ui and logic involved in creating a new project. 
- HomeComponent: the lading page. 
- SortComponent: contains ui and logic used to sort cards. 
