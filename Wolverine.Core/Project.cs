using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Linq;
using System.Xml.Serialization;

namespace Wolverine.Core
{
    public class Project
    {
        public Project()
        {
            Id = Guid.NewGuid().ToString();
            CreationDate = DateTimeOffset.Now;
            Groups = new ObservableCollection<Group>();
        }

        public static Project Default
        {
            get
            {
                var defaultProject = new Project();
                defaultProject.Author = null;
                var unsortedGroup = new Group()
                {
                    Title = "Unsorted Group",
                    Description = "You add cards here which the participants will sort.", 
                    IsUnsorted = true
                };
                unsortedGroup.Cards.Add(Card.Default);
                var group1 = new Group()
                {
                    Title = "Sorted Group-1",
                    Description = "You will sort the cards under this group.", 
                };
                group1.Cards.Add(Card.Default);
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

                defaultProject.Groups.Add(unsortedGroup);
                defaultProject.Groups.Add(group1);
                defaultProject.Groups.Add(group2);
                defaultProject.Groups.Add(group3);

                return defaultProject;
            }
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public ICollection<Group> Groups { get; set; }

        [XmlIgnore]
        [NotMapped]
        public Group UnsortedGroup
        {
            get
            {
                return Groups.First(x => x.IsUnsorted == true);
            }
        }

        [XmlIgnore]
        [NotMapped]
        public IEnumerable<Group> DefaultGroups
        {
            get
            {
                return Groups.Where(x => x.IsUnsorted == false);
            }
        }
    }

    public class CategorizedProject
    {
        public CategorizedProject()
        {
            // Reserved. 
        }

        public CategorizedProject(Project project)
        {
            Id = project.Id;
            Name = project.Name;
            Description = project.Description;
            Author = project.Author;
            CreationDate = project.CreationDate;
            DefaultGroups = project.DefaultGroups;
            UnsortedGroup = project.UnsortedGroup;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public IEnumerable<Group> DefaultGroups { get; set; }
        public Group UnsortedGroup { get; set; }

        public Project ToProject()
        {
            var asProject = new Project();
            asProject.Id = Id;
            asProject.Name = Name;
            asProject.Description = Description;
            asProject.Author = Author;
            asProject.CreationDate = CreationDate;
            var groups = new List<Group>();
            groups.Add(UnsortedGroup);
            groups = groups.Union(DefaultGroups).ToList<Group>();
            asProject.Groups = groups;
            return asProject;
        }
    }
}