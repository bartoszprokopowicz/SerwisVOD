using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VOD.Models
{
    public partial class Filmy
    {
        public Filmy()
        {
            AktorzyFilmy = new HashSet<AktorzyFilmy>();
            KrajeFilmy = new HashSet<KrajeFilmy>();
            RezyserzyFilmy = new HashSet<RezyserzyFilmy>();
            UzytkownicyFilmy = new HashSet<UzytkownicyFilmy>();
        }

        public int FilmId { get; set; }
        public string TytulOrg { get; set; }
        public string TytulPol { get; set; }
        public string Opis { get; set; }
        public string Plakat { get; set; }
        [DataType(DataType.Currency)]
        public decimal Cena { get; set; }
        [DataType(DataType.Date)]
        public DateTime DataPremiery { get; set; }
        public int GatunekId { get; set; }
        public int PracownikId { get; set; }
        public Gatunki Gatunek { get; set; }
        public Uzytkownicy Pracownik { get; set; }
        public OcenaFilm OcenaFilm { get; set; }
        [NotMapped]
        public int Ocena { get; set; }
        public ICollection<AktorzyFilmy> AktorzyFilmy { get; set; }
        public ICollection<KrajeFilmy> KrajeFilmy { get; set; }
        public ICollection<RezyserzyFilmy> RezyserzyFilmy { get; set; }
        public ICollection<UzytkownicyFilmy> UzytkownicyFilmy { get; set; }
    }
}
