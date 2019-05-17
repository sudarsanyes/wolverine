using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Wolverine.Core
{
    public class Project : INotifyPropertyChanged
    {
        public Project()
        {
            ID = Guid.NewGuid().ToString();
            CreationDate = DateTimeOffset.Now;
            DefaultGroups = new ObservableCollection<Group>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static Project Default
        {
            get
            {
                var defaultProject = new Project();
                defaultProject.Author = "Unknown Author";
                var group1 = new Group()
                {
                    Title = "Sorted Group-1",
                    Description = "You will sort the cards under this group.", 
                };
                var group2 = new Group()
                {
                    Title = "Sorted Group-2",
                    Description = "You can sort the cards under this group too."
                };
                var group3 = new Group()
                {
                    Title = "Sorted Group-3",
                    Description = "You may sort the cards here as well."
                };
                group1.Cards.Add(Card.Default);
                defaultProject.DefaultGroups.Add(group1);
                defaultProject.DefaultGroups.Add(group2);
                defaultProject.DefaultGroups.Add(group3);
                defaultProject.UnsortedGroup = Group.Default;

                return defaultProject;
            }
        }

        private string id;

        public string ID
        {
            get { return id; }
            set
            {
                id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ID)));
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        private DateTimeOffset creationDate;

        public DateTimeOffset CreationDate
        {
            get { return creationDate; }
            set
            {
                creationDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CreationDate)));
            }
        }

        private string author;

        public string Author
        {
            get { return author; }
            set
            {
                author = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Author)));
            }
        }

        private Group unsortedGroup;

        public Group UnsortedGroup
        {
            get { return unsortedGroup; }
            set
            {
                unsortedGroup = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UnsortedGroup)));
            }
        }

        public ICollection<Group> DefaultGroups { get; set; }
    }
}