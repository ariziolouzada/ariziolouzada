using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace ariziolouzada
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["logadoAst"] = "0";
            Session["logadoSbEssencial"] = "0";
            Session["logadoEspacoBeleza"] = "0";
            Session["logadoAcaoEntreAmigos"] = "0";
            Session["captchaSbEssencial"] = string.Empty;
            Session["paremetrosDefault"] = string.Empty;
            Session["idFcExcluir"] = "0";
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}