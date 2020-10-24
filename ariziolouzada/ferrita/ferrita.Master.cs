using System;
using System.Web.UI;

namespace ariziolouzada.ferrita
{
    public partial class ferrita : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["logadoFerritaSystem"] == null)
                {
                    Response.Redirect("~/ferrita/login.aspx");
                }
                else if (Session["logadoFerritaSystem"].ToString().Equals("0"))
                {
                    Response.Redirect("~/ferrita/login.aspx");
                }

                if (!IsPostBack)
                {
                    lblNome.Text = "Andrea Emilia FERRI";
                }
            }
            catch (Exception ex)
            {
                MessageBoxScript("Page_Load-Erro:" + ex.Message);
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


    }
}