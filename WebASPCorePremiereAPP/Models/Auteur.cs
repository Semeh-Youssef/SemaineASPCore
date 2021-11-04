using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebASPCorePremiereAPP.Models
{
    public class Auteur
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public ICollection<Livre> Livres { get; set; }

    }
}
