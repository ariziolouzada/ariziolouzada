using ariziolouzada.classes;
using CriptografiaSgpm;
using System;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;

namespace ariziolouzada.ast.epi
{
    public partial class servicos : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                CarregaTabela();

            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>Page_Load-ERRO:" + ex.Message + "</p></div>";
            }
        }

        private void CarregaTabela()
        {
            try
            {

                var tabelaHtml = new StringBuilder();
                var lista = ServicoPedido.ListaDeServicoPedidos(txtDataIncial.Value, txtDataFinal.Value);

                if (lista.Count == 0)
                {
                    tabelaHtml.Append("<div class=\"panel panel-warning\"><div class=\"panel-heading\"> <h4 class=\"panel-title\"><i class=\"icon ion-clock text-warning\">" +
                                      "</i>!! Nenhum serviço cadastrado !!</h4></div></div>");
                }
                else
                {

                    tabelaHtml.Append("<table class='table table-striped table-bordered table-hover dataTables-example'>");
                    tabelaHtml.Append("<thead><tr><th>Data</th><th>Descrição</th><th>Qtde</th><th>Valor Unitário</th><th>Valor Total</th><th class='center'>Ações</th>");
                    tabelaHtml.Append("</tr></thead><tbody>");

                    foreach (var serv in lista)
                    {
                        var idCriotografado = Criptografia.Encrypt(serv.Id.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        tabelaHtml.Append("<tr class='gradeX'>");
                        tabelaHtml.Append(string.Format("<td>{0}</td>", serv.Data.ToShortDateString()));
                        tabelaHtml.Append(string.Format("<td>{0}</td>", serv.Descricao));
                        tabelaHtml.Append(string.Format("<td>{0}</td>", serv.Qtde));
                        tabelaHtml.Append(string.Format("<td>{0}</td>", serv.ValorUnitario));
                        tabelaHtml.Append(string.Format("<td>{0}</td>", serv.ValorTotal));
                        tabelaHtml.Append("<td style=\"text-align: center; vertical-align: middle\">");

                        tabelaHtml.Append(
                                            string.Format("<a href=\"neweditservico.aspx?id={0}\"><span class=\"fa fa-pencil-square-o fa-2x\" data-original-title=\"\" title=\"Editar\" ></span></a>", idCriotografado) +
                                            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                                            string.Format("<a href =\"#\" data-toggle=\"modal\" data-target=\"#modalDeletePedSvc\" onclick=\"capturarIdServicoExcluir({0})\"><span class=\"fa fa-times-circle-o fa-2x\" data-original-title=\"\" title=\"Excluir\" ></span></a>", serv.Id)
                                        );

                        tabelaHtml.Append("</td></tr>");
                    }
                    tabelaHtml.Append("</tbody></table>");
                }

                ltlTabelaServicos.Text = tabelaHtml.ToString();

            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaTabelaEpi-ERRO:" + ex.Message + "</p></div>";
            }

        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            CarregaTabela();
        }


        [WebMethod]
        public static string CapturarIdServicoExcluir(string id)
        {
            try
            {
                HttpContext.Current.Session["idSvcExcluir"] = id;
                var svc = ServicoPedido.PesquisaServicoPedido(int.Parse(id));
                if (svc != null)
                {
                    var dadosMemoria = new StringBuilder();
                    dadosMemoria.Append("<div class=\"form-group\">");
                    dadosMemoria.Append("<label for=\"pwd\" class=\"col-lg-2 control-label\">Descrição:</label>");
                    dadosMemoria.Append("<div class=\"col-lg-10\"><input id=\"txtModalDescMemo\" name=\"txtModalDescMemo\" type=\"text\" class=\"form-control\"  disabled=\"disabled\" value=\"" + svc.Descricao + "\" />");
                    dadosMemoria.Append("</div></div><br /><br />");
                    return dadosMemoria.ToString();
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        protected void btnExcluirPedSvc_Click(object sender, EventArgs e)
        {
            //tesatarpq não está funcionando
            try
            {
                var idSvc = HttpContext.Current.Session["idSvcExcluir"].ToString();

                if (ServicoPedido.Excluir(int.Parse(idSvc)))
                {
                    var idProj = Request.QueryString["id"];
                    idProj = Criptografia.Decrypt(idProj.Replace('_', '+'));
                    CarregaTabela();
                }
                else
                {
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
                                "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ERRO ao Salvar Objetivo Estratégico!!</p></div>";
                }
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ERRO:" + ex.Message + "</p></div>";
            }
        }


    }
}