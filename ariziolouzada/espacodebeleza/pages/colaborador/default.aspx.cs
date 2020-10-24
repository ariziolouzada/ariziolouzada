using CriptografiaSgpm;
using espacobeleza_cl;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;

namespace ariziolouzada.espacodebeleza.pages.colaborador
{
    public partial class _default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                CarregaDados();


            }


        }

        private void CarregaDados()
        {
            try
            {
                ltlDataHoje.Text = string.Format("<span class=\"label label-success pull-right\">{0}</span><h5>{1}</h5>", DateTime.Now.Date.ToShortDateString(), NomeDiaSemana(DateTime.Now.Date));

                var idProfissional = CookieEspBel.Recupera("idProfissional", true);

                var totalHoje = ComandaItem.SomaProfissionalValorComissao(int.Parse(idProfissional), DateTime.Now.Date);
                ltlTotalHoje.Text = string.Format("<h1 class=\"no-margins\">{0}</h1><div class=\"stat-percent font-bold text-success\">{1} <i class=\"fa fa-bolt\"></i></div>", string.Format("{0:C}", totalHoje), PorcentagemDeDiasSemana(DateTime.Now.Date));

                var listaDiasSemana = ListaDiasSemana(DateTime.Now.Date);
                var totalSemana = TotalSemana(listaDiasSemana, int.Parse(idProfissional));
                ltlTotalSemana.Text = string.Format("<h1 class=\"no-margins\">{0}</h1><div class=\"stat-percent font-bold text-info\">{1} <i class=\"fa fa-level-up\"></i></div>", string.Format("{0:C}", totalSemana), PorcentagemDeDiasSemana(DateTime.Now.Date));
                var semana = string.Format("{0} à {1}", listaDiasSemana[0].ToString("dd"), listaDiasSemana[6].ToString("dd/MM"));

                if (listaDiasSemana[0].Month != listaDiasSemana[6].Month)
                    semana = string.Format("{0} à {1}", listaDiasSemana[0].ToString("dd/MM"), listaDiasSemana[6].ToString("dd/MM"));

                ltlSemana.Text = string.Format("<span class=\"label label-info pull-right\">{0}</span>", semana);


                var idEmpresaContratante = CookieEspBel.Recupera("idEmpresaContratante", true);
                var qtdeSvc = ComandaItem.QtdeItensComandaPorProfissional(int.Parse(idProfissional), listaDiasSemana[0].Date, listaDiasSemana[6].Date);
                var qtdeTotalSvc = ComandaItem.QtdeItensComanda(int.Parse(idEmpresaContratante), listaDiasSemana[0].Date, listaDiasSemana[6].Date);
                decimal porcentagem = qtdeTotalSvc > 0 ? (decimal)qtdeSvc / (decimal)qtdeTotalSvc : 0;
                porcentagem = porcentagem * 100;
                ltlServicos.Text = string.Format("<h1 class=\"no-margins\">{0} de {2}</h1><div class=\"font-bold text-navy\">{1}% <i class=\"fa fa-cog\"></i><small>Serviços</small></div>", qtdeSvc, string.Format("{0:0.00}", porcentagem), qtdeTotalSvc);

                var qtdeCmda = Comanda.QtdeTotalComanda(int.Parse(idEmpresaContratante), listaDiasSemana[0].Date, listaDiasSemana[6].Date, 0);
                var qtdeTotalCmda = Comanda.QtdeComandaPorProfissional(int.Parse(idProfissional), listaDiasSemana[0].Date, listaDiasSemana[6].Date, 0);
                porcentagem = qtdeTotalCmda > 0 ? (decimal)qtdeCmda / (decimal)qtdeTotalCmda : 0;
                porcentagem = porcentagem * 100;
                ltlComandas.Text = string.Format("<h1 class=\"no-margins\">{0} de {2}</h1><div class=\"font-bold text-navy\">{1}% <i class=\"fa fa-trello\"></i><small>Comandas</small></div>", qtdeCmda, string.Format("{0:0.00}", porcentagem), qtdeTotalCmda);
                
                CarregaTabela(int.Parse(idProfissional), listaDiasSemana[0].Date, listaDiasSemana[6].Date, int.Parse(ddlFiltro.SelectedValue));
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> CarregaDadosComanda-ERRO:" + ex.Message + "</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }
        }

        private decimal TotalSemana(List<DateTime> lista, int idProfissional)
        {
            decimal soma = 0;
            foreach (var dia in lista)
            {
                soma = soma + ComandaItem.SomaProfissionalValorComissao(idProfissional, dia);
            }
            return soma;
        }

        private string PorcentagemDeDiasSemana(DateTime data)
        {
            var porcento = "0%";
            var numeroDiaSemana = (int)data.DayOfWeek;
            if (numeroDiaSemana > 1)//Para não caucular divisão por zero
                numeroDiaSemana = numeroDiaSemana - 1;

            porcento = string.Format("{0}%", numeroDiaSemana * 20);
            return porcento;
        }

        private List<DateTime> ListaDiasSemana(DateTime data)
        {
            var lista = new List<DateTime>();
            //teste
            //data = new DateTime(2019, 09, 08);

            var numeroDiaSemana = (int)data.DayOfWeek;
            var aux = 6 - numeroDiaSemana;
            data = data.AddDays(aux);//define a data do ultimo dia da semana
            for (int i = 6; i >= 0; i--)
            {
                var dataDia = data.AddDays(-i);
                lista.Add(dataDia);
            }
            return lista;
        }

        private string NomeDiaSemana(DateTime data)
        {
            var nomeDiaSemana = "Segunda";
            //data = new DateTime(2019, 09, 01);
            var numeroDiaSemana = (int)data.DayOfWeek;

            switch (numeroDiaSemana)
            {
                case 0:
                    nomeDiaSemana = "Domingo";
                    break;
                case 1:
                    nomeDiaSemana = "Segunda";
                    break;
                case 2:
                    nomeDiaSemana = "Terça";
                    break;
                case 3:
                    nomeDiaSemana = "Quarta";
                    break;
                case 4:
                    nomeDiaSemana = "Quinta";
                    break;
                case 5:
                    nomeDiaSemana = "Sexta";
                    break;
                case 6:
                    nomeDiaSemana = "Sábado";
                    break;
            }

            return nomeDiaSemana;
        }


        private void CarregaTabela(int idProfissional, DateTime dataInicio, DateTime dataFinal, int filtro)
        {
            try
            {
                var tabelaHtml = new StringBuilder();
                List<ComandaItem> lista = null;
                if (filtro == 1)
                    lista = ComandaItem.ListaServicos(idProfissional, DateTime.Now.Date);
                else
                    lista = ComandaItem.ListaServicos(idProfissional, dataInicio, dataFinal);

                if (lista.Count == 0)
                {
                    tabelaHtml.Append("<div class=\"panel panel-warning\"><div class=\"panel-heading\"> <h4 class=\"panel-title\"><i class=\"icon ion-clock text-warning\">" +
                                      "</i>!! Nenhum Item cadastrado !!</h4></div></div>");
                }
                else
                {
                    //terminar

                    tabelaHtml.Append("<table id=\"tabelaClientes\" class=\"table table-striped table-bordered \"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Comanda</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Cliente</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Serviço</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Valor Serviço</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Valor Comissão</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Situação Comanda</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Pagamento</th>");

                    tabelaHtml.Append("</tr></thead>");
                    tabelaHtml.Append("<tbody>");

                    foreach (var item in lista)
                    {
                        tabelaHtml.Append("<tr>");
                        //Comanda                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", item.IdComanda));
                        //Cliente                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left;\" >{0}</td>", item.Cliente));
                        //Servico
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", item.Servico));
                        //Valor    Serviço                
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format("{0:C}", item.Valor)));
                        //Valor     Comissão               
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format("{0:C}", item.ValorComissao)));

                        //Situação
                        var situacao = string.Format("<span class=\"badge badge-{0}\">{1}</span>", item.IdStatusComanda == 1 ? "primary" : "danger", item.SituacaoComanda);
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", situacao));
                        //Forma Pgto
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", item.FormaPgto));

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

        protected void ddlFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idProfissional = CookieEspBel.Recupera("idProfissional", true);
            var listaDiasSemana = ListaDiasSemana(DateTime.Now.Date);
            var filtro = ddlFiltro.SelectedValue;            
            CarregaTabela(int.Parse(idProfissional), listaDiasSemana[0].Date, listaDiasSemana[6].Date, int.Parse(filtro));
        }
    }
}