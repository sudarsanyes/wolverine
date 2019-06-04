using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Wolverine.Core
{
    public class Card
    {
        public Card(Card copy)
        {
            Id = Guid.NewGuid().ToString();
            Title = copy.Title;
            Description = copy.Description;
            Order = copy.Order;
            Reference = copy.Id;
        }

        public Card()
        {
            Id = Guid.NewGuid().ToString();
        }

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

        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public string Reference { get; set; }
    }
}