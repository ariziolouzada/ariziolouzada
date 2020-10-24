using CriptografiaSgpm;
using sbessencial_cl;
using System;
using System.Web.UI;

namespace ariziolouzada.sbessencial.sistema
{
    public partial class alterarsenha : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtSenhaAtual.Focus();
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../default.aspx");
        }


        private bool VerificaCamposBtnConfirmarResteSenha()
        {

            if (txtSenhaAtual.Value == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-danger fade in m-b-15\"><strong>Error!</strong>Campo Senha Atual de prenchimento obrigatório!<span class=\"close\" data-dismiss=\"alert\">X</span></div>";
                txtSenhaAtual.Focus();
                return false;
            }

            if (txtNovaSenha1.Value == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-danger fade in m-b-15\"><strong>Error!</strong>Campo Nova Senha de prenchimento obrigatório!<span class=\"close\" data-dismiss=\"alert\">X</span></div>";
                txtNovaSenha1.Focus();
                return false;
            }

            if (txtNovaSenha2.Value == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-danger fade in m-b-15\"><strong>Error!</strong>Campo Confirma Nova Senha de prenchimento obrigatório!<span class=\"close\" data-dismiss=\"alert\">X</span></div>";
                txtNovaSenha2.Focus();
                return false;
            }

            if (txtNovaSenha1.Value != txtNovaSenha2.Value)
            {
                ltlMsn.Text = "<div class=\"alert alert-danger fade in m-b-15\"><strong>Error!</strong>Os campos da senha nova não são iguais!<span class=\"close\" data-dismiss=\"alert\">X</span></div>";
                txtNovaSenha2.Focus();
                return false;
            }


            //var fs = new ChecaForcaSenha();
            //var forcaSenha = fs.GetForcaDaSenha(txtSenhaNova2.Text);
            //if (forcaSenha.ToString().Equals("Inaceitavel") || forcaSenha.ToString().Equals("Fraca"))
            //{
            //    ltlMsnModalResetSenha2.Text = "<div class=\"alert alert-danger fade in m-b-15\"><strong>Error!</strong>A nova senha não atende aos critérios de complexidade!<br />" +
            //                                  "Ela deve possuir no mínimo 8 caracteres, sendo pelo menos uma letra maiúscula, um número e um caracter especial.<span class=\"close\" data-dismiss=\"alert\">X</span></div>";
            //    txtSenhaNova2.Focus();
            //    return false;
            //}

            return true;
        }


        private bool AlterarSenha()
        {
            try
            {
                var idUser = CookieSbe.Recupera("idUserLogado");
                idUser = Criptografia.Decrypt(idUser);
                var login = CookieSbe.Recupera("LoginUserLogado");
                var senhaCriptografada = Criptografia.Encrypt(txtSenhaAtual.Value);
                if (UsuarioSbe.AutenticacaoUsuario(login.ToUpper(), senhaCriptografada))
                {
                    senhaCriptografada = Criptografia.Encrypt(txtNovaSenha1.Value);
                    if (!UsuarioSbe.EditarSenha(int.Parse(idUser), senhaCriptografada))
                    {
                        ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                                         "!! Erro ao alterar a senha !!</div>";
                        return false;
                    }
                    return true;
                }
                else
                {
                    ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                                  "!! Senha atual inválida !!</div>";
                    return false;
                }
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>AlterarSenha-ERRO:" + ex.Message + "</p></div>";
                return false;
            }
        }


        private void LimparCampos()
        {
            txtSenhaAtual.Value = string.Empty;
            txtNovaSenha1.Value = string.Empty;
            txtNovaSenha2.Value = string.Empty;
        }


        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (VerificaCamposBtnConfirmarResteSenha())
            {
                if (AlterarSenha())
                {
                    LimparCampos();
                    ltlMsn.Text = "<div class=\"alert alert-block alert-success fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-check-square-o fa-lg\"></i> Senha alterada com sucesso!!!</p></div>";
                }
            }
        }


    }
}