using System;
using System.Collections.Generic;

namespace VOD.Models
{
    public partial class OcenaFilm
    {
        public decimal? Ocena { get; set; }
        public int FilmId { get; set; }

        public Filmy Film { get; set; }
    }
}
