using CriptografiaSgpm;
using espacobeleza_cl;
using System;
using System.Web.UI;

namespace ariziolouzada.espacodebeleza
{
    public partial class login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            txtLogin.Focus();
        }

        /// <summary>
        ///  OK 1-fazer a possibilidade de dividir a forma de pgto, podendo por exemplo pagar parte em dinheiro
        /// e outra parte em cartão;
        ///     2-Permitir o pagto antecioado de serviço, como pacotes para noivas, promoções, etc..
        ///     3-Criar relatórios: forma de pgto (dinheiro, cartão, etc...) por período
        /// </summary>
        

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            try
            {
                //if(true)
                if (VerificaCampos())
                {
                    if(true)
                    //if (LogarSistema())
                    {
                        var userLogado = UsuarioEspBel.Pesquisar(txtLogin.Value);

                        var idUserLogado = Criptografia.Encrypt(userLogado.Id.ToString());
                        CookieEspBel.Grava("idUserLogado", idUserLogado);
                        CookieEspBel.Grava("idEmpresaContratante", userLogado.IdEmpresaContratante.ToString(), true);
                        CookieEspBel.Grava("idProfissional", userLogado.IdProfissional.ToString(), true);
                        CookieEspBel.Grava("LoginUserLogado", txtLogin.Value.ToUpper());
                        CookieEspBel.Grava("imgUserLogado", userLogado.Img);

                        //Session["usuarioLogadoSbEssencial"] = userLogado;

                        Session["logadoEspacoBeleza"] = "1";
                        //ltlMsn.Text = "<div class=\"alert alert-success alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                        //                "Login realizado com sucesso!</div>";

                        if (userLogado.IdProfissional == 0)
                        {
                            Response.Redirect("~/espacodebeleza/default.aspx");
                        }
                        else
                        {
                            Response.Redirect("~/espacodebeleza/pages/colaborador/default.aspx");
                        }

                    }
                    else
                    {
                        ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                                        "!! Usuário ou senha Inválidos !!</div>";

                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "fecharModal", "fecharModal()", true);

                        txtLogin.Focus();
                    }

                }
                else
                {

                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "fecharModal", "fecharModal()", true);

                    txtLogin.Focus();
                }

            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                "ERRO: " + ex.Message + "</div>";

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "fecharModal", "fecharModal()", true);

            }

        }

        private bool LogarSistema()
        {
            var senhaCriptografada = Criptografia.Encrypt(txtSenha.Value);
            if (!UsuarioEspBel.AutenticacaoUsuario(txtLogin.Value.ToUpper(), senhaCriptografada))
            {
                //UsuarioSbe.InserirErroLogin();
                pnlCaptcha.Visible = true;
                return false;
            }
            return true;
        }

        private bool VerificaCampos()
        {
            if (txtLogin.Value == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                "Campo Login não preenchido!</div>";
                txtLogin.Focus();
                return false;
            }

            if (txtSenha.Value == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                "Campo Senha não preenchido!</div>";
                txtSenha.Focus();
                return false;
            }

            if (pnlCaptcha.Visible)
            {
                if (txtCaptcha.Value == string.Empty)
                {
                    ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                    "Campo dos Caracteres da imagem não preenchido!</div>";
                    txtCaptcha.Focus();
                    return false;
                }

                if (Page.IsValid)
                {
                    var captcha = Session["captchaSbEssencial"].ToString();
                    if (txtCaptcha.Value.ToUpper() != captcha)
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