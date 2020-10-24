using espacobeleza_cl;
using System;
using System.Web.UI;

namespace ariziolouzada.espacodebeleza.pages.colaborador
{
    public partial class espacobelezacolaborador : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Caso não esteja logado redireciona para a página inicial
            if (Session["logadoEspacoBeleza"].ToString() == "0")
                Response.Redirect("~/espacodebeleza/login.aspx");

            var idUserLogado = CookieEspBel.Recupera("idUserLogado", true);
            if (!UsuarioEspBelPermissao.Possui(int.Parse(idUserLogado), 11))
            {
                Response.Redirect("~/espacodebeleza/logout.aspx");
            }

        }
    }
}