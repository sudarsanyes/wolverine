using System;
using System.Collections.Generic;
using System.Text;

namespace Wolverine.Core
{
    public abstract class AbstractStorageManager
    {
        public abstract string Create(string name);

        public abstract Project Load(string id);

        public abstract string LoadAsString(string id);

        public abstract bool Save(Project project);

        public abstract bool Delete(string id);

        public abstract string Create(Project project);
    }
}