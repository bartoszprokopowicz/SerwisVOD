using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VOD.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VOD.ViewModels.FilmyViewModels
{
    public class FilmyInfo
    {
        public IEnumerable<Filmy> filmy { get; set; }
        public SelectList gatunki;
        public List<Kraje> kraje;
        public List<Aktorzy> aktorzy;
        public List<Rezyserzy> rezyserzy;
    }
}
