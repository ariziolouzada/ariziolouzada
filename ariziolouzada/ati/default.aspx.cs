using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ariziolouzada.classes;

namespace ariziolouzada.ati
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CarregaSaudacao();
        }


        /// <summary>
        /// Metodo para exibir uma mensagem utilizando alert do JavaScript.
        /// </summary>
        /// <param name="msg">Mensagem a ser exibida.</param>
        public void MessageBoxScript(string msg)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mensagem do Sistema", "alert('" + msg + "');", true);
        }


        private void CarregaSaudacao()
        {
            var saudacao = "";

            if ((DateTime.Now.Hour > 0) && (DateTime.Now.Hour <= 12))
                saudacao = "Bom dia !";
            else if ((DateTime.Now.Hour > 12) && (DateTime.Now.Hour <= 6))
                saudacao = "Boa tarde !";
            else
                saudacao = "Boa noite !";

            lblSaldacao.Text = saudacao;
        }


        private bool EnviarEmail()
        {

            //Define os dados do e-mail
            const string nomeRemetente = "ATIST - Assessoria de Tecnologia da Informação na Segurança do Trabalho";
            const string emailRemetente = "contato@ariziolouzada.com.br";
            //string senha = "Ast2016**";

            const string emailDestinatario = "bebetoliveira@hotmail.com";
            const string emailDestinatarioCopia = "ariziolouzada@hotmail.com";

            var assuntoMensagem = txtAssunto.Value;
            var conteudoMensagem = txtMensagem.Value;

            return Email.EnviarComCopia(
                                         nomeRemetente
                                         , emailRemetente
                                         , emailDestinatario
                                         , emailDestinatarioCopia
                                         , assuntoMensagem
                                         , conteudoMensagem
                                     );


        }

        protected void btnEnviarMensagem_Click(object sender, EventArgs e)
        {
            try
            {

                if (VeriricaCamposBrnEnviarMsn())
                {
                    if (EnviarEmail())
                    {
                        MessageBoxScript("Email enviado com sucesso!");
                        LimparCampos();
                    }
                    else
                    {
                        ltlMsn.Text = "<div class='alert alert-danger alert-dismissable'>" +
                        "<button aria-hidden='true' data-dismiss='alert' class='close' type='button'>X</button>" +
                        "Erro ao salva enviar email!!<a class='alert-link' href='#'>Alert Link</a>.</div>";

                    }
                }

            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                              "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                              "btnEnviarMensagem_Click-ERRO: " + ex.Message + "</div>";
            }

        }

        private void LimparCampos()
        {
            txtNome.Value = string.Empty;
            txtMensagem.Value = string.Empty;
            txtEmail.Value = string.Empty;
            txtAssunto.Value = string.Empty;
            ltlMsn.Text = string.Empty;

        }

        private bool VeriricaCamposBrnEnviarMsn()
        {
            if (txtNome.Value == string.Empty)
            {
                MessageBoxScript("Campo NOME é de preenchimento obrigatório!");
                //txtNome.Focus();
                return false;
            }

            if (txtEmail.Value == string.Empty)
            {
                MessageBoxScript("Campo EMAIL é de preenchimento obrigatório!");
                //txtNome.Focus();
                return false;
            }

            if (Email.IsEmailValido(txtEmail.Value) == false)
            {
                MessageBoxScript("EMAIL inválido!");
                //txtNome.Focus();
                return false;
            }

            if (txtAssunto.Value == string.Empty)
            {
                MessageBoxScript("Campo ASSUNTO é de preenchimento obrigatório!");
                //txtNome.Focus();
                return false;
            }

            if (txtMensagem.Value == string.Empty)
            {
                MessageBoxScript("Campo MENSAGEM é de preenchimento obrigatório!");
                //txtNome.Focus();
                return false;
            }

            return true;
        }
    }
}