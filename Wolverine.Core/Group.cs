using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Wolverine.Core
{
    public class Group
    {
        public Group()
        {
            Id = Guid.NewGuid().ToString();
            Cards = new ObservableCollection<Card>();
            IsUnsorted = false;
        }

        public static Group Default
        {
            get
            {
                return new Group()
                {
                    Title = "Untitled-Group",
                    Description = "This is a sample group. Participant will sort cards in to this group",
                    Cards = new ObservableCollection<Card>()
                    {
                        Card.Default
                    }
                };
            }
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsUnsorted { get; set; }
        public ICollection<Card> Cards { get; set; }
    }
}