using System;
using System.Collections.Generic;

namespace VOD.Models
{
    public partial class KrajeFilmy
    {
        public int FilmId { get; set; }
        public int KrajId { get; set; }

        public Filmy Film { get; set; }
        public Kraje Kraj { get; set; }
    }
}
