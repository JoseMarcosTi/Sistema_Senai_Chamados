using Andrade.Chamado.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andrade.Chamado.Domain.Entidades
{
    /// <summary>
    /// Classe responsável pela entidade chamados
    /// </summary>
    [Table("Chamados")]
    public class ChamadoDomain : BaseDomain
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public EnSetor Setor { get; set; }
        public EnStatus Status { get; set; }

        [ForeignKey("Usuario")]
        public Guid IdUsuario { get; set; }
        public virtual UsuarioDomain Usuario { get; set; }
    }
}
