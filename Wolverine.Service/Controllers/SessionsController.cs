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
    public class SessionsController : Controller
    {
        private IHostEnvironment env;
        private ProjectManager projectManager;

        public SessionsController(IHostEnvironment env)
        {
            this.env = env;
            projectManager = new ProjectManager(new EFStorageManager());
        }

        public string Index()
        {
            return "SessionsController/Index";
        }

        public string Help()
        {
            return string.Format($"The following are the apis available under this route: \r\nIndex \r\nList \r\nAnalyze(ID) \r\nHelp");
        }

        public IList<SortSession> List()
        {
            using (var context = new ProjectContext())
            {
                return context.SortSessions.ToList();
            }
        }

        [HttpGet("{id}")]
        public SortResult Analyze(string id)
        {
            using (var context = new ProjectContext())
            {
                var project = projectManager.Load(id);
                if (project != null)
                {
                    var result = new SortResult(project);
                    var associatedSessions = context.SortSessions.Where(x => x.Reference == id).Include("Project.Groups.Cards");
                    result.UpdateMap(associatedSessions.ToArray());
                    foreach (var cardMap in result.CardGroupMap)
                    {
                        var resolvedGroups = new List<GroupMap>();
                        foreach (var group in cardMap.Value)
                        {
                            var resolvedGroup = context.Groups.First(x => x.Id == group.Item1);
                            var resolvedSession = context.SortSessions.First(x => x.Id == group.Item2);
                            resolvedGroups.Add(new GroupMap(resolvedGroup, new SortSessionInfo(resolvedSession.Id, resolvedSession.Participant)));
                        }
                        result.CardGroupResolvedMap.Add(new KeyValuePair<Card, GroupMap[]>(cardMap.Key, resolvedGroups.ToArray()));
                    }
                    return result;
                }
                return null;
            }
        }
    }
}