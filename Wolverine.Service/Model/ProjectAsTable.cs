using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Wolverine.Core;

namespace Wolverine.Service.Model
{
    public class ProjectAsTable1
    {
        public ProjectAsTable1()
        {
            ID = Guid.NewGuid().ToString();
        }

        public string ID { get; set; }

        public string ProjectData { get; set; }

        [NotMapped]
        public Project Project => JsonConvert.DeserializeObject<Project>(ProjectData);
    }
}