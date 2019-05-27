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
            Id = Guid.NewGuid().ToString();
            SessionInstance = DateTimeOffset.Now;
        }

        public string Id { get; set; }
        public string Comments { get; set; }
        public string Participant { get; set; }
        public string Reference { get; set; }
        public Project Project { get; set; }
        public DateTimeOffset SessionInstance { get; set; }
    }
}