using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Wolverine.Core
{
    public class SortSession
    {
        public SortSession()
        {
            // Reserved. 
        }

        public string ID { get; set; }
        public string Comments { get; set; }
        public string Participant { get; set; }
        public Project Project { get; set; }
        public DateTimeOffset SessionInstance { get; set; }
    }
}