using Andrade.Chamado.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andrade.Chamado.Domain.Contratos
{
    public interface IUsuarioRepositorio : IDisposable, IRepositorioBase<UsuarioDomain>
    {
        UsuarioDomain Login(string email, string senha);
    }
}
