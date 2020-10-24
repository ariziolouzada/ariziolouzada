using CriptografiaSgpm;
using sbessencial_cl;
using System;

namespace ariziolouzada.sbessencial.minhagenda
{
    public partial class alterarsenha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            var url = string.Format("default.aspx");
            Response.Redirect(url);
        }


        protected void btnAlterarSenha_Click(object sender, EventArgs e)
        {
            if (VerificaCampos())
            {
                if (SalvarNovaSenha())
                {
                    txtSenha.Value = txtSenhaConfirma.Value = string.Empty;
                    
                    ltlMsn.Text = "<div class=\"alert alert-block alert-success fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +                        
                                    "</button><p><i class=\"fa fa-check-square fa-lg\"></i> Senha alterada com sucesso!</p></div>";
                        
                }
                else
                {
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +                        
                                    "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> Erro: Não foi possível alterar a senha!</p></div>";
                        
                }
            }
        }


        private bool SalvarNovaSenha()
        {
            var idUserAgendaLogado = CookieSbe.Recupera("idUserAgendaLogado");
            var senha = Criptografia.Encrypt(txtSenha.Value);

            return Cliente.EditarSenha(int.Parse(idUserAgendaLogado), senha);
        }


        private bool VerificaCampos()
        {
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

            return true;
        }

    }
}