using Andrade.Chamado.Domain.Entidades;
using Andrade.Chamados.Web.Models;
using Andrade.Chamados.Web.ViewModels.Chamado;
using Andrade.Chamados.Web.ViewModels.Usuario;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Andrade.Chamados.Web.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap(typeof(CadastrarUsuarioViewModel), typeof(UsuarioDomain));

            Mapper.CreateMap(typeof(UsuarioViewModel), typeof(UsuarioDomain));

            Mapper.CreateMap(typeof(ChamadoViewModel), typeof(ChamadoDomain));
        }
    }
}