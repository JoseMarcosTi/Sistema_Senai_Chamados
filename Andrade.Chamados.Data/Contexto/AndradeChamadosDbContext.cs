using Andrade.Chamado.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andrade.Chamados.Data.Contexto
{
    public class AndradeChamadosDbContext : DbContext
    {
        public AndradeChamadosDbContext() : base(@"Data Source=.\SqlExpress;Initial Catalog = AndradeChamadosDb; user id = marcos; password=12345;")
        {

        }

        public DbSet<UsuarioDomain> Usuarios { get; set; }

    }
}
