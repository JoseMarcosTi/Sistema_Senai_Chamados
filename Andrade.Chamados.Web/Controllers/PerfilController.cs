using Andrade.Chamado.Domain.Entidades;
using Andrade.Chamados.Data.Repositorios;
using Andrade.Chamados.Web.Util;
using Andrade.Chamados.Web.ViewModels.Conta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Andrade.Chamados.Web.Controllers
{
    [Authorize]
    public class PerfilController : Controller
    {
        // GET: Perfil
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult AlterarSenha()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AlterarSenha(AlterarSenhaViewModel senha)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Erro = "Dados inválidos. Verifique!";
                }

                // Obtêm as CLAIMS do usuário logado
                var identity = User.Identity as ClaimsIdentity;

                //Pega o valor do ID do usuário
                var id = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                var telefone = identity.Claims.FirstOrDefault(x => x.Type == "Telefone").Value;

                // Cria uma instancia de UsuarioRepositorio
                using (UsuarioRepositorio _repUsuario = new UsuarioRepositorio())
                {
                    // Busca pelo o usuário pelo seu Id
                    UsuarioDomain usuarioDomain = _repUsuario.BuscarPorId(new Guid(id));

                    // Verifica se a senha informada é igual a senha atual
                    if (Hash.GerarHash(senha.SenhaAtual) != usuarioDomain.Senha)
                    {
                        ModelState.AddModelError("SenhaAtual", " Senha incorreta");
                        return View();
                    }

                    usuarioDomain.Senha = Hash.GerarHash(senha.NovaSenha);
                    _repUsuario.Alterar(usuarioDomain);
                    TempData["Sucesso"] = " Senha alterada";
                    return RedirectToAction("Index", "Usuario");
                }
            }
            catch (System.Exception ex)
            {
                ViewBag.Erro = " Ocorreu um erro" + ex.Message;
                return View();
            }            
        }


    }
}