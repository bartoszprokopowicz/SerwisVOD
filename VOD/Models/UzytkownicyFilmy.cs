using System;
using System.Collections.Generic;

namespace VOD.Models
{
    public partial class UzytkownicyFilmy
    {
        public int UzytkownikId { get; set; }
        public int FilmId { get; set; }
        public decimal? Ocena { get; set; }

        public Filmy Film { get; set; }
        public Uzytkownicy Uzytkownik { get; set; }
    }
}
