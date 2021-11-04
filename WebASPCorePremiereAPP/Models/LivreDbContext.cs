using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebASPCorePremiereAPP.Models
{
    public class LivreDbContext :  DbContext
    {
        public LivreDbContext(DbContextOptions<LivreDbContext> options) : base(options)
    {

    }

    public DbSet<Livre> Livres { get; set; }
    public DbSet<Theme> Themes { get; set; }
        public DbSet<Auteur>Auteurs  { get; set; }
    }

}