using CriptografiaSgpm;
using ferrita_Cl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ariziolouzada.ferrita.fornecedor
{
    public partial class newedit : System.Web.UI.Page
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

                            pnlStatus.Visible = !id.Equals("0");


                            if (!id.Equals("0"))
                            {
                                //Editar 
                                CarregaDados(int.Parse(id));
                                lblTitulo.Text = "EDITAR";
                                //pnlStatus.Visible = true;
                            }
                            else
                            {
                                //Cadastro
                                //pnlStatus.Visible = false;

                                //if (Session["cadCliente"] != null)
                                //    if (Session["cadCliente"].ToString().Equals("OK"))
                                //    {
                                //        Session["cadCliente"] = "";
                                //        ltlMsn.Text = "<div class=\"alert alert-block alert-success fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">OK" +
                                //                     "</button><p><i class=\"fa fa-thumbs-o-up fa-lg\"></i> Cadastro realizado com sucesso!!!</p></div>";
                                //    }

                            }
                        }
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

            try
            {
                var fornercedor = Fornecedor.PesquisaFornecedorPeloId(id);
                txtNome.Value = fornercedor.NomeFornecedor;
                txtCnpj.Value = fornercedor.CnpjFornecedor;
                txtTelefone.Value = fornercedor.Telefone;
                ddlStatus.SelectedValue = fornercedor.IdStatusFornecedor.ToString();
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaDados-ERRO:" + ex.Message + "</p></div>";
            }
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }


        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (VerificaCamposBtnSalvar())
            {
                if (SalvarFornecedor())
                {
                    btnCancelar_Click(sender, e);
                }
            }
        }


        private bool SalvarFornecedor()
        {
            try
            {
                var fornercedor = new Fornecedor();
                fornercedor.NomeFornecedor = txtNome.Value;
                fornercedor.CnpjFornecedor = txtCnpj.Value;
                fornercedor.Telefone = txtTelefone.Value;

                var id = Request.QueryString["id"];
                id = Criptografia.Decrypt(id.Replace('_', '+'));

                if (id.Equals("0")) //Cadastro
                {
                    return Fornecedor.InserirFornecedor(fornercedor);
                }

                //Editar
                fornercedor.IdFornecedor = int.Parse(id);
                fornercedor.IdStatusFornecedor = int.Parse(ddlStatus.SelectedValue);

                return Fornecedor.UpdateFornecedor(fornercedor);

            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> SalvarProduto-ERRO:" + ex.Message + "</p></div>";
                return false;
            }
        }


        private bool VerificaCamposBtnSalvar()
        {
            if (txtNome.Value == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Campo NOME é de preenchimento obrigatório!</p></div>";

                txtNome.Focus();
                return false;
            }

            if (txtTelefone.Value == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Campo TELEFONE é de preenchimento obrigatório!</p></div>";

                txtTelefone.Focus();
                return false;
            }


            return true;
        }

    }
}