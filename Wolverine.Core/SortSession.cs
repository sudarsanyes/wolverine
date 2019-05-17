using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Wolverine.Core
{
    public class SortSession : INotifyPropertyChanged
    {
        public SortSession()
        {
            // Reserved. 
        }

        public event PropertyChangedEventHandler PropertyChanged;

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

        private string participant;

        public string Participant
        {
            get { return participant; }
            set
            {
                participant = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Participant)));
            }
        }

        private DateTimeOffset sessionDate;

        public DateTimeOffset SessionDate
        {
            get { return sessionDate; }
            set
            {
                sessionDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SessionDate)));
            }
        }

        private Project project;

        public Project Project
        {
            get { return project; }
            set
            {
                project = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SessionDate)));
            }
        }
    }
}