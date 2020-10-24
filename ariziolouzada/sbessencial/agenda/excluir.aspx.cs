using CriptografiaSgpm;
using sbessencial_cl;
using System;
using System.Web.UI;

namespace ariziolouzada.sbessencial.agenda
{
    public partial class excluir : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString.HasKeys())
                {
                    if (Request.QueryString["id"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["id"]))
                    {
                        var id = Request.QueryString["id"];
                        id = Criptografia.Decrypt(id.Replace('_', '+'));

                        if (Agenda.Excluir(long.Parse(id)))
                        {
                            if (Session["paremetrosDefault"].ToString() != string.Empty)
                            {
                                var paremetrosDefault = Session["paremetrosDefault"].ToString();
                                Response.Redirect(string.Format("default.aspx?dia={0}", paremetrosDefault));
                            }
                            else
                            {
                                Response.Redirect("default.aspx");
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>Page_Load-ERRO:" + ex.Message + "</p></div>";
            }
        }

    }
}