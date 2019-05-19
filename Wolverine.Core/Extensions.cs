using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wolverine.Core;

namespace Wolverine.Core
{
    public static class Extensions
    {
        public static string AsJson(this Project project)
        {
            return JsonConvert.SerializeObject(project);
        }
        public static string AsJson(this SimplifiedProject project)
        {
            return JsonConvert.SerializeObject(project);
        }
    }
}