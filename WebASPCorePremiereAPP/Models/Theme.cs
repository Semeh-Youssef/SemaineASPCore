using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebASPCorePremiereAPP.Models
{
    public class Theme
    {
        public int Id { get; set; }
        [Required, MinLength(3)]
        public string Libelle { get; set; }

        [Required, MinLength(3,ErrorMessage ="La note doit etre supérieur à 3 caractères")]
       
        public string Note { get; set; }

        // une thématique peut  avoir plusieurs livres 

        public  ICollection<Livre> Livres { get; set; }


    }
}
