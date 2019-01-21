using System;
using System.Collections.Generic;

namespace VOD.Models
{
    public partial class AktorzyFilmy
    {
        public int FilmId { get; set; }
        public int AktorId { get; set; }

        public Aktorzy Aktor { get; set; }
        public Filmy Film { get; set; }
    }
}
