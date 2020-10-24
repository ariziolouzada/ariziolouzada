using System;
using System.Web.UI;

namespace ariziolouzada.sbessencial
{
    public partial class logout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["logadoSbEssencial"] = "0";
            Response.Redirect("~/sbessencial/login.aspx");
        }
    }
}