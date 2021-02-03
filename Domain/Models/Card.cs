using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardCollection.Domain.Models
{
    public class Card 
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public double Grade { get; set; }
        public string Notes { get; set; }
        public Photo Photo { get; set; }
        public string PhotoLink { get; set; }
        public int CardSetId { get; set; }

        public CardSet CardSet { get; set; }
    }
}
