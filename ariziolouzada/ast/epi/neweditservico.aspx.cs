using ariziolouzada.classes;
using CriptografiaSgpm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ariziolouzada.ast.epi
{
    public partial class neweditservico : System.Web.UI.Page
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
                            Session["idSvc"] = id;

                            if (!id.Equals("0"))
                            {
                                //Editar EPI
                                CarregaDdlTipoServico(false);
                                CarregaDados(int.Parse(id));
                                lblCabecalho.Text = "Editar Serviço";

                                //txtQtd.Disabled = false;
                            }
                            else
                            {
                                //Cadastro de EPI
                                CarregaDdlTipoServico(true);
                            }
                        }
                    }
                }
                else
                {

                    if (Session["idSvc"] != null)
                    {

                    }

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

            var svc = ServicoPedido.PesquisaServicoPedido(id);
            if (svc != null)
            {
                txtData.Value = svc.Data.ToShortDateString();
                txtQtd.Value = svc.Qtde.ToString();
                txtPrecoCusto.Value = string.Format("{0:C}", svc.ValorUnitario).Replace("R$", "").Trim();
                txtValorTotal.Value = string.Format("{0:C}", svc.ValorTotal).Replace("R$", "").Trim();

                ddlTipo.SelectedValue = svc.IdTipo.ToString();

                //ScriptManager.RegisterClientScriptBlock(Page,Page.GetType(),"mensagem", string.Format("carregaDllServico({0})", svc.IdTipo ), true);
                CarregaDdlServico(svc.IdTipo, false);
                ddlServico.SelectedValue = svc.IdServico.ToString();
            }

        }

        private void CarregaDdlTipoServico(bool comSelecione)
        {
            try
            {

                var lista = ServicoTipo.ListaTipoServico(comSelecione);
                ddlTipo.Items.Clear();
                ddlTipo.DataSource = lista;
                ddlTipo.DataValueField = "Id";
                ddlTipo.DataTextField = "Descricao";
                ddlTipo.DataBind();

                if (comSelecione)
                    ddlTipo.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaDdlTipoServico-ERRO:" + ex.Message + "</p></div>";
            }
        }

        private void CarregaDdlServico(int idTipo, bool comSelecione)
        {
            try
            {

                var lista = Servico.ListaDeServicos(idTipo, 1, comSelecione);
                ddlServico.Items.Clear();
                ddlServico.DataSource = lista;
                ddlServico.DataValueField = "Id";
                ddlServico.DataTextField = "Descricao";
                ddlServico.DataBind();

                if (comSelecione)
                    ddlServico.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaDdlServico-ERRO:" + ex.Message + "</p></div>";
            }

        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //tratar qdo mudar a seleçao do tipo do serviço. deve recarregar a ddlSertivo e limpar a qtde, o preço de custo e total 

            if (ddlTipo.SelectedIndex > 0)
            {
                //Carrega a ddlServico
                CarregaDdlServico(int.Parse(ddlTipo.SelectedValue), true);

            }
            else
            {

                ddlServico.Items.Clear();
            }
        }



        [WebMethod]
        public static string CarregaDllServico(string tipo)
        {
            try
            {
                //HttpContext.Current.Session["dadosPA"] = dados;
                var listaServicos = Servico.ListaDeServicos(int.Parse(tipo), 1, true);
                var selectServico = new StringBuilder();

                //selectServico.Append("<div id='selectServico'><select  class='form-control' id='ddlServico'>");
                selectServico.Append("<select  class='form-control' id='ddlServico' name='ddlServico'>");

                //Itens do select
                foreach (var svc in listaServicos)
                    selectServico.Append(string.Format("<option value='{0}'>{1}</option>", svc.Id, svc.Descricao));


                //selectServico.Append("</select></div>");
                selectServico.Append("</select>");

                return selectServico.ToString();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }


        [WebMethod]
        public static string CarregaPrecoCusto(string tipo)
        {
            try
            {

                var svc = Servico.PesquisaServico(int.Parse(tipo));
                HttpContext.Current.Session["idSvc"] = svc.Id.ToString();
                HttpContext.Current.Session["valorSvc"] = svc.Valor.ToString();
                return svc.Valor.ToString();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        [WebMethod]
        public static string CarregaPrecoTotal(string qtde, string pcoCusto)
        {
            try
            {
                var qtd = qtde != string.Empty ? int.Parse(qtde) : 0;
                var pco = decimal.Parse(pcoCusto);
                var total = qtd * pco;
                HttpContext.Current.Session["valorTotal"] = total.ToString();
                HttpContext.Current.Session["qtde"] = qtd;

                return total.ToString();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("servicos.aspx");
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (VerificaCamposBtnSalvar())
            {
                if (SalvarDados())
                {
                    btnCancelar_Click(sender, e);
                }
            }
        }

        private bool SalvarDados()
        {

            try
            {

                var id = Session["idSvc"].ToString();
                var vlr = Session["valorSvc"] == null ? txtPrecoCusto.Value : Session["valorSvc"].ToString();
                var vlrTl = Session["valorTotal"] == null ? txtValorTotal.Value : Session["valorTotal"].ToString();
                var qtde = Session["qtde"] == null ? txtQtd.Value : Session["qtde"].ToString();

                var svc = new ServicoPedido()
                {
                    Id = int.Parse(id),
                    IdServico = int.Parse(ddlServico.SelectedValue),
                    Data = DateTime.Parse(txtData.Value),
                    Qtde = int.Parse(qtde),
                    ValorUnitario = decimal.Parse(vlr),
                    ValorTotal = decimal.Parse(vlrTl)
                };

                if (id.Equals("0"))
                    return ServicoPedido.Inserir(svc);
                else
                    return ServicoPedido.Editar(svc);

            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>SalvarDados-ERRO:" + ex.Message + "</p></div>";
                return false;
            }

        }

        private bool VerificaCamposBtnSalvar()
        {

            if (txtData.Value == string.Empty)
            {
                txtData.Focus();
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Informe a Data da execução do serviço.</p></div>";
                return false;
            }


            if (ddlTipo.SelectedValue.StartsWith("Selec"))
                if (ddlTipo.SelectedIndex == 0)
                {
                    ddlTipo.Focus();
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Selecione o Tipo do serviço.</p></div>";
                    return false;
                }

            //if (ddlServico.SelectedIndex == 0)
            //{
            //    ddlServico.Focus();
            //    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
            //                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Selecione o servico.</p></div>";
            //    return false;
            //}

            //if (txtQtd.Value == string.Empty)
            //{
            //    txtQtd.Focus();
            //    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
            //                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Informe a quantidade do serviço.</p></div>";
            //    return false;
            //}


            //if (txtPrecoCusto.Value == string.Empty)
            //{
            //    //txtData.Focus();
            //    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
            //                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Preço de custo do serviço inválido.</p></div>";
            //    return false;
            //}

            //if (txtValorTotal.Value == string.Empty)
            //{
            //    //txtData.Focus();
            //    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
            //                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Valor Total do serviço inválido.</p></div>";
            //    return false;
            //}


            return true;
        }
    }
}