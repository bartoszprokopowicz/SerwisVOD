using System;
using System.Collections.Generic;

namespace VOD.Models
{
    public partial class RezyserzyFilmy
    {
        public int FilmId { get; set; }
        public int RezyserId { get; set; }

        public Filmy Film { get; set; }
        public Rezyserzy Rezyser { get; set; }
    }
}
