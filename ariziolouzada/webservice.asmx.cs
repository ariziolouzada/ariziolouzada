using CriptografiaSgpm;
using sbessencial_cl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ariziolouzada
{
    /// <summary>
    /// Descrição resumida de WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        /// <summary>
        /// Método para realizar o login na área ADM do APP
        /// </summary>
        /// <param name="login">Login do usuário</param>
        /// <param name="senha">Senha do Usuário</param>
        /// <returns>Retorna login ok em caso positivo e string vazia caso negativo.</returns>
        [WebMethod]
        public string LogarAdm(string login, string senha)
        {
            var senhaCriptografada = Criptografia.Encrypt(senha);
            if (UsuarioSbe.AutenticacaoUsuario(login.ToUpper(), senhaCriptografada))
                return "login_ok";

            return string.Empty;
        }



    }
}
