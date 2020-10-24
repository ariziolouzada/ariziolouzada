using ariziolouzada.classes;
using System;
using System.Text;
using System.Web.Services;
using System.Web.UI;

namespace ariziolouzada.acaoentreamigos
{
    public partial class vendedor : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Caso não esteja logado redireciona para a página inicial
                if (Session["logadoAcaoEntreAmigos"].ToString() == "0")
                    Response.Redirect("~/acaoentreamigos/default.aspx");

                if (!IsPostBack)
                {

                    CarregaDdlVendedor(true);

                    hdfIdVenda.Value = DateTime.Now.ToString("yyyyMMddHHmmssfff");

                    AcaoEntreAmigosTemp.ExcluirRegistrosNaoUtilizados();
                }

            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                              "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> Pag_Load-ERRO:" + ex.Message + "</p></div>";
            }
        }

        private void CarregaDdlVendedor(bool comSel)
        {
            var lista = AcaoEntreAmigosVendedor.CarregaDdl(comSel);
            ddlVendedor.Items.Clear();
            ddlVendedor.DataSource = lista;
            ddlVendedor.DataValueField = "IdVendedor";
            ddlVendedor.DataTextField = "NomeVendedor";
            ddlVendedor.DataBind();
        }


        [WebMethod]
        public static string VerificaNumero(string numero, string idVenda)
        {
            try
            {
                var aeat = AcaoEntreAmigosTemp.Pesquisar(int.Parse(numero), long.Parse(idVenda));
                if (aeat != null)
                    return "Número já inserido!!";

                var aea = AcaoEntreAmigos.PesquisaNumero(int.Parse(numero));
                if (aea == null)
                    return "Número Inválido!!";

                if (aea.Numero > 16000)
                    return "Número Inválido. Fora do limite de 16000!!";

                if (aea.IdStatus == 1)
                    return "Número já vendido!!!";


                return "NumOk";
            }
            catch (Exception ex)
            {
                return "Erro: " + ex.Message;
            }
        }

        [WebMethod]
        public static string AdicionaNumero(string numero, string idVenda)
        {
            try
            {
                var aeat = new AcaoEntreAmigosTemp(int.Parse(numero), long.Parse(idVenda));
                if (AcaoEntreAmigosTemp.Inserir(aeat))
                {
                    return "NumAddSuccess";
                }
                return "Erro ao inserir o numero!!";
            }
            catch (Exception ex)
            {
                return "Erro: " + ex.Message;
            }
        }

        [WebMethod]
        public static string ApagarNumero(string numero, string idVenda)
        {
            try
            {
                var aeat = new AcaoEntreAmigosTemp(int.Parse(numero), long.Parse(idVenda));
                if (AcaoEntreAmigosTemp.Excluir(aeat))
                {
                    return "NumDelSuccess";
                }
                return "Erro ao apagar o numero!!";
            }
            catch (Exception ex)
            {
                return "Erro: " + ex.Message;
            }
        }

        [WebMethod]
        public static string CarregaTabelaNumAdd(string idVenda)
        {
            try
            {
                var lista = AcaoEntreAmigosTemp.Lista(long.Parse(idVenda));
                var tabelaHtml = new StringBuilder();
                tabelaHtml.Append("<table class=\"table\">");

                foreach (var item in lista)
                {
                    tabelaHtml.Append("<tr>");
                    tabelaHtml.Append(string.Format("<td style=\"font-weight: bold; vertical-align: middle; text-align: center;\">#</td>"));
                    tabelaHtml.Append(string.Format("<td style=\"font-weight: bold; vertical-align: middle; text-align: center;\"><h2>{0}</h2></td>", item.NumeroStr));
                    var linkDel = string.Format("<a onclick=\"acaoBtnApagarNumero({0},'{1}');\" class=\"btn btn-white btn-bitbucket\" title=\"Excluir número\"><i class=\"fa fa-trash-o\"></i></a>", item.Numero, item.IdVenda);
                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\">{0}</td>", linkDel));
                    tabelaHtml.Append("</tr>");
                }

                tabelaHtml.Append("</table>");
                return tabelaHtml.ToString();
            }
            catch (Exception ex)
            {
                return "Erro: " + ex.Message;
            }
        }

        [WebMethod]
        public static string AcaoBtnCancelar(string idVenda)
        {
            try
            {
                var lista = AcaoEntreAmigosTemp.Lista(long.Parse(idVenda));
                if (lista.Count > 0)
                {
                    if (AcaoEntreAmigosTemp.ExcluirVenda(long.Parse(idVenda)))
                    {
                        return "CancelSuccess";
                    }
                }
                else
                {
                    return "CancelSuccess";
                }

                return "Erro ao cancelar venda!!";

            }
            catch (Exception ex)
            {
                return "Erro: " + ex.Message;
            }
        }

        [WebMethod]
        public static string SalvarVenda(string idVenda, string nome, string idVendedor, string tel, string email)
        {
            try
            {
                var lista = AcaoEntreAmigosTemp.Lista(long.Parse(idVenda));
                if (lista.Count == 0)
                    return "Erro: É necessário adicionar pelo menos 01(um) número para realizar a venda!!";

                var listaNumeros = new StringBuilder();

                var qtdNum = 0;
                foreach (var item in lista)
                {
                    //CRIAR O OBJ AcaoEntreAmigos
                    var aea = new AcaoEntreAmigos()
                    {
                        IdStatus = 1
                        ,
                        IdVendedor = int.Parse(idVendedor)
                        ,
                        NomeComprador = nome
                        ,
                        Telefone = tel
                        ,
                        Email = email
                        ,
                        Numero = item.Numero
                        ,
                        DataVenda = DateTime.Now
                    };

                    if (AcaoEntreAmigos.Editar(aea))
                        qtdNum++;

                    listaNumeros.Append(string.Format("--> <b>{0}</b><br />", item.NumeroStr));
                }

                //mandar email
                var envioEmail = EnviarEmailComprador(email, nome, listaNumeros.ToString());
                if (!envioEmail.Equals("true"))                
                    return envioEmail;
                

                //3-APAGAR A AcaoEntreAmigosTemp
                AcaoEntreAmigosTemp.ExcluirVenda(long.Parse(idVenda));

                if (qtdNum == lista.Count)
                    return "VendaSuccess";


                return "Erro ao cancelar venda!!";

            }
            catch (Exception ex)
            {
                return "Erro: " + ex.Message;
            }
        }

        private static string EnviarEmailComprador(string email, string nome, string listaNum)
        {
            try
            {

                //ltlMsn.Text = "";
                string nomeRemetente = "Ação Entre Amigos";
                string emailRemetente = "contato@ariziolouzada.com.br";
                string emailDestinatario = email;
                string assuntoMensagem = "Números comprados na Ação Entre Amigos";
                string conteudoMensagem = "PARABÉNS! <b>" + nome + "</b><br />Você está participando da AÇÃO ENTRE AMIGOS" +
                                          " e concorre a uma VAN EXECUTIVA através do(s) números:<br /><h2>" +
                                          listaNum +
                                          "</h2><br />Acompanhe todo andamento através do instagram @RECEITADAROCA.";


                var result = Email.EnviarLocaWeb(nomeRemetente, emailRemetente, emailDestinatario, assuntoMensagem, conteudoMensagem);
                return result;

            }
            catch (Exception ex)
            {
                return "Erro: " + ex.Message;
            }

        }


        //private void CarregaDdlNumero()
        //{
        //    var lista = AcaoEntreAmigos.Lista(0);
        //    ddlNumero.Items.Clear();
        //    ddlNumero.DataSource = lista;
        //    ddlNumero.DataValueField = "Numero";
        //    ddlNumero.DataTextField = "NumeroStr";
        //    ddlNumero.DataBind();
        //}

    }


    //public string EnviarEmail(string email, string nome)
    //{
    //    try
    //    {

    //        //ltlMsn.Text = "";
    //        string nomeRemetente = "Ação Entre Amigos";
    //        string emailRemetente = "contato@ariziolouzada.com.br";
    //        string emailDestinatario = email;
    //        string assuntoMensagem = "Numeros comprados Ação enter Amigos";
    //        string conteudoMensagem = "PARABÉNS! <b>FULANO</b> VOCÊ ESTÁ PARTICIPANDO DA AÇÃO ENTRE AMIGOS" +
    //                                  " E CONCORRE A UMA VAN EXECUTIVA ATRAVÉS DOS NÚMEROS XX, XXX ." +
    //                                  " ACOMPANHE TODO ANDAMENTO ATRAVÉS DO INSTAGRAM @RECEITADAROCA";


    //        var result = Email.EnviarLocaWeb(nomeRemetente, emailRemetente, emailDestinatario, assuntoMensagem, conteudoMensagem);
    //        return result;           

    //    }
    //    catch (Exception ex)
    //    {
    //        return "Erro: " + ex.Message;
    //    }
    //}


}