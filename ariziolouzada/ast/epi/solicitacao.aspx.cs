using System;
using System.Web.UI;
using ariziolouzada.classes;
using CriptografiaSgpm;

namespace ariziolouzada.ast.epi
{
    public partial class solicitacao : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString.HasKeys())
            {
                var hashSoliciracao = string.Empty;
                var resultSoliciracao = string.Empty;
                if (Request.QueryString["hash"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["hash"]))
                {
                    hashSoliciracao = Request.QueryString["hash"];
                    hashSoliciracao = hashSoliciracao.Replace('_', '+');
                    //hashSoliciracao = Criptografia.Decrypt(hash);
                }

                if (Request.QueryString["result"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["result"]))
                {
                    var result = Request.QueryString["result"];
                    resultSoliciracao = Criptografia.Decrypt(result);
                }

                if (hashSoliciracao != string.Empty && resultSoliciracao != string.Empty)
                    SalvarResultadoSolicitacao(hashSoliciracao, resultSoliciracao);

            }

            MostraEnderecoIp();

        }

        private void MostraEnderecoIp()
        {
            string strEnderecoIP;
            strEnderecoIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (strEnderecoIP == null)
                strEnderecoIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            var ipLocal = System.Web.HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"];
            var hostName = System.Web.HttpContext.Current.Request.UserHostName;

            lblEnderecoIp.Text = string.Format("{0} | {1} | {2}", hostName, strEnderecoIP, ipLocal);
        }

        private void SalvarResultadoSolicitacao(string hash, string result)
        {
            try
            {
                if (SolicitacaoEpi.EditarResultado(hash, result))
                {
                    if (EnviarEmail(hash, result))
                    {
                        result = result.Equals("SIM") ? "deferida" : "indeferida";
                        ltlMsn.Text = "<div class=\"alert alert-success alert-dismissable\">" +
                              "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                              string.Format("Solicitação foi {0}!!</div>", result);
                    }
                    else
                    {
                        ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                              "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                              "Erro ao enviar a resposta da solicitação de EPI!!</div>";
                    }
                }
                else
                {
                    ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                          "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                          "Erro ao salva a solicitação de EPI!!</div>";
                }
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                              "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                              "SalvarResultadoSolicitacao-ERRO: " + ex.Message + "</div>";
            }
        }

        private bool EnviarEmail(string hash, string result)
        {
            hash = Criptografia.Decrypt(hash);
            var itensHash = hash.Split(';');

            var usuario = Usuario.Pesquisar(int.Parse(itensHash[2]));

            //Define os dados do e-mail
            const string nomeRemetente = "AST - PETROBRAS";
            const string emailRemetente = "astpetrobras@ariziolouzada.com.br";
            //string senha = "Ast2016**";

            string emailDestinatario = usuario.Email;

            const string assuntoMensagem = "Solicitação de EPI - Resultado";

            //relacionar os EPI solicitados
            string texto = result.Equals("SIM") ? "DEFERIDA" : "INDEFERIDA";

            var conteudoMensagem = string.Format("Sr(a): {0}<br />Email: {1}<br />A sua solicitação de EPI's foi <b>{2}</b>.<br />Favor entrar em contato com o responsável.", usuario.Nome.ToUpper(), usuario.Email, texto);

            return Email.Enviar(
                                         nomeRemetente
                                         , emailRemetente
                                         , emailDestinatario
                                         , assuntoMensagem
                                         , conteudoMensagem
                                     );


        }


    }
}