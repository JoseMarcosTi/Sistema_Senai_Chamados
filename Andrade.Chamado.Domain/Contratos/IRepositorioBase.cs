using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andrade.Chamado.Domain.Contratos
{
    public interface IRepositorioBase<TDomain> where TDomain : class
    {
        TDomain BuscarPorId(Guid id, string[] includes = null);

        List<TDomain> Listar(string[] includes = null);

        bool Inserir(TDomain domain);

        bool Alterar(TDomain domain);

        bool Deletar(TDomain domain);


    }
}
