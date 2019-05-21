using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Wolverine.Core
{
    public class ProjectManager
    {
        private AbstractStorageManager storage;

        public ProjectManager(AbstractStorageManager storageManager)
        {
            storage = storageManager;
        }

        public SimplifiedProject Project { get; private set; }

        public SimplifiedProject Load(string id)
        {
            var project = storage.Load(id);
            return new SimplifiedProject(project);
        }

        public Project LoadProject(string id)
        {
            return storage.Load(id);
        }

        public string LoadAsString(string id)
        {
            var project = storage.Load(id);
            return new SimplifiedProject(project).AsJson();
        }

        public bool Save(SimplifiedProject project)
        {
            return storage.Save(project.ToProject());
        }

        public string Create(string name)
        {
            return storage.Create(name);
        }

        public string Create(SimplifiedProject project)
        {
            if (project.UnsortedGroup == null)
            {
                project.UnsortedGroup = Group.DefaultUnsorted;
            }
            if (project.DefaultGroups == null)
            {
                project.DefaultGroups = new List<Group>() { Group.Default };
            }
            if (project.CreationDate == default(DateTimeOffset))
            {
                project.CreationDate = DateTimeOffset.Now;
            }
            return storage.Create(project.ToProject());
        }

        public bool Delete(string id)
        {
            return storage.Delete(id);
        }
    }
}