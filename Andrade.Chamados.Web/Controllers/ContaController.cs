using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Andrade.Chamados.Web.Controllers
{
    public class ContaController : Controller
    {
        // GET: Conta
        public ActionResult Login()
        {
            ViewBag.Mensagem = " Welcome user! ";
            return View();
        }

        public ActionResult CadastrarUsuario()
        {
            ViewBag.Mensagem = " You're already cadastrated! ";
            return View();
        }
    }
}