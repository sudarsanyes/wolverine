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

        public Project Project { get; private set; }

        public Project Load(string id)
        {
            Project = storage.Load(id);
            return Project;
        }

        public string LoadAsString(string id)
        {
            Project = storage.Load(id);
            return storage.LoadAsString(id);
        }

        public bool Save(Project project)
        {
            return storage.Save(project);
        }

        public string Create(string name)
        {
            return storage.Create(name);
        }
    }
}