using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Wolverine.Core
{
    public class Group : INotifyPropertyChanged
    {
        public Group()
        {
            ID = Guid.NewGuid().ToString();
            Cards = new ObservableCollection<Card>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static Group Default
        {
            get
            {
                return new Group()
                {
                    Title = "Untitled-Group",
                    Description = "This is an unsorted group. Add your cards to this group.",
                    Cards = new ObservableCollection<Card>()
                    {
                        Card.Default
                    }
                };
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

        private string title;

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
            }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
            }
        }

        public ICollection<Card> Cards { get; set; }
    }
}