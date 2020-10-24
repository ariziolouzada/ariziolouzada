using System;
using System.Web.UI;

namespace ariziolouzada.espacodebeleza
{
    public partial class logout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["logadoEspacoBeleza"] = "0";
            Response.Redirect("~/espacodebeleza/login.aspx");
        }
    }
}