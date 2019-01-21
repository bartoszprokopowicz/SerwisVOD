using System;
using System.Collections.Generic;

namespace VOD.Models
{
    public partial class Aktorzy
    {
        public Aktorzy()
        {
            AktorzyFilmy = new HashSet<AktorzyFilmy>();
        }

        public int AktorId { get; set; }
        public int DaneosoboweId { get; set; }

        public Daneosobowe Daneosobowe { get; set; }
        public ICollection<AktorzyFilmy> AktorzyFilmy { get; set; }
    }
}
