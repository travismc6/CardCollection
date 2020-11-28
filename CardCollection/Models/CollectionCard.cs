using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardCollection.Models
{
    public class CollectionCard
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public int Condition { get; set; }
        public string Notes { get; set; }
        public int CollectionId { get; set; }

        public ICollection<Photo> Photos { get; set; }

        public Collection Collection { get; set; }

        public Card Card { get; set; }
    }
}
