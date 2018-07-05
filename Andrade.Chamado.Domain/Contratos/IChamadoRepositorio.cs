using Andrade.Chamado.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andrade.Chamado.Domain.Contratos
{
    public interface IChamadoRepositorio : IDisposable, IRepositorioBase<ChamadoDomain>
    {
        List<ChamadoDomain> Listar(Guid idUsuario, string[] includes = null);

    }
}
