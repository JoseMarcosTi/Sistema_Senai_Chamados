using System.Security.Cryptography;
using System.Text;

namespace Andrade.Chamados.Web.Util
{
    public class Hash
    {
        /// <summary>
        ///  Retorna um texto encriptografado
        /// </summary>
        /// <param name="Texto"> Texto que irá ser encriptografado</param>
        /// <returns></returns> Retorna o texto encriptografado
        public static string GerarHash(string Texto)
        {
            //Declaro uma variavel do tipo StrignBuider
            StringBuilder result = new StringBuilder();

            // Declaro uma variavel do tipo SHA256 para encriptografia
            SHA256 sha256 = SHA256Managed.Create();

            // Converto o texto recebido como paramentro em bytes
            byte[] bytes = Encoding.UTF8.GetBytes(Texto);

            // Gera um Hash de acordo com a variavel bytes
            byte[] hash = sha256.ComputeHash(bytes);

            // Percorre o hash e vai concatenando o texto
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X"));
            }

            // Retorna o texto encriptografado
            return result.ToString();
        }
    }
}