using ariziolouzada.classes;
using CriptografiaSgpm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ariziolouzada.ast.epi
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                CarregaTabelaEpi();

            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>Page_Load-ERRO:" + ex.Message + "</p></div>";
            }
        }

        private void CarregaTabelaEpi()
        {
            try
            {

                var tabelaHtml = new StringBuilder();
                var listaEpis = Material.ListaDeMateriais(0);

                if (listaEpis.Count == 0)
                {
                    tabelaHtml.Append("<div class=\"panel panel-warning\"><div class=\"panel-heading\"> <h4 class=\"panel-title\"><i class=\"icon ion-clock text-warning\">" +
                                      "</i>!! Nenhum EPI cadastrado !!</h4></div></div>");
                }
                else
                {

                    tabelaHtml.Append("<table class='table table-striped table-bordered table-hover dataTables-example'>");
                    tabelaHtml.Append("<thead><tr><th>Descrição</th><th>Valor Unitário</th><th>Qtde Estoque</th><th class='center'>Ações</th>");
                    tabelaHtml.Append("</tr></thead><tbody>");

                    foreach (var epi in listaEpis)
                    {
                        var idCriotografado = Criptografia.Encrypt(epi.Id.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        tabelaHtml.Append("<tr class='gradeX'>");
                        tabelaHtml.Append(string.Format("<td>{0}</td>", epi.Descricao));
                        tabelaHtml.Append(string.Format("<td>{0}</td>", epi.PrecoCusto));
                        tabelaHtml.Append(string.Format("<td>{0}</td>", epi.QtdeEstoque));
                        tabelaHtml.Append(string.Format("<td style=\"text-align: center; vertical-align: middle\" ><a href=\"neweditepi.aspx?id={0}\"><span class=\"fa fa-pencil-square-o fa-2x\" data-original-title=\"\" title=\"Editar\" ></span></a></td>", idCriotografado));
                        tabelaHtml.Append("</tr>");
                    }
                    tabelaHtml.Append("</tbody></table>");
                    ltlTabelaEpi.Text = tabelaHtml.ToString();                                    
                }


            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaTabelaEpi-ERRO:" + ex.Message + "</p></div>";
            }

        }

    }
}