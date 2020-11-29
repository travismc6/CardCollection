using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardCollection.Dtos
{
    public class CardSetDto
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
}
