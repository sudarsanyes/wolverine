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
    public class SortController : Controller
    {
        private IHostEnvironment env;
        private ProjectManager projectManager;

        public SortController(IHostEnvironment env)
        {
            this.env = env;
            projectManager = new ProjectManager(new EFStorageManager());
        }

        public string Index()
        {
            return "SortController/Index";
        }

        public string Help()
        {
            return string.Format($"The following are the apis available under this route: \r\nIndex \r\nList \r\nCreate(ProjectId) \r\nUpload(SortSession) \r\nHelp");
        }

        public IList<SortSession> List()
        {
            using (var context = new ProjectContext())
            {
                return context.SortSessions.Include("Project.Groups.Cards").ToList();
            }
        }

        [HttpGet("{projectId}")]
        public string Create(string projectId)
        {
            return projectManager.CreateSort(projectId);
        }

        [HttpPost()]
        public bool Upload([FromBody] SortSession sortSession)
        {
            return projectManager.SaveSort(sortSession);
        }

        [HttpGet("{id}")]
        public SortSession Download(string id)
        {
            return projectManager.LoadSort(id);
        }
    }
}