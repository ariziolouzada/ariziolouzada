using CriptografiaSgpm;
using espacobeleza_cl;
using System;
using System.Web.UI;

namespace ariziolouzada.espacodebeleza
{
    public partial class espacobeleza : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Caso não esteja logado redireciona para a página inicial
            if (Session["logadoEspacoBeleza"].ToString() == "0")
                Response.Redirect("~/espacodebeleza/login.aspx");

            if (!IsPostBack)
            {
                CarregaDadosUsuarioLogado();
            }

        }


        /// <summary>
        /// Metodo para exibir uma mensagem utilizando alert do JavaScript.
        /// </summary>
        /// <param name="msg">Mensagem a ser exibida.</param>
        public void MessageBoxScript(string msg)
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Mensagem do Sistema", "alert('" + msg + "');", true);
        }


        private void CarregaDadosUsuarioLogado()
        {
            try
            {
                var idUserLogado = CookieEspBel.Recupera("idUserLogado");
                var loginUserLogado = CookieEspBel.Recupera("LoginUserLogado");
                var imgUserLogado = CookieEspBel.Recupera("imgUserLogado");

                if (idUserLogado != string.Empty)
                {
                    idUserLogado = Criptografia.Decrypt(idUserLogado);

                    // var userLogado = (UsuarioSbe)Session["usuarioLogadoSbEssencial"];
                    //if (userLogado != null)
                    //{
                    lblNomeUser.Text = loginUserLogado;
                    var pathImgUser = "http://www.ariziolouzada.com.br/espacodebeleza/imagens/usuario";
                    var imageUser = imgUserLogado != string.Empty ? imgUserLogado : "profile.png";
                    ltlImgUser.Text = string.Format("<img alt=\"image\" class=\"img-circle\" src=\"{1}/{0}\" width=\"100\" />", imageUser, pathImgUser);
                    //}
                    //else
                    //{
                    //    MessageBoxScript("Erro ao carregar os dados do Usuário Logado!!");
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBoxScript("CarregaDadosUsuarioLogado-Erro: " + ex.Message);
            }
        }


        private void CarregaItensMenu()
        {
            try
            {
                var idUserLogado = CookieEspBel.Recupera("idUserLogado");
                //Verifica se é ADM do Site se não for varifica cada permissão
                if (!UsuarioEspBelPermissao.Possui(int.Parse(idUserLogado), 1))
                {
                    //menuAgendaMinhaAgda.Visible = UsuarioEspBelPermissao.Possui(int.Parse(idUserLogado), 4);
                    menuAgenda.Visible = UsuarioEspBelPermissao.Possui(int.Parse(idUserLogado), 2);
                    //menuAgenda.Visible = menuAgendaMinhaAgda.Visible || menuAgendaWeb.Visible;

                    menuCliente.Visible = UsuarioEspBelPermissao.Possui(int.Parse(idUserLogado), 3);
                    menuProfissional.Visible = UsuarioEspBelPermissao.Possui(int.Parse(idUserLogado), 4);
                    menuServico.Visible = UsuarioEspBelPermissao.Possui(int.Parse(idUserLogado), 5);
                    //menuProduto.Visible = UsuarioEspBelPermissao.Possui(int.Parse(idUserLogado), 6);
                    menuComanda.Visible = UsuarioEspBelPermissao.Possui(int.Parse(idUserLogado), 7);
                    menuFinanceiro.Visible = UsuarioEspBelPermissao.Possui(int.Parse(idUserLogado), 8);
                    menuFinancEntradaDiaria.Visible = UsuarioEspBelPermissao.Possui(int.Parse(idUserLogado), 9);
                    menuFinancEntradaPeriodo.Visible = UsuarioEspBelPermissao.Possui(int.Parse(idUserLogado), 10);
                    menuColaborador.Visible = UsuarioEspBelPermissao.Possui(int.Parse(idUserLogado), 11);

                }

            }
            catch (Exception ex)
            {
                MessageBoxScript("CarregaItensMenu-Erro: " + ex.Message);
            }
        }

    }
}