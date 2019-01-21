using System;
using System.Collections.Generic;

namespace VOD.Models
{
    public partial class Kraje
    {
        public Kraje()
        {
            KrajeFilmy = new HashSet<KrajeFilmy>();
        }

        public int KrajId { get; set; }
        public string KrajKod { get; set; }
        public string KrajNazwa { get; set; }

        public ICollection<KrajeFilmy> KrajeFilmy { get; set; }
    }
}
