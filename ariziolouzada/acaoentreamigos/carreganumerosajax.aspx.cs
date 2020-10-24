using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ariziolouzada.acaoentreamigos
{
    public partial class carreganumerosajax : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.HasKeys())
            {
                if (Request.QueryString["term"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["term"]))
                {
                    var valuePesquisa = Request.QueryString["term"];
                    var listaNumeros = new EfetivoDao().Listar(valuePesquisa);
                    string json = JsonConvert.SerializeObject(listaNumeros, Formatting.Indented);

                    Response.Clear();
                    Response.ContentType = "application/json; charset=utf-8";
                    Response.Write(json);
                    Response.End();
                }
            }
        }
    }
}