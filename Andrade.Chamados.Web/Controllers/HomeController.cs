using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Andrade.Chamados.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Title = " Home index controller ";
            ViewBag.Mensagem = " Hello my friend, welcome! ";
            return View();
        }
    }
}