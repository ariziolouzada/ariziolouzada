using CriptografiaSgpm;
using sbessencial_cl;
using System;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;

namespace ariziolouzada.sbessencial.agenda
{
    public partial class newedit : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if (Request.QueryString.HasKeys())
                    {
                        if (Request.QueryString["id"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["id"]))
                        {
                            var id = Request.QueryString["id"];
                            id = Criptografia.Decrypt(id.Replace('_', '+'));

                            CarregaDdlServico(true);

                            if (!id.Equals("0"))
                            {
                                //Edição
                                CarregaDdlCliente(false);
                                CarregaDdlProfissonal(false);
                                CarregaDados(long.Parse(id));
                                lblCabecalho.Text = "Editar Item da Agenda";
                                pnlStatus.Visible = true;
                            }
                            else
                            {
                                //Cadastro 
                                pnlStatus.Visible = false;
                                CarregaDdlCliente(true);
                                CarregaDdlProfissonal(true);

                                hdfIdRegistroAgenda.Value = FluxoCaixa.GerarID();
                                Session["idNovoCliente"] = "0";

                                if (Session["cadAgenda"] != null)
                                    if (Session["cadAgenda"].ToString().Equals("OK"))
                                    {
                                        Session["cadAgenda"] = "";
                                        ltlMsn.Text = "<div class=\"alert alert-block alert-success fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">OK" +
                                                     "</button><p><i class=\"fa fa-thumbs-o-up fa-lg\"></i> Cadastro realizado com sucesso!!!</p></div>";
                                    }

                            }
                        }
                    }

                    // método que apaga do banco todos registros se referencia na tbl_fluxo de caixa e a tbl_fluxo_caixa_servico/produto
                    AgendaServicos.LimpaTabelaRegistrosInvalido();

                }

            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>Page_Load-ERRO:" + ex.Message + "</p></div>";
            }

        }


        private void CarregaDados(long id)
        {
            try
            {

                //erro ao carregar dados
                var agd = Agenda.Pesquisar(id);
                if (agd != null)
                {
                    txtData.Value = agd.DataHora.ToString("yyyy-MM-dd");
                    ddlHora.SelectedValue = agd.DataHora.ToString("HH");
                    ddlMinuto.SelectedValue = agd.DataHora.ToString("mm");
                    ddlCliente.SelectedValue = agd.IdCliente.ToString();
                    txtObservacao.Value = agd.Observacao;
                    //txtTotalCaixa.Value = string.Format("{0:C}", agd.Valor);
                    ddlStatus.SelectedValue = agd.IdStatus.ToString();
                    hdfIdRegistroAgenda.Value = agd.IdRegistroAgenda.ToString();
                    ddlProfissional.SelectedValue = agd.IdProfissional.ToString();
                    CarregaTabelaServicoFluxoCx(agd.IdRegistroAgenda);
                }
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>Page_Load-ERRO:" + ex.Message + "</p></div>";
            }
        }


        private void CarregaDdlServico(bool comSelecione)
        {
            var lista = Servico.Lista(comSelecione, "", 1);
            ddlServico.Items.Clear();
            ddlServico.DataSource = lista;
            ddlServico.DataValueField = "Id";
            ddlServico.DataTextField = "Descricao";
            ddlServico.DataBind();

            if (comSelecione)
                ddlServico.SelectedIndex = 0;
        }


        private void CarregaDdlCliente(bool comSelecione)
        {
            //implementar o Chosen select 
            //template products-WB0R5L90S- form_advanced.html 

            var lista = Cliente.Lista(comSelecione, "");
            ddlCliente.Items.Clear();
            ddlCliente.DataSource = lista;
            ddlCliente.DataValueField = "Id";
            ddlCliente.DataTextField = "Nome";
            ddlCliente.DataBind();

            if (comSelecione)
                ddlCliente.SelectedIndex = 0;
        }


        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            //ver pq não voltou pesquisando pelos parametros
            //Response.Redirect("~/sbessencial/financeiro/caixa");
            if (Session["paremetrosDefault"].ToString() != string.Empty)
            {
                var paremetrosDefault = Session["paremetrosDefault"].ToString();
                Response.Redirect(string.Format("default.aspx?dia={0}", paremetrosDefault));
            }
            else
            {
                Response.Redirect("default.aspx");
            }
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/sbessencial/financeiro/caixa/newedit.aspx?id=X5A1oqTnjBE=");
            Response.Redirect("newedit.aspx?id=X5A1oqTnjBE=");
        }


        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (VerificaCampos())
            {
                if (SalvarDados())
                {
                    var id = Request.QueryString["id"];
                    id = Criptografia.Decrypt(id.Replace('_', '+'));

                    if (id.Equals("0"))
                    {
                        Session["cadAgenda"] = "OK";
                        btnCancelar_Click(sender, e);
                    }
                    else
                    {
                        btnVoltar_Click(sender, e);
                    }
                }
            }
        }


        private void CarregaDdlProfissonal(bool comSelecione)
        {
            var lista = Profissional.Lista(comSelecione);
            ddlProfissional.Items.Clear();
            ddlProfissional.DataSource = lista;
            ddlProfissional.DataValueField = "Id";
            ddlProfissional.DataTextField = "Nome";
            ddlProfissional.DataBind();

            if (comSelecione)
                ddlProfissional.SelectedIndex = 0;
        }


        private bool VerificaCampos()
        {
            try
            {
                var id = Request.QueryString["id"];
                id = Criptografia.Decrypt(id.Replace('_', '+'));

                ltlMsn.Text = string.Empty;
                // Desrição
                if (id.Equals("0"))
                    if (txtData.Value == string.Empty)
                    {
                        txtData.Focus();
                        ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                     "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Informe a DATA.</p></div>";
                        return false;
                    }

                if (id.Equals("0"))
                    if (Session["idNovoCliente"].ToString().Equals("0"))
                        if (ddlCliente.SelectedIndex == 0)
                        {
                            ddlCliente.Focus();
                            ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                         "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Selecione o Cliente.</p></div>";
                            return false;
                        }

                try
                {
                    var data = Convert.ToDateTime(txtData.Value);

                    if (id.Equals("0"))
                    {
                        if (data < DateTime.Now.Date)
                        {
                            txtData.Focus();
                            ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                         "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: DATA INVÁLIDA. A data NÂO pode ser menor que hoje!!</p></div>";
                            return false;
                        }

                        data = Agenda.DataHoraAgenda(txtData.Value, ddlHora.SelectedValue, ddlMinuto.SelectedValue);
                        if (data <= DateTime.Now)
                        {
                            txtData.Focus();
                            ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                         "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: HORÁRIO INVÁLIDO. A HORA NÂO pode ser menor que a atual!!</p></div>";
                            return false;
                        }
                    }
                }
                catch (FormatException fx)
                {
                    txtData.Focus();
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: DATA INVÁLIDA.</p></div>";
                    return false;
                }


                if (id.Equals("0"))
                {
                    if (ddlHora.SelectedIndex == 0)
                    {
                        ddlHora.Focus();
                        ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                     "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Selecione a Hora.</p></div>";
                        return false;
                    }

                    if (ddlMinuto.SelectedIndex == 0)
                    {
                        ddlMinuto.Focus();
                        ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                     "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Selecione o Minuto.</p></div>";
                        return false;
                    }
                }

                var somaSrv = AgendaServicos.SomaValorItensLista(long.Parse(hdfIdRegistroAgenda.Value));
                if (somaSrv == 0)
                {
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                  "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Selecione no mínimo um serviço ou a soma dos serviços é zero.</p></div>";
                    ddlServico.Focus();
                    return false;
                }

                //if (!pnlFormaPgto.Style.Value.Equals("display: none;") )
                //{
                //    if (ddlFormaPgto.SelectedIndex == 0)
                //    {
                //        ddlFormaPgto.Focus();
                //        ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                //                     "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Selecione a Forma de pagamento.</p></div>";
                //        return false;
                //    }
                //}

                return true;
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>VerificaCampos-ERRO:" + ex.Message + "</p></div>";
                return false;
            }
        }


        private bool SalvarDados()
        {
            try
            {
                var agd = new Agenda()
                {
                    IdCliente = ddlCliente.SelectedValue.Equals("0") ? int.Parse(Session["idNovoCliente"].ToString()) : int.Parse(ddlCliente.SelectedValue),
                    DataHora = Agenda.DataHoraAgenda(txtData.Value, ddlHora.SelectedValue, ddlMinuto.SelectedValue),
                    Observacao = txtObservacao.Value,
                    IdStatus = 1,
                    IdProfissional = int.Parse(ddlProfissional.SelectedValue),
                    IdRegistroAgenda = long.Parse(hdfIdRegistroAgenda.Value)
                };

                var id = Request.QueryString["id"];
                id = Criptografia.Decrypt(id.Replace('_', '+'));

                if (id.Equals("0"))
                {
                    //Cadastro
                    if (!Agenda.Inserir(agd))
                    {
                        ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                     "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Não foi possível cadastrar Fluxo do caixa!!!</p></div>";
                        return false;
                    }
                    return true;
                }

                //Edição
                agd.IdRegistroAgenda = long.Parse(hdfIdRegistroAgenda.Value);
                agd.IdStatus = int.Parse(ddlStatus.SelectedValue);
                if (!Agenda.Editar(agd))
                {
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Não foi possível cadastrar este item da Agenda!!!</p></div>";
                    return false;
                }


                //se for EXECUTADO insere o Fluxo de caixa
                if (ddlStatus.SelectedValue.Equals("4"))
                {
                    //VERIFICA SE JÁ EXISTA pra poder salvar o fluxo de caixa
                    if (!FluxoCaixa.Existe(hdfIdRegistroAgenda.Value))
                    {
                        var fc = new FluxoCaixa()
                        {
                            IdFormaPgto = int.Parse(ddlFormaPgto.SelectedValue),
                            IdTipo = 1,
                            IdSaida = 1,
                            IdCliente = int.Parse(ddlCliente.SelectedValue),
                            Data = DateTime.Parse(txtData.Value),
                            LoginCadastro = CookieSbe.Recupera("LoginUserLogado"),
                            Obsercacao = "",
                            IdServico = long.Parse(hdfIdRegistroAgenda.Value)  //FluxoCaixa.GerarID())
                        };

                        //inser os serv~ços da agenda no fluxo de caixa
                        var listaSvcAgenda = AgendaServicos.Lista(fc.IdServico);
                        foreach (var item in listaSvcAgenda)
                        {
                            if (!FluxoCaixaServico.Existe(fc.IdServico, item.IdServico))
                            {
                                var fcs = new FluxoCaixaServico(fc.IdServico, item.IdServico, item.Valor);
                                if (FluxoCaixaServico.Inserir(fcs))
                                {

                                }
                            }
                        }

                        var somaSrv = FluxoCaixaServico.SomaValorItensLista(fc.IdServico);
                        //var somaProd = FluxoCaixaProduto.SomaValorItensLista(fc.IdServico);
                        //var total = somaSrv + somaProd;
                        fc.Valor = somaSrv;

                        //Inserir no banco - Cadastro

                        if (!FluxoCaixa.Inserir(fc))
                        {
                            ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                         "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Não foi possível cadastrar Fluxo do caixa!!!</p></div>";
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>SalvarDados-ERRO:" + ex.Message + "</p></div>";
                return false;
            }
        }


        protected void ddlServico_SelectedIndexChanged(object sender, EventArgs e)
        {
            //terminar colocar pra recever o valor do serviço no txtvalorservico
            if (ddlServico.SelectedIndex > 0)
            {
                var svc = Servico.Pesquisar(int.Parse(ddlServico.SelectedValue));
                txtValorServico.Value = string.Format("{0:C}", svc.Valor);
                //Session["idSevicoSel"] = ddlServico.SelectedValue;
            }
            else
            {
                txtValorServico.Value = string.Empty;
            }
        }


        public void CarregaTabelaServicoFluxoCx(long idServicoFluxoCx)
        {
            try
            {
                //Carrega a tabela do banco
                var listaServicos = AgendaServicos.Lista(idServicoFluxoCx);

                if (listaServicos.Count > 0)
                {
                    var tabelaHtml = new StringBuilder();
                    tabelaHtml.Append("<table id=\"ltlTabelaServicos\" class=\"table table-condensed table-striped table-hover table-bordered pull-left\"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Item</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Descrição</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Valor</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"></th>");

                    tabelaHtml.Append("</tr></thead>");
                    tabelaHtml.Append("<tbody>");

                    decimal total = 0;
                    foreach (var srv in listaServicos)
                    {

                        //tabelaHtml.Append("<tr class=\"odd gradeX\">");
                        tabelaHtml.Append("<tr>");

                        //Posto/Grad                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", srv.Item));
                        //Nome                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", srv.DescricaoServico));

                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format("{0:C}", srv.Valor)));
                        total = total + srv.Valor;
                        // ========= AÇÕES ==========

                        //Excluir  
                        //tabelaHtml.Append(string.Format("<td style=\"text-align: center\"><a href=\"#\" data-toggle=\"modal\" data-target=\"#myModal4\" class=\"btn btn-xs btn-danger\" onclick=\"capturarIdSrvExcluir({0})\" data-original-title=\"\"  title=\"Excluir registro.\" ><i class=\"fa fa-eraser\"></i>  Excluir</a></td>", srv.Id));
                        tabelaHtml.Append(string.Format("<td style=\"text-align: center\"><a href=\"#\" class=\"btn btn-xs btn-danger\" onclick=\"capturarIdSrvExcluir({0})\" data-original-title=\"\"  title=\"Excluir registro.\" ><i class=\"fa fa-eraser\"></i>  Excluir</a></td>", srv.Id));

                        tabelaHtml.Append("</tr>");
                    }
                    // colspan=\"2\"
                    tabelaHtml.Append("<tr class=\"danger\"><td colspan=\"2\" style=\"text-align: right\"><b>Total dos Serviços</b></td><td><b>" + string.Format("{0:C}", total) + "</b></td><td></td></tr>");

                    tabelaHtml.Append("</tbody></table>");

                    ltltabelaServicos.Text = tabelaHtml.ToString();
                }

            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaTabelaServicoFluxoCx-ERRO:" + ex.Message + "</p></div>";
            }

        }



        [WebMethod]
        public static string PesquisaValorServico(string idServico)
        {
            try
            {
                //Insere no banco
                var svc = Servico.Pesquisar(int.Parse(idServico));
                if (svc != null)
                    return string.Format("{0:C}", svc.Valor);
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }



        [WebMethod]
        public static string AddServicoTabela(string idFluxoCaixa, string idServico, string valor)
        {
            try
            {
                //Insere no banco
                var fcs = new AgendaServicos(long.Parse(idFluxoCaixa), int.Parse(idServico), decimal.Parse(valor));
                if (AgendaServicos.Inserir(fcs))
                {
                    //Atualiza a numeração dos itens da tabela
                    AgendaServicos.AtualizaItens(long.Parse(idFluxoCaixa));

                    //Carrega a tabela do banco e envia 
                    return CarregaTabelaServicoFluxoCx(idFluxoCaixa);
                }
                return "<div id=\"tabelaServicos\" class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                        "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>AddServicoTabela-ERRO: Não foi possivel adicionar serviço!!</p></div>";
            }
            catch (Exception ex)
            {
                return "<div id=\"tabelaServicos\" class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                        "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>AddServicoTabela-ERRO:" + ex.Message + "</p></div>";
            }

        }


        [WebMethod]
        public static string ExcluirServicoCaixa(string idFluxoCaixa)
        {
            try
            {
                var idSrvCx = HttpContext.Current.Session["IdSrvExcluir"].ToString();
                //exclui no banco                
                if (AgendaServicos.Excluir(int.Parse(idSrvCx)))
                {
                    //Atualiza a numeração dos itens da tabela
                    AgendaServicos.AtualizaItens(long.Parse(idFluxoCaixa));

                    //Carrega a tabela do banco e envia 
                    return CarregaTabelaServicoFluxoCx(idFluxoCaixa);
                }
                return "<div id=\"tabelaServicos\" class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                        "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>AddServicoTabela-ERRO: Não foi possivel apagar serviço!!</p></div>";
            }
            catch (Exception ex)
            {
                return "<div id=\"tabelaServicos\" class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                        "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ExcluirServicoCaixa-ERRO:" + ex.Message + "</p></div>";
            }

        }


        //[WebMethod]
        //public static string AtualizaTotalCaixa(string idFluxoCaixa)
        //{
        //    try
        //    {
        //        //Soma o valor do Fluxo do Caixa após a ação realizada
        //        return FluxoCaixa.TotalCaixa(long.Parse(idFluxoCaixa));

        //    }
        //    catch (Exception ex)
        //    {
        //        return "<div id=\"tabelaProdutos\" class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
        //                "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ExcluirProdutoCaixa-ERRO:" + ex.Message + "</p></div>";
        //    }

        //}


        [WebMethod]
        public static string CarregaTabelaServicoFluxoCx(string idServicoFluxoCx)
        {
            try
            {
                //Carrega a tabela do banco
                var listaServicos = AgendaServicos.Lista(long.Parse(idServicoFluxoCx));

                if (listaServicos.Count > 0)
                {
                    var tabelaHtml = new StringBuilder();
                    tabelaHtml.Append("<table id=\"tabelaServicos\" class=\"table table-condensed table-striped table-hover table-bordered pull-left\"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Item</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Descrição</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Valor</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"></th>");

                    tabelaHtml.Append("</tr></thead>");
                    tabelaHtml.Append("<tbody>");

                    decimal total = 0;
                    foreach (var srv in listaServicos)
                    {

                        //tabelaHtml.Append("<tr class=\"odd gradeX\">");
                        tabelaHtml.Append("<tr>");

                        //Posto/Grad                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", srv.Item));
                        //Nome                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", srv.DescricaoServico));

                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format("{0:C}", srv.Valor)));
                        total = total + srv.Valor;
                        // ========= AÇÕES ==========

                        //Excluir  
                        //tabelaHtml.Append(string.Format("<td style=\"text-align: center\"><a href=\"#\" data-toggle=\"modal\" data-target=\"#myModal4\" class=\"btn btn-xs btn-danger\" onclick=\"capturarIdSrvExcluir({0})\" data-original-title=\"\"  title=\"Excluir registro.\" ><i class=\"fa fa-eraser\"></i>  Excluir</a></td>", srv.Id));
                        tabelaHtml.Append(string.Format("<td style=\"text-align: center\"><a href=\"#\" class=\"btn btn-xs btn-danger\" onclick=\"capturarIdSrvExcluir({0})\" data-original-title=\"\"  title=\"Excluir registro.\" ><i class=\"fa fa-eraser\"></i>  Excluir</a></td>", srv.Id));

                        tabelaHtml.Append("</tr>");
                    }
                    tabelaHtml.Append("<tr class=\"danger\"><td colspan=\"2\" style=\"text-align: right\"><b>Total dos Serviços</b></td><td colspan=\"2\"><b>" + string.Format("{0:C}", total) + "</b></td></tr>");
                    tabelaHtml.Append("</tbody></table>");

                    return tabelaHtml.ToString();
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "<div id=\"tabelaServicos\" class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                        "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>AddServicoTabela-ERRO:" + ex.Message + "</p></div>";
            }

        }


        [WebMethod]
        public static string CapturarIdSrvExcluir(string id)
        {
            try
            {
                HttpContext.Current.Session["IdSrvExcluir"] = id;
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }

        [WebMethod]
        public static string CadastrarNovoCliente(string nome)
        {
            try
            {

                if (VerificaBtnSalvarNovoCliente(nome))
                {

                    var cliente = new Cliente()
                    {
                        Nome = nome.ToUpper(),
                        LoginCadastro = CookieSbe.Recupera("LoginUserLogado"),
                    };

                    var idCadastrado = Cliente.InserirNomeRetornndoId(cliente);
                    if (idCadastrado > 0)
                    {
                        HttpContext.Current.Session["idNovoCliente"] = idCadastrado.ToString();
                        return idCadastrado.ToString();
                    }

                    //HttpContext.Current.Session["IdSrvExcluir"] = id;
                    return string.Empty;
                }
                else
                {
                    return "-1";
                }

            }
            catch (Exception)
            {
                return string.Empty;
            }

        }

        protected static bool VerificaBtnSalvarNovoCliente(string nome)
        {
            //ltlMsn.Text = string.Empty;            
            var clt = Cliente.Pesquisar(nome);
            if (clt != null)
            {
                //txtNome.Focus();
                return false;
            }
            return true;
        }

        //não está sendo usado
        protected void btnSalvarNovoCliente_Click(object sender, EventArgs e)
        {
            //if (VerificaBtnSalvarNovoCliente())
            {
                var cliente = new Cliente()
                {
                    Nome = txtNome.Value.ToUpper(),
                    LoginCadastro = CookieSbe.Recupera("LoginUserLogado"),
                };

                var idCadastrado = Cliente.InserirNomeRetornndoId(cliente);
                if (idCadastrado > 0)
                {
                    //passar por parametro da url abaixo o id so novo cliente cadastrado pra q ado recarregar já fique selecionado
                    Response.Redirect("newedit.aspx?id=X5A1oqTnjBE=");
                }
            }
        }

        //protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    pnlFormaPgto.Visible = ddlFormaPgto.SelectedValue.Equals("4");            
        //}
    }
}