using System;
using System.Web;

namespace ariziolouzada.classes
{
    public class Cookie
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

    }
}
