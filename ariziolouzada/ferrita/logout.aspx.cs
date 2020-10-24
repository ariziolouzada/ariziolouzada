using System;
using System.Web.UI;

namespace ariziolouzada.ferrita
{
    public partial class logout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSim_Click(object sender, EventArgs e)
        {
            Session["logadoFerritaSystem"] = "0";
            Response.Redirect("~/ferrita/login.aspx");
        }

        protected void btnNao_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ferrita");
        }
    }
}