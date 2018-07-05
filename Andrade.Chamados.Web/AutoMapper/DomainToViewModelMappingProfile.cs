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
    public class DomainToViewModelMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap(typeof(UsuarioDomain), typeof(CadastrarUsuarioViewModel));

            Mapper.CreateMap(typeof(UsuarioDomain), typeof(UsuarioViewModel));

            Mapper.CreateMap(typeof(ChamadoDomain), typeof(ChamadoViewModel));

        }
    }
}