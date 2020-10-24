using CriptografiaSgpm;
using sbessencial_cl;
using System;
using System.Text;
using System.Web.UI;

namespace ariziolouzada.sbessencial.produto
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
                var lista = Produto.Lista(false, txtPesquisa.Value, 0);

                if (lista.Count == 0)
                {
                    tabelaHtml.Append("<div class=\"panel panel-warning\"><div class=\"panel-heading\"> <h4 class=\"panel-title\"><i class=\"icon ion-clock text-warning\">" +
                                      "</i>!! Nenhum Produto cadastrado !!</h4></div></div>");
                }
                else
                {
                    //terminar

                    tabelaHtml.Append("<table id=\"tabelaServicos\" class=\"table table-striped table-bordered \"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Descrição</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Estoque</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Pç Custo</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Pç Venda</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Status</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Ações</th>");

                    tabelaHtml.Append("</tr></thead>");
                    tabelaHtml.Append("<tbody>");


                    foreach (var pdto in lista)
                    {
                        var idCriotografado = Criptografia.Encrypt(pdto.Id.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        //tabelaHtml.Append("<tr class=\"odd gradeX\">");
                        tabelaHtml.Append("<tr>");

                        //Descrição                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", pdto.Descricao));

                        //Estoque                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", pdto.QtdEstoque));

                        //Preço Custo
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format("{0:C}", pdto.PrecoCusto)));

                        //Preço Venda
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format("{0:C}", pdto.PrecoVenda)));

                        //Situação
                        var situacao = string.Format("<span class=\"badge badge-{0}\">{1}</span>"
                                                        , pdto.IdStatus == 1 ? "success" : pdto.IdStatus == 2 ? "warning" : "danger"
                                                        , pdto.IdStatus == 1 ? "Ativo" : pdto.IdStatus == 2 ? "Inativo" : "Excluido"
                                                     );
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", situacao));

                        // ========= AÇÕES ==========

                        //Editar                        
                        tabelaHtml.Append(string.Format("<td style=\"text-align: center\"><a href=\"newedit.aspx?id={0}\" class=\"btn btn-xs btn-info\" data-original-title=\"\"  title=\"Editar dados Produto.\" ><i class=\"fa fa-pencil-square\"></i>  Editar</a></td>", idCriotografado));

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

    }
}