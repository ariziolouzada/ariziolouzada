using CriptografiaSgpm;
using sbessencial_cl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrataStringSystem;

namespace ariziolouzada.sbessencial.minhagenda
{
    public partial class registro : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //implementar


        private bool VerificaCampos()
        {
            if (txtNome.Value == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                "Campo Nome não preenchido!</div>";
                txtNome.Focus();
                return false;
            }

            if (txtLogin.Value == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                "Campo Login não preenchido!</div>";
                txtLogin.Focus();
                return false;
            }

            var clt = Cliente.PesquisarTelCel(txtLogin.Value);
            if (clt != null)
            {
                if (clt.Email == string.Empty)
                {
                    ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                                   string.Format("Celular já está cadastrada para {0}, mas seu email não está, entre em contato com o Salão para que seja atualizado seu dados!</div>", clt.Nome);

                }
                else
                {
                    if (clt.Email == txtEmail.Value.ToLower())
                    {
                        ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                                       string.Format("Celualr já está cadastrada para {0}, caso não lembre de sua senha, clique para enviar para seu email cadastrado uma senha temporária!</div>", clt.Nome);

                    }
                    else
                    {
                        ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                                      string.Format("Celular já está cadastrada para {0}, mas o email digitado é diferente do email cadastrado: {1}!</div>", clt.Nome, clt.Email);
                    }

                }

                txtLogin.Focus();
                return false;
            }


            if (txtEmail.Value == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                "Campo Email não preenchido!</div>";
                txtEmail.Focus();
                return false;
            }

            if (!Validacao.IsEmailValido(txtEmail.Value))
            {
                ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                                "Email Inválido!</div>";
                txtEmail.Focus();
                return false;
            }

            clt = Cliente.PesquisarPeloEmail(txtEmail.Value.ToLower());
            if (clt != null)
            {
                ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                               string.Format("Email já cadastrada para a(o) cliente {0}.</div>", clt.Nome);
                txtEmail.Focus();
                return false;
            }

            if (txtSenha.Value == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                "Campo Senha não preenchido!</div>";
                txtSenha.Focus();
                return false;
            }

            if (txtSenhaConfirma.Value == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                "Confirme a Senha!</div>";
                txtSenhaConfirma.Focus();
                return false;
            }

            if (txtSenha.Value != txtSenhaConfirma.Value)
            {
                ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                                "As Senha não coferem!</div>";
                txtSenha.Focus();
                return false;
            }

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

            return true;
        }


        protected void btnRegistrar_Click(object sender, EventArgs e)
        {

            try
            {

                if (VerificaCampos())
                {
                    if (SalvarDados())
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

        private bool SalvarDados()
        {
            var cliente = new Cliente()
            {
                Nome = txtNome.Value.ToUpper(),
                DataNascto = DateTime.MinValue,
                Email = txtEmail.Value.ToLower(),
                Facebook = "",
                Instagram = "",
                TelCelular1 = txtLogin.Value,
                TelCelular2 = "",
                TelFixo = "",
                LoginCadastro = "Pela Web",
                Observacao = "",
                Senha = Criptografia.Encrypt(txtSenha.Value)
            };

            var idUserAgendaLogado = Cliente.InserirRetornandoId(cliente);
            if (idUserAgendaLogado > 0)
            {
                CookieSbe.Grava("idUserAgendaLogado", idUserAgendaLogado.ToString());
            }
            else
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Não foi possível cadastrar CLIENTE!!!</p></div>";
                return false;
            }
            return true;
        }
    }
}