using CriptografiaSgpm;
using espacobeleza_cl;
using System;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;

namespace ariziolouzada.espacodebeleza.pages.colaborador
{
    public partial class colaborador : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString.HasKeys())
            {
                if (Request.QueryString["id"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["id"]))
                {
                    //RECUPERAR O COOKIE
                    var idProfissional = "0";
                    HttpCookie cookie = HttpContext.Current.Request.Cookies["idProfissional"];
                    if (cookie != null && cookie.Value != null)
                        idProfissional = cookie.Value;
                    idProfissional = Criptografia.Decrypt(idProfissional);

                    hdfIdProfissional.Value = idProfissional;//teste ID da patricia fernandes

                    var id = Request.QueryString["id"];
                    id = Criptografia.Decrypt(id.Replace('_', '+'));
                    if (id.Equals("0"))
                    {
                        if (!IsPostBack)
                        {
                            CarregaDados();
                        }

                    }
                    else
                    {

                        CarregaDadosComanda(long.Parse(id));
                    }
                }
            }
        }

        private void CarregaDados()
        {

            ltlNomeProfissional.Text = string.Format("<small><b>{0}</b></small>", ProfissionalEspBel.PesquisaNome(int.Parse(hdfIdProfissional.Value)));

            hdfIdComandaTemp.Value = Comanda.GeraIdTemp();
            ltlCabecalho.Text = string.Format("<h5>Nº {0} - Situação: Em aberta</h5>", hdfIdComandaTemp.Value);

            CarregaDdlCliente(true);
            txtData.Value = DateTime.Now.Date.ToString("yyyy-MM-dd");
            CarregaDdlServico(int.Parse(hdfIdProfissional.Value), true);
        }


        private void CarregaDadosComanda(long id)
        {
            try
            {
                var comanda = Comanda.Pesquisar(id);
                if (comanda != null)
                {
                    hdfIdComandaTemp.Value = comanda.Id.ToString();
                    ltlCabecalho.Text = string.Format("<h5>Nº {0} - Situação: Em aberta</h5>", hdfIdComandaTemp.Value);

                    txtData.Value = comanda.Data.ToString("yyyy-MM-dd");
                    CarregaDdlCliente(false);
                    ddlCliente.SelectedValue = comanda.IdCliente.ToString();

                    CarregaDdlServico(int.Parse(hdfIdProfissional.Value), true);

                    ltlServicosComanda.Text = CarregaTabela(comanda.Id.ToString());
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
                ddlCliente.SelectedIndex = 0;
        }


        private void CarregaDdlServico(int idProfissional, bool comSelecione)
        {
            //RECUPERAR O COOKIE
            var idEmpresaContratante = "0";
            HttpCookie cookie = Request.Cookies["idEmpresaContratante"];
            if (cookie != null && cookie.Value != null)
                idEmpresaContratante = cookie.Value;

            idEmpresaContratante = Criptografia.Decrypt(idEmpresaContratante);

            var listaSvcProf = ServicoEspBel.ListaServicoProfissional(comSelecione, idProfissional, 1, int.Parse(idEmpresaContratante));
            ddlServico.Items.Clear();
            ddlServico.DataSource = listaSvcProf;
            ddlServico.DataValueField = "Id";
            ddlServico.DataTextField = "Descricao";
            ddlServico.DataBind();

            if (comSelecione)
                ddlServico.SelectedIndex = 0;
        }


        protected void ddlServico_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlServico.SelectedIndex > 0)
                {

                    // Erro, não traz o valor do serviço

                    ////RECUPERAR O COOKIE
                    //var idEmpresaContratante = "0";
                    //HttpCookie cookie = Request.Cookies["idEmpresaContratante"];
                    //if (cookie != null && cookie.Value != null)
                    //    idEmpresaContratante = cookie.Value;

                    //idEmpresaContratante = Criptografia.Decrypt(idEmpresaContratante);

                    txtValor.Value = string.Format("{0:C}", ServicoEspBel.PesquisarValor(int.Parse(ddlServico.SelectedValue)));
                }
                else
                {
                    txtValor.Value = string.Empty;
                }

                //var id = Request.QueryString["id"];
                //id = Criptografia.Decrypt(id.Replace('_', '+'));
                //if (!id.Equals("0"))
                //    CarregaTabela(long.Parse(id));
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ddlServico_SelectedIndexChanged-ERRO:" + ex.Message + "</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
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
                                      "</i>!! Nenhum Item cadastrado!!</h4></div></div>");
                }
                else
                {
                    tabelaHtml.Append("<table id=\"tabelaClientes\" class=\"table table-striped table-bordered \"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Item</th>");
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

                        tabelaHtml.Append("<tr style=\"font-size: 12pt;\">");

                        //Item                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format(@"{0:00}", item)));

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


        [WebMethod]
        public static string AdicionarItemComanda(string idComanda, string idProfissional, string idServico, string qtde, string valor)
        {
            try
            {
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
        public static string VerificaItemComanda(string idComanda)
        {
            try
            {
                var soma = ComandaItem.SomaItensCmda(long.Parse(idComanda));
                return soma.ToString();
            }
            catch (Exception ex)
            {
                return "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                       "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> VerificaItemComanda-ERRO:" + ex.Message + "</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }
        }

        [WebMethod]
        public static string CarregaValorServico(string idServico)
        {
            try
            {
                var valorSvc = string.Format("{0:C}", ServicoEspBel.PesquisarValor(int.Parse(idServico)));
                return valorSvc.Replace("R$", "");
                //return valorSvc.Equals("R$0,00") ? "" : valorSvc;
            }
            catch (Exception ex)
            {
                return "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                       "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> CarregaValorServico-ERRO:" + ex.Message + "</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
            }
        }


        protected void btnVoltar_Click(object sender, EventArgs e)
        {

            Response.Redirect("listacomandas.aspx?data=" + txtData.Value);

        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/sbessencial/financeiro/caixa/newedit.aspx?id=X5A1oqTnjBE=");
            //Response.Redirect("newedit.aspx?id=X5A1oqTnjBE=");
            Response.Redirect("comanda.aspx");
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
            if (valorComanda == 0)
            {
                ltlMsn.Text = "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-warning fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                        "</button><p><i class=\"fa fa-exclamation-triangle fa-lg\"></i> Atenção: Não a servicos adicionados a comanda em tela.</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";

                return false;
            }

            if (txtData.Value == string.Empty)
            {
                ltlMsn.Text = "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-warning fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                        "</button><p><i class=\"fa fa-exclamation-triangle fa-lg\"></i> Atenção: Digite a data da comanda em tela.</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
                txtData.Focus();
                return false;
            }

            //var id = Request.QueryString["id"];
            //id = Criptografia.Decrypt(id.Replace('_', '+'));

            //if (id.Equals("0"))
            if (ddlCliente.SelectedIndex == 0)
            {
                ltlMsn.Text = "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-warning fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                                        "</button><p><i class=\"fa fa-exclamation-triangle fa-lg\"></i> Atenção: Selecione o Cliente da comanda em tela.</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
                ddlCliente.Focus();
                return false;
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
                    Id = long.Parse(hdfIdComandaTemp.Value),
                    IdCliente = int.Parse(ddlCliente.SelectedValue),
                    IdStatus = 1,
                    IdFormaPgto = 6,
                    Data = DateTime.Parse(txtData.Value),
                    IdEmpresaContratante = int.Parse(idEmpresaContratante),
                    Valor = ComandaItem.SomaItensCmda(long.Parse(hdfIdComandaTemp.Value))
                };

                var id = Request.QueryString["id"];
                id = Criptografia.Decrypt(id.Replace('_', '+'));

                if (id.Equals("0") || id.Equals("-1"))
                {
                    //Cadastro
                    if (!Comanda.Inserir(comanda))
                    {
                        ltlMsn.Text = "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                            "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> SalvarDadosComanda-ERRO: não foi possível salvar a comanda em tela.</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
                        return false;
                    }
                    return true;
                }

                //editar
                //comanda.Id = int.Parse(id);

                if (!Comanda.Editar(comanda))
                {
                    ltlMsn.Text = "<div class=\"form-group\"><div class=\" col-md-12 col-lg-12\"><div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                        "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> SalvarDadosComanda-ERRO: não foi possível editar a comanda em tela.</p></div></div></div><br /><div class=\"hr-line-dashed\"></div>";
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


    }
}