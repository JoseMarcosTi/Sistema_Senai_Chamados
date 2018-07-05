using Andrade.Chamado.Domain.Contratos;
using Andrade.Chamado.Domain.Entidades;
using Andrade.Chamados.Data.Contexto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andrade.Chamados.Data.Repositorios
{
    public class ChamadoRepositorio : IChamadoRepositorio
    {
        //String de conexão com banco de dados
        private readonly AndradeChamadosDbContext _contexto;

        // CONSTRUTOR DA CLASSE CHAMADOREPOSITORIO
        public ChamadoRepositorio()
        {
            _contexto = new AndradeChamadosDbContext();
        }


        // ------------- ALTERAR ------------
        public bool Alterar(ChamadoDomain domain)
        {
            _contexto.Entry<ChamadoDomain>(domain).State = System.Data.Entity.EntityState.Modified;
            int linhasAlteradas = _contexto.SaveChanges();
            if (linhasAlteradas > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //-------------- BUSCAR POR ID DO CHAMADO ------------
        public ChamadoDomain BuscarPorId(Guid id, string[] includes = null)
        {
            var query = _contexto.Chamados.AsQueryable();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.FirstOrDefault(x => x.Id == id);
        }


        //-------------- DELETAR ----------------
        public bool Deletar(ChamadoDomain domain)
        {
            var chamado = _contexto.Chamados.Single(x => x.Id == domain.Id);
            _contexto.Chamados.Remove(chamado);
            int linhasExcluidas = _contexto.SaveChanges();

            if (linhasExcluidas > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void Dispose()
        {
            _contexto.Dispose();
        }


        //-------------- INSERIR ----------------
        public bool Inserir(ChamadoDomain domain)
        {
            _contexto.Chamados.Add(domain);
            int linhasAdicionadas = _contexto.SaveChanges();

            if (linhasAdicionadas > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


       //--------------- LISTAR POR ID DO USUÁRIO -----------------
        public List<ChamadoDomain> Listar(Guid idUsuario, string[] includes = null)
        {
            var query = _contexto.Chamados.AsQueryable();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.Where( x => x.IdUsuario == idUsuario).ToList();
        }


        //-------------- LISTAR CHAMADOS ----------------------
        public List<ChamadoDomain> Listar(string[] includes = null)
        {
            var query = _contexto.Chamados.AsQueryable();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.ToList();
        }

    }
}
