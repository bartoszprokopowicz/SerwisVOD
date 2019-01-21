using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VOD.Models
{
    public partial class Daneosobowe
    {
        public Daneosobowe()
        {
            Aktorzy = new HashSet<Aktorzy>();
            Rezyserzy = new HashSet<Rezyserzy>();
            Uzytkownicy = new HashSet<Uzytkownicy>();
        }

        public int DaneosoboweId { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        [DataType(DataType.Date)]
        public DateTime DataUrodzin { get; set; }

        public ICollection<Aktorzy> Aktorzy { get; set; }
        public ICollection<Rezyserzy> Rezyserzy { get; set; }
        public ICollection<Uzytkownicy> Uzytkownicy { get; set; }
    }
}
