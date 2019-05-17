# wolverine
Card Sorting Tool

### Getting Started
To be able to build and run the code, you will need the following. 
1. Visual Studio 2019 with the following workloads. 
- ASP.NET & Web Development.
- Windows .NET Desktop Development. 
2. Visual Studio Code. 
3. Internet connectivity. 

Once the clone is created, open the Visual Studio Solution file from *Wolverine\Wolverine.sln* and **build** the solution. 
After the build succeeds, run the following command on powershell.

` dotnet ef database update `

Running the about command will create the Wolverine.db (sqlite) under *Wolverine\Wolverine.Service*. 
Once the database is created, set the startup as *Wolverine.Service* and **run** it. This will start IIS Express and the service apis can be accessed via http calls. 

Open the folder *Wolverine\Wolverine.Web\Wolverine-Angular* in Visual Studio Code and install all the node packages by running the command `npm install`. Once all the dependencies are installed, you can run the angular application by executing `ng serve`. 
