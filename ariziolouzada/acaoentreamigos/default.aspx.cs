using ariziolouzada.classes;
using System;
using System.Text;
using System.Web.UI;

namespace ariziolouzada.acaoentreamigos
{
    public partial class _default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CarregaTabela();
                }

            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                              "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> Page_Load-ERRO:" + ex.Message + "</p></div>";
            }
        }

        private void CarregaTabela()
        {
            try
            {
                var tabelaHtml = new StringBuilder();
                tabelaHtml.Append("<table id=\"tabelaNumeros\" class=\"table table-striped table-bordered table-hover \">");
                //tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"><h3>Profissional</h3></th>");

                var idstatus = int.Parse(ddlFiltroNumeros.SelectedValue);
                if (idstatus < 2)
                {
                    var listaNumero = AcaoEntreAmigos.CarregaTabelaPaginaDefault(idstatus);
                    tabelaHtml.Append("<tbody><tr>");
                    var aux = 1;
                    foreach (var num in listaNumero)
                    {
                        var numero = string.Format("{0:00000}", num.Numero);
                        //Numero                    
                        tabelaHtml.Append(string.Format("<td style=\"color: {1}; font-weight: bold; vertical-align: middle; text-align: center;\" >{0}</td>", numero, num.IdStatus == 0 ? "green" : "red"));

                        if (idstatus == -1)
                        {
                            if (num.Numero % 16 == 0)
                                tabelaHtml.Append("</tr><tr>");
                        }
                        else
                        {
                            if (aux % 15 == 0)
                                tabelaHtml.Append("</tr><tr>");
                        }
                        aux++;
                        //style="background-color: red;"
                    }
                    tabelaHtml.Append("</tbody></table>");
                }

                ltlTabelaNumeros.Text = tabelaHtml.ToString();
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                              "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> CarregaTabela-ERRO:" + ex.Message + "</p></div>";
            }
        }


        private void InsertInicial()
        {
            for (int i = 1; i < 16001; i++)
            {
                var numero = string.Format("{0:00000}", i);
                if (!AcaoEntreAmigos.Inserir(numero))
                {
                    //Parado em caso de erro
                    i = 16001;
                }
            }

        }

        protected void ddlFiltroNumeros_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTabela();
        }
    }
}