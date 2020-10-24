using sbessencial_cl;
using CriptografiaSgpm;
using System;
using System.Web.UI;

namespace ariziolouzada.sbessencial
{
    public partial class sbessencial : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Caso não esteja logado redireciona para a página inicial
            if (Session["logadoSbEssencial"].ToString() == "0")
                Response.Redirect("~/sbessencial/login.aspx");

            CarregaDadosUsuarioLogado();

        }


        /// <summary>
        /// Metodo para exibir uma mensagem utilizando alert do JavaScript.
        /// </summary>
        /// <param name="msg">Mensagem a ser exibida.</param>
        public void MessageBoxScript(string msg)
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Mensagem do Sistema", "alert('" + msg + "');", true);
        }

        private void CarregaItensMenu()
        {
            try
            {
                var idUserLogado = CookieSbe.Recupera("idUserLogado");
                //Verifica se é ADM do Site se não for varifica cada permissão
                if (!UsuarioSbePermissao.Possui(int.Parse(idUserLogado), 1))
                {
                    menuAgendaMinhaAgda.Visible = UsuarioSbePermissao.Possui(int.Parse(idUserLogado), 4);
                    menuAgendaWeb.Visible = UsuarioSbePermissao.Possui(int.Parse(idUserLogado), 10);
                    menuAgenda.Visible = menuAgendaMinhaAgda.Visible || menuAgendaWeb.Visible;

                    menuCliente.Visible = UsuarioSbePermissao.Possui(int.Parse(idUserLogado), 5);
                    menuProfissional.Visible = UsuarioSbePermissao.Possui(int.Parse(idUserLogado), 6);
                    menuServico.Visible = UsuarioSbePermissao.Possui(int.Parse(idUserLogado), 7);
                    menuProduto.Visible = UsuarioSbePermissao.Possui(int.Parse(idUserLogado), 8);
                    menuFinanceiro.Visible = UsuarioSbePermissao.Possui(int.Parse(idUserLogado), 9);
                    menuFinancCaixa.Visible = UsuarioSbePermissao.Possui(int.Parse(idUserLogado), 11);
                    menuFinancFechamto.Visible = UsuarioSbePermissao.Possui(int.Parse(idUserLogado), 12);
                    menuFinancTipoSaida.Visible = UsuarioSbePermissao.Possui(int.Parse(idUserLogado), 13);
                    menuUsuario.Visible = UsuarioSbePermissao.Possui(int.Parse(idUserLogado), 14);
                }

            }
            catch (Exception ex)
            {
                MessageBoxScript("CarregaItensMenu-Erro: " + ex.Message);
            }
        }

        private void CarregaDadosUsuarioLogado()
        {
            try
            {
                var idUserLogado = CookieSbe.Recupera("idUserLogado");
                var loginUserLogado = CookieSbe.Recupera("LoginUserLogado");
                var imgUserLogado = CookieSbe.Recupera("imgUserLogado");

                if (idUserLogado != string.Empty)
                {
                    idUserLogado = Criptografia.Decrypt(idUserLogado);

                    // var userLogado = (UsuarioSbe)Session["usuarioLogadoSbEssencial"];
                    //if (userLogado != null)
                    //{
                    lblNomeUser.Text = loginUserLogado;
                    var pathImgUser = "http://www.ariziolouzada.com.br/sbessencial/imagem/usuario";
                    var imageUser = imgUserLogado != string.Empty ? imgUserLogado : "profile.jpg";
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

    }
}