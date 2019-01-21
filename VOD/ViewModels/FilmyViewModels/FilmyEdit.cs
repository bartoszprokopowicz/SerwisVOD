using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VOD.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VOD.ViewModels.FilmyViewModels
{
    public class FilmyEdit
    {
        public Filmy filmy { get; set; }
        public SelectList kraje;
        public SelectList aktorzy { get; set; }
        public SelectList rezyserzy { get; set; }
        public IEnumerable<String> wybranyRezyser { get; set; }
        public IEnumerable<String> wybranyAktor { get; set; }
        public IEnumerable<String> wybranyKraj { get; set; }
        
        
    }
    public class pelneImie
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string PelneImie { get { return Imie + " " + Nazwisko; } }
    }
}
