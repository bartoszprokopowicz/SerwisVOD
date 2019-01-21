using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace VOD.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class Uzytkownicy : IdentityUser<int>
    {
        public Uzytkownicy() : base() {
            UzytkownicyFilmy = new HashSet<UzytkownicyFilmy>();
        }
        public DateTime DataUtworzenia { get; set; }
        public int DaneosoboweId { get; set; }
        public Daneosobowe Daneosobowe { get; set; }
        public ICollection<UzytkownicyFilmy> UzytkownicyFilmy { get; set; }
    }
}
