using ariziolouzada.classes;
using CriptografiaSgpm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ariziolouzada.ast.sistema
{
    public partial class alterarsenha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {

                var senha1 = Request.Form["txtSenha1"];
                var senha2 = Request.Form["txtSenha2"];

                if (senha1.Equals(senha2))
                {

                    var idUserLogado = Cookie.Recupera("idUserLogado");
                    idUserLogado = Criptografia.Decrypt(idUserLogado);

                    var senhaCriptografada = Criptografia.Encrypt(senha1);
                    if (Usuario.EditarSenha(long.Parse(idUserLogado), senhaCriptografada))
                    {
                        //Response.Redirect("~/default.aspx");

                        ltlMsn.Text = "<div class=\"alert alert-success alert-dismissable\">" +
                                      "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                                      "Senha alterada com sucesso!!</div>";
                    }
                    else
                    {
                        ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                                      "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                                      "ERRO ao tentar alterar a senha!!</div>";
                    }
                }
                else
                {
                    ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                                  "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                                  "ERRO: Senhas digitadas não são iguais!!</div>";
                }
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                              "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                              "ERRO: " + ex.Message + "</div>";
            }
        }
    }
}