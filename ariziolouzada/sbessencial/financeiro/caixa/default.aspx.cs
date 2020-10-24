using CriptografiaSgpm;
using sbessencial_cl;
using System;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ariziolouzada.sbessencial.financeiro.caixa
{
    public partial class _default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                CarregaDdlAno(false);
                var mes = DateTime.Now.Month.ToString();
                var ano = DateTime.Now.Year.ToString();
                var tipo = "0";

                if (Request.QueryString.HasKeys())
                {
                    if (Request.QueryString["mes"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["mes"]))
                    {
                        mes = Request.QueryString["mes"];
                    }

                    if (Request.QueryString["ano"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["ano"]))
                    {
                        ano = Request.QueryString["ano"];
                    }

                    if (Request.QueryString["tipo"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["tipo"]))
                    {
                        tipo = Request.QueryString["tipo"];
                    }


                }

                ddlMes.SelectedValue = mes;
                ddlAno.SelectedValue = ano;
                ddlTipo.SelectedValue = tipo;

                CarregaTabela();
            }
        }


        private void CarregaDdlAno(bool comSelecione)
        {
            var lista = FluxoCaixa.ListaAno(comSelecione);

            var anoAtual = DateTime.Now.Year.ToString();
            if (!lista.Contains(anoAtual))
                lista.Add(anoAtual);

            ddlAno.Items.Clear();
            foreach (var ano in lista)
            {
                ddlAno.Items.Add(new ListItem(ano, ano));
            }

            //ddlAno.DataSource = lista;
            //ddlAno.DataValueField = "Id";
            //ddlAno.DataTextField = "Nome";
            //ddlAno.DataBind();

            //if (comSelecione)
            ddlAno.SelectedIndex = 0;
        }


        private void CarregaTabela()
        {
            try
            {
                var mes = int.Parse(ddlMes.SelectedValue);
                var ano = int.Parse(ddlAno.SelectedValue);
                var tipo = int.Parse(ddlTipo.SelectedValue);

                var totalEntrada = FluxoCaixa.Total(mes, ano, 1);
                var totalSaida = FluxoCaixa.Total(mes, ano, 2);
                var totalDeposito = FluxoCaixa.Total(mes, ano, 3);
                var saldo = totalEntrada - totalSaida;

                txtTotalEntrada.Value = string.Format("{0:C}", totalEntrada);
                txtTotalSaida.Value = string.Format("{0:C}", totalSaida);
                txtDeposito.Value = string.Format("{0:C}", totalDeposito);
                txtSaldo.Value = string.Format("{0:C}", saldo);

                var tabelaHtml = new StringBuilder();
                var lista = FluxoCaixa.Lista(mes, ano, tipo);

                if (lista.Count == 0)
                {
                    tabelaHtml.Append("<div class=\"panel panel-warning\"><div class=\"panel-heading\"> <h4 class=\"panel-title\"><i class=\"icon ion-clock text-warning\">" +
                                      "</i>!! Nenhum Item cadastrado !!</h4></div></div>");
                }
                else
                {
                    tabelaHtml.Append("<table id=\"tabelaFluxoCx\" class=\"table table-striped table-bordered\"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Tipo</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Data</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Forma Pgto</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Valor</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Cliente</th>");

                    if (ddlTipo.SelectedValue.Equals("2"))
                        tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"></th>");

                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Obs.</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Ações</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"></th>");

                    tabelaHtml.Append("</tr></thead>");
                    tabelaHtml.Append("<tbody>");


                    foreach (var fc in lista)
                    {
                        var idCriotografado = Criptografia.Encrypt(fc.Id.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        //tabelaHtml.Append("<tr class=\"odd gradeX\">");
                        tabelaHtml.Append("<tr>");
                        //Tipo    
                        var cssTipo = fc.IdTipo == 1 ? "primary" : fc.IdTipo == 2 ? "danger" : "warning";
                        var tipoStr = fc.IdTipo == 3 ? fc.TipoSaida : fc.Tipo;
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" ><span class=\"badge badge-{1}\">{0}</span></td>", tipoStr, cssTipo));

                        //data                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", fc.Data.ToShortDateString()));
                        //Forma Pgto                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", fc.FormaPgto));

                        //Valor      
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format("{0:C}", fc.Valor)));

                        // ========= AÇÕES ==========

                        //cLIENTE
                        //var cliente = string.Format("<button type=\"button\" class=\"btn btn-primary\" data-container=\"body\" data-toggle=\"popover\" data-placement=\"top\" data-content=\"{0}\" data-original-title=\"\" title=\"\"><i class=\"fa fa-female\"></i></button>", fc.Cliente);
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left;\" >{0}</td>", fc.Cliente));
                        //<button type="button" class="btn btn-primary" data-container="body" data-toggle="popover" data-placement="top" data-content="{0}" data-original-title="" title=""><i class="fa fa-female"></i></button>

                        if (ddlTipo.SelectedValue.Equals("2"))
                            tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left;\" >{0}</td>", fc.TipoSaida));

                        //Observação
                        var obs = string.Empty;
                        if (fc.Obsercacao != string.Empty)
                            obs = string.Format("<button type=\"button\" class=\"btn btn-default dim\" data-container=\"body\" data-toggle=\"popover\" data-placement=\"top\" data-content=\"{0}\" data-original-title=\"\" title=\"\"><i class=\"fa fa-warning\"></i></button>", fc.Obsercacao);

                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", obs));

                        //Editar                        
                        tabelaHtml.Append(string.Format("<td style=\"text-align: center\"><a href=\"newedit.aspx?id={0}\" class=\"btn btn-xs btn-info\" data-original-title=\"\"  title=\"Editar dados.\" ><i class=\"fa fa-pencil-square\"></i>  Editar</a></td>", idCriotografado));

                        //Excluir                        
                        //tabelaHtml.Append(string.Format("<td style=\"text-align: center\"><a href=\"javascript:;\" class=\"btn btn-xs btn-danger\" data-original-title=\"\"  title=\"Excluir registro.\" ><i class=\"fa fa-eraser\"></i>  Excluir</a></td>", idCriotografado));
                        tabelaHtml.Append(string.Format("<td style=\"text-align: center\"><a href=\"#\" data-toggle=\"modal\" data-target=\"#myModal4\" class=\"btn btn-xs btn-danger\" onclick=\"capturarId({0})\" data-original-title=\"\"  title=\"Excluir registro.\" ><i class=\"fa fa-eraser\"></i>  Excluir</a></td>", fc.Id));

                        //<button type="button" class="btn btn-danger" data-toggle="modal" data-target="#myModal4">Excluir</ button >

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


        protected void btnCarregarLista_Click(object sender, EventArgs e)
        {
            ltlMsn.Text = "";

            var mes = int.Parse(ddlMes.SelectedValue);
            var ano = int.Parse(ddlAno.SelectedValue);
            var tipo = int.Parse(ddlTipo.SelectedValue);
            Session["paremetrosDefault"] = string.Format("{0};{1};{2}", mes, ano, tipo);

            Response.Redirect(string.Format("default.aspx?mes={0}&ano={1}&tipo={2}", mes, ano, tipo));

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


        [WebMethod]
        public static string CapturarId(string id)
        {
            try
            {
                HttpContext.Current.Session["idFcExcluir"] = id;
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-danger fade in m-b-15\"><strong>ddlAno_SelectedIndexChanged-Error: </strong> " +
                       ex.Message + "<span class=\"close\" data-dismiss=\"alert\">X</span></div>";
            }
        }


        [WebMethod]
        public static string ExcluirId()
        {

            // aaakkii//não está fechando o modal depois qe exclui. VERIFICAR!!!!
            try
            {
                var id = HttpContext.Current.Session["idFcExcluir"].ToString();

                if (FluxoCaixa.Apagar(int.Parse(id)))
                {
                    return HttpContext.Current.Session["paremetrosDefault"].ToString();
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-danger fade in m-b-15\"><strong>ddlAno_SelectedIndexChanged-Error: </strong> " +
                       ex.Message + "<span class=\"close\" data-dismiss=\"alert\">X</span></div>";
            }
        }


        protected void btnSimDelReg_Click(object sender, EventArgs e)
        {
            try
            {
                var id = Session["idFcExcluir"].ToString();
                if (FluxoCaixa.Apagar(int.Parse(id)))
                {
                    btnCarregarLista_Click(sender, e);
                }
                else
                {
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                  "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO ao excluir o registro!!</p></div>";
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