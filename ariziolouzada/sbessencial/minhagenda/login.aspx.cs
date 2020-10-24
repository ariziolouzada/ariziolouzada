using CriptografiaSgpm;
using sbessencial_cl;
using System;
using System.Web.UI;

namespace ariziolouzada.sbessencial.minhagenda
{
    public partial class login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Session["logadoAgendaSbEssencial"] = "0";
            CookieSbe.Apaga("idUserAgendaLogado");

            txtLogin.Focus();


        }


        private bool LogarSistema()
        {
            try
            {
                var clt = Cliente.PesquisarTelCel(txtLogin.Value);
                if (clt != null)
                {
                    if (clt.IdStatus != 1)
                    {
                        ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\"" +
                                " class=\"close\" type=\"button\">X</button> Cliente cadastrado, mas inativo! Favor Entrar em contato com o Essencial Salão para ativar seu cadastro!</div>";

                        return false;
                    }

                    var senhaCriptografada = Criptografia.Encrypt(txtSenha.Value);

                    if (clt.Senha.Equals(senhaCriptografada))
                    {
                        CookieSbe.Grava("idUserAgendaLogado", clt.Id.ToString());
                        //CookieSbe.Grava("loginUserAgendaLogado", txtLogin.Value.ToUpper());

                        return true;
                    }
                    else
                    {
                        if (clt.Senha.Equals("0"))
                        {
                            //pq o cliente está cadsatrado mas não possie senha cadastrada
                            ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\"" +
                                " class=\"close\" type=\"button\">X</button> Você está cadastrada mas ainda não foi criado uma senha para você!" +
                                "<br />Entre em contato com o Tel. (99629-5894) do Salão e solicite a liberação.</div>";

                        }
                        else
                        {
                            ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\"" +
                                " class=\"close\" type=\"button\">X</button> Você está cadastrada! Clique no link abaixo para se cadastrar!</div>";

                        }
                        return false;
                    }

                }
                else
                {
                    //Cliente não cadastrado. pelo menos o celular

                    ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\"" +
                        " class=\"close\" type=\"button\">X</button> Voçê não está cadastrada! Clique no link abaixo para se cadastrar!</div>";

                    return false;
                }

            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> FecharAgenda-ERRO:" + ex.Message + "</p></div>";
                return false;
            }
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

            //if (pnlCaptcha.Visible)
            //{
            //    if (txtCaptcha.Text == string.Empty)
            //    {
            //        ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
            //        "Campo dos Caracteres da imagem não preenchido!</div>";
            //        txtCaptcha.Focus();
            //        return false;
            //    }

            //    if (Page.IsValid)
            //    {
            //        var captcha = Session["captchaSbEssencial"].ToString();
            //        if (txtCaptcha.Text.ToUpper() != captcha)
            //        {
            //            ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
            //                            "Campo Caracteres da imagem não conferem!</div>";
            //            txtCaptcha.Focus();
            //            return false;
            //        }
            //    }
            //}

            return true;
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
                        //var userLogado = UsuarioSbe.Pesquisar(txtLogin.Value);

                        //var idUserLogado = Criptografia.Encrypt(userLogado.Id.ToString());
                        //CookieSbe.Grava("idUserLogado", idUserLogado);
                        //CookieSbe.Grava("LoginUserLogado", txtLogin.Value.ToUpper());
                        //CookieSbe.Grava("imgUserLogado", userLogado.Img);

                        //Session["usuarioLogadoSbEssencial"] = userLogado;

                        Session["logadoAgendaSbEssencial"] = "1";

                        Response.Redirect("~/sbessencial/minhagenda/default.aspx");
                    }
                    else
                    {
                        ltlMsn.Text = ltlMsn.Text + "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" " +
                            "data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>!! Usuário ou senha Inválidos !!</div>";
                        txtLogin.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\"" +
                              " type=\"button\">X</button>ERRO: " + ex.Message + "</div>";
            }

        }

    }
}