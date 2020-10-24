using CriptografiaSgpm;
using ferrita_Cl;
using System;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;

namespace ariziolouzada.ferrita.colaborador
{
    public partial class neweditcomanda : Page
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

                            CarregaDados(int.Parse(id));

                        }

                        if (Request.QueryString["nc"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["nc"]))
                        {
                            var nc = Request.QueryString["nc"];
                            nc = Criptografia.Decrypt(nc.Replace('_', '+'));

                            pnlStatus.Visible = !nc.Equals("0");

                            if (!nc.Equals("0"))
                            {
                                CarregaDadosCmda(long.Parse(nc));

                            }
                            else
                            {
                                CarregaDdlFormaPgto(true);
                            }
                        }

                    }

                    // CarregaDados();

                    //Apaga tos os registros da tabela com a situação igual a 0-Temporario
                    ColaboradorComandaItem.ApagarItensTemporarios();

                }

            }
            catch (Exception ex)
            {
                ltlMsn.Text = string.Format("<div class=\"alert alert-danger alert-dismissible\">" +
                                            "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">X</button>" +
                                            "<h4><i class=\"icon fa fa-ban\"></i> Page_Load-Erro:</h4>{0}</div>", ex.Message);
            }
        }

        private void CarregaDados(int id)
        {

            var colab = Colaborador.PesquisaPeloId(id);
            lblNomeColaborador.Text = colab.Nome;

            hdfIdColaborador.Value = id.ToString();

            if (txtNumeroCmda.Value == string.Empty)
                txtNumeroCmda.Value = GeraNumeroCmda();


            txtData.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void CarregaDadosCmda(long numCmda)
        {
            var cmda = ColaboradorComanda.Pesquisa(numCmda);

            txtNumeroCmda.Value = cmda.NumeroComanda.ToString();
            txtValorTotal.Value = cmda.ValorTotal.ToString("#,##0.00");

            CarregaDdlFormaPgto(false);
            ddlFormaPgto.SelectedValue = cmda.IdFormaPgto.ToString();
            ddlStatus.SelectedValue = cmda.IdStatus.ToString();

            CarregaTabela();
            pnlProdutosSelecionados.Visible = true;
        }

        private string GeraNumeroCmda()
        {
            var numero = string.Format("{0}", DateTime.Now.ToString("yyyyMMddHHmmss"));
            return numero;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("listacmdacolab.aspx?id=" + Request.QueryString["id"]);
        }


        private void CarregaDdlFormaPgto(bool comSelecione)
        {
            var lista = ComandaFormaPgto.ListaDdl(comSelecione);
            ddlFormaPgto.Items.Clear();
            ddlFormaPgto.DataSource = lista;
            ddlFormaPgto.DataValueField = "Id";
            ddlFormaPgto.DataTextField = "Descricao";
            ddlFormaPgto.DataBind();

            if (comSelecione)
                ddlFormaPgto.SelectedIndex = 0;
        }

        private void CarregaDdlFornecedor(bool comSelecione)
        {
            var lista = ServicoProduto.ListaDdlServicoProduto(comSelecione);
            ddlProduto.Items.Clear();
            ddlProduto.DataSource = lista;
            ddlProduto.DataValueField = "IdProduto";
            ddlProduto.DataTextField = "DescricaoProduto";
            ddlProduto.DataBind();

            if (comSelecione)
                ddlProduto.SelectedIndex = 0;
        }


        private ColaboradorComanda CarregaClasseCmda()
        {
            //instancia a classe da comanda e carregaas propriedades
            var cmda = new ColaboradorComanda();
            cmda.NumeroComanda = long.Parse(txtNumeroCmda.Value);
            cmda.IdColaborador = int.Parse(hdfIdColaborador.Value);
            cmda.DataCmda = DateTime.Parse(txtData.Value);
            cmda.ValorTotal = decimal.Parse(txtValorTotal.Value);
            cmda.IdFormaPgto = int.Parse(ddlFormaPgto.SelectedValue);
            return cmda;
        }


        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            var nc = Request.QueryString["nc"];
            nc = Criptografia.Decrypt(nc.Replace('_', '+'));

            var cmda = CarregaClasseCmda();

            if (nc.Equals("0"))
            {
                if (ColaboradorComanda.Inserir(cmda))
                {
                    //validar os itens
                    if (!ColaboradorComandaItem.ValidarItensTemporarios(cmda.NumeroComanda))
                    {
                        ltlMsn.Text = string.Format("<div class=\"alert alert-danger alert-dismissible\">" +
                                               "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">X</button>" +
                                               "<h4><i class=\"icon fa fa-ban\"></i> Atenção:</h4> Não foi possível validar os itens da comanda.</div>");

                        ColaboradorComanda.Apagar(cmda.NumeroComanda);
                    }

                    //Atualuzar o estoque 
                    var listaItensCmda = ColaboradorComandaItem.Lista(cmda.NumeroComanda);

                    var qtdeItensAtualizado = 0;
                    foreach (var item in listaItensCmda)
                    {
                        var svcProd = new ServicoProduto();
                        svcProd.IdProduto = item.IdProduto;
                        svcProd.TamanhoUnico = item.TamanhoUnico;
                        svcProd.QtdeEstoque = item.Qtde;
                        svcProd.QtdeEstqTamanhoP = item.QtdeTamanhoP;
                        svcProd.QtdeEstqTamanhoM = item.QtdeTamanhoM;
                        svcProd.QtdeEstqTamanhoG = item.QtdeTamanhoG;
                        svcProd.QtdeEstqTamanhoGG = item.QtdeTamanhoGG;
                        svcProd.QtdeEstqTamanhoEG = item.QtdeTamanhoEG;

                        if (ServicoProduto.AtualizarEstoque(svcProd))
                        {

                            ++qtdeItensAtualizado;
                        }
                    }

                    if (qtdeItensAtualizado != listaItensCmda.Count)
                    {
                        ltlMsn.Text = string.Format("<div class=\"alert alert-danger alert-dismissible\">" +
                                      "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">X</button>" +
                                      "<h4><i class=\"icon fa fa-ban\"></i> Atenção:</h4> Houve um ERRO a atualização do estoque dos itens desta comanda.</div>");
                    }
                }

            }
            else
            {
                cmda.IdStatus = int.Parse(ddlStatus.SelectedValue);
                ColaboradorComanda.Update(cmda);
            }

            btnCancelar_Click(sender, e);
        }


        protected void btnAdicionarProduto_Click(object sender, EventArgs e)
        {
            CarregaDdlFornecedor(true);
            txtQtdeUnica.Value = string.Empty;
            pnlAdicionarProduto.Visible = true;

            if (ltlTabela.Text == string.Empty)
                pnlProdutosSelecionados.Visible = false;
        }


        private bool VerificaCampoBtnAddProdSel()
        {

            ltlMsn.Text = string.Empty;

            if (ddlProduto.SelectedIndex == 0)
            {
                ltlMsn.Text = string.Format("<div class=\"alert alert-danger alert-dismissible\">" +
                                            "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">X</button>" +
                                            "<h4><i class=\"icon fa fa-ban\"></i> Atenção:</h4> Selecione o Produto.</div>");
                ddlProduto.Focus();
                return false;
            }

            if (hdfTamUnico.Value.Equals("SIM"))
            {
                if (txtQtdeUnica.Value == string.Empty)
                {
                    ltlMsn.Text = string.Format("<div class=\"alert alert-danger alert-dismissible\">" +
                                                "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">X</button>" +
                                                "<h4><i class=\"icon fa fa-ban\"></i> Erro:</h4> Digite a Qtde do Produto selecionado.</div>");
                    txtQtdeUnica.Focus();
                    return false;
                }

                var qtdEstoque = int.Parse(hdfQtdeProduto.Value);
                var qtdProdSel = int.Parse(txtQtdeUnica.Value);
                if (qtdProdSel > qtdEstoque)
                {
                    ltlMsn.Text = string.Format("<div class=\"alert alert-danger alert-dismissible\">" +
                                                                    "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">X</button>" +
                                                                    "<h4><i class=\"icon fa fa-ban\"></i> Erro:</h4> Qtde do Produto selecionado insuficiente. " +
                                                                    string.Format("O mesmo só possui {0} unidade(s) disponíveis.</div>", qtdEstoque));
                    txtQtdeUnica.Focus();
                    return false;
                }
            }
            else
            {
                if (txtQtdTamP.Value.Equals("") && txtQtdTamM.Value.Equals("") &&
                    txtQtdTamG.Value.Equals("") && txtQtdTamGG.Value.Equals("") &&
                    txtQtdTamEG.Value.Equals("")
                    )
                {
                    ltlMsn.Text = string.Format("<div class=\"alert alert-danger alert-dismissible\">" +
                                                                    "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">X</button>" +
                                                                    "<h4><i class=\"icon fa fa-ban\"></i> Erro:</h4> Digite ao menos ma Qtde do Produto selecionado.</div>");
                    txtQtdTamP.Focus();
                    return false;
                }
                //Qtde em estoque
                var arrayQtdEst = hdfQtdeProduto.Value.Split(';');
                var qtdEstqTamP = int.Parse(arrayQtdEst[0]);
                var qtdEstqTamM = int.Parse(arrayQtdEst[1]);
                var qtdEstqTamG = int.Parse(arrayQtdEst[2]);
                var qtdEstqTamGG = int.Parse(arrayQtdEst[3]);
                var qtdEstqTamEG = int.Parse(arrayQtdEst[4]);

                //qtde solicitada
                var qtdTamP = txtQtdTamP.Value.Equals("") ? 0 : int.Parse(txtQtdTamP.Value);
                var qtdTamM = txtQtdTamM.Value.Equals("") ? 0 : int.Parse(txtQtdTamM.Value);
                var qtdTamG = txtQtdTamG.Value.Equals("") ? 0 : int.Parse(txtQtdTamG.Value);
                var qtdTamGG = txtQtdTamGG.Value.Equals("") ? 0 : int.Parse(txtQtdTamGG.Value);
                var qtdTamEG = txtQtdTamEG.Value.Equals("") ? 0 : int.Parse(txtQtdTamEG.Value);

                if (qtdTamP > qtdEstqTamP)
                {
                    ltlMsn.Text = string.Format("<div class=\"alert alert-danger alert-dismissible\">" +
                                  "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">X</button>" +
                                  "<h4><i class=\"icon fa fa-ban\"></i> Erro:</h4> Qtde do tamanho 'P' do Produto selecionado insuficiente. " +
                                  string.Format("O mesmo só possui {0} unidade(s) disponíveis.</div>", qtdEstqTamP));
                    txtQtdTamP.Focus();
                    return false;
                }

                if (qtdTamM > qtdEstqTamM)
                {
                    ltlMsn.Text = string.Format("<div class=\"alert alert-danger alert-dismissible\">" +
                                  "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">X</button>" +
                                  "<h4><i class=\"icon fa fa-ban\"></i> Erro:</h4> Qtde do tamanho 'M' do Produto selecionado insuficiente. " +
                                  string.Format("O mesmo só possui {0} unidade(s) disponíveis.</div>", qtdEstqTamM));
                    txtQtdTamM.Focus();
                    return false;
                }

                if (qtdTamG > qtdEstqTamG)
                {
                    ltlMsn.Text = string.Format("<div class=\"alert alert-danger alert-dismissible\">" +
                                  "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">X</button>" +
                                  "<h4><i class=\"icon fa fa-ban\"></i> Erro:</h4> Qtde do tamanho 'G' do Produto selecionado insuficiente. " +
                                  string.Format("O mesmo só possui {0} unidade(s) disponíveis.</div>", qtdEstqTamG));
                    txtQtdTamG.Focus();
                    return false;
                }

                if (qtdTamGG > qtdEstqTamGG)
                {
                    ltlMsn.Text = string.Format("<div class=\"alert alert-danger alert-dismissible\">" +
                                  "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">X</button>" +
                                  "<h4><i class=\"icon fa fa-ban\"></i> Erro:</h4> Qtde do tamanho 'GG' do Produto selecionado insuficiente. " +
                                  string.Format("O mesmo só possui {0} unidade(s) disponíveis.</div>", qtdEstqTamGG));
                    txtQtdTamGG.Focus();
                    return false;
                }

                if (qtdTamEG > qtdEstqTamEG)
                {
                    ltlMsn.Text = string.Format("<div class=\"alert alert-danger alert-dismissible\">" +
                                  "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">X</button>" +
                                  "<h4><i class=\"icon fa fa-ban\"></i> Erro:</h4> Qtde do tamanho 'EG' do Produto selecionado insuficiente. " +
                                  string.Format("O mesmo só possui {0} unidade(s) disponíveis.</div>", qtdEstqTamEG));
                    txtQtdTamEG.Focus();
                    return false;
                }

            }


            return true;
        }


        protected void AtualizarItensCmda()
        {
            try
            {
                var total = (decimal)0;
                var numeroCmda = long.Parse(txtNumeroCmda.Value);
                var lista = ColaboradorComandaItem.Lista(numeroCmda);

                for (int i = 0; i < lista.Count; i++)
                {
                    //editar o numero do item
                    ColaboradorComandaItem.EditarNumeroItem(numeroCmda, lista[i].Item, i + 1);

                    //somatoria valor totalk
                    total = total + lista[i].ValorTotal;
                }


                txtValorTotal.Value = total.ToString("#,##0.00");
                //foreach (var item in lista)
                //{

                //}

            }
            catch (Exception ex)
            {
                ltlMsn.Text = string.Format("<div class=\"alert alert-danger alert-dismissible\">" +
                                            "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">X</button>" +
                                            "<h4><i class=\"icon fa fa-ban\"></i> Page_Load-Erro:</h4>{0}</div>", ex.Message);
            }
        }


        protected void btnAddProdSel_Click(object sender, EventArgs e)
        {
            if (VerificaCampoBtnAddProdSel())
            {
                if (SalvarProdutoSelecionado())
                {
                    var total = decimal.Parse(hdfValorTotal.Value);
                    var totalProd = txtValorTotal.Value.Equals("") ? (decimal)0 : decimal.Parse(txtValorTotal.Value);
                    var somaTotais = totalProd + total;
                    txtValorTotal.Value = somaTotais.ToString("#,##0.00");
                    hdfQtdeProduto.Value = "0";
                    //Carregar a tabela com os produto q foi seleciona e 
                    CarregaTabela();


                    pnlAdicionarProduto.Visible = false;
                    pnlProdutosSelecionados.Visible = true;
                }
            }
        }


        private bool SalvarProdutoSelecionado()
        {
            try
            {
                var prodSel = new ColaboradorComandaItem();

                prodSel.NumeroComanda = long.Parse(txtNumeroCmda.Value);
                prodSel.IdProduto = int.Parse(ddlProduto.SelectedValue);
                prodSel.IdSituacao = 0;//para produtosselscionados, qdo salvar muda para 1

                if (hdfTamUnico.Value.Equals("SIM"))
                {
                    prodSel.Qtde = int.Parse(txtQtdeUnica.Value);
                }
                else
                {
                    var qtdTamP = txtQtdTamP.Value.Equals("") ? 0 : int.Parse(txtQtdTamP.Value);
                    var qtdTamM = txtQtdTamM.Value.Equals("") ? 0 : int.Parse(txtQtdTamM.Value);
                    var qtdTamG = txtQtdTamG.Value.Equals("") ? 0 : int.Parse(txtQtdTamG.Value);
                    var qtdTamGG = txtQtdTamGG.Value.Equals("") ? 0 : int.Parse(txtQtdTamGG.Value);
                    var qtdTamEG = txtQtdTamEG.Value.Equals("") ? 0 : int.Parse(txtQtdTamEG.Value);

                    prodSel.Qtde = qtdTamP + qtdTamM + qtdTamG + qtdTamGG + qtdTamEG;
                }

                prodSel.TamanhoUnico = hdfTamUnico.Value;
                prodSel.QtdeTamanhoP = txtQtdTamP.Value.Equals("") ? 0 : int.Parse(txtQtdTamP.Value);
                prodSel.QtdeTamanhoM = txtQtdTamM.Value.Equals("") ? 0 : int.Parse(txtQtdTamM.Value);
                prodSel.QtdeTamanhoG = txtQtdTamG.Value.Equals("") ? 0 : int.Parse(txtQtdTamG.Value);
                prodSel.QtdeTamanhoGG = txtQtdTamGG.Value.Equals("") ? 0 : int.Parse(txtQtdTamGG.Value);
                prodSel.QtdeTamanhoEG = txtQtdTamEG.Value.Equals("") ? 0 : int.Parse(txtQtdTamEG.Value);

                prodSel.ValorUnitario = decimal.Parse(hdfValorunitario.Value);
                prodSel.ValorTotal = prodSel.ValorUnitario * prodSel.Qtde;
                hdfValorTotal.Value = prodSel.ValorTotal.ToString();

                var qtdeItens = ColaboradorComandaItem.Lista(prodSel.NumeroComanda).Count;
                prodSel.Item = qtdeItens + 1;

                return ColaboradorComandaItem.Inserir(prodSel);

            }
            catch (Exception ex)
            {
                ltlMsn.Text = string.Format("<div class=\"alert alert-danger alert-dismissible\">" +
                                            "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">X</button>" +
                                            "<h4><i class=\"icon fa fa-ban\"></i> Page_Load-Erro:</h4>{0}</div>", ex.Message);
                return false;
            }
        }


        protected void ddlProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProduto.SelectedIndex > 0)
            {
                var idProd = ddlProduto.SelectedValue;
                var sp = ServicoProduto.Pesquisa(int.Parse(idProd));
                hdfValorunitario.Value = sp.PrecoVenda.ToString();
                hdfTamUnico.Value = sp.TamanhoUnico;

                if (sp.TamanhoUnico.Equals("NÃO"))
                {
                    hdfQtdeProduto.Value = string.Format("{0};{1};{2};{3};{4}"
                                                            , sp.QtdeEstqTamanhoP
                                                            , sp.QtdeEstqTamanhoM
                                                            , sp.QtdeEstqTamanhoG
                                                            , sp.QtdeEstqTamanhoGG
                                                            , sp.QtdeEstqTamanhoEG
                                                         );
                    pnlQtdeUnica.Visible = false;
                    pnlQtdes.Visible = true;
                }
                else
                {
                    hdfQtdeProduto.Value = sp.QtdeEstoque.ToString();
                    pnlQtdeUnica.Visible = true;
                    pnlQtdes.Visible = false;
                }

            }
            else
            {
                hdfValorunitario.Value = "0";
                hdfTamUnico.Value = "0";
            }
        }


        private void CarregaTabela()
        {
            try
            {

                var tabelaHtml = new StringBuilder();

                var lista = ColaboradorComandaItem.Lista(long.Parse(txtNumeroCmda.Value));

                if (lista.Count == 0)
                {
                    tabelaHtml.Append("<div class=\"panel panel-warning\"><div class=\"panel-heading\"> <h4 class=\"panel-title\"><i class=\"icon ion-clock text-warning\">" +
                                      "</i>!! Nenhum produto cadastrado !!</h4></div></div>");
                }
                else
                {

                    tabelaHtml.Append("<table id=\"tabela\" class=\"table table-bordered table-hover\"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Item</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">descrição</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Qtde</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Vlr. Unitário</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Vlr. Total</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"></th>");
                    //tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"></th>");
                    tabelaHtml.Append("</tr></thead><tbody>");

                    foreach (var item in lista)
                    {
                        //var idCriotografado = Criptografia.Encrypt(item.Id.ToString());
                        //idCriotografado = idCriotografado.Replace('+', '_');

                        tabelaHtml.Append("<tr>");
                        //Item                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", item.Item));
                        //Descrição                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", item.DescricaoProduto));
                        //qtde                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", item.Qtde));
                        //vlr Unitário                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", item.ValorUnitario));
                        //Total                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", item.ValorTotal));

                        //Situação
                        //var situacao = string.Format("<span class=\"label label-{0}\">{1}</span>"
                        //                                , item.IdStatus == 1 ? "success" : item.IdStatus == 2 ? "warning" : "danger"
                        //                                , item.IdStatus == 1 ? "Ativo" : item.IdStatus == 2 ? "Inativo" : "Excluido"
                        //                             );
                        //tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", situacao));

                        // ========= AÇÕES ==========
                        //Editar                        
                        //tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\"><a href=\"#\" class=\"btn btn-xs btn-info\" data-original-title=\"\"  title=\"Editar dados produto.\" onclick=\"capturarId({0}, 'edit')\"  ><i class=\"fa fa-pencil-square\"></i>  Editar</a></td>", item.Item));

                        //Deletar                        
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\"><a href=\"#\" class=\"btn btn-xs btn-danger\" data-original-title=\"\"  title=\"Apagar produto.\" onclick=\"capturarId({0}, 'del')\"  data-toggle=\"modal\" data-target=\"#modal-danger\"><i class=\"fa fa-minus-square\"></i>  Apagar</a></td>", item.Item));

                        tabelaHtml.Append("</tr>");
                    }
                }
                tabelaHtml.Append("</tbody></table>");//Fim da tabela
                ltlTabela.Text = tabelaHtml.ToString();
            }
            catch (Exception ex)
            {
                ltlMsn.Text = string.Format("<div class=\"alert alert-danger alert-dismissible\">" +
                                            "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">X</button>" +
                                            "<h4><i class=\"icon fa fa-ban\"></i> CarregaTabela-Erro:</h4>{0}</div>", ex.Message);
            }
        }


        protected void btnExcluirProduto_Click(object sender, EventArgs e)
        {

            try
            {
                var idProdExcluir = Session["idProdSelExcluir"].ToString();
                var numCmda = long.Parse(txtNumeroCmda.Value);
                if (!ColaboradorComandaItem.ApagarItem(numCmda, int.Parse(idProdExcluir)))
                {
                    ltlMsn.Text = string.Format("<div class=\"alert alert-danger alert-dismissible\">" +
                                  "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">X</button>" +
                                  "<h4><i class=\"icon fa fa-ban\"></i> Erro:</h4> Não foi possível excluir o Produto selecionado insuficiente.");
                }
                Session["idProdSelExcluir"] = "";

                //Atualizar numero dos itens
                //Atualizar o valor total
                AtualizarItensCmda();

                CarregaTabela();
            }
            catch (Exception ex)
            {
                ltlMsn.Text = string.Format("<div class=\"alert alert-danger alert-dismissible\">" +
                                            "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">X</button>" +
                                            "<h4><i class=\"icon fa fa-ban\"></i> CarregaTabela-Erro:</h4>{0}</div>", ex.Message);
            }
        }


        [WebMethod]
        public static string CapturarId(string id, string acao)
        {
            try
            {
                if (acao.Equals("del"))
                {
                    HttpContext.Current.Session["idProdSelExcluir"] = id;
                }

                if (acao.Equals("edit"))
                {
                    HttpContext.Current.Session["idProdSelEditar"] = id;
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-danger fade in m-b-15\"><strong>CapturarId-Error: </strong> " +
                       ex.Message + "<span class=\"close\" data-dismiss=\"alert\">X</span></div>";
            }
        }

        protected void btnCancelarAddProdSel_Click(object sender, EventArgs e)
        {
            txtQtdeUnica.Value = string.Empty;
            pnlAdicionarProduto.Visible = false;
            pnlProdutosSelecionados.Visible = true;
        }
    }
}