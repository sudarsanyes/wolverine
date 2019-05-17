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
                return newProject.ID;
            }
        }

        public override Project Load(string id)
        {
            using (var dbContext = new ProjectContext())
            {
                var project = dbContext.Projects.Include("DefaultGroups.Cards").Include("UnsortedGroup.Cards").FirstOrDefault(x => x.ID == id);
                return project;
            }
        }

        public override string LoadAsString(string id)
        {
            using (var dbContext = new ProjectContext())
            {
                var project = dbContext.Projects.Include("DefaultGroups.Cards").Include("UnsortedGroup.Cards").FirstOrDefault(x => x.ID == id);
                return project.ProjectAsJson();
            }
        }

        public override bool Save(Project project)
        {
            var projectData = JsonConvert.SerializeObject(project);
            using (var dbContext = new ProjectContext())
            {
                var existingProject = dbContext.Projects.FirstOrDefault(x => x.Name == project.Name);
                if (existingProject == null)
                {
                    dbContext.Add(project);
                }
                else
                {
                    dbContext.Remove(existingProject);
                    dbContext.Add(project);
                }
                dbContext.SaveChanges();
            }
            return true;
        }
    }
}