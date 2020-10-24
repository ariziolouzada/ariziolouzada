using ariziolouzada.classes;
using CriptografiaSgpm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ariziolouzada.mycarmanutencao.veiculo
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

                            ltlCabecalho.Text = id.Equals("0") ? "Cadastro de Veículo" : "Editar Veículo";
                            pnlSituacao.Visible = !id.Equals("0");
                            if (!id.Equals("0"))
                            {
                                //Editar 
                                CarregaDados(int.Parse(id));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ltlMsn.Text = string.Format("<div class=\"row\"><div class=\"col-sm-12\"><div class=\"alert alert-danger alert-dismissible\">"
                                            //+ "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">X</button>"
                                            + "<h5><i class=\"icon fas fa-ban\"></i>Page_Load-Erro:</h5>{0}</div></div></div>", ex.Message);
            }

        }

        private void CarregaDados(int id)
        {
            try
            {
                var carro = MyCar.Pesquisar(id);
                if (carro != null)
                {
                    txtAnoFabMod.Value = carro.AnoModeloFabricacao;
                    txtChassi.Value = carro.Chassi;
                    txtCor.Value = carro.Cor;
                    txtMarcaModelo.Value = carro.MarcaModel;
                    txtAnoFabMod.Value = carro.AnoModeloFabricacao;
                    txtPlacas.Value = carro.Placas;
                    ddlSituacao.SelectedValue = carro.IdStatus.ToString();

                    txtDataCompra.Value = carro.DataCompra.ToString("yyyy-MM-dd");
                    txtValorCompra.Value = string.Format("{0:C}", carro.ValorCompra);

                    txtDataVenda.Value = carro.DataVenda != DateTime.MinValue
                                        ? carro.DataVenda.ToString("yyyy-MM-dd")
                                        : string.Empty;

                    txtValorVenda.Value = carro.ValorVenda != 0
                                            ? string.Format("{0:C}", carro.ValorVenda)
                                            : string.Empty;
                }
            }
            catch (Exception ex)
            {
                ltlMsn.Text = string.Format("<div class=\"row\"><div class=\"col-sm-12\"><div class=\"alert alert-danger alert-dismissible\">"
                                            //+ "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">X</button>"
                                            + "<h5><i class=\"icon fas fa-ban\"></i>CarregaDados-Erro:</h5>{0}</div></div></div>", ex.Message);
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (VerificarCampos())
            {
                if (SalvarDadosCarro())
                {
                    btnCancelar_Click(sender, e);
                }
            }
        }

        private bool VerificarCampos()
        {
            ltlMsn.Text = string.Empty;
            if (txtMarcaModelo.Value == string.Empty)
            {
                ltlMsn.Text = string.Format("<div class=\"row\"><div class=\"col-sm-12\"><div class=\"alert alert-danger alert-dismissible\">"
                                            + "<h5><i class=\"icon fas fa-ban\"></i>Erro:</h5>Campo <b>MarcaModelo</b> obrigatório!!</div></div></div>");
                txtMarcaModelo.Focus();
                return false;
            }

            if (txtAnoFabMod.Value == string.Empty)
            {
                ltlMsn.Text = string.Format("<div class=\"row\"><div class=\"col-sm-12\"><div class=\"alert alert-danger alert-dismissible\">"
                                            + "<h5><i class=\"icon fas fa-ban\"></i>Erro:</h5>Campo <b>AnoFabMod</b> obrigatório!!</div></div></div>");
                txtAnoFabMod.Focus();
                return false;
            }

            if (txtPlacas.Value == string.Empty)
            {
                ltlMsn.Text = string.Format("<div class=\"row\"><div class=\"col-sm-12\"><div class=\"alert alert-danger alert-dismissible\">"
                                            + "<h5><i class=\"icon fas fa-ban\"></i>Erro:</h5>Campo <b>Placas</b> obrigatório!!</div></div></div>");
                txtPlacas.Focus();
                return false;
            }

            if (txtChassi.Value == string.Empty)
            {
                ltlMsn.Text = string.Format("<div class=\"row\"><div class=\"col-sm-12\"><div class=\"alert alert-danger alert-dismissible\">"
                                            + "<h5><i class=\"icon fas fa-ban\"></i>Erro:</h5>Campo <b>Chassi</b> obrigatório!!</div></div></div>");
                txtChassi.Focus();
                return false;
            }

            if (txtCor.Value == string.Empty)
            {
                ltlMsn.Text = string.Format("<div class=\"row\"><div class=\"col-sm-12\"><div class=\"alert alert-danger alert-dismissible\">"
                                            + "<h5><i class=\"icon fas fa-ban\"></i>Erro:</h5>Campo <b>Cor</b> obrigatório!!</div></div></div>");
                txtCor.Focus();
                return false;
            }

            if (txtDataCompra.Value == string.Empty)
            {
                ltlMsn.Text = string.Format("<div class=\"row\"><div class=\"col-sm-12\"><div class=\"alert alert-danger alert-dismissible\">"
                                            + "<h5><i class=\"icon fas fa-ban\"></i>Erro:</h5>Campo <b>Data da Compra</b> obrigatório!!</div></div></div>");
                txtDataCompra.Focus();
                return false;
            }

            if (txtValorCompra.Value == string.Empty)
            {
                ltlMsn.Text = string.Format("<div class=\"row\"><div class=\"col-sm-12\"><div class=\"alert alert-danger alert-dismissible\">"
                                            + "<h5><i class=\"icon fas fa-ban\"></i>Erro:</h5>Campo <b>Valor da Compra</b> obrigatório!!</div></div></div>");
                txtValorCompra.Focus();
                return false;
            }


            return true;
        }

        private bool SalvarDadosCarro()
        {
            var id = Request.QueryString["id"];
            id = Criptografia.Decrypt(id.Replace('_', '+'));

            var carro = new MyCar();
            carro.AnoModeloFabricacao = txtAnoFabMod.Value;
            carro.Chassi = txtChassi.Value;
            carro.Cor = txtCor.Value;
            carro.MarcaModel = txtMarcaModelo.Value;
            carro.AnoModeloFabricacao = txtAnoFabMod.Value;
            carro.Placas = txtPlacas.Value;
            carro.DataCompra = Convert.ToDateTime(txtDataCompra.Value);
            carro.ValorCompra = Convert.ToDecimal(txtValorCompra.Value.Replace("R$", "").Trim());

            if (id.Equals("0"))
            {//Cadastro

                return MyCar.Inserir(carro);
            }

            //Edição
            carro.Id = int.Parse(id);
            carro.IdStatus = int.Parse(ddlSituacao.SelectedValue);
            carro.DataVenda = txtDataVenda.Value != string.Empty
                             ? DateTime.Parse(txtDataVenda.Value)
                             : DateTime.MinValue;

            carro.ValorVenda = txtValorVenda.Value != string.Empty
                               ? decimal.Parse(txtValorVenda.Value.Replace("R$", "").Trim()) : 0;

            return MyCar.Editar(carro);
        }

    }
}