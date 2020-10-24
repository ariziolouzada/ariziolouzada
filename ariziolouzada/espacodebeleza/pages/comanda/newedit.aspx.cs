using System;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using CriptografiaSgpm;
using espacobeleza_cl;

namespace ariziolouzada.espacodebeleza.pages.comanda
{
    public partial class newedit : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                //Erro na hora de adicionar mais de uma forma de pgto. 
                //estar com erro na veiw da forma de exibição, está mostrando qtde errada de regsitro0
                if (!IsPostBack)
                {
                    ComandaItem.ApagaItensSemReferencia();
                    ComandaItemProduto.ApagaItensSemReferencia();
                    ComandaFormaPgto.ApagaItensSemReferencia();

                    if (Request.QueryString.HasKeys())
                    {
                        if (Request.QueryString["idag"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["idag"]))
                        {
                            hdfIdComandaTemp.Value = ltlnumeroComanda.Text = Comanda.GeraIdTemp();
                            var idag = Request.QueryString["idag"];
                            idag = Criptografia.Decrypt(idag.Replace('_', '+'));
                            hdfIdAgenda.Value = idag;
                            CarregaDadosAgenda(int.Parse(idag));
                        }

                        if (Request.QueryString["id"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["id"]))
                        {
                            var id = Request.QueryString["id"];
                            id = Criptografia.Decrypt(id.Replace('_', '+'));
                            if (id.Equals("0"))
                            {
                                if (Request.QueryString["data"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["data"]))
                                {
                                    txtData.Value = Request.QueryString["data"];
                                }
                                else
                                {
                                    //carrega sempre o dia atual em tela
                                    txtData.Value = DateTime.Now.Date.ToString("yyyy-MM-dd");
                                }

                                CarregaDdlCliente(true);
                                CarregaDdlProduto(true);

                                hdfIdComandaTemp.Value = ltlnumeroComanda.Text = Comanda.GeraIdTemp();

                            }
                            else
                            {
                                if (!id.Equals("-1"))
                                {

                                    CarregaDadosComanda(long.Parse(id));

                                }
                            }
                            CarregaDdlProfissiao(true);
                            CarregaDdlProduto(true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>Page_Load-ERRO:" + ex.Message + "</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }
        }

        private void CarregaDadosComanda(long id)
        {
            try
            {
                var comanda = Comanda.Pesquisar(id);
                if (comanda != null)
                {
                    hdfIdComandaTemp.Value = ltlnumeroComanda.Text = comanda.Id.ToString();
                    //txtValor.Value = string.Format("{0:C}", comanda.Valor);
                    txtData.Value = comanda.Data.ToString("yyyy-MM-dd");
                    CarregaDdlCliente(false);
                    ddlCliente.SelectedValue = comanda.IdCliente.ToString();
                    ddlFormaPgto.SelectedValue = hdfIdFormaPgto.Value = comanda.IdFormaPgto.ToString();
                    hdfIdComandaTemp.Value = comanda.Id.ToString();
                    ddlStatus.SelectedValue = comanda.IdStatus.ToString();
                    CarregaTabela(comanda.Id);
                    CarregaTabelaProdutos(comanda.Id);

                    ltlTotalCmda.Text = string.Format("<span style=\"font-size: 16pt; font-weight: bold; color: red;\">TOTAL COMANDA: {0}</span>", string.Format("{0:C}", comanda.Valor));

                    if (comanda.IdFormaPgto == 6)
                        CarregaTabelaFormaPgto(comanda.Id);

                    pnlFormasDePgto.Visible = comanda.IdFormaPgto == 6;

                    CarregaDdlProduto(true);
                    //ScriptManager.RegisterStartupScript(this, GetType(), "_carregaItensComanda", "carregaItensComanda();", true);
                }
                else
                {
                    ltlMsn.Text = "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                  "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> CarregaDadosComanda-ERRO: Dados da Comanda não encontrados!!</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
                }
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> CarregaDadosComanda-ERRO:" + ex.Message + "</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }
        }

        private void CarregaValorCmda(long idComanda)
        {
            var totalCmda = ComandaItem.SomaItensCmda(idComanda) + ComandaItemProduto.SomaItensCmda(idComanda);
            ltlTotalCmda.Text = string.Format("<div id=\"lblTotalCmda\"><span style=\"font-size: 16pt; font-weight: bold; color: red;\">TOTAL COMANDA: {0}</span></div>", string.Format("{0:C}", totalCmda));
        }

        private void CarregaDdlCliente(bool comSelecione)
        {
            //RECUPERAR O COOKIE
            var idEmpresaContratante = "0";
            HttpCookie cookie = Request.Cookies["idEmpresaContratante"];
            if (cookie != null && cookie.Value != null)
                idEmpresaContratante = cookie.Value;

            idEmpresaContratante = Criptografia.Decrypt(idEmpresaContratante);

            var lista = ClienteEspBel.Lista(comSelecione, "", int.Parse(idEmpresaContratante));
            ddlCliente.Items.Clear();
            ddlCliente.DataSource = lista;
            ddlCliente.DataValueField = "Id";
            ddlCliente.DataTextField = "Nome";
            ddlCliente.DataBind();

            if (comSelecione)
                ddlProfissao.SelectedIndex = 0;
        }

        private void CarregaDdlProfissiao(bool comSelecione)
        {
            var lista = ProfissaoEspBel.Lista(comSelecione);
            ddlProfissao.Items.Clear();
            ddlProfissao.DataSource = lista;
            ddlProfissao.DataValueField = "Id";
            ddlProfissao.DataTextField = "Descricao";
            ddlProfissao.DataBind();

            if (comSelecione)
                ddlProfissao.SelectedIndex = 0;
        }

        private void CarregaDdlProduto(bool comSelecione)
        {
            var lista = ProdutoEspBel.Lista(comSelecione, 1);
            ddlProduto.Items.Clear();
            ddlProduto.DataSource = lista;
            ddlProduto.DataValueField = "Id";
            ddlProduto.DataTextField = "Descricao";
            ddlProduto.DataBind();

            if (comSelecione)
                ddlProfissao.SelectedIndex = 0;
        }


        private void CarregaDdlProfissional(int idProfissao, bool comSelecione, int idEmpresaContratante)
        {
            var listaProf = ProfissionalEspBel.Lista(comSelecione, idProfissao, idEmpresaContratante);
            ddlProfissional.Items.Clear();
            ddlProfissional.DataSource = listaProf;
            ddlProfissional.DataValueField = "Id";
            ddlProfissional.DataTextField = "Nome";
            ddlProfissional.DataBind();

            if (comSelecione)
                ddlProfissional.SelectedIndex = 0;
        }


        private void CarregaDdlServico(int idProfissional, bool comSelecione, int idEmpresaContratante)
        {
            var listaSvcProf = ServicoEspBel.ListaServicoProfissional(comSelecione, idProfissional, 1, idEmpresaContratante);
            ddlServico.Items.Clear();
            ddlServico.DataSource = listaSvcProf;
            ddlServico.DataValueField = "Id";
            ddlServico.DataTextField = "Descricao";
            ddlServico.DataBind();

            if (comSelecione)
                ddlServico.SelectedIndex = 0;
        }


        private void CarregaDadosAgenda(int idAgenda)
        {
            var agenda = Agenda.Pesquisar(idAgenda);
            if (agenda != null)
            {
                txtData.Value = agenda.DataAgenda.ToString("yyyy-MM-dd");
                CarregaDdlCliente(false);
                ddlCliente.SelectedValue = agenda.IdCliente.ToString();

                CarregaServicoAgenda(agenda);
            }
        }


        protected void ddlProfissao_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlProfissao.SelectedIndex > 0)
                {
                    //RECUPERAR O COOKIE
                    var idEmpresaContratante = "0";
                    HttpCookie cookie = Request.Cookies["idEmpresaContratante"];
                    if (cookie != null && cookie.Value != null)
                        idEmpresaContratante = cookie.Value;

                    idEmpresaContratante = Criptografia.Decrypt(idEmpresaContratante);

                    CarregaDdlProfissional(int.Parse(ddlProfissao.SelectedValue), true, int.Parse(idEmpresaContratante));

                    ddlServico.Items.Clear();
                    txtValor.Value = string.Empty;
                }
                else
                {
                    ddlProfissional.Items.Clear();
                    ddlServico.Items.Clear();
                    txtValor.Value = string.Empty;
                }

                var id = Request.QueryString["id"];
                id = Criptografia.Decrypt(id.Replace('_', '+'));
                if (!id.Equals("0"))
                {
                    if (!id.Equals("-1"))
                        CarregaTabela(long.Parse(id));
                    else
                        CarregaTabela(long.Parse(hdfIdComandaTemp.Value));
                }
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ddlProfissao_SelectedIndexChanged-ERRO:" + ex.Message + "</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }
        }

        protected void ddlProfissional_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlProfissional.SelectedIndex > 0)
                {
                    //RECUPERAR O COOKIE
                    var idEmpresaContratante = "0";
                    HttpCookie cookie = Request.Cookies["idEmpresaContratante"];
                    if (cookie != null && cookie.Value != null)
                        idEmpresaContratante = cookie.Value;

                    idEmpresaContratante = Criptografia.Decrypt(idEmpresaContratante);

                    CarregaDdlServico(int.Parse(ddlProfissional.SelectedValue), true, int.Parse(idEmpresaContratante));
                    txtValor.Value = string.Empty;
                }
                else
                {
                    ddlServico.Items.Clear();
                    txtValor.Value = string.Empty;
                }

                var id = Request.QueryString["id"];
                id = Criptografia.Decrypt(id.Replace('_', '+'));
                if (!id.Equals("0"))
                    CarregaTabela(long.Parse(id));
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ddlProfissional_SelectedIndexChanged-ERRO:" + ex.Message + "</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }
        }

        protected void ddlServico_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlServico.SelectedIndex > 0)
                {
                    //RECUPERAR O COOKIE
                    var idEmpresaContratante = "0";
                    HttpCookie cookie = Request.Cookies["idEmpresaContratante"];
                    if (cookie != null && cookie.Value != null)
                        idEmpresaContratante = cookie.Value;

                    idEmpresaContratante = Criptografia.Decrypt(idEmpresaContratante);

                    txtValor.Value = string.Format("{0:C}", ServicoEspBel.PesquisarValor(int.Parse(ddlServico.SelectedValue)));

                }
                else
                {
                    txtValor.Value = string.Empty;
                }

                var id = Request.QueryString["id"];
                id = Criptografia.Decrypt(id.Replace('_', '+'));
                if (!id.Equals("0"))
                    CarregaTabela(long.Parse(id));
                else
                    CarregaTabela(long.Parse(hdfIdComandaTemp.Value));

            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ddlProfissional_SelectedIndexChanged-ERRO:" + ex.Message + "</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }
        }


        private void CarregaServicoAgenda(Agenda agenda)
        {
            try
            {
                ltlMsn.Text = string.Empty;
                var tabelaHtml = new StringBuilder();

                tabelaHtml.Append("<div id=\"ltlTabelaComandas\"><table id=\"tabelaClientes\" class=\"table table-striped table-bordered \"><thead><tr>");
                tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Item</th>");
                tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Profissional</th>");
                tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Serviço</th>");
                tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Valor</th>");
                tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"></th>");
                tabelaHtml.Append("</tr></thead>");
                tabelaHtml.Append("<tbody>");

                decimal totalServico = 0;

                var comissaoItem = ServicoProfissionalEspBel.PesquisaComissao(agenda.IdServico, agenda.IdProfissional);
                var cdaItem = new ComandaItem(hdfIdComandaTemp.Value, agenda.IdProfissional.ToString(), agenda.IdServico.ToString(), "1", agenda.Valor.ToString(), comissaoItem);
                var idItemComda = ComandaItem.InserirRetornandoId(cdaItem);
                if (idItemComda == 0)
                {
                    ltlMsn.Text = "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> CarregaServicoAgenda-ERRO: Não foi possível criar item da comanda.</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
                    return;
                }

                var idCriotografado = Criptografia.Encrypt(idItemComda.ToString());
                idCriotografado = idCriotografado.Replace('+', '_');

                tabelaHtml.Append("<tr>");

                //Item 
                var item = 1;
                tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format(@"{0:00}", item)));

                //Profissional                    
                tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", agenda.Profissional));

                //Serviço                    
                tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", agenda.Servico));
                totalServico = totalServico + agenda.Valor;

                //Valor                    
                tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format("{0:C}", agenda.Valor)));

                // ========= AÇÕES ==========

                //Excluir                        
                tabelaHtml.Append(string.Format("<td style=\"text-align: center\"><a href=\"javascript:;\" onclick=\"exclirItemComanda('{0}');\"  class=\"btn btn-xs btn-danger\" data-original-title=\"\"  title=\"Excluir Item da Comanda.\" ><i class=\"fa fa-trash-o fa-2x\"></i></a></td>", idCriotografado));

                item++;
                tabelaHtml.Append("</tr>");
                //}
                //Total   bgcolor=\"#CFCFCF\"
                tabelaHtml.Append("<tr style=\"font-size: 14pt;\">");
                tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: right\" colspan=\"3\"><b>TOTAL</b></td>"));
                tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\"  colspan=\"2\"><b>{0}</b></td>", string.Format("{0:C}", totalServico)));
                tabelaHtml.Append("</tr>");

                tabelaHtml.Append("</tbody></table>");

                ltlTabelaComandas.Text = tabelaHtml.ToString();

            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> CarregaServicoAgenda-ERRO:" + ex.Message + "</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }

        }

        private void CarregaTabela(long idComanda)
        {
            try
            {
                var tabelaHtml = new StringBuilder();
                var lista = ComandaItem.ListaCarregaTabela(idComanda);

                if (lista.Count == 0)
                {
                    tabelaHtml.Append("<div id=\"ltlTabelaComandas\"><div class=\"panel panel-warning\"><div class=\"panel-heading\"> <h4 class=\"panel-title\"><i class=\"icon ion-clock text-warning\">" +
                                      "</i>!! Nenhum Item cadastrado-02!!</h4></div></div></div>");
                }
                else
                {
                    tabelaHtml.Append("<div id=\"ltlTabelaComandas\"><table id=\"tabelaClientes\" class=\"table table-striped table-bordered \"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Item</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Profissional</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Serviço</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Valor</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"></th>");
                    tabelaHtml.Append("</tr></thead>");
                    tabelaHtml.Append("<tbody>");

                    decimal totalServico = 0;
                    var item = 1;
                    foreach (var cdaItem in lista)
                    {
                        var idCriotografado = Criptografia.Encrypt(cdaItem.Id.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        tabelaHtml.Append("<tr>");

                        //Item                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format(@"{0:00}", item)));

                        //Profissional                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", cdaItem.Profissional));

                        //Serviço                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", cdaItem.Servico));
                        totalServico = totalServico + cdaItem.Valor;

                        //Valor                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format("{0:C}", cdaItem.Valor)));

                        // ========= AÇÕES ==========

                        //Excluir                        
                        tabelaHtml.Append(string.Format("<td style=\"text-align: center\"><a href=\"javascript:;\" onclick=\"exclirItemComanda('{0}');\"  class=\"btn btn-xs btn-danger\" data-original-title=\"\"  title=\"Excluir Item da Comanda.\" ><i class=\"fa fa-trash-o fa-2x\"></i></a></td>", idCriotografado));

                        item++;
                        tabelaHtml.Append("</tr>");
                    }
                    //Total   bgcolor=\"#CFCFCF\"
                    tabelaHtml.Append("<tr style=\"font-size: 14pt;\">");
                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: right\" colspan=\"3\"><b>TOTAL</b></td>"));
                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\"  colspan=\"2\"><b>{0}</b></td>", string.Format("{0:C}", totalServico)));
                    tabelaHtml.Append("</tr>");

                    tabelaHtml.Append("</tbody></table>");
                }
                ltlTabelaComandas.Text = tabelaHtml.ToString();

                CarregaValorCmda(idComanda);
            }
            catch (Exception ex)
            {
                ltlMsn.Text = ltlMsn.Text + "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaTabela-ERRO:" + ex.Message + "</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }

        }

        private void CarregaTabelaProdutos(long idComanda)
        {
            try
            {
                var tabelaHtml = new StringBuilder();
                var lista = ComandaItemProduto.ListaCarregaTabela(idComanda);

                if (lista.Count == 0)
                {
                    tabelaHtml.Append("<div class=\"panel panel-warning\"><div class=\"panel-heading\"> <h4 class=\"panel-title\"><i class=\"icon ion-clock text-warning\">" +
                                      "</i>!! Nenhum Item cadastrado-01!!</h4></div></div>");
                }
                else
                {
                    tabelaHtml.Append("<table id=\"tabelaClientes\" class=\"table table-striped table-bordered \"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Item</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Produto</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Qtde</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Valor</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"></th>");
                    tabelaHtml.Append("</tr></thead>");
                    tabelaHtml.Append("<tbody>");

                    decimal totalProdutos = 0;
                    var item = 1;
                    foreach (var cdaItem in lista)
                    {
                        var idCriotografado = Criptografia.Encrypt(cdaItem.Id.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        tabelaHtml.Append("<tr>");

                        //Item                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format(@"{0:00}", item)));

                        //Produto                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", cdaItem.DescricaoProduto));

                        //Qtde                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", cdaItem.Qtde));

                        //Valor                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format("{0:C}", cdaItem.Valor)));
                        totalProdutos = totalProdutos + cdaItem.Valor;

                        // ========= AÇÕES ==========
                        //Excluir                        
                        tabelaHtml.Append(string.Format("<td style=\"text-align: center\"><a href=\"javascript:;\"  onclick=\"exclirItemProdutoComanda('{0}');\"  class=\"btn btn-xs btn-danger\" data-original-title=\"\"  title=\"Excluir Item da Comanda.\" ><i class=\"fa fa-trash-o fa-2x\"></i></a></td>", idCriotografado));

                        item++;
                        tabelaHtml.Append("</tr>");
                    }
                    //Total   bgcolor=\"#CFCFCF\"
                    tabelaHtml.Append("<tr style=\"font-size: 14pt;\">");
                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: right\" colspan=\"3\"><b>TOTAL</b></td>"));
                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\"  colspan=\"2\"><b>{0}</b></td>", string.Format("{0:C}", totalProdutos)));
                    tabelaHtml.Append("</tr>");

                    tabelaHtml.Append("</tbody></table>");
                }

                CarregaValorCmda(idComanda);
                ltlTabelaProdutos.Text = tabelaHtml.ToString();
            }
            catch (Exception ex)
            {
                ltlMsn.Text = ltlMsn.Text + "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaTabelaProduto-ERRO:" + ex.Message + "</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }

        }

        private void CarregaTabelaFormaPgto(long idComanda)
        {
            try
            {
                var tabelaHtml = new StringBuilder();
                var lista = ComandaFormaPgto.ListaDaTabela(idComanda);

                if (lista.Count == 0)
                {
                    tabelaHtml.Append("<div class=\"panel panel-warning\"><div class=\"panel-heading\"> <h4 class=\"panel-title\"><i class=\"icon ion-clock text-warning\">" +
                                      "</i>!! Nenhum Item cadastrado-01!!</h4></div></div>");
                }
                else
                {
                    tabelaHtml.Append("<div id=\"ltlTabelaFormasPgtoCmda\"><table id=\"tabelaClientes\" class=\"table table-striped table-bordered \"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Item</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Forma de Pgto</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Valor</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"></th>");
                    tabelaHtml.Append("</tr></thead>");
                    tabelaHtml.Append("<tbody>");

                    decimal totalProdutos = 0;
                    var item = 1;
                    foreach (var cdaItem in lista)
                    {
                        var idCriotografado = Criptografia.Encrypt(cdaItem.Id.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        tabelaHtml.Append("<tr>");

                        //Item                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format(@"{0:00}", item)));

                        //Produto                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", cdaItem.FormaPgto));

                        //Valor                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format("{0:C}", cdaItem.Valor)));
                        totalProdutos = totalProdutos + cdaItem.Valor;

                        // ========= AÇÕES ==========
                        //Excluir                        
                        tabelaHtml.Append(string.Format("<td style=\"text-align: center\"><a href=\"javascript:;\"  onclick=\"exclirItemFormaPgtoComanda('{0}');\"  class=\"btn btn-xs btn-danger\" data-original-title=\"\"  title=\"Excluir Forma de Pgto da Comanda.\" ><i class=\"fa fa-trash-o fa-2x\"></i></a></td>", idCriotografado));

                        item++;
                        tabelaHtml.Append("</tr>");
                    }
                    //Total   bgcolor=\"#CFCFCF\"
                    tabelaHtml.Append("<tr style=\"font-size: 14pt;\">");
                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: right\" colspan=\"3\"><b>TOTAL</b></td>"));
                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\"  colspan=\"2\"><b>{0}</b></td>", string.Format("{0:C}", totalProdutos)));
                    tabelaHtml.Append("</tr>");

                    tabelaHtml.Append("</tbody></table></div>");
                }

                ltlTabelaFormasPgtoCmda.Text = tabelaHtml.ToString();
            }
            catch (Exception ex)
            {
                ltlMsn.Text = ltlMsn.Text + "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaTabelaFormaPgto-ERRO:" + ex.Message + "</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }

        }


        private static string CarregaTabela(string idComanda)
        {
            try
            {
                var tabelaHtml = new StringBuilder();
                var lista = ComandaItem.ListaCarregaTabela(long.Parse(idComanda));

                if (lista.Count == 0)
                {
                    tabelaHtml.Append("<div class=\"panel panel-warning\"><div class=\"panel-heading\"> <h4 class=\"panel-title\"><i class=\"icon ion-clock text-warning\">" +
                                      "</i>!! Nenhum Item cadastrado-01!!</h4></div></div>");
                }
                else
                {
                    tabelaHtml.Append("<table id=\"tabelaClientes\" class=\"table table-striped table-bordered \"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Item</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Profissional</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Serviço</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Valor</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"></th>");
                    tabelaHtml.Append("</tr></thead>");
                    tabelaHtml.Append("<tbody>");

                    decimal totalServico = 0;
                    var item = 1;
                    foreach (var cdaItem in lista)
                    {
                        var idCriotografado = Criptografia.Encrypt(cdaItem.Id.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        tabelaHtml.Append("<tr>");

                        //Item                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format(@"{0:00}", item)));

                        //Profissional                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", cdaItem.Profissional));

                        //Serviço                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", cdaItem.Servico));
                        totalServico = totalServico + cdaItem.Valor;

                        //Valor                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format("{0:C}", cdaItem.Valor)));

                        // ========= AÇÕES ==========

                        //Excluir                        
                        tabelaHtml.Append(string.Format("<td style=\"text-align: center\"><a href=\"javascript:;\"  onclick=\"exclirItemComanda('{0}');\"  class=\"btn btn-xs btn-danger\" data-original-title=\"\"  title=\"Excluir Item da Comanda.\" ><i class=\"fa fa-trash-o fa-2x\"></i></a></td>", idCriotografado));

                        item++;
                        tabelaHtml.Append("</tr>");
                    }
                    //Total   bgcolor=\"#CFCFCF\"
                    tabelaHtml.Append("<tr style=\"font-size: 14pt;\">");
                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: right\" colspan=\"3\"><b>TOTAL</b></td>"));
                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\"  colspan=\"2\"><b>{0}</b></td>", string.Format("{0:C}", totalServico)));
                    tabelaHtml.Append("</tr>");

                    tabelaHtml.Append("</tbody></table>");
                }
                return tabelaHtml.ToString();

            }
            catch (Exception ex)
            {
                return "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaTabela-ERRO:" + ex.Message + "</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }

        }

        private static string CarregaTabelaProdutos(string idComanda)
        {
            try
            {
                var tabelaHtml = new StringBuilder();
                var lista = ComandaItemProduto.ListaCarregaTabela(long.Parse(idComanda));

                if (lista.Count == 0)
                {
                    tabelaHtml.Append("<div class=\"panel panel-warning\"><div class=\"panel-heading\"> <h4 class=\"panel-title\"><i class=\"icon ion-clock text-warning\">" +
                                      "</i>!! Nenhum Item cadastrado-01!!</h4></div></div>");
                }
                else
                {
                    tabelaHtml.Append("<table id=\"tabelaClientes\" class=\"table table-striped table-bordered \"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Item</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Produto</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Qtde</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Valor</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"></th>");
                    tabelaHtml.Append("</tr></thead>");
                    tabelaHtml.Append("<tbody>");

                    decimal totalProdutos = 0;
                    var item = 1;
                    foreach (var cdaItem in lista)
                    {
                        var idCriotografado = Criptografia.Encrypt(cdaItem.Id.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        tabelaHtml.Append("<tr>");

                        //Item                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format(@"{0:00}", item)));

                        //Produto                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", cdaItem.DescricaoProduto));

                        //Qtde                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", cdaItem.Qtde));

                        //Valor                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format("{0:C}", cdaItem.Valor)));
                        totalProdutos = totalProdutos + cdaItem.Valor;

                        // ========= AÇÕES ==========
                        //Excluir                        
                        tabelaHtml.Append(string.Format("<td style=\"text-align: center\"><a href=\"javascript:;\"  onclick=\"exclirItemProdutoComanda('{0}');\"  class=\"btn btn-xs btn-danger\" data-original-title=\"\"  title=\"Excluir Item da Comanda.\" ><i class=\"fa fa-trash-o fa-2x\"></i></a></td>", idCriotografado));

                        item++;
                        tabelaHtml.Append("</tr>");
                    }
                    //Total   bgcolor=\"#CFCFCF\"
                    tabelaHtml.Append("<tr style=\"font-size: 14pt;\">");
                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: right\" colspan=\"3\"><b>TOTAL</b></td>"));
                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\"  colspan=\"2\"><b>{0}</b></td>", string.Format("{0:C}", totalProdutos)));
                    tabelaHtml.Append("</tr>");

                    tabelaHtml.Append("</tbody></table>");
                }
                return tabelaHtml.ToString();

            }
            catch (Exception ex)
            {
                return "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaTabelaProdutos-ERRO:" + ex.Message + "</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }

        }

        private static string CarregaTabelaFormaPgto(string idComanda)
        {
            try
            {
                var tabelaHtml = new StringBuilder();
                var lista = ComandaFormaPgto.ListaDaTabela(long.Parse(idComanda));

                if (lista.Count == 0)
                {
                    tabelaHtml.Append("<div class=\"panel panel-warning\"><div class=\"panel-heading\"> <h4 class=\"panel-title\"><i class=\"icon ion-clock text-warning\">" +
                                      "</i>!! Nenhum Item cadastrado-01!!</h4></div></div>");
                }
                else
                {
                    tabelaHtml.Append("<table id=\"tabelaClientes\" class=\"table table-striped table-bordered \"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Item</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Forma Pgto</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Valor</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"></th>");
                    tabelaHtml.Append("</tr></thead>");
                    tabelaHtml.Append("<tbody>");

                    decimal totalProdutos = 0;
                    var item = 1;
                    foreach (var cdaItem in lista)
                    {
                        var idCriotografado = Criptografia.Encrypt(cdaItem.Id.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        tabelaHtml.Append("<tr>");

                        //Item                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format(@"{0:00}", item)));

                        //Produto                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", cdaItem.FormaPgto));

                        //Valor                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format("{0:C}", cdaItem.Valor)));
                        totalProdutos = totalProdutos + cdaItem.Valor;

                        // ========= AÇÕES ==========
                        //Excluir                        
                        tabelaHtml.Append(string.Format("<td style=\"text-align: center\"><a href=\"javascript:;\"  onclick=\"exclirItemFormaPgtoComanda('{0}');\"  class=\"btn btn-xs btn-danger\" data-original-title=\"\"  title=\"Excluir Forma de Pgto da Comanda.\" ><i class=\"fa fa-trash-o fa-2x\"></i></a></td>", idCriotografado));

                        item++;
                        tabelaHtml.Append("</tr>");
                    }
                    //Total   bgcolor=\"#CFCFCF\"
                    tabelaHtml.Append("<tr style=\"font-size: 14pt;\">");
                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: right\" colspan=\"3\"><b>TOTAL</b></td>"));
                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\"  colspan=\"2\"><b>{0}</b></td>", string.Format("{0:C}", totalProdutos)));
                    tabelaHtml.Append("</tr>");

                    tabelaHtml.Append("</tbody></table>");
                }
                return tabelaHtml.ToString();

            }
            catch (Exception ex)
            {
                return "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaTabelaFormaPgto-ERRO:" + ex.Message + "</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }

        }

        [WebMethod]
        public static string AtualizaTotalComanda(string idComanda)
        {
            try
            {   //não está salvando
                var totalCmda = ComandaItem.SomaItensCmda(long.Parse(idComanda)) + ComandaItemProduto.SomaItensCmda(long.Parse(idComanda));
                return string.Format("<div id=\"lblTotalCmda\"><span style=\"font-size: 16pt; font-weight: bold; color: red;\">TOTAL COMANDA: {0}</span></div>", string.Format("{0:C}", totalCmda));
            }
            catch (Exception ex)
            {
                return "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> AtualizaTotalComanda-ERRO:" + ex.Message + "</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }
        }


        [WebMethod]
        public static string AdicionarItemComanda(string idComanda, string idProfissional, string idServico, string qtde, string valor)
        {
            try
            {   //não está salvando
                var comissaoItem = ServicoProfissionalEspBel.PesquisaComissao(int.Parse(idServico), int.Parse(idProfissional));
                var cdaItem = new ComandaItem(idComanda, idProfissional, idServico, qtde, valor, comissaoItem);
                if (ComandaItem.Inserir(cdaItem))
                {
                    return CarregaTabela(idComanda);
                }

                return "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                       "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ERRO: Não foi possível salvar os dados do Item da Comanda!!</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }
            catch (Exception ex)
            {
                return "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> AdicionatItemComanda-ERRO:" + ex.Message + "</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }
        }


        [WebMethod]
        public static string AdicionarProdutoComanda(string idComanda, string idProduto, string qtde, string valor)
        {
            try
            {
                //aakkiii//terminar

                //var comissaoItem = ServicoProfissionalEspBel.PesquisaComissao(int.Parse(idServico), int.Parse(idProfissional));
                var cdaItem = new ComandaItemProduto(idComanda, idProduto, qtde, valor);
                if (ComandaItemProduto.Inserir(cdaItem))
                {
                    return CarregaTabelaProdutos(idComanda);
                }

                return "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                       "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ERRO: Não foi possível adicionar o produto a Comanda!!</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }
            catch (Exception ex)
            {
                return "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> AdicionatItemComanda-ERRO:" + ex.Message + "</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }
        }

        [WebMethod]
        public static string AdicionarFormaPgtoComanda(string idComanda, string idFormaPgto, string valor)
        {
            try
            {
                //aakkiii//terminar

                //var comissaoItem = ServicoProfissionalEspBel.PesquisaComissao(int.Parse(idServico), int.Parse(idProfissional));
                var cdaItem = new ComandaFormaPgto(idComanda, idFormaPgto, valor);
                if (ComandaFormaPgto.Inserir(cdaItem))
                {
                    return CarregaTabelaFormaPgto(idComanda);
                }

                return "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                       "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ERRO: Não foi possível adicionar o produto a Comanda!!</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }
            catch (Exception ex)
            {
                return "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> AdicionatItemComanda-ERRO:" + ex.Message + "</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }
        }


        [WebMethod]
        public static string ExclirItemComanda(string idItemComanda, string idComanda)
        {
            try
            {
                idItemComanda = Criptografia.Decrypt(idItemComanda.Replace("_", "+"));
                if (ComandaItem.Apagar(long.Parse(idItemComanda)))
                {
                    return CarregaTabela(idComanda);
                }

                return "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                       "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ERRO: Não foi possível excluir os dados do Item da Comanda!!</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }
            catch (Exception ex)
            {
                return "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> AdicionatItemComanda-ERRO:" + ex.Message + "</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }
        }

        [WebMethod]
        public static string ExclirItemProdutoComanda(string idItemComanda, string idComanda)
        {
            try
            {
                idItemComanda = Criptografia.Decrypt(idItemComanda.Replace("_", "+"));
                if (ComandaItemProduto.Apagar(long.Parse(idItemComanda)))
                {
                    return CarregaTabelaProdutos(idComanda);
                }

                return "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                       "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ERRO: Não foi possível excluir os dados do Item da Comanda!!</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }
            catch (Exception ex)
            {
                return "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> AdicionatItemComanda-ERRO:" + ex.Message + "</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }
        }

        [WebMethod]
        public static string ExclirItemFormaPgtoComanda(string idItemComanda, string idComanda)
        {
            try
            {
                idItemComanda = Criptografia.Decrypt(idItemComanda.Replace("_", "+"));
                if (ComandaFormaPgto.Apagar(long.Parse(idItemComanda)))
                {
                    return CarregaTabelaFormaPgto(idComanda);
                }

                return "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                       "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ERRO: Não foi possível excluir a forma de Pgto da Comanda!!</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }
            catch (Exception ex)
            {
                return "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> exclirItemFormaPgtoComanda-ERRO:" + ex.Message + "</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }
        }

        [WebMethod]
        public static string VerificaItemComanda(string idComanda)
        {
            try
            {

                var soma = ComandaItem.SomaItensCmda(long.Parse(idComanda));
                return soma.ToString();
                //var soma = ComandaItem.SomaItensCmda(long.Parse(idComanda));
                //if (soma > 0)
                //{
                //    return "1";
                //}

                //return "";
                //"<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                //   "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ERRO: Não foi possível verificar se existe Item da Comanda!!</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }
            catch (Exception ex)
            {
                return "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> VerificaItemComanda-ERRO:" + ex.Message + "</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }
        }


        [WebMethod]
        public static string CarregaValorProduto(string idProd)
        {
            try
            {
                var valorProd = string.Format("{0:C}", ProdutoEspBel.PesquisarValor(int.Parse(idProd)));
                return valorProd;
                //"<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                //   "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ERRO: Não foi possível verificar se existe Item da Comanda!!</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }
            catch (Exception ex)
            {
                return "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> CarregaValorProduto-ERRO:" + ex.Message + "</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {

            Response.Redirect("default.aspx?data=" + txtData.Value);

            ////Response.Redirect("~/sbessencial/cliente");
            //if (Session["paremetrosDefault"].ToString() != string.Empty)
            //{
            //    var paremetrosDefault = Session["paremetrosDefault"].ToString();
            //    Response.Redirect(string.Format("default.aspx?nome={0}", paremetrosDefault));
            //}
            //else
            //{
            //    Response.Redirect("default.aspx");
            //}


        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/sbessencial/financeiro/caixa/newedit.aspx?id=X5A1oqTnjBE=");
            Response.Redirect("newedit.aspx?id=X5A1oqTnjBE=");
        }


        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (VerificaBtnSalvar())
            {
                if (SalvarDadosComanda())
                {
                    btnVoltar_Click(sender, e);
                }
            }

        }


        private bool VerificaBtnSalvar()
        {
            ltlMsn.Text = "";
            var valorComanda = ComandaItem.SomaItensCmda(long.Parse(hdfIdComandaTemp.Value));
            var valorComandaProd = ComandaItemProduto.SomaItensCmda(long.Parse(hdfIdComandaTemp.Value));
            if (valorComandaProd == 0)
            {
                if (valorComanda == 0)
                {
                    ltlMsn.Text = "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-warning fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                            "</button><p><i class=\"fa fa-exclamation-triangle fa-lg\"></i> Atenção: Não a servicos nem Produtos adicionados a comanda em tela.</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";

                    return false;
                }

            }
            //if (txtNumero.Value == string.Empty)
            //{
            //    ltlMsn.Text = "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-warning fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
            //                            "</button><p><i class=\"fa fa-exclamation-triangle fa-lg\"></i> Atenção: Digite o número da comanda em tela.</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            //    txtNumero.Focus();
            //    return false;
            //}
            if (txtData.Value == string.Empty)
            {
                ltlMsn.Text = "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-warning fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                        "</button><p><i class=\"fa fa-exclamation-triangle fa-lg\"></i> Atenção: Digite a data da comanda em tela.</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
                txtData.Focus();
                return false;
            }

            var id = Request.QueryString["id"];
            id = Criptografia.Decrypt(id.Replace('_', '+'));

            if (id.Equals("0"))
                if (ddlCliente.SelectedIndex == 0)
                {
                    ltlMsn.Text = "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-warning fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                                            "</button><p><i class=\"fa fa-exclamation-triangle fa-lg\"></i> Atenção: Selecione o Cliente da comanda em tela.</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
                    ddlCliente.Focus();
                    return false;
                }

            if (long.Parse(id) > 0)//Somente na Edição da comanda
            {
                if (ddlStatus.SelectedValue.Equals("2"))//Somente se estiver selsionado como FECHADA
                    if (ddlFormaPgto.SelectedIndex == 0)
                    {
                        ltlMsn.Text = "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-warning fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                      "</button><p><i class=\"fa fa-exclamation-triangle fa-lg\"></i> Atenção: Selecione a Fora de Pagamento da comanda em tela.</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
                        ddlFormaPgto.Focus();
                        return false;
                    }
            }

            return true;
        }


        private bool SalvarDadosComanda()
        {
            try
            {
                //RECUPERAR O COOKIE
                var idEmpresaContratante = "0";
                HttpCookie cookie = Request.Cookies["idEmpresaContratante"];
                if (cookie != null && cookie.Value != null)
                    idEmpresaContratante = cookie.Value;
                idEmpresaContratante = Criptografia.Decrypt(idEmpresaContratante);

                var comanda = new Comanda()
                {
                    Id = long.Parse(hdfIdComandaTemp.Value)
                    ,
                    IdCliente = int.Parse(ddlCliente.SelectedValue),
                    IdStatus = int.Parse(ddlStatus.SelectedValue),
                    IdFormaPgto = int.Parse(ddlFormaPgto.SelectedValue) //.Equals("0") ? 6 : int.Parse(ddlFormaPgto.SelectedValue)//Aguardando pgto //int.Parse(ddlFormaPgto.SelectedValue)
                    ,
                    Data = DateTime.Parse(txtData.Value)
                    ,
                    IdEmpresaContratante = int.Parse(idEmpresaContratante)
                    ,
                    Valor = ComandaItem.SomaItensCmda(long.Parse(hdfIdComandaTemp.Value))
                            + ComandaItemProduto.SomaItensCmda(long.Parse(hdfIdComandaTemp.Value))
                };

                var id = Request.QueryString["id"];
                id = Criptografia.Decrypt(id.Replace('_', '+'));

                if (id.Equals("0") || id.Equals("-1"))
                {
                    //Cadastro
                    //comanda.IdFormaPgto = ddlFormaPgto.SelectedValue.Equals("0") ? 6 : int.Parse(ddlFormaPgto.SelectedValue);//Aguardando pgto
                    if (!Comanda.Inserir(comanda))
                    {
                        ltlMsn.Text = "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                            "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> SalvarDadosComanda-ERRO: não foi possível salvar a comanda em tela.</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
                        return false;
                    }
                    else
                    {
                        var idag = Request.QueryString["idag"];
                        if (idag != null)
                        {//Se for diferente de NUll é pq vei redirecionado da agenda
                            idag = Criptografia.Decrypt(idag.Replace('_', '+'));
                            //Editar o idComanda da agenda para criar o relacionament ente elas
                            if (!Agenda.EditarIdComanda(int.Parse(idag), comanda.Id))
                            {
                                ltlMsn.Text = "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                              "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> SalvarDadosComanda-ERRO: não foi possível relacionar a comanda a agenda em tela.</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
                            }
                        }
                    }

                    if (comanda.IdFormaPgto < 6)
                    {
                        var cdaItem = new ComandaFormaPgto(hdfIdComandaTemp.Value, ddlFormaPgto.SelectedValue, comanda.Valor.ToString());
                        if (!ComandaFormaPgto.Inserir(cdaItem))
                        {
                            ltlMsn.Text = "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                          "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> SalvarDadosComanda-ERRO: não foi possível salvar a forma de pgto da comanda em tela.</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
                            return false;
                        }
                    }



                    return true;
                }

                //editar
                //caso não seja selecionado a situação da comanda na hora de editar e for alterado a forma de pgto
                //automaticamente passará para a situação de FECHADA
                if (ddlFormaPgto.SelectedIndex > 0)
                    comanda.IdStatus = 2;


                if (!Comanda.Editar(comanda))
                {
                    ltlMsn.Text = "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                        "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> SalvarDadosComanda-ERRO: não foi possível editar a comanda em tela.</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";

                }
                else
                {
                    //se for difereten é pq teve mudança na forma de pgto.
                    if (hdfIdFormaPgto.Value != ddlFormaPgto.SelectedValue)
                    {
                        if (!ddlFormaPgto.SelectedValue.Equals("6"))
                        {
                            //apagar os registro das forma dePgto da tbl_comanda_forma_pgto
                            if (ComandaFormaPgto.ApagarRegistroFormaPgtoComanda(long.Parse(hdfIdComandaTemp.Value)))
                            {
                                //e cadastrar apena o registro de uma forma de pgto na tbl_comanda_forma_pgto
                                var cdaItem = new ComandaFormaPgto(hdfIdComandaTemp.Value, ddlFormaPgto.SelectedValue, comanda.Valor.ToString());
                                if (!ComandaFormaPgto.Inserir(cdaItem))
                                {
                                    ltlMsn.Text = "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                                  "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> SalvarDadosComanda-ERRO: não foi possível salvar a forma de pgto da comanda em tela.</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
                                    return false;
                                }
                            }
                            else
                            {
                                ltlMsn.Text = "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                              "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> SalvarDadosComanda-ERRO: não foi possível apagar os registros antigos da forma de pgto da comanda em tela.</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
                                return false;
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                  "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> SalvarDadosComanda-ERRO:" + ex.Message + "</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
                return false;
            }
        }

        protected void ddlProdutos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ltlMsn.Text = string.Empty;
                if (ddlProduto.SelectedIndex > 0)
                {
                    ////RECUPERAR O COOKIE
                    //var idEmpresaContratante = "0";
                    //HttpCookie cookie = Request.Cookies["idEmpresaContratante"];
                    //if (cookie != null && cookie.Value != null)
                    //    idEmpresaContratante = cookie.Value;

                    //idEmpresaContratante = Criptografia.Decrypt(idEmpresaContratante);

                    txtValorProd.Value = string.Format("{0:C}", ProdutoEspBel.PesquisarValor(int.Parse(ddlProduto.SelectedValue)));

                }
                else
                {
                    txtValor.Value = string.Empty;
                }

                var id = Request.QueryString["id"];
                id = Criptografia.Decrypt(id.Replace('_', '+'));
                if (!id.Equals("0"))
                    CarregaTabela(long.Parse(id));
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ddlProduto_SelectedIndexChanged-ERRO:" + ex.Message + "</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }
        }

        protected void ddlFormaPgto_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlFormasDePgto.Visible = ddlFormaPgto.SelectedValue.Equals("6");
            if (ddlFormaPgto.SelectedIndex > 0)
            {
                CarregaTabela(long.Parse(hdfIdComandaTemp.Value));
                CarregaTabelaProdutos(long.Parse(hdfIdComandaTemp.Value));
                CarregaValorCmda(long.Parse(hdfIdComandaTemp.Value));
            }
        }
    }
}