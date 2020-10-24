using ariziolouzada.classes;
using CriptografiaSgpm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ariziolouzada.acaoentreamigos
{
    public partial class admin : Page
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
                    var valueBegin = "";
                    var valueEnd = "";

                    if (Request.QueryString.HasKeys())
                    {
                        if (Request.QueryString["valueBegin"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["valueBegin"]))
                        {
                            valueBegin = Request.QueryString["valueBegin"];
                        }
                        if (Request.QueryString["valueEnd"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["valueEnd"]))
                        {
                            valueEnd = Request.QueryString["valueEnd"];
                        }
                        txtNumPesqInicio.Value = valueBegin;
                        txtNumPesqFim.Value = valueEnd;
                        btnPesquisar_Click(sender, e);
                    }

                    CarregaTabelaVendedores();

                    CarregaDadosTabRelatorio();
                }

            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                              "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> Pag_Load-ERRO:" + ex.Message + "</p></div>";
            }
        }

        private void CarregaDadosTabRelatorio()
        {
            try
            {
                var qtdeNumDispo = AcaoEntreAmigos.QtdeNumeros(0);
                var qtdeNumVendidos = AcaoEntreAmigos.QtdeNumeros(1);
                lblNumDisponiveis.Text = qtdeNumDispo.ToString();
                lblNumVendidos.Text = qtdeNumVendidos.ToString();

            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                              "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> CarregaDadosTabRelatorio-ERRO:" + ex.Message + "</p></div>";
            }
        }

        private void CarregaTabelaNumeros(int acao)
        {
            var listaNumero = new List<AcaoEntreAmigos>();

            if (acao == 1)
                listaNumero = AcaoEntreAmigos.Lista(int.Parse(ddlSearch.SelectedValue));

            if (acao == 2)
                listaNumero = AcaoEntreAmigos.ListaFilter(int.Parse(txtNumPesqInicio.Value), int.Parse(txtNumPesqFim.Value));

            ltlTabelaNumeros.Text = MontaHtmlTabelaNumeros(listaNumero);
        }

        private string MontaHtmlTabelaNumeros(List<AcaoEntreAmigos> listaNumero)
        {
            var tabelaHtml = new StringBuilder();
            tabelaHtml.Append("<table id=\"tabelaNumeros\" class=\"table table-striped table-bordered \">");
            tabelaHtml.Append("<thea><tr>");

            tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"><h3>Número</h3></th>");
            tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"><h3>Situação</h3></th>");
            tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"><h3>Vendedor</h3></th>");
            tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"><h3>Nome Comprador</h3></th>");
            tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"><h3>Telefone(s)</h3></th>");
            tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"><h3>Email</h3></th>");
            tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"><h3>Data Venda</h3></th>");
            tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"></th>");
            tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"></th>");

            tabelaHtml.Append("</tr></thead><tbody>");


            foreach (var num in listaNumero)
            {
                tabelaHtml.Append("<tr>");
                var numero = string.Format("{0:00000}", num.Numero);
                //Numero                    
                //tabelaHtml.Append(string.Format("<td style=\"color: {1}; font-weight: bold; vertical-align: middle; text-align: center;\" >{0}</td>", numero, num.IdStatus == 0 ? "green" : "red"));
                tabelaHtml.Append(string.Format("<td style=\"font-weight: bold; vertical-align: middle; text-align: center;\" >{0}</td>", numero));
                //Situação
                var sit = string.Format("<small class=\"label label-{1}\">{0}</small>"
                                        , num.IdStatus == 0 ? "DISPONÍVEL" : "VENDIDO"
                                        , num.IdStatus == 0 ? "primary" : "danger"
                                       );
                tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", sit));
                //Vendedor
                tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", num.NomeVendedor));
                //Nome Comprador
                tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", num.NomeComprador));
                //Tel. comprador
                tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", num.Telefone));
                //Email
                tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", num.Email));
                //Data Venda
                tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>",
                                  num.DataVenda.ToString().Equals("01/01/1900 00:00:00") ? string.Empty : num.DataVenda.ToString("dd/MM/yyyy 'às' HH:mm"))
                                 );

                var idCrypto = Criptografia.Encrypt(num.Numero.ToString());
                idCrypto = idCrypto.Replace('/', '_').Replace('+', '.').Replace('=', '-');

                var htmlBtnEditar = string.Format("<a href=\"javascript:;\" onclick=\"carregaNumeroEdit('{0}')\" " + //class=\"btn btn-outline btn-success btn-sm\"
                    " title=\"Editar\" data-toggle=\"modal\" data-target=\"#myModalEditNumero\"><i class=\"fa fa-pencil fa-lg\"></i></a>", idCrypto);

                tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", htmlBtnEditar));

                //Reenviar email
                var htmlBtnReenviarEmail = num.IdStatus == 1 ? string.Format("<a href=\"javascript:;\"  onclick=\"reenviarEmail('{0}')\"><i class=\"fa fa-paper-plane fa-lg\"></i></a>", idCrypto) : "";

                tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" title=\"Reenviar email\" >{0}</td>", htmlBtnReenviarEmail));

                //tabelaHtml.Append(string.Format("<td style=\"color: {1}; font-weight: bold; vertical-align: middle; text-align: center;\" >{0}</td>", numero, num.IdStatus == 0 ? "green" : "red"));


                //style="background-color: red;"
                tabelaHtml.Append("</tr>");
            }

            tabelaHtml.Append("</tbody></table>");

            return tabelaHtml.ToString();
        }

        private void CarregaTabelaVendedores()
        {
            var tabelaHtml = new StringBuilder();
            tabelaHtml.Append("<table id=\"tabelaVendedores\" class=\"table table-striped table-bordered  table-hover dataTables-example\">");
            tabelaHtml.Append("<thea><tr>");

            tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"><h3>ID</h3></th>");
            tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"><h3>Vendedor</h3></th>");
            tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"><h3>Situação</h3></th>");
            tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"><h3></h3></th>");

            tabelaHtml.Append("</tr></thead><tbody>");

            var listaVendedor = AcaoEntreAmigosVendedor.Lista(-1);

            foreach (var vdr in listaVendedor)
            {
                tabelaHtml.Append("<tr>");

                //ID
                tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", vdr.IdVendedor));
                //Vendedor
                tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", vdr.NomeVendedor));
                //Situação
                var sit = string.Format("<small class=\"label label-{1}\">{0}</small>"
                                        , vdr.IdStatus == 0 ? "INATIVO" : "ATIVO"
                                        , vdr.IdStatus == 0 ? "danger" : "primary"
                                       );
                tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", sit));

                //=== AÇÕES ======
                //eDITAR
                var idCrypto = Criptografia.Encrypt(vdr.IdVendedor.ToString());
                idCrypto = idCrypto.Replace('/', '_').Replace('+', '.').Replace('=', '-');

                var htmlBtnEditar = string.Format("<a href=\"javascript:;\" onclick=\"carregaIdVendedorEdit('{0}', '{1}', {2})\" " + //class=\"btn btn-outline btn-success btn-sm\"
                    " title=\"Editar\" data-toggle=\"modal\" data-target=\"#myModalEditInvest\"><i class=\"fa fa-pencil fa-lg\"></i></a>", idCrypto, vdr.NomeVendedor, vdr.IdStatus);

                tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", htmlBtnEditar));


                //tabelaHtml.Append(string.Format("<td style=\"color: {1}; font-weight: bold; vertical-align: middle; text-align: center;\" >{0}</td>", numero, num.IdStatus == 0 ? "green" : "red"));
                //style="background-color: red;"
                tabelaHtml.Append("</tr>");
            }

            tabelaHtml.Append("</tbody></table>");

            ltlTabelaVendedores.Text = tabelaHtml.ToString();
        }

        protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSearch.SelectedIndex > 0)
                {
                    CarregaTabelaNumeros(1);

                    //pnlFiltro.Visible = ddlSearch.SelectedValue.Equals("");
                }
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                              "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ddlSearch_SelectedIndexChanged-ERRO:" + ex.Message + "</p></div>";
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                if (VerificaCamposBtnPesquisa())
                {
                    hdfValueBegin.Value = txtNumPesqInicio.Value;
                    hdfValueEnd.Value = txtNumPesqFim.Value;

                    CarregaTabelaNumeros(2);
                }

            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">" +
                              "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> btnPesquisar_Click-ERRO:" + ex.Message + "</p></div>";
            }
        }

        private bool VerificaCamposBtnPesquisa()
        {
            ltlMsn.Text = "";
            if (txtNumPesqInicio.Value == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">" +
                              "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ERRO: Digite o número inicial do intervalo da pesquisa!!</p></div>";
                return false;
            }

            if (txtNumPesqFim.Value == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">" +
                              "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ERRO: Digite os número final do intervalo da pesquisa!!</p></div>";
                return false;
            }

            var inicio = int.Parse(txtNumPesqInicio.Value);
            var fim = int.Parse(txtNumPesqFim.Value);
            if (inicio > fim)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">" +
                              "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ERRO: O número inicial não pode ser maior que o final!!</p></div>";
                return false;
            }

            return true;
        }

        private static string EnviarEmailComprador(string email, string nome, string listaNum)
        {
            try
            {

                string nomeRemetente = "Ação Entre Amigos";
                string emailRemetente = "contato@ariziolouzada.com.br";
                string emailDestinatario = email;
                string assuntoMensagem = "Número comprado na Ação Entre Amigos";
                string conteudoMensagem = "PARABÉNS <b><u>" + nome + "</u></b> !!!<br /><br />Você está participando da AÇÃO ENTRE AMIGOS" +
                                          " e concorre a uma VAN EXECUTIVA através do número <b><u>" + listaNum +
                                          "</u></b><br /><br />Acompanhe todo andamento através do instagram @RECEITADAROCA." + 
                                          "<br /><br /><i>Mensagem automática do sistema. Não precisa responder.</i>";


                var result = Email.EnviarLocaWeb(nomeRemetente, emailRemetente, emailDestinatario, assuntoMensagem, conteudoMensagem);
                return result;

            }
            catch (Exception ex)
            {
                return "Erro: " + ex.Message;
            }

        }

        [WebMethod]
        public static string ReenviarEmail(string numero)
        {
            try
            {
                numero = numero.Replace('_', '/').Replace('.', '+').Replace('-', '=');
                numero = Criptografia.Decrypt(numero);
                var aea = AcaoEntreAmigos.PesquisaNumero(int.Parse(numero));
                if (aea != null)
                {
                    var resulatdo = EnviarEmailComprador(aea.Email, aea.NomeComprador, aea.NumeroStr);
                    if (!resulatdo.Equals("true"))
                    {
                        return "Erro ao enviar EMAIL, Tente novamente!!";
                    }
                }
                return "reenvioEmailOk";
            }
            catch (Exception ex)
            {
                return "Erro: " + ex.Message;
            }
        }

        [WebMethod]
        public static string EditarVendedor(string id, string nome, string idStatus)
        {
            try
            {
                id = id.Replace('_', '/').Replace('.', '+').Replace('-', '=');
                id = Criptografia.Decrypt(id);
                var vdr = new AcaoEntreAmigosVendedor(int.Parse(id), nome, int.Parse(idStatus));
                if (AcaoEntreAmigosVendedor.Editar(vdr))
                    return "EditVendedorOk";

                return "Erro ao editar vendedor!!";
            }
            catch (Exception ex)
            {
                return "Erro: " + ex.Message;
            }
        }

        [WebMethod]
        public static string EditarNumero(string numero, string nomeComprador, string email, string tel, string dataVda, string idVendedor, string idStatus)
        {
            try
            {
                numero = numero.Replace('_', '/').Replace('.', '+').Replace('-', '=');
                numero = Criptografia.Decrypt(numero);

                var aea = new AcaoEntreAmigos();
                aea.Numero = int.Parse(numero);
                aea.IdStatus = int.Parse(idStatus);
                aea.IdVendedor = int.Parse(idVendedor);
                aea.NomeComprador = nomeComprador;
                aea.Telefone = tel;
                aea.Email = email;
                aea.DataVenda = DateTime.Parse(dataVda);

                if (AcaoEntreAmigos.Editar(aea))
                    return "EditNumeroOk";

                return "Erro ao editar número!!";
            }
            catch (Exception ex)
            {
                return "Erro: " + ex.Message;
            }
        }

        [WebMethod]
        public static string CarregaNumeroEdit(string numero)
        {
            try
            {
                var htmlCamposModal = new StringBuilder();
                numero = numero.Replace('_', '/').Replace('.', '+').Replace('-', '=');
                numero = Criptografia.Decrypt(numero);
                var aea = AcaoEntreAmigos.PesquisaNumero(int.Parse(numero));
                if (aea != null)
                {
                    //campo Numero
                    htmlCamposModal.Append("<div class=\"form-group\"><label>Número</label>");
                    htmlCamposModal.Append(string.Format("<input id=\"txtNumeroEdit\" type=\"text\" class=\"form-control\"  value=\"{0}\" disabled=\"\"/></div>", aea.NumeroStr));

                    //campo Comprador
                    htmlCamposModal.Append("<div class=\"form-group\"><label>Comprador</label>");
                    htmlCamposModal.Append(string.Format("<input id=\"txtCompradorEdit\" type=\"text\" class=\"form-control\" value=\"{0}\"/></div>", aea.NomeComprador));

                    //campo Telefone
                    htmlCamposModal.Append("<div class=\"form-group\"><label>Telefone(s)</label>");
                    htmlCamposModal.Append(string.Format("<input id=\"txtTelefoneEdit\" type=\"text\" class=\"form-control\" value=\"{0}\"/></div>", aea.Telefone));

                    //campo Email
                    htmlCamposModal.Append("<div class=\"form-group\"><label>Email</label>");
                    htmlCamposModal.Append(string.Format("<input id=\"txtEmailEdit\" type=\"text\" class=\"form-control\" value=\"{0}\"/></div>", aea.Email));

                    //campo data Venda
                    htmlCamposModal.Append("<div class=\"form-group\"><label>Data Venda</label>");
                    htmlCamposModal.Append(string.Format("<input id=\"txtDataVendaEdit\" type=\"date\" class=\"form-control\" value=\"{0}\"/></div>"
                                            , aea.DataVenda.ToString().Equals("01/01/1900 00:00:00") ? string.Empty : aea.DataVenda.ToString("yyyy-MM-dd"))
                                          );

                    //campo Vendedor
                    htmlCamposModal.Append("<div class=\"form-group\"><label>Vendedor</label>");
                    //htmlCamposModal.Append(string.Format("<input id=\"txtVe Edit\" type=\"text\" class=\"form-control\" value=\"{0}\"/></div>", aea.NomeVendedor));

                    htmlCamposModal.Append("<select id=\"ddlVendedorEdit\" class=\"form-control\">");
                    var listaVendedor = AcaoEntreAmigosVendedor.Lista(-1);
                    foreach (var item in listaVendedor)
                    {
                        var selected = item.IdVendedor == aea.IdVendedor ? " selected=\"selected\"" : "";
                        htmlCamposModal.Append(string.Format("<option value=\"{0}\" {2}>{1}</option>", item.IdVendedor, item.NomeVendedor, selected));
                    }
                    htmlCamposModal.Append("</select></div>");

                    //campo Situação
                    htmlCamposModal.Append("<div class=\"form-group\"><label>Situação</label>");
                    htmlCamposModal.Append("<select id=\"ddlStatusEdit\" class=\"form-control\">");

                    var selected_1 = aea.IdStatus == 0 ? " selected=\"selected\"" : "";
                    htmlCamposModal.Append(string.Format("<option value=\"0\" {0}>Disponível</option>", selected_1));
                    selected_1 = aea.IdStatus == 1 ? " selected=\"selected\"" : "";
                    htmlCamposModal.Append(string.Format("<option value=\"1\" {0}>Vendido</option>", selected_1));
                    htmlCamposModal.Append("</select></div>");

                }
                return htmlCamposModal.ToString();
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">" +
                              "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> btnPesquisar_Click-ERRO:" + ex.Message + "</p></div>";
            }
        }

    }
}