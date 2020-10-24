using CriptografiaSgpm;
using System;
using System.Web;

namespace espacobeleza_cl
{
    public class CookieEspBel
    {

        public static void Grava(string nome, string valor)
        {
            try
            {
                var cookie = new HttpCookie(nome) { Value = valor };
                var somarTempo = new TimeSpan(0, 1, 0, 0);
                cookie.Expires = DateTime.Now + somarTempo;
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Grava(string nome, string valor, bool encriptografar)
        {
            try
            {
                //Criptografa o valor do cookie
                if (encriptografar)
                    valor = Criptografia.Encrypt(valor);

                var cookie = new HttpCookie(nome) { Value = valor };
                var somarTempo = new TimeSpan(0, 1, 0, 0);
                cookie.Expires = DateTime.Now + somarTempo;
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Recupera(string nome)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[nome];
            if (cookie != null && cookie.Value != null)
                return cookie.Value;

            return string.Empty;
        }

        public static string Recupera(string nome, bool decriptografar)
        {
            var valor = string.Empty;

            HttpCookie cookie = HttpContext.Current.Request.Cookies[nome];
            if (cookie != null && cookie.Value != null)
                valor = cookie.Value;

            //Descriptografa o valor do cookie
            if (decriptografar)
                valor = Criptografia.Decrypt(valor);

            return valor;
        }

        public static void Apaga(string nome)
        {
            try
            {
                var cookie = new HttpCookie(nome);
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
