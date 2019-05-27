using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Wolverine.Core
{
    public class FileStorageManger : AbstractStorageManager
    {
        public override string Create(string name)
        {
            var newProject = new Project() { Name = name, CreationDate = DateTimeOffset.Now };
            var json = JsonConvert.SerializeObject(newProject);
            using (StreamWriter writer = new StreamWriter(File.Create(name)))
            {
                writer.WriteLine(json);
            }
            return newProject.Id.ToString();
        }

        public override Project Load(string id)
        {
            return JsonConvert.DeserializeObject<Project>(File.OpenText(id).ReadToEnd());
        }

        public override string LoadAsString(string id)
        {
            return File.OpenText(id).ReadToEnd();
        }

        public override bool Save(Project project)
        {
            var json = JsonConvert.SerializeObject(project);
            using (StreamWriter writer = new StreamWriter(File.Create(project.Name)))
            {
                writer.WriteLine(json);
            }
            return true;
        }

        public override bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public override string Create(Project project)
        {
            throw new NotImplementedException();
        }

        public override string CreateSort(string projectId, Project clonedProject)
        {
            throw new NotImplementedException();
        }

        public override bool SaveSort(SortSession session)
        {
            throw new NotImplementedException();
        }
    }
}