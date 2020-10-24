using System;
using System.Web;
using System.Web.UI;
using CriptografiaSgpm;
using sbessencial_cl;

namespace ariziolouzada.sbessencial
{
    public partial class login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            txtLogin.Focus();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            try
            {
                //if(true)
                if (VerificaCampos())
                {
                    //if(true)
                    if (LogarSistema())
                    {
                        var userLogado = UsuarioSbe.Pesquisar(txtLogin.Text);

                        var idUserLogado = Criptografia.Encrypt(userLogado.Id.ToString());
                        CookieSbe.Grava("idUserLogado", idUserLogado);
                        CookieSbe.Grava("LoginUserLogado", txtLogin.Text.ToUpper());
                        CookieSbe.Grava("imgUserLogado", userLogado.Img);

                        //Session["usuarioLogadoSbEssencial"] = userLogado;

                        Session["logadoSbEssencial"] = "1";
                        //ltlMsn.Text = "<div class=\"alert alert-success alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                        //                "Login realizado com sucesso!</div>";
                        Response.Redirect("~/sbessencial/default.aspx");
                    }
                    else
                    {
                        ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                                        "!! Usuário ou senha Inválidos !!</div>";
                        txtLogin.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                "ERRO: " + ex.Message + "</div>";
            }

        }

        private bool LogarSistema()
        {
            var senhaCriptografada = Criptografia.Encrypt(txtSenha.Text);
            if( !UsuarioSbe.AutenticacaoUsuario(txtLogin.Text.ToUpper(), senhaCriptografada) )
            {
                //UsuarioSbe.InserirErroLogin();
                pnlCaptcha.Visible = true;                
                return false;
            }
            return true;
        }

        private bool VerificaCampos()
        {
            if (txtLogin.Text == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                "Campo Login não preenchido!</div>";
                txtLogin.Focus();
                return false;
            }

            if (txtSenha.Text == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                "Campo Senha não preenchido!</div>";
                txtSenha.Focus();
                return false;
            }

            if (pnlCaptcha.Visible)
            {
                if (txtCaptcha.Text == string.Empty)
                {
                    ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                    "Campo dos Caracteres da imagem não preenchido!</div>";
                    txtCaptcha.Focus();
                    return false;
                }

                if (Page.IsValid)
                {
                    var captcha = Session["captchaSbEssencial"].ToString();
                    if (txtCaptcha.Text.ToUpper() != captcha)
                    {
                        ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                                        "Campo Caracteres da imagem não conferem!</div>";
                        txtCaptcha.Focus();
                        return false;
                    }
                }
            }

            return true;
        }

    }
}