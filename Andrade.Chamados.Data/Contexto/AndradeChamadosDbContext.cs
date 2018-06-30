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

        public override int SaveChanges()
        {
            try
            {
                foreach (var entry in ChangeTracker.Entries().Where(e => e.Entity.GetType().GetProperty("DataCriacao") != null))
                {
                    if (new Guid(entry.Property("Id").CurrentValue.ToString()) == Guid.Empty)
                    {
                        entry.Property("Id").CurrentValue = Guid.NewGuid();
                    }

                    if (entry.State == EntityState.Added)
                    {
                        entry.Property("DataCriacao").CurrentValue = DateTime.Now;
                        entry.Property("DataAlteracao").CurrentValue = DateTime.Now;
                    }

                    if (entry.State == EntityState.Modified)
                    {
                        entry.Property("DataCriacao").IsModified = false;
                        entry.Property("DataAlteracao").CurrentValue = DateTime.Now;
                    }
                }

                return base.SaveChanges();
            }
            catch (System.Exception ex)
            {

                throw new SystemException(ex.Message);
            }  
        }

    }
}
