using System;

namespace Andrade.Chamados.Web.Helpers
{
    public class MyHelper
    {
        public static string DireitosReservados()
        {
            return DateTime.Now.Year + " - Andrade - Todos os direitos reservados";
        }

        public static string BoasVindas()
        {
            return "Seja bem vindo, ";
        }
    }
}