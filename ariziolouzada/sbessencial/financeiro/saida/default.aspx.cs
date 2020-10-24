using System;
using System.Web.UI;
using CriptografiaSgpm;
using sbessencial_cl;
using System.Text;

namespace ariziolouzada.sbessencial.financeiro.saida
{
    public partial class _default : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString.HasKeys())
                {
                    if (Request.QueryString["nome"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["nome"]))
                        txtPesquisa.Value = Request.QueryString["nome"];

                }

                CarregaTabela();
            }
        }


        private void CarregaTabela()
        {
            try
            {
                var tabelaHtml = new StringBuilder();
                var lista = TipoSaida.Lista(false, txtPesquisa.Value);

                if (lista.Count == 0)
                {
                    tabelaHtml.Append("<div class=\"panel panel-warning\"><div class=\"panel-heading\"> <h4 class=\"panel-title\"><i class=\"icon ion-clock text-warning\">" +
                                      "</i>!! Nenhum Item cadastrado !!</h4></div></div>");
                }
                else
                {
                    //terminar

                    tabelaHtml.Append("<table id=\"tabelaTipoSaida\" class=\"table table-condensed table-striped table-hover table-bordered pull-left\"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Código</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Descrição</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Ações</th>");

                    tabelaHtml.Append("</tr></thead>");
                    tabelaHtml.Append("<tbody>");


                    foreach (var saida in lista)
                    {
                        var idCriotografado = Criptografia.Encrypt(saida.Id.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        //tabelaHtml.Append("<tr class=\"odd gradeX\">");
                        tabelaHtml.Append("<tr>");

                        //Nome                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", saida.Id));
                        
                        //Descrição
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left;\" >{0}</td>", saida.Descricao));

                        // ========= AÇÕES ==========

                        //Editar                        
                        tabelaHtml.Append(string.Format("<td style=\"text-align: center;\"><a href=\"newedit.aspx?id={0}\" class=\"btn btn-xs btn-info\" data-original-title=\"\"  title=\"Editar dados.\" ><i class=\"fa fa-pencil-square\"></i>  Editar</a></td>", idCriotografado));

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
            var nome = txtPesquisa.Value;
            Session["paremetrosDefault"] = string.Format("{0}", nome);
            Response.Redirect(string.Format("default.aspx?nome={0}", nome));

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