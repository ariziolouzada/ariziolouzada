using sbessencial_cl;
using System;
using System.Web.UI;

namespace ariziolouzada.sbessencial.minhagenda
{
    public partial class minhagenda : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Caso não esteja logado redireciona para a página inicial
            if (Session["logadoAgendaSbEssencial"] != null)
            {
                if (Session["logadoAgendaSbEssencial"].ToString() == "0")
                    Response.Redirect("~/sbessencial/minhagenda/login.aspx");
            }
            else
            {
                Response.Redirect("~/sbessencial/minhagenda/login.aspx");
            }

            if (!IsPostBack)
            {
                CarregaDadosUserLogado();


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


        private void CarregaDadosUserLogado()
        {
            try
            {

                var idUserAgendaLogado = CookieSbe.Recupera("idUserAgendaLogado");
                var clt = Cliente.Pesquisar(int.Parse(idUserAgendaLogado));
                if (clt != null)
                {
                    lblNomeUser.Text = clt.Nome;

                }

            }
            catch (Exception ex)
            {
                MessageBoxScript("CarregaDadosUserLogado-Erro: " + ex.Message);
            }
        }


    }
}