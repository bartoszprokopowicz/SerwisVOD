using System;
using System.Collections.Generic;

namespace VOD.Models
{
    public partial class Rezyserzy
    {
        public Rezyserzy()
        {
            RezyserzyFilmy = new HashSet<RezyserzyFilmy>();
        }

        public int RezyserId { get; set; }
        public int DaneosoboweId { get; set; }

        public Daneosobowe Daneosobowe { get; set; }
        public ICollection<RezyserzyFilmy> RezyserzyFilmy { get; set; }
    }
}
