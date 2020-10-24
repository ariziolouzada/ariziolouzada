using System;
using System.Web.UI;
using CriptografiaSgpm;
using sbessencial_cl;
using System.Text;
using System.Web.Services;

namespace ariziolouzada.sbessencial.servico
{
    public partial class _default : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString.HasKeys())
                {
                    if (Request.QueryString["desc"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["desc"]))
                        txtPesquisa.Value = Request.QueryString["desc"];

                }

                CarregaTabela();
            }
        }


        private void CarregaTabela()
        {
            try
            {
                var tabelaHtml = new StringBuilder();
                var lista = Servico.CarregaTabelaServico(false, txtPesquisa.Value, int.Parse(ddlStatus.SelectedValue));

                if (lista.Count == 0)
                {
                    tabelaHtml.Append("<div class=\"panel panel-warning\"><div class=\"panel-heading\"> <h4 class=\"panel-title\"><i class=\"icon ion-clock text-warning\">" +
                                      "</i>!! Nenhum Item cadastrado !!</h4></div></div>");
                }
                else
                {
                    //terminar

                    tabelaHtml.Append("<table id=\"tabelaServicos\" class=\"table table-striped table-bordered \"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Descrição</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Valor</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Status</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Ações</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"></th>");

                    tabelaHtml.Append("</tr></thead>");
                    tabelaHtml.Append("<tbody>");


                    foreach (var srv in lista)
                    {
                        var idCriotografado = Criptografia.Encrypt(srv.Id.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        //tabelaHtml.Append("<tr class=\"odd gradeX\">");
                        tabelaHtml.Append("<tr>");

                        //Descrição                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", srv.Descricao));
                        
                        //Valor
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format("{0:C}", srv.Valor)));

                        //Situação
                        var situacao = string.Format("<span class=\"badge badge-{0}\">{1}</span>"
                                                        , srv.IdStatus == 1 ? "success" : srv.IdStatus == 2 ? "warning" : "danger"
                                                        , srv.IdStatus == 1 ? "Ativo" : srv.IdStatus == 2 ? "Inativo" : "Excluido"
                                                     );
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", situacao));

                        // ========= AÇÕES ==========

                        //Editar                        
                        tabelaHtml.Append(string.Format("<td style=\"text-align: center\"><a href=\"newedit.aspx?id={0}\" class=\"btn btn-xs btn-info\" data-original-title=\"\"  title=\"Editar dados.\" ><i class=\"fa fa-pencil-square fa-lg\"></i>  Editar</a></td>", idCriotografado));

                        //Excluir   

                        if(srv.IdStatus < 3)
                            tabelaHtml.Append(string.Format("<td style=\"text-align: center\"><a href=\"javascript:;\"  onclick=\"capturarIdServicoExcluir('{0}');\" class=\"btn btn-xs btn-danger\" data-toggle=\"modal\" data-target=\"#myModal6\" data-original-title=\"\"  title=\"Excluir.\" ><i class=\"fa fa-trash fa-lg\"></i> Excluir</a></td>", idCriotografado));
                        else
                            tabelaHtml.Append("<td></td>");                    


                        tabelaHtml.Append("</tr>");
                    }
                    tabelaHtml.Append("</tbody></table>");
                }
                ltlTabela.Text = tabelaHtml.ToString();

            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaTabela-ERRO:" + ex.Message + "</p></div>";
            }

        }


        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            ltlMsn.Text = "";
            var desc = txtPesquisa.Value;
            Session["paremetrosDefault"] = string.Format("{0}", desc);
            Response.Redirect(string.Format("default.aspx?desc={0}", desc));

            //if (ddlMes.SelectedIndex > 0)
            //{
            //    CarregaTabela();
            //}
            //else
            //{
            //    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
            //                    "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Selecione o mês.</p></div>";
            //    ddlMes.Focus();
            //}
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTabela();
        }


        [WebMethod]
        public static string ExcluirServico(string idServico)
        {
            try
            {
                idServico = Criptografia.Decrypt(idServico.Replace('_', '+'));
                if (!Servico.EditarStatus(int.Parse(idServico), 3))
                {
                    return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                           "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Não foi possivel excluir (mudar status) o Serviço.</p></div>";
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO:" + ex.Message + "</p></div>";
            }
        }


    }
}