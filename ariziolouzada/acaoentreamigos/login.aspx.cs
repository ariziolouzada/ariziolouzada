using ariziolouzada.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ariziolouzada.acaoentreamigos
{
    public partial class login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["logadoAcaoEntreAmigos"] = "0";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (VerificaCamposLogin())
                {
                    //if (true)
                    if (Autenticacao())
                    {
                        Session["logadoAcaoEntreAmigos"] = "1";

                        if (ddlTipoUsuario.SelectedValue.Equals("1"))
                        {
                            Response.Redirect("vendedor.aspx");
                        }
                        else
                        {
                            Response.Redirect("admin.aspx");
                        }
                    }
                    else
                    {
                        ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                       "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ERRO: Usuário ou Senha invalido!!!</p></div>";
                    }
                }
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                              "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> Pag_Load-ERRO:" + ex.Message + "</p></div>";
            }

        }

        private bool Autenticacao()
        {
            return AcaoEntreAmigos.AutenticarUser(int.Parse(ddlTipoUsuario.SelectedValue), txtSenha.Value);
        }

        private bool VerificaCamposLogin()
        {
            if (ddlTipoUsuario.SelectedIndex == 0)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">" +
                              "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ERRO: Selecione o tipo de usuário!!!</p></div>";
                return false;
            }

            if (txtSenha.Value == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">" +
                              "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ERRO: digite a senha do tipo de usuário selecionado!!!</p></div>";
                return false;
            }
            return true;
        }

        //protected void btnEnviarEmail_Click(object sender, EventArgs e)
        //{

        //    EnviarEmail();
        //}


        //private void EnviarEmail()
        //{
        //    try
        //    {
        //        string nomeRemetente = "Ação Entre Amigos";
        //        string emailRemetente = "contato@ariziolouzada.com.br";
        //        string emailDestinatario = "ariziolouzada@gmail.com.br";
        //        string assuntoMensagem = "Numeros comprados";
        //        string conteudoMensagem = "PARABÉNS! FULANO VOCÊ ESTÁ PARTICIPANDO DA AÇÃO ENTRE AMIGOS" +
        //                                  " E CONCORRE A UMA VAN EXECUTIVA ATRAVÉS DOS NÚMEROS XX, XXX ." +
        //                                  " ACOMPANHE TODO ANDAMENTO ATRAVÉS DO INSTAGRAM @RECEITADAROCA";

        //        if (Email.EnviarLocaWeb(nomeRemetente, emailRemetente, emailDestinatario, assuntoMensagem, conteudoMensagem))
        //        {
        //            ltlMsn.Text = "<h1  style=\"color: green; font-weight: bold;\">Email enviado com sucesso!!</h1>";
        //        }
        //        else
        //        {
        //            ltlMsn.Text = "<h1  style=\"color: red; font-weight: bold;\">Erro no envio do Email!!</h1>";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
        //                      "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> EnviarEmail-ERRO:" + ex.Message + "</p></div>";
        //    }
        //}

    }
}