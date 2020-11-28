using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardCollection.Dtos
{
    public class CardsForChecklistDto
    {
        public int CollectionId { get; set; }
        //list of sets
        public List<SetForChecklistDto> Sets = new List<SetForChecklistDto>();
    }

    public class SetForChecklistDto
    {
        public int Year { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }

        // list of cards that are owned
        public ICollection<CardForChecklistDto> Cards { get; set; }

    }

    public class CardForChecklistDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string PlayerName { get; set; }
        public string Notes { get; set; }
        public string SetName { get; set; }
        public string Brand { get; set; }
        public int Year { get; set; }
        public bool HasCard { get; set; }
    }
}
