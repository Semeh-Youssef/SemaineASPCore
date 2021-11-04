using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebASPCorePremiereAPP.Models
{
    public class Livre
    {

        public int Id { get; set; }

        [Required, MinLength(5)]
        public string Titre { get; set; }
        [Range(1920,2030)]
        public int Annee { get; set; }
        public string Resume { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string ISBN { get; set; }

        public string Photo { get; set; }
        public int ThemeId { get; set; }

        // propriété de bavigation
        public Theme Theme { get; set; }

        public ICollection<Auteur> Auteurs { get; set; }
    }
}
