using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wolverine.Core;
using Microsoft.EntityFrameworkCore;

namespace Wolverine.Service.Model
{
    public class EFStorageManager : AbstractStorageManager
    {
        public override string Create(string name)
        {
            using (var dbContext = new ProjectContext())
            {
                var newProject = Project.Default;
                newProject.Name = name;
                dbContext.Projects.Add(newProject);
                dbContext.SaveChanges();
                return newProject.Id.ToString();
            }
        }

        public override Project Load(string id)
        {
            using (var dbContext = new ProjectContext())
            {
                var project = dbContext.Projects.Include("Groups.Cards").FirstOrDefault(x => x.Id == id);
                return project;
            }
        }

        public override string LoadAsString(string id)
        {
            using (var dbContext = new ProjectContext())
            {
                var project = dbContext.Projects.Include("Groups.Cards").FirstOrDefault(x => x.Id == id);
                return project.AsJson();
            }
        }

        public override bool Save(Project project)
        {
            var projectData = JsonConvert.SerializeObject(project);
            using (var dbContext = new ProjectContext())
            {
                var existingProject = dbContext.Projects.Include("Groups.Cards").FirstOrDefault(x => x.Name == project.Name);
                if (existingProject == null)
                {
                    dbContext.Add(project);
                    dbContext.SaveChanges();
                }
                else
                {
                    dbContext.Remove(existingProject);
                    dbContext.SaveChanges();
                    dbContext.Add(project);
                    dbContext.SaveChanges();
                }
            }
            return true;
        }

        public override bool Delete(string id)
        {
            using (var dbContext = new ProjectContext())
            {
                var existingProject = dbContext.Projects.FirstOrDefault(x => x.Id == id);
                if (existingProject != null)
                {
                    dbContext.Projects.Remove(existingProject);
                }
                dbContext.SaveChanges();
            }
            return true;
        }
    }
}