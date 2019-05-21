using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Wolverine.Core;
using Wolverine.Service.Model;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Wolverine.Service.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectsController : Controller
    {
        private IHostEnvironment env;
        private ProjectManager projectManager;

        public ProjectsController(IHostEnvironment env)
        {
            this.env = env;
            projectManager = new ProjectManager(new EFStorageManager());
        }

        public string Index()
        {
            return "ProjectsController/Index";
        }

        public string Help()
        {
            return string.Format($"The following are the apis available under this route: \r\nIndex \r\nList \r\nCreate(Name) \r\nCreate(Project) \r\nDownload(ID) \r\nDownloadAsString(ID) \r\nUpload(ID) \r\nHelp");
        }

        public IList<Project> List()
        {
            using (var context = new ProjectContext())
            {
                return context.Projects.Include("Groups.Cards").ToList();
            }
        }

        [HttpPost("{name}")]
        public string Create(string name)
        {
            return projectManager.Create(name);
        }

        [HttpPost()]
        public string Create([FromBody]SimplifiedProject project)
        {
            return projectManager.Create(project);
        }

        [HttpGet("{id}")]
        public string DownloadAsString(string id)
        {
            projectManager.Load(id);
            return null;
        }

        [HttpGet("{id}")]
        public SimplifiedProject Download(string id)
        {
            return projectManager.Load(id);
        }

        [HttpPost()]
        public bool Upload([FromBody] SimplifiedProject project)
        {
            return projectManager.Save(project);
        }

        [HttpGet("{id}")]
        public bool Delete(string id)
        {
            return projectManager.Delete(id);
        }
    }
}