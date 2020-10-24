using ariziolouzada.classes;
using CriptografiaSgpm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace ariziolouzada.ast.epi
{
    public partial class solicitar : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (VerificaSelecaoItem())
                {

                    var idUserLogado = Cookie.Recupera("idUserLogado");
                    idUserLogado = Criptografia.Decrypt(idUserLogado);

                    var userLogado = Usuario.Pesquisar(int.Parse(idUserLogado));
                    if (userLogado != null)
                    {
                        if (SalvarSolicitacaoEpi(userLogado.Id))
                        {

                            if (EnviarEmailSolicitacaoEpt(userLogado))
                            {

                                ltlMsn.Text = "<div class=\"alert alert-success alert-dismissable\">" +
                                              "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                                              "Solicitação realizada com sucesso!!<br />Em breve você receberá um email com a resposta de sua solicitação.</div>";
                            }
                            else
                            {
                                ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                                              "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                                              "ERRO ao tentar enviar o email da solicitação de EPI!!</div>";
                                //Exclui se não for enviado o email 
                                SolicitacaoEpi.Excluir(hdlHashSolicitacao.Value);
                            }
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                              "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                              "btnConfirmar_Click-ERRO: " + ex.Message + "</div>";
            }

        }

        private bool SalvarSolicitacaoEpi(int idUser)
        {

            var listaIdEpi = new List<string>();
            if (ckCapacete.Checked)
                listaIdEpi.Add("1;Capacete");

            if (ckCalcado.Checked)
                listaIdEpi.Add("4;Calçado de Proteção");

            if (ckOculos.Checked)
                listaIdEpi.Add("2;Óculos de Proteção");

            if (ckProtetor.Checked)
                listaIdEpi.Add("3;Protetor Auricular");

            //O hash é uma composição data/hora no momento da criação, do número de itens selecionados
            //e do ID do usuário logado no sistema.
            var hash = string.Format("{0};{1};{2}", DateTime.Now, listaIdEpi.Count, idUser);
            var hashSoliciracao = Criptografia.Encrypt(hash);
            hashSoliciracao = hashSoliciracao.Replace('+','_');
            hdlHashSolicitacao.Value = hashSoliciracao;

            var solicitacao = new SolicitacaoEpi()
            {
                IdUsuario = idUser
                ,
                Hash = hashSoliciracao
            };

            foreach (var item in listaIdEpi)
            {
                var itens = item.Split(';');
                solicitacao.IdEpi = int.Parse(itens[0]);
                if (!SolicitacaoEpi.Inserir(solicitacao))
                {
                    ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                                              "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                                              "ERRO ao tentar salvar solicitação de EPI " + itens[1] + "!!</div>";
                    return false;
                }
            }

            return listaIdEpi.Count > 0;
        }

        private bool VerificaSelecaoItem()
        {
            ltlMsn.Text = string.Empty;
            if (!ckCalcado.Checked && !ckCapacete.Checked && !ckOculos.Checked && !ckProtetor.Checked)
            {
                ltlMsn.Text = "<div class=\"alert alert-warning alert-dismissable\">" +
                              "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                              "ATENÇÃO: Selecione ao menos um item para confirmar a solicitação!!</div>";
                return false;
            }
            return true;
        }

        private string EpiSelecionados()
        {
            var lista = new StringBuilder("<br /><ul>");

            if (ckCapacete.Checked)
                lista.Append("<li><h4>Capacete;</h4></li>");

            if (ckCalcado.Checked)
                lista.Append("<li><h4>Calçado;</h4></li>");

            if (ckOculos.Checked)
                lista.Append("<li><h4>Oculos de Segurança;</h4></li>");

            if (ckProtetor.Checked)
                lista.Append("<li><h4>Protetor Auricular;</h4></li>");

            lista.Append("</ul>");
            return lista.ToString();
        }

        private bool EnviarEmailSolicitacaoEpt(Usuario usuario)
        {

            //Define os dados do e-mail
            const string nomeRemetente = "AST - PETROBRAS";
            const string emailRemetente = "astpetrobras@ariziolouzada.com.br";
            //string senha = "Ast2016**";

            //Host da porta SMTP
            //string SMTP = "smtp.hotshower.com.br";
            //string SMTP = "email-ssl.com.br";

            string emailDestinatario = usuario.Email;
            //string emailComCopia        = "email@comcopia.com.br";
            //string emailComCopiaOculta  = "email@comcopiaoculta.com.br";

            const string assuntoMensagem = "Solicitação de EPI";

            //relacionar os EPI solicitados
            string texto = EpiSelecionados();

            var msn = new StringBuilder();
            msn.Append(string.Format("Solicitante: {0}<br />Email: {1}<br />Solicitação dos seguintes EPI's:{2}", usuario.Nome.ToUpper(), usuario.Email, texto));
            string conteudoMensagem = msn.ToString();

            //Envio do email de resposta ao solicitante
            var enviouEmailSolicitacao = Email.Enviar(
                                          nomeRemetente
                                        , emailRemetente
                                        , emailDestinatario
                                        , assuntoMensagem
                                        , conteudoMensagem
                                    );

            //Envio do email ao rseponsável pelo atendimento 
            //emailDestinatario = "bebetoliveira@hotmail.com";
            emailDestinatario = "ariziolouzada@gmail.com";

            var sim = Criptografia.Encrypt("SIM");
            var nao = Criptografia.Encrypt("NAO");

            var linkSim = string.Format("<a href=\"http://ariziolouzada.com.br/ast/epi/solicitacao.aspx?hash={0}&result={1}\">SIM</a>", hdlHashSolicitacao.Value, sim);
            var linkNao = string.Format("<a href=\"http://ariziolouzada.com.br/ast/epi/solicitacao.aspx?hash={0}&result={1}\">NÃO</a>", hdlHashSolicitacao.Value, nao);
            
            texto = string.Format("<br />Você defere a solicitação feita acima: {0} OU {1}", linkSim, linkNao);

            msn.Append(texto);
            conteudoMensagem = msn.ToString();
            var enviouEmailREsp = Email.Enviar(
                                                      nomeRemetente
                                                    , emailRemetente
                                                    , emailDestinatario
                                                    , assuntoMensagem
                                                    , conteudoMensagem
                                                );

            return enviouEmailSolicitacao & enviouEmailREsp;
        }


    }
}