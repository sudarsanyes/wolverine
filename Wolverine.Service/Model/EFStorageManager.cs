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
                newProject.IsSessionZero = true;
                dbContext.Projects.Add(newProject);
                dbContext.SaveChanges();
                return newProject.Id;
            }
        }

        public override string Create(Project project)
        {
            using (var dbContext = new ProjectContext())
            {
                if (string.IsNullOrWhiteSpace(project.Id))
                {
                    project.Id = Guid.NewGuid().ToString();
                }
                project.IsSessionZero = true;
                dbContext.Projects.Add(project);
                dbContext.SaveChanges();
                return project.Id.ToString();
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

        public override string CreateSort(string projectId, Project clonedProject)
        {
            using (var dbContext = new ProjectContext())
            {
                var sortSession = new SortSession()
                {
                    Project = clonedProject,
                    ProjectId = clonedProject.Id,
                    Reference = projectId
                };
                dbContext.SortSessions.Add(sortSession);
                dbContext.SaveChanges();
                return sortSession.Id;
            }
        }

        public override bool SaveSort(SortSession session)
        {
            using (var dbContext = new ProjectContext())
            {
                // For some reason, the default group still seems to contain all the cards. 
                // Make sure to remove all the cards from the default or unsorted group. 
                session.Project.Groups.First(x => x.IsUnsorted == true).Cards.Clear();

                // Save the cloned project. 
                var existingProject = dbContext.Projects.FirstOrDefault(x => x.Id == session.Project.Id);
                if (existingProject == null)
                {
                    dbContext.Add(session.Project);
                    dbContext.SaveChanges();
                }
                else
                {
                    dbContext.Remove(existingProject);
                    dbContext.SaveChanges();
                    dbContext.Add(session.Project);
                    dbContext.SaveChanges();
                }

                // Save the sort session. 
                var existingSession = dbContext.SortSessions.FirstOrDefault(x => x.Id == session.Id);
                if (existingSession == null)
                {
                    dbContext.Add(session);
                    dbContext.SaveChanges();
                }
                else
                {
                    dbContext.Remove(existingSession);
                    dbContext.SaveChanges();
                    dbContext.Add(session);
                    dbContext.SaveChanges();
                }
            }
            return true;
        }

        public override SortSession LoadSort(string id)
        {
            using (var dbContext = new ProjectContext())
            {
                var existingSession = dbContext.SortSessions.FirstOrDefault(x => x.Id == id);
                existingSession.Project = dbContext.Projects.Include("Groups.Cards").FirstOrDefault(x => x.Id == existingSession.ProjectId);
                existingSession.Project.Groups = existingSession.Project.Groups.OrderBy(x => !x.IsUnsorted).ToList<Group>();
                return existingSession;
            }
        }
    }
}