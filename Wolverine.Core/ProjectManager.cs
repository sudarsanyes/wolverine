using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Wolverine.Core
{
    public class ProjectManager
    {
        private AbstractStorageManager storage;

        public ProjectManager(AbstractStorageManager storageManager)
        {
            storage = storageManager;
        }

        public Project Project { get; private set; }

        public Project Load(string id)
        {
            var project = storage.Load(id);
            project.Groups = project.Groups.OrderBy(x => !x.IsUnsorted).ToList<Group>();
            return project;
        }

        public Project LoadAsProject(string id)
        {
            return storage.Load(id);
        }

        public Project LoadProject(string id)
        {
            return storage.Load(id);
        }

        public string LoadAsString(string id)
        {
            var project = storage.Load(id);
            return project.AsJson();
        }

        public bool Save(Project project)
        {
            project.Groups = project.Groups.OrderBy(x => !x.IsUnsorted).ToList<Group>();
            return storage.Save(project);
        }

        public string Create(string name)
        {
            return storage.Create(name);
        }

        public string Create(Project project)
        {
            if (project.Groups == null || project.Groups.FirstOrDefault(x => x.IsUnsorted) == null)
            {
                project.Groups.Add(Group.DefaultUnsorted);
            }
            if (project.CreationDate == default)
            {
                project.CreationDate = DateTimeOffset.Now;
            }
            return storage.Create(project);
        }

        public bool Delete(string id)
        {
            return storage.Delete(id);
        }
    }
}