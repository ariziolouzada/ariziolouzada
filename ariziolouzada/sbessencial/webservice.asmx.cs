using CriptografiaSgpm;
using sbessencial_cl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ariziolouzada.sbessencial
{
    /// <summary>
    /// Webservice para os serviços do APP Salão Essencial.
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class webservice : WebService
    {
        /// <summary>
        /// Método para realizar o Login
        /// </summary>
        /// <param name="login">Login do usuário.</param>
        /// <param name="senha">Senha do susuário.</param>
        /// <returns></returns>
        [WebMethod]
        public string LoginAdm(string login, string senha)
        {
            var senhaCriptografada = Criptografia.Encrypt(senha);
            if (UsuarioSbe.AutenticacaoUsuario(login.ToUpper(), senhaCriptografada))
                return "loginOk";

            return string.Empty;
        }
    }
}
