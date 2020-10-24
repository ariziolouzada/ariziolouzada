using CriptografiaSgpm;
using System;
using System.Web.UI;
using sbessencial_cl;
using System.Web.Services;
using System.Text;
using System.Web;

namespace ariziolouzada.sbessencial.financeiro.caixa
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
                            CarregaDdlProduto(true);

                            if (!id.Equals("0"))
                            {
                                //Edição
                                CarregaDdlCliente(false);
                                CarregaDados(int.Parse(id));
                                //lblCabecalho.Text = "Editar";
                                pnlStatus.Visible = true;
                            }
                            else
                            {
                                //Cadastro 
                                pnlStatus.Visible = false;
                                CarregaDdlCliente(true);

                                hdfIdServicosFluxoCaixa.Value = hdfIdProdutosFluxoCaixa.Value = FluxoCaixa.GerarID();

                                if (Session["cadCaixa"] != null)
                                    if (Session["cadCaixa"].ToString().Equals("OK"))
                                    {
                                        Session["cadCaixa"] = "";
                                        ltlMsn.Text = "<div class=\"alert alert-block alert-success fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">OK" +
                                                     "</button><p><i class=\"fa fa-thumbs-o-up fa-lg\"></i> Cadastro realizado com sucesso!!!</p></div>";
                                    }

                            }
                        }
                    }

                    // método que apaga do banco todos registros se referencia na tbl_fluxo de caixa e a tbl_fluxo_caixa_servico/produto
                    FluxoCaixaServico.LimpaTabelaRegistrosInvalido();
                    FluxoCaixaProduto.LimpaTabelaRegistrosInvalido();

                }

            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>Page_Load-ERRO:" + ex.Message + "</p></div>";
            }

        }


        private void CarregaDados(int id)
        {
            try
            {
                var fc = FluxoCaixa.Pesquisar(id);
                if (fc != null)
                {
                    txtData.Value = fc.Data.ToString("yyyy-MM-dd");
                    ddlCliente.SelectedValue = fc.IdCliente.ToString();
                    ddlTipo.SelectedValue = fc.IdTipo.ToString();

                    txtObservacao.Value = fc.Obsercacao;
                    txtTotalCaixa.Value = string.Format("{0:C}", fc.Valor);
                    ddlStatus.SelectedValue = fc.IdStatus.ToString();
                    CarregaDdlTipoSaida(false);
                    ddlTipoSaida.SelectedValue = fc.IdSaida.ToString();
                    hdfIdServicosFluxoCaixa.Value = hdfIdProdutosFluxoCaixa.Value = fc.IdServico.ToString();

                    pnlTipoSaida.Visible = fc.IdTipo == 2;
                    if (pnlTipoSaida.Visible)
                        txtValorSaida.Value = string.Format("{0:C}", fc.Valor);

                    pnlValorDeposito.Visible = fc.IdTipo == 3;
                    if (pnlValorDeposito.Visible)
                    {
                        txtValorDeposito.Value = string.Format("{0:C}", fc.Valor);
                        ddlBancoDeposito.SelectedValue = fc.IdSaida.ToString();

                        ddlFormaPgtoDeposito.Visible = true;
                        ddlFormaPgto.Visible = false;
                        ddlFormaPgtoDeposito.SelectedValue = fc.IdFormaPgto.ToString();
                    }
                    else
                    {
                        ddlFormaPgto.Visible = true;
                        ddlFormaPgto.SelectedValue = fc.IdFormaPgto.ToString();
                    }

                    pnlTotalCx.Visible = !pnlValorDeposito.Visible;

                    pnlServico.Visible = fc.IdTipo == 1;
                    if (pnlServico.Visible)
                        CarregaTabelaServicoFluxoCx(fc.IdServico);

                    pnlProduto.Visible = fc.IdTipo == 1;
                    if (pnlProduto.Visible)
                        CarregaTabelaProdutoFluxoCx(fc.IdServico);


                }
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>Page_Load-ERRO:" + ex.Message + "</p></div>";
            }
        }


        private void CarregaDdlTipoSaida(bool comSelecione)
        {
            var lista = TipoSaida.Lista(comSelecione, "");
            ddlTipoSaida.Items.Clear();
            ddlTipoSaida.DataSource = lista;
            ddlTipoSaida.DataValueField = "Id";
            ddlTipoSaida.DataTextField = "Descricao";
            ddlTipoSaida.DataBind();

            if (comSelecione)
                ddlTipoSaida.SelectedIndex = 0;
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


        private void CarregaDdlProduto(bool comSelecione)
        {
            var lista = Produto.Lista(comSelecione, "", 1);
            ddlProduto.Items.Clear();
            ddlProduto.DataSource = lista;
            ddlProduto.DataValueField = "Id";
            ddlProduto.DataTextField = "Descricao";
            ddlProduto.DataBind();

            if (comSelecione)
                ddlServico.SelectedIndex = 0;
        }


        private void CarregaDdlCliente(bool comSelecione)
        {
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
                var paremetrosDefault = Session["paremetrosDefault"].ToString().Split(';');
                var mes = int.Parse(paremetrosDefault[0]);
                var ano = int.Parse(paremetrosDefault[1]);
                var tipo = int.Parse(paremetrosDefault[2]);
                Response.Redirect(string.Format("default.aspx?mes={0}&ano={1}&tipo={2}", mes, ano, tipo));
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
                        Session["cadCaixa"] = "OK";
                        btnCancelar_Click(sender, e);
                    }
                    else
                    {
                        btnVoltar_Click(sender, e);
                    }
                }
            }
        }


        private void LimparCampos()
        {
            txtData.Value = string.Empty;
            txtObservacao.Value = string.Empty;
            //txtValor.Value = string.Empty;
            ddlCliente.SelectedIndex = 0;
            ddlFormaPgto.SelectedIndex = 0;
            ddlTipo.SelectedIndex = 0;
            if (pnlTipoSaida.Visible)
                ddlTipoSaida.SelectedIndex = 0;

            pnlTipoSaida.Visible = false;
        }


        private bool VerificaCampos()
        {
            try
            {
                var id = Request.QueryString["id"];
                id = Criptografia.Decrypt(id.Replace('_', '+'));


                ltlMsn.Text = string.Empty;
                // Desrição
                if (txtData.Value == string.Empty)
                {
                    txtData.Focus();
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Informe a DATA.</p></div>";
                    return false;
                }

                // Preço de custo
                if (id.Equals("0"))
                {
                    if (ddlFormaPgto.Visible)
                        if (ddlFormaPgto.SelectedIndex == 0)
                        {
                            ddlFormaPgto.Focus();
                            ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                         "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Selecione a forma de Pagamento.</p></div>";
                            return false;
                        }

                    if (ddlFormaPgtoDeposito.Visible)
                    {
                        if (ddlFormaPgtoDeposito.SelectedIndex == 0)
                        {
                            ddlFormaPgtoDeposito.Focus();
                            ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                         "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Selecione a forma de Pagamento.</p></div>";
                            return false;
                        }

                        if (ddlBancoDeposito.SelectedIndex == 0)
                        {
                            ddlBancoDeposito.Focus();
                            ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                         "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Selecione o Banco de Depósito.</p></div>";
                            return false;
                        }
                    }
                }
                //// Qtde Estoque


                //if (ddlTipo.SelectedIndex == 0)
                //{
                //    ddlTipo.Focus();
                //    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                //                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Selecione o tipo.</p></div>";
                //    return false;
                //}s

                if (id.Equals("0"))
                    if (ddlCliente.SelectedIndex == 0)
                    {
                        ddlCliente.Focus();
                        ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                     "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Selecione o Cliente.</p></div>";
                        return false;
                    }

                if (pnlTipoSaida.Visible)
                {
                    if (txtValorSaida.Value == string.Empty)
                    {
                        txtValorSaida.Focus();
                        ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                     "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Informe o VALOR.</p></div>";
                        return false;
                    }

                    try
                    {
                        var vlr = Convert.ToDecimal(txtValorSaida.Value.Replace("R$", ""));
                    }
                    catch (FormatException fx)
                    {
                        txtValorSaida.Focus();
                        ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                     "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Valor Inválido.</p></div>";
                        return false;
                    }
                }
                else
                {
                    if (ddlTipo.SelectedValue.Equals("1"))
                    {
                        var somaSrv = FluxoCaixaServico.SomaValorItensLista(long.Parse(hdfIdServicosFluxoCaixa.Value));
                        var somaProd = FluxoCaixaProduto.SomaValorItensLista(long.Parse(hdfIdProdutosFluxoCaixa.Value));
                        var total = somaSrv + somaProd;
                        if (total == 0)
                        {
                            ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                          "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Selecione no mínimo um serviço.</p></div>";
                            ddlServico.Focus();
                            return false;
                        }
                    }
                }

                if (pnlValorDeposito.Visible)
                {
                    if (txtValorDeposito.Value == string.Empty)
                    {
                        txtValorDeposito.Focus();
                        ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                     "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Informe o VALOR do Depósito.</p></div>";
                        return false;
                    }

                    try
                    {
                        var vlr = Convert.ToDecimal(txtValorDeposito.Value.Replace("R$", ""));
                    }
                    catch (FormatException fx)
                    {
                        txtValorSaida.Focus();
                        ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                     "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Valor doDepósito Inválido.</p></div>";
                        return false;
                    }
                }

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

                var fc = new FluxoCaixa()
                {
                    IdFormaPgto = int.Parse(ddlFormaPgto.SelectedValue),
                    IdTipo = int.Parse(ddlTipo.SelectedValue),
                    IdCliente = int.Parse(ddlCliente.SelectedValue),
                    Data = DateTime.Parse(txtData.Value),
                    LoginCadastro = CookieSbe.Recupera("LoginUserLogado"),
                    Obsercacao = txtObservacao.Value,
                    IdServico = long.Parse(hdfIdServicosFluxoCaixa.Value)
                };


                //verificar o campo valor vindo do txt certo

                if (pnlTipoSaida.Visible)
                {
                    fc.Valor = decimal.Parse(txtValorSaida.Value.Replace("R$", ""));
                }
                else
                {
                    if (ddlTipo.SelectedValue.Equals("1"))
                    {
                        //Tipo Entrada
                        var somaSrv = FluxoCaixaServico.SomaValorItensLista(long.Parse(hdfIdServicosFluxoCaixa.Value));
                        var somaProd = FluxoCaixaProduto.SomaValorItensLista(long.Parse(hdfIdProdutosFluxoCaixa.Value));
                        var total = somaSrv + somaProd;
                        fc.Valor = total;//decimal.Parse(txtValor.Value.Replace("R$", ""))
                    }

                    if (ddlTipo.SelectedValue.Equals("3"))
                    {
                        //Depósitro
                        fc.Valor = decimal.Parse(txtValorDeposito.Value.Replace("R$", ""));
                        fc.IdFormaPgto = int.Parse(ddlFormaPgtoDeposito.SelectedValue);
                    }
                }

                var id = Request.QueryString["id"];
                id = Criptografia.Decrypt(id.Replace('_', '+'));

                fc.IdSaida = ddlTipo.SelectedValue.Equals("1") ? 1 : int.Parse(ddlTipoSaida.SelectedValue);
                //fc.IdServico = pnlServico.Visible ? int.Parse(ddlServico.SelectedValue) : 0;

                if (ddlTipo.SelectedValue.Equals("3"))
                    fc.IdSaida = int.Parse(ddlBancoDeposito.SelectedValue);


                if (id.Equals("0"))
                {
                    //Cadastro
                    if (!FluxoCaixa.Inserir(fc))
                    {
                        ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                     "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Não foi possível cadastrar Fluxo do caixa!!!</p></div>";
                        return false;
                    }
                    else
                    {
                        //Atualiza o estoque do produto
                        var listaProdutos = FluxoCaixaProduto.Lista(fc.IdServico);
                        var index = 0;
                        foreach (var prod in listaProdutos)
                        {
                            if (Produto.AtualizaEstoque(prod.IdProduto, prod.Qtde))
                                index++;
                        }

                        if (index != listaProdutos.Count)
                        {
                            ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                     "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Houve um erro na atualização do estoque dos produtos!!!</p></div>";
                            return false;
                        }
                    }
                    return true;
                }

                //Edição
                fc.Id = int.Parse(id);
                fc.IdStatus = int.Parse(ddlStatus.SelectedValue);
                if (!FluxoCaixa.Editar(fc))
                {
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Não foi possível cadastrar Fluxo do caixa!!!</p></div>";
                    return false;
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


        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {

            var id = Request.QueryString["id"];
            id = Criptografia.Decrypt(id.Replace('_', '+'));

            ltlMsn.Text = string.Empty;

            ddlFormaPgtoDeposito.Visible = ddlTipo.SelectedValue.Equals("3");
            ddlFormaPgto.Visible = !ddlFormaPgtoDeposito.Visible;

            if (ddlTipo.SelectedValue.Equals("1"))
            {
                ddlCliente.SelectedIndex = 0;
                ddlTipoSaida.SelectedIndex = 0;
                txtValorSaida.Value = string.Empty;

                pnlProduto.Visible = pnlServico.Visible = true;
                pnlTipoSaida.Visible = false;

                CarregaDdlServico(id.Equals("0"));
            }

            if (ddlTipo.SelectedValue.Equals("2"))
            {
                CarregaDdlTipoSaida(id.Equals("0"));
                ddlCliente.SelectedValue = "8";//move o cliente para o SALAO
            }
            
            pnlTipoSaida.Visible = ddlTipo.SelectedValue.Equals("2");
            pnlTotalCx.Visible = pnlProduto.Visible = pnlServico.Visible = ddlTipo.SelectedValue.Equals("1");
            pnlValorDeposito.Visible = ddlTipo.SelectedValue.Equals("3");
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
                var listaServicos = FluxoCaixaServico.Lista(idServicoFluxoCx);

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


        public void CarregaTabelaProdutoFluxoCx(long idProdutoFluxoCx)
        {
            try
            {
                //Carrega a tabela do banco
                var listaProdutos = FluxoCaixaProduto.Lista(idProdutoFluxoCx);

                if (listaProdutos.Count > 0)
                {
                    var tabelaHtml = new StringBuilder();
                    tabelaHtml.Append("<table id=\"ltlTabelaProdutos\" class=\"table table-condensed table-striped table-hover table-bordered pull-left\"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Item</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Descrição</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Valor</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Qtde</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Total</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"></th>");

                    tabelaHtml.Append("</tr></thead>");
                    tabelaHtml.Append("<tbody>");

                    decimal total = 0;
                    foreach (var prod in listaProdutos)
                    {

                        //tabelaHtml.Append("<tr class=\"odd gradeX\">");
                        tabelaHtml.Append("<tr>");

                        //Posto/Grad                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", prod.Item));
                        //Nome                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left;\" >{0}</td>", prod.DescricaoProduto));
                        //valor
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", string.Format("{0:C}", prod.Valor)));
                        //Qtde                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", prod.Qtde));
                        //Total
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", string.Format("{0:C}", prod.Total)));

                        total = total + prod.Total;
                        // ========= AÇÕES ==========

                        //Excluir  
                        //tabelaHtml.Append(string.Format("<td style=\"text-align: center\"><a href=\"#\" data-toggle=\"modal\" data-target=\"#myModal4\" class=\"btn btn-xs btn-danger\" onclick=\"capturarIdSrvExcluir({0})\" data-original-title=\"\"  title=\"Excluir registro.\" ><i class=\"fa fa-eraser\"></i>  Excluir</a></td>", srv.Id));
                        tabelaHtml.Append(string.Format("<td style=\"text-align: center\"><a href=\"#\" class=\"btn btn-xs btn-danger\" onclick=\"capturarIdProdExcluir({0})\" data-original-title=\"\"  title=\"Excluir registro.\" ><i class=\"fa fa-eraser\"></i>  Excluir</a></td>", prod.Id));

                        tabelaHtml.Append("</tr>");
                    }
                    // colspan=\"2\"
                    tabelaHtml.Append("<tr class=\"danger\"><td colspan=\"2\" style=\"text-align: right\"><b>Total dos Produtos</b></td><td><b>" + string.Format("{0:C}", total) + "</b></td><td></td><td></td></tr>");

                    tabelaHtml.Append("</tbody></table>");

                    ltlTabelaProdutos.Text = tabelaHtml.ToString();
                }

            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaTabelaProdutoFluxoCx-ERRO:" + ex.Message + "</p></div>";
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
        public static string PesquisaValorProduto(string idProd)
        {
            try
            {
                //Insere no banco
                var prod = Produto.Pesquisar(int.Parse(idProd));
                if (prod != null)
                    return string.Format("{0:C}", prod.PrecoVenda);
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
                var fcs = new FluxoCaixaServico(long.Parse(idFluxoCaixa), int.Parse(idServico), decimal.Parse(valor));
                if (FluxoCaixaServico.Inserir(fcs))
                {
                    //Atualiza a numeração dos itens da tabela
                    FluxoCaixaServico.AtualizaItens(long.Parse(idFluxoCaixa));

                    //Atualiza o valor doi Fluxo do Caixa após a ação realizada
                    if (FluxoCaixa.Existe(idFluxoCaixa))
                        FluxoCaixa.AtualizaValor(long.Parse(idFluxoCaixa));

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
        public static string AddProdutoTabela(string idFluxoCaixa, string idProd, string valor, string qtde)
        {
            try
            {
                //Insere no banco
                var fcs = new FluxoCaixaProduto(long.Parse(idFluxoCaixa), int.Parse(idProd), decimal.Parse(valor), int.Parse(qtde));
                if (FluxoCaixaProduto.Inserir(fcs))
                {
                    //Atualiza a numeração dos itens da tabela
                    FluxoCaixaProduto.AtualizaItens(long.Parse(idFluxoCaixa));

                    //Atualiza o valor doi Fluxo do Caixa após a ação realizada
                    if (FluxoCaixa.Existe(idFluxoCaixa))
                        FluxoCaixa.AtualizaValor(long.Parse(idFluxoCaixa));

                    //Carrega a tabela do banco e envia 
                    return CarregaTabelaProdutoFluxoCx(idFluxoCaixa);
                }
                return "<div id=\"tabelaServicos\" class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                        "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>AddProdutoTabela-ERRO: Não foi possivel adicionar serviço!!</p></div>";
            }
            catch (Exception ex)
            {
                return "<div id=\"tabelaServicos\" class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                        "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>AddProdutoTabela-ERRO:" + ex.Message + "</p></div>";
            }

        }


        [WebMethod]
        public static string ExcluirServicoCaixa(string idFluxoCaixa)
        {
            try
            {
                var idSrvCx = HttpContext.Current.Session["IdSrvExcluir"].ToString();
                //exclui no banco                
                if (FluxoCaixaServico.Excluir(int.Parse(idSrvCx)))
                {
                    //Atualiza a numeração dos itens da tabela
                    FluxoCaixaServico.AtualizaItens(long.Parse(idFluxoCaixa));

                    //Atualiza o valor doi Fluxo do Caixa após a ação realizada
                    FluxoCaixa.AtualizaValor(long.Parse(idFluxoCaixa));

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


        [WebMethod]
        public static string ExcluirProdutoCaixa(string idFluxoCaixa)
        {
            try
            {
                var idProdCx = HttpContext.Current.Session["IdProdExcluir"].ToString();
                //exclui no banco                
                if (FluxoCaixaProduto.Excluir(int.Parse(idProdCx)))
                {
                    //Atualiza a numeração dos itens da tabela
                    FluxoCaixaProduto.AtualizaItens(long.Parse(idFluxoCaixa));

                    //Atualiza o valor doi Fluxo do Caixa após a ação realizada
                    FluxoCaixa.AtualizaValor(long.Parse(idFluxoCaixa));

                    //Carrega a tabela do banco e envia 
                    return CarregaTabelaProdutoFluxoCx(idFluxoCaixa);
                }
                return "<div id=\"tabelaProdutos\" class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                        "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>AddProdutoTabela-ERRO: Não foi possivel apagar o produto!!</p></div>";
            }
            catch (Exception ex)
            {
                return "<div id=\"tabelaProdutos\" class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                        "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ExcluirProdutoCaixa-ERRO:" + ex.Message + "</p></div>";
            }

        }


        [WebMethod]
        public static string AtualizaTotalCaixa(string idFluxoCaixa)
        {
            try
            {
                //Soma o valor do Fluxo do Caixa após a ação realizada
                return FluxoCaixa.TotalCaixa(long.Parse(idFluxoCaixa));

            }
            catch (Exception ex)
            {
                return "<div id=\"tabelaProdutos\" class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                        "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ExcluirProdutoCaixa-ERRO:" + ex.Message + "</p></div>";
            }

        }


        [WebMethod]
        public static string CarregaTabelaServicoFluxoCx(string idServicoFluxoCx)
        {
            try
            {
                //Carrega a tabela do banco
                var listaServicos = FluxoCaixaServico.Lista(long.Parse(idServicoFluxoCx));

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
        public static string CarregaTabelaProdutoFluxoCx(string idProdutoFluxoCx)
        {
            try
            {
                //Carrega a tabela do banco
                var listaServicos = FluxoCaixaProduto.Lista(long.Parse(idProdutoFluxoCx));

                if (listaServicos.Count > 0)
                {
                    var tabelaHtml = new StringBuilder();
                    tabelaHtml.Append("<table id=\"tabelaProdutos\" class=\"table table-condensed table-striped table-hover table-bordered pull-left\"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Item</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Descrição</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Valor</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Qtde</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Total</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"></th>");

                    tabelaHtml.Append("</tr></thead>");
                    tabelaHtml.Append("<tbody>");

                    decimal total = 0;
                    foreach (var prod in listaServicos)
                    {

                        //tabelaHtml.Append("<tr class=\"odd gradeX\">");
                        tabelaHtml.Append("<tr>");

                        //Posto/Grad                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", prod.Item));
                        //Nome                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left;\" >{0}</td>", prod.DescricaoProduto));
                        //valor
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", string.Format("{0:C}", prod.Valor)));
                        //Qtde                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", prod.Qtde));
                        //Total
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", string.Format("{0:C}", prod.Total)));

                        total = total + prod.Total;
                        // ========= AÇÕES ==========

                        //Excluir  
                        //tabelaHtml.Append(string.Format("<td style=\"text-align: center\"><a href=\"#\" data-toggle=\"modal\" data-target=\"#myModal4\" class=\"btn btn-xs btn-danger\" onclick=\"capturarIdSrvExcluir({0})\" data-original-title=\"\"  title=\"Excluir registro.\" ><i class=\"fa fa-eraser\"></i>  Excluir</a></td>", srv.Id));
                        tabelaHtml.Append(string.Format("<td style=\"text-align: center\"><a href=\"#\" class=\"btn btn-xs btn-danger\" onclick=\"capturarIdProdExcluir({0})\" data-original-title=\"\"  title=\"Excluir registro.\" ><i class=\"fa fa-eraser\"></i>  Excluir</a></td>", prod.Id));

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
                        "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>AddprodutoTabela-ERRO:" + ex.Message + "</p></div>";
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
        public static string CapturarIdProdExcluir(string id)
        {
            try
            {
                HttpContext.Current.Session["IdProdExcluir"] = id;

                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }



    }
}