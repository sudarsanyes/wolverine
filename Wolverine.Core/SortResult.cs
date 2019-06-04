using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wolverine.Core
{

    public class SortResult
    {
        public SortResult(Project referencedProject)
        {
            ReferencedProject = referencedProject;
            ProjectId = referencedProject.Id;
            CardGroupMap = new List<KeyValuePair<Card, Tuple<string, string>[]>>();
            CardGroupResolvedMap = new List<KeyValuePair<Card, GroupMap[]>>();
        }

        public string ProjectId { get; set; }
        public Project ReferencedProject { get; set; }
        public IList<KeyValuePair<Card, Tuple<string, string>[]>> CardGroupMap { get; set; }

        [NotMapped]
        public IList<KeyValuePair<Card, GroupMap[]>> CardGroupResolvedMap { get; set; }

        public void UpdateMap(SortSession[] sortSessions)
        {
            IList<KeyValuePair<Card, Tuple<string, string>[]>> cardMap = new List<KeyValuePair<Card, Tuple<string, string>[]>>();
            foreach (var card in ReferencedProject.UnsortedGroup.Cards)
            {
                IList<Tuple<string, string>> groupArray = new List<Tuple<string, string>>();
                foreach (var session in sortSessions)
                {
                    var reference = session.Project.FindReferencedGroup(card);
                    if (!string.IsNullOrWhiteSpace(reference))
                    {
                        groupArray.Add(new Tuple<string, string>(reference, session.Id));
                    }
                }
                cardMap.Add(new KeyValuePair<Card, Tuple<string, string>[]>(card, groupArray.ToArray()));
            }
            CardGroupMap = cardMap;
        }
    }

    public class GroupMap
    {
        public GroupMap(Group group, SortSessionInfo session)
        {
            Group = group;
            Session = session;
        }

        public Group Group { get; set; }
        public SortSessionInfo Session { get; set; }
    }

    public class SortSessionInfo
    {
        public SortSessionInfo(string id, string participant)
        {
            Id = id;
            Participant = participant;
        }

        public string Id { get; set; }
        public string Participant { get; set; }
    }
}