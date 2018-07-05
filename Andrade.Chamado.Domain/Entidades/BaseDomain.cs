using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andrade.Chamado.Domain.Entidades
{
    /// <summary>
    /// Dominío base do sistema
    /// </summary>
    public class BaseDomain
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
