using CriptografiaSgpm;
using espacobeleza_cl;
using System;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;

namespace ariziolouzada.espacodebeleza.pages.colaborador
{
    public partial class listacomandas : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString.HasKeys())
                {
                    if (Request.QueryString["data"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["data"]))
                        txtData.Value = Request.QueryString["data"];
                }
                else
                {
                    //carrega sempre o dia atual em tela
                    txtData.Value = DateTime.Now.Date.ToString("yyyy-MM-dd");
                }

                CarregaDados();

                CarregaTabela();
            }
        }


        private void CarregaDados()
        {
            try
            {
                //RECUPERAR O COOKIE
                var idProfissional = "0";
                HttpCookie cookie = HttpContext.Current.Request.Cookies["idProfissional"];
                if (cookie != null && cookie.Value != null)
                    idProfissional = cookie.Value;
                idProfissional = Criptografia.Decrypt(idProfissional);

                if (idProfissional.Equals("0"))
                {
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                  "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaDados-ERRO: idProfissional INVÁLIDO</p></div>";
                    return;
                }

                hdfIdProfissional.Value = idProfissional;
                ltlNomeProfissional.Text = string.Format("<small><b>{0}</b></small>", ProfissionalEspBel.PesquisaNome(int.Parse(idProfissional)));
                //

                //RECUPERAR O COOKIE
                var idEmpresaContratante = "0";
                cookie = HttpContext.Current.Request.Cookies["idEmpresaContratante"];
                if (cookie != null && cookie.Value != null)
                    idEmpresaContratante = cookie.Value;

                idEmpresaContratante = Criptografia.Decrypt(idEmpresaContratante);

                var total = Comanda.Soma(DateTime.Parse(txtData.Value), int.Parse(idEmpresaContratante), int.Parse(ddlTipo.SelectedValue));
                ltlTotalSomaComanda.Text = string.Format("<span style=\"font-size: 16pt; color: red; \"><b>TOTAL {0} </b></span>", string.Format("{0:C}", total));

                //if (ddlTipo.SelectedValue.Equals("2"))
                //{
                //colocar o valor a receber pelo colaborador qdo exibir comandas fechadas
                var valor = ComandaItem.SomaProfissionalValorComissao(int.Parse(idProfissional), DateTime.Parse(txtData.Value));
                ltlTotalSomaComanda.Text = ltlTotalSomaComanda.Text +
                    string.Format("<br /><span style=\"font-size: 12pt; color: green; \"><b>À Receber {0} </b></span>", string.Format("{0:C}", valor));
                //}
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaDados-ERRO:" + ex.Message + "</p></div>";
            }
        }

        [WebMethod]
        public static string CarregaTabelaComandas(string data, string idProfissional, string idTipo)
        {
            try
            {
                //RECUPERAR O COOKIE
                var idEmpresaContratante = "0";
                HttpCookie cookie = HttpContext.Current.Request.Cookies["idEmpresaContratante"];
                if (cookie != null && cookie.Value != null)
                    idEmpresaContratante = cookie.Value;

                idEmpresaContratante = Criptografia.Decrypt(idEmpresaContratante);

                var tabelaHtml = new StringBuilder();
                //var lista = Comanda.ListaPorProfissional(DateTime.Parse(data), int.Parse(idProfissional), int.Parse(idTipo));
                var lista = Comanda.Lista(DateTime.Parse(data), int.Parse(idEmpresaContratante), int.Parse(idTipo));

                decimal total = 0;
                if (lista.Count == 0)
                {
                    tabelaHtml.Append("<div class=\"panel panel-warning\"><div class=\"panel-heading\"> <h4 class=\"panel-title\"><i class=\"icon ion-clock text-warning\">" +
                                      "</i>!! Nenhuma Comanda cadastrada !!</h4></div></div>");
                }
                else
                {
                    //terminar
                    tabelaHtml.Append("<table id=\"tabelaClientes\" class=\"table table-striped table-bordered \"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Número</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Cliente</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Serviço(s)</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Valor</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Status</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Forma Pgto</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Ações</th>");

                    tabelaHtml.Append("</tr></thead>");
                    tabelaHtml.Append("<tbody>");

                    foreach (var comanda in lista)
                    {
                        var idCriotografado = Criptografia.Encrypt(comanda.Id.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        //tabelaHtml.Append("<tr class=\"odd gradeX\">");
                        tabelaHtml.Append("<tr>");

                        //Numero                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format("{0:000}", comanda.Id)));

                        //Cliente                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left;\" >{0}</td>", comanda.Cliente));

                        //Servicos realizados
                        var btnSvc = string.Format("<button type=\"button\" onclick=\"carregaServicoCmdaModal('{0}');\" class=\"btn btn-outline btn-success  dim\" data-toggle=\"modal\" data-target=\"#myModal2\" title=\"Serviços da comanda.\"> <i class=\"fa fa-cogs fa-2x\"></i> </button>", idCriotografado);
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", btnSvc));

                        //Valor                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format("{0:C}", comanda.Valor)));
                        total = total + comanda.Valor;
                        //Situação
                        var situacao = string.Format("<span class=\"badge badge-{0}\">{1}</span>", comanda.IdStatus == 1 ? "primary" : "danger", comanda.Status);
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", situacao));

                        //Forma Pgto                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", comanda.FormaPgto));

                        // ========= AÇÕES ==========

                        //Editar     
                        if (comanda.IdStatus == 1)//Só edita as comanda em aberto
                            tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\"><button type=\"button\" onclick=\"editarDadosComanda('{0}');\" class=\"btn btn-outline btn-info  dim\" title=\"Editar dados comanda.\"> <i class=\"fa fa-pencil fa-2x\"></i> </button></td>", idCriotografado));
                        //tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\"><a href=\"comanda.aspx?id={0}\" class=\"btn btn-info btn-xs\" data-original-title=\"\"  title=\"Editar dados comanda.\" ><i class=\"fa fa-pencil fa-2x\"></i> </a></td>", idCriotografado));
                        else
                            tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left;\" >{0}</td>", ""));


                        tabelaHtml.Append("</tr>");
                    }
                    tabelaHtml.Append("</tbody></table>");
                }
                return tabelaHtml.ToString();
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaTabelaComandas-ERRO:" + ex.Message + "</p></div>";
            }

        }


        private void CarregaTabela()
        {
            try
            {
                //RECUPERAR O COOKIE
                var idEmpresaContratante = "0";
                HttpCookie cookie = Request.Cookies["idEmpresaContratante"];
                if (cookie != null && cookie.Value != null)
                    idEmpresaContratante = cookie.Value;

                idEmpresaContratante = Criptografia.Decrypt(idEmpresaContratante);

                var idProfissional = 5;
                ltlNomeProfissional.Text = string.Format("<small><b>{0}</b></small>", ProfissionalEspBel.PesquisaNome(idProfissional));

                var tabelaHtml = new StringBuilder();
                var lista = Comanda.ListaPorProfissional(DateTime.Parse(txtData.Value), idProfissional, int.Parse(ddlTipo.SelectedValue));

                //decimal total = 0;
                if (lista.Count == 0)
                {
                    tabelaHtml.Append("<div id=\"divTabelaComandas\" class=\"panel panel-warning\"><div class=\"panel-heading\"> <h4 class=\"panel-title\"><i class=\"icon ion-clock text-warning\">" +
                                      "</i>!! Nenhum Item cadastrado !!</h4></div></div>");
                }
                else
                {
                    //terminar
                    tabelaHtml.Append("<div id=\"divTabelaComandas\"><table id=\"tabelaClientes\" class=\"table table-striped table-bordered \"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Número</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Cliente</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Serviço(s)</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Valor</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Status</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Forma Pgto</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Ações</th>");

                    tabelaHtml.Append("</tr></thead>");
                    tabelaHtml.Append("<tbody>");

                    foreach (var comanda in lista)
                    {
                        var idCriotografado = Criptografia.Encrypt(comanda.Id.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        //tabelaHtml.Append("<tr class=\"odd gradeX\">");
                        tabelaHtml.Append("<tr>");

                        //Numero                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format("{0:000}", comanda.Id)));

                        //Cliente                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left;\" >{0}</td>", comanda.Cliente));

                        //Servicos realizados
                        var btnSvc = string.Format("<button type=\"button\" onclick=\"carregaServicoCmdaModal('{0}');\" class=\"btn btn-outline btn-success  dim\" data-toggle=\"modal\" data-target=\"#myModal2\" title=\"Serviços da comanda.\"> <i class=\"fa fa-cogs fa-2x\"></i> </button>", idCriotografado);
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", btnSvc));

                        //Valor                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format("{0:C}", comanda.Valor)));
                        //total = total + comanda.Valor;
                        //Situação
                        var situacao = string.Format("<span class=\"badge badge-{0}\">{1}</span>", comanda.IdStatus == 1 ? "primary" : "danger", comanda.Status);
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", situacao));

                        //Forma Pgto                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", comanda.FormaPgto));

                        // ========= AÇÕES ==========

                        //Editar     
                        if (comanda.IdStatus == 1)//Só edita as comanda em aberto
                            //tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\"><a href=\"comanda.aspx?id={0}\" class=\"btn btn-info btn-xs\" data-original-title=\"\"  title=\"Editar dados comanda.\" ><i class=\"fa fa-pencil fa-2x\"></i> </a></td>", idCriotografado));
                            tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\"><button type=\"button\" onclick=\"editarDadosComanda('{0}');\" class=\"btn btn-outline btn-info  dim\" title=\"Editar dados comanda.\"> <i class=\"fa fa-pencil fa-2x\"></i> </button></td>", idCriotografado));
                        else
                            tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left;\" >{0}</td>", ""));


                        tabelaHtml.Append("</tr>");
                    }
                    tabelaHtml.Append("</tbody></table></div>");
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
            if (txtData.Value != string.Empty)
            {
                CarregaTabela();

            }
            else
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ERRO: Entre com a data para carregar a lista de comandas!</p></div>";
                txtData.Focus();
            }
            //var nome = txtPesquisa.Value;
            //Session["paremetrosDefault"] = string.Format("{0}", nome);
            //Response.Redirect(string.Format("default.aspx?nome={0}", nome));

        }


        private static string CarregaTblSvcComandaModal(string idComanda)
        {
            try
            {
                var tabelaHtml = new StringBuilder();
                idComanda = Criptografia.Decrypt(idComanda.Replace('_', '+'));
                var lista = ComandaItem.Lista(long.Parse(idComanda));

                if (lista.Count == 0)
                {
                    tabelaHtml.Append("<div class=\"panel panel-warning\"><div class=\"panel-heading\"> <h4 class=\"panel-title\"><i class=\"icon ion-clock text-warning\">" +
                                      "</i>!! Nenhum Item cadastrado !!</h4></div></div>");
                }
                else
                {
                    tabelaHtml.Append("<table id=\"tabelaClientes\" class=\"table table-striped table-bordered \"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Item</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Serviço</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Profissional</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Valor</th>");
                    tabelaHtml.Append("</tr></thead>");
                    tabelaHtml.Append("<tbody>");

                    //decimal totalServico = 0;
                    var item = 1;
                    foreach (var cdaItem in lista)
                    {
                        var idCriotografado = Criptografia.Encrypt(cdaItem.Id.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        tabelaHtml.Append("<tr>");

                        //Item                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format(@"{0:00}", item)));

                        //Serviço                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", cdaItem.Servico));
                        //totalServico = totalServico + cdaItem.Valor;

                        //Profissional                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", cdaItem.Profissional));

                        //Valor                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format("{0:C}", cdaItem.Valor)));

                        item++;
                        tabelaHtml.Append("</tr>");
                    }
                    ////Total   bgcolor=\"#CFCFCF\"
                    //tabelaHtml.Append("<tr style=\"font-size: 14pt;\">");
                    //tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: right\" colspan=\"3\"><b>TOTAL</b></td>"));
                    //tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\"  colspan=\"2\"><b>{0}</b></td>", string.Format("{0:C}", totalServico)));
                    //tabelaHtml.Append("</tr>");

                    tabelaHtml.Append("</tbody></table>");
                }
                return tabelaHtml.ToString();

            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaTabela-ERRO:" + ex.Message + "</p></div>";
            }

        }


        [WebMethod]
        public static string CarregaServicoCmdaModal(string idComanda)
        {
            try
            {
                return CarregaTblSvcComandaModal(idComanda);
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-danger fade in m-b-15\"><strong>ResetarSenha-Error: </strong> " +
                        ex.Message + "<span class=\"close\" data-dismiss=\"alert\">X</span></div>";
            }
        }


        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTabela();
            CarregaDados();
        }


    }
}