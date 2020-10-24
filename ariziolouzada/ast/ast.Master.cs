using ariziolouzada.classes;
using CriptografiaSgpm;
using System;
using System.Web.Services;
using System.Web.UI;

namespace ariziolouzada.ast
{
    public partial class ast : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Caso não esteja logado redireciona para a página inicial
            if (Session["logadoAst"].ToString() == "0")
                Response.Redirect("~/ast/login.aspx");

            CarregaDadosUsuarioLogado();

        }

        private void CarregaDadosUsuarioLogado()
        {
            var idUserLogado = Cookie.Recupera("idUserLogado");
            idUserLogado = Criptografia.Decrypt(idUserLogado);

            var userLogado = Usuario.Pesquisar(int.Parse(idUserLogado));
            if (userLogado != null)
            {
                lblNomeUser.Text = userLogado.Nome;
                var pathImgUser = "http://www.ariziolouzada.com.br/ast/imagens/usuario";
                var imageUser = userLogado.Imagem != string.Empty ? userLogado.Imagem : "profile.jpg";
                ltlImgUser.Text = string.Format("<img alt=\"image\" class=\"img-circle\" src=\"{1}/{0}\" width=\"150\" />", imageUser, pathImgUser);
            }
        }
        
        
    }
}