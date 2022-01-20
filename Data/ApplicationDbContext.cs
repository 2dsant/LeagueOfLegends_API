using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeagueOfLegends_API.Models;
using Microsoft.EntityFrameworkCore;

namespace LeagueOfLegends_API.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Personagem> Personagens { get; set; }
        public DbSet<Habilidade> Habilidades { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

    }
}