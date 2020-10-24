using System;
using System.Web;

namespace sbessencial_cl
{
   public class CookieSbe
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

        public static string Recupera(string nome)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[nome];
            if (cookie != null && cookie.Value != null)
                return cookie.Value;

            return string.Empty;
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
