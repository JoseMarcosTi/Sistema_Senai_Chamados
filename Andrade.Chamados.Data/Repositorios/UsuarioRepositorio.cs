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
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly AndradeChamadosDbContext _contexto;

        public UsuarioRepositorio()
        {
            _contexto = new AndradeChamadosDbContext();
        }

        /// <summary>
        /// MéTODO PARA ALTERAR DADOS DO USUÁRIO NO BANCO DE DADOS
        /// </summary>
        /// <param name="domain"> Altera dados do usuário</param>
        /// <returns> Retorna true caso os dados forem alterados e false caso ocorra algum erro </returns>
        public bool Alterar(UsuarioDomain domain)
        {
            _contexto.Entry<UsuarioDomain>(domain).State = System.Data.Entity.EntityState.Modified;
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

        /// <summary>
        /// MéTODO PARA BUSCAR USUÁRIO POR ID
        /// </summary>
        /// <param name="id"> ID do Usuário</param>
        /// <param name="includes"> Lista de usuários </param>
        /// <returns> Retorna uma lista de usuários através do ID dos cadastrados no banco de dados </returns>
        public UsuarioDomain BuscarPorId(Guid id, string[] includes = null)
        {
            var query = _contexto.Usuarios.AsQueryable();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.FirstOrDefault( x => x.Id == id);
        }

        /// <summary>
        /// MéTODO PARA DELETAR USUÁRIOS NO BANCO DE DADOS
        /// </summary>
        /// <param name="domain"> Deletar os dados de um usuário</param>
        /// <returns> Retorna true se o usuário for excluido e false, caso ocorra algum erro </returns>
        public bool Deletar(UsuarioDomain domain)
        {
            var usuario = _contexto.Usuarios.Single(o => o.Id == domain.Id);
            _contexto.Usuarios.Remove(usuario);
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

        /// <summary>
        /// Metodo para encerrar as operações
        /// </summary>
        public void Dispose()
        {
            _contexto.Dispose();
        }

        /// <summary>
        /// MéTODO PARA INSERIR USUÁRIOS NO BANCO DE DADOS
        /// </summary>
        /// <param name="domain"> Dados do usuário</param>
        /// <returns> Retorna true para usuários cadastrados e false para usuários caso não seja cadastrado. </returns>        
        public bool Inserir(UsuarioDomain domain)
        {
            _contexto.Usuarios.Add(domain);
           int linhasIncluidas = _contexto.SaveChanges();

            if (linhasIncluidas > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// MéTODO PARA LISTAR OS USUÁRIOS DO BANCO DE DADOS
        /// </summary>
        /// <param name="includes"> Lista de usuários</param>
        /// <returns> Retorna uma lista de usuários </returns>
        public List<UsuarioDomain> Listar(string[] includes = null)
        {
            var query = _contexto.Usuarios.AsQueryable();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                // ESTÁ LINHA DE CODIGO ABAIXO EXECUTA A MESMA FUNçÃO QUE O FOREACH
                //query = includes.Aggregate(query, (current, include) => current.Include(include));

            }

            return query.ToList();
        }

        /// <summary>
        /// MéTODO PARA O USUÁRIO LOGAR NO SISTEMA
        /// </summary>
        /// <param name="email"> E-mail do usuário</param>
        /// <param name="senha"> Senha do usuário</param>
        /// <returns> Retorna se o usuário caso este esteja cadastrado </returns>
        public UsuarioDomain Login(string email, string senha)
        {
            UsuarioDomain usuarioDomain = _contexto.Usuarios.FirstOrDefault(x => x.Email == email && x.Senha == senha);

            if (usuarioDomain == null)
            {
                return null;
            }
            else
            {
                return usuarioDomain;
            }
        }


    }
}
