using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Wolverine
{
    public class Card : INotifyPropertyChanged
    {
        public Card()
        {
            ID = Guid.NewGuid().ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static Card Default
        {
            get
            {
                return new Card()
                {
                    Title = "Card-1",
                    Description = "This is a sample card.",
                    Order = 0
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

        private int order;

        public int Order
        {
            get { return order; }
            set
            {
                order = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Order)));
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
    }
}