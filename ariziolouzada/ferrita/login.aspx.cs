using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ariziolouzada.ferrita
{
    public partial class login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Session["logadoFerritaSystem"] = "0";

                txtEmail.Focus();

            }
            catch (Exception ex)
            {
                ltlMsn.Text = string.Format("<div class=\"alert alert-danger alert-dismissible\">" +
                                            "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">X</button>" +
                                            "<h4><i class=\"icon fa fa-ban\"></i> Page_Load-Erro:</h4>{0}</div>", ex.Message);
            }
        }


        private bool VerificaCampos()
        {

            try
            {

                if (txtEmail.Value == string.Empty)
                {
                    ltlMsn.Text = "<div class=\"alert alert-danger fade in m-b-15\"><strong>Erro!</strong> Campo Email em branco!<span class=\"close\" data-dismiss=\"alert\">X</span></div>";
                    txtEmail.Focus();
                    return false;
                }

                if (txtSenha.Value == string.Empty)
                {
                    ltlMsn.Text = "<div class=\"alert alert-danger fade in m-b-15\"><strong>Erro!</strong> Campo Senha em branco!<span class=\"close\" data-dismiss=\"alert\">X</span></div>";
                    txtSenha.Focus();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> VerificaCampos-ERRO:" + ex.Message + "</p></div>";

                //ltlMsnErroLogin.Text = Teste.ToString();

                return false;
            }
        }


        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            if (VerificaCampos())
            {
                if (Logar())
                {
                    Session["logadoFerritaSystem"] = "1";
                    Response.Redirect("default.aspx");
                }
            }
        }


        private bool Logar()
        {
            var email = txtEmail.Value.ToLower();
            if (!email.Equals("aeferri32@gmail.com"))
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ERRO: Email ou senha incorreto!</p></div>";
                txtEmail.Focus();
                return false;
            }

            if (!txtSenha.Value.Equals("sgtferri2018"))
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ERRO: Email ou senha incorreto!</p></div>";
                txtEmail.Focus();
                return false;
            }

            return true;
        }

    }
}