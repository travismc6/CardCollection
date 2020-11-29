using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardCollection.Domain.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<CollectionCard> CollectionCards { get; set; }
    }
}
