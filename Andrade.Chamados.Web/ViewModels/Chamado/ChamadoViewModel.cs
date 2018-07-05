using Andrade.Chamado.Domain.Enum;
using Andrade.Chamados.Web.ViewModels.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Andrade.Chamados.Web.ViewModels.Chamado
{
    public class ChamadoViewModel : BaseViewModel
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public EnSetor Setor { get; set; }
        public EnStatus Status { get; set; }
        public Guid IdUsuario { get; set; }

        public virtual UsuarioViewModel Usuario { get; set; }

        public SelectList ListaSetores { get; set; }
        public SelectList ListaStatus { get; set; }
    }
}