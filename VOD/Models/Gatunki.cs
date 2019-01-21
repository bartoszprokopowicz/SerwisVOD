using System;
using System.Collections.Generic;

namespace VOD.Models
{
    public partial class Gatunki
    {
        public Gatunki()
        {
            Filmy = new HashSet<Filmy>();
        }

        public int GatunekId { get; set; }
        public string Gatunek { get; set; }

        public ICollection<Filmy> Filmy { get; set; }
    }
}
