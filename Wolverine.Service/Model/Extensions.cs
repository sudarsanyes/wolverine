using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wolverine.Core;

namespace Wolverine.Service.Model
{
    public static class Extensions
    {
        public static string ProjectAsJson(this Project project)
        {
            return JsonConvert.SerializeObject(project);
        }
    }
}