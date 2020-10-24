using CriptografiaSgpm;
using sbessencial_cl;
using System;
using System.Web.UI;

namespace ariziolouzada.sbessencial.cliente
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

                            if (!id.Equals("0"))
                            {
                                //Editar 
                                CarregaDados(int.Parse(id));
                                lblCabecalho.Text = "EDITAR CLIENTE";
                                pnlStatus.Visible = true;
                            }
                            else
                            {
                                //Cadastro
                                pnlStatus.Visible = false;

                                if (Session["cadCliente"] != null)
                                    if (Session["cadCliente"].ToString().Equals("OK"))
                                    {
                                        Session["cadCliente"] = "";
                                        ltlMsn.Text = "<div class=\"alert alert-block alert-success fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">OK" +
                                                     "</button><p><i class=\"fa fa-thumbs-o-up fa-lg\"></i> Cadastro realizado com sucesso!!!</p></div>";
                                    }

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
                var cliente = Cliente.Pesquisar(id);
                if (cliente != null)
                {
                    txtObservacao.Value = cliente.Observacao;
                    txtNome.Value = cliente.Nome;
                    txtData.Value = cliente.DataNascto != DateTime.MinValue ? cliente.DataNascto.ToString("yyyy-MM-dd") : string.Empty;
                    txtEmail.Value = cliente.Email;
                    txtFacebook.Value = cliente.Facebook;
                    txtInstagram.Value = cliente.Instagram;
                    txtTelCel1.Value = cliente.TelCelular1;
                    txtTelCel2.Value = cliente.TelCelular2;
                    txtTelFixo.Value = cliente.TelFixo;
                    ddlStatus.SelectedValue = cliente.IdStatus.ToString();
                }
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaDados-ERRO:" + ex.Message + "</p></div>";
            }
        }


        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/sbessencial/cliente");
            if (Session["paremetrosDefault"].ToString() != string.Empty)
            {
                var paremetrosDefault = Session["paremetrosDefault"].ToString();
                Response.Redirect(string.Format("default.aspx?nome={0}", paremetrosDefault ));
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
                    //Session["idEpi"] = id;

                    if (id.Equals("0"))
                    {
                        Session["cadCliente"] = "OK";
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
            txtEmail.Value = string.Empty;
            txtFacebook.Value = string.Empty;
            txtInstagram.Value = string.Empty;
            txtNome.Value = string.Empty;
            txtObservacao.Value = string.Empty;
            txtTelCel1.Value = string.Empty;
            txtTelCel2.Value = string.Empty;
            txtTelFixo.Value = string.Empty;
        }


        private bool VerificaCampos()
        {
            try
            {
                // NOME
                if (txtNome.Value == string.Empty)
                {
                    txtNome.Focus();
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Informe o NOME do Cliente.</p></div>";
                    return false;
                }

                var id = Request.QueryString["id"];
                id = Criptografia.Decrypt(id.Replace('_', '+'));
                if (id.Equals("0"))
                {
                    var clt = Cliente.Pesquisar(txtNome.Value);
                    if (clt != null)
                    {
                        txtNome.Focus();
                        ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
                                     "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Já existe um cliente cadastrado com este Nome!!!</p></div>";
                        return false;
                    }
                }
                //// DATA NESCTO
                //if (txtData.Value != string.Empty)
                //{
                //    //txtData.Focus();
                //    //ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
                //    //             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Informe a DATA de Nascimento.</p></div>";
                //    //return false;
                //    try
                //    {
                //        var dt = Convert.ToDateTime(txtData.Value);
                //    }
                //    catch (FormatException fx)
                //    {
                //        txtData.Focus();
                //        ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
                //                     "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Informe a DATA de Nascimento INVÁLIDA.</p></div>";
                //        return false;
                //    }

                //}

                //// 
                //if (txtEmail.Value == string.Empty)
                //{
                //    txtEmail.Focus();
                //    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
                //                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Informe o .</p></div>";
                //    return false;
                //}

                //// 
                //if (txtFacebook.Value == string.Empty)
                //{
                //    txtFacebook.Focus();
                //    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
                //                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Informe o .</p></div>";
                //    return false;
                //}

                //// 
                //if (txtInstagram.Value == string.Empty)
                //{
                //    txtInstagram.Focus();
                //    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
                //                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Informe o .</p></div>";
                //    return false;
                //}

                // 
                if (txtTelCel1.Value == string.Empty && txtTelCel2.Value == string.Empty && txtTelFixo.Value == string.Empty)
                {
                    txtTelCel1.Focus();
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Informe o pelo menos um telefone celular para contato.</p></div>";
                    return false;
                }


                //var id = Request.QueryString["id"];
                //id = Criptografia.Decrypt(id.Replace('_', '+'));
                ////Session["idEpi"] = id;

                //if (!id.Equals("0"))
                //{
                //    //Só verifica qdo for o edição
                //    if (ddlStatus.SelectedIndex == 0)
                //    {
                //        ddlStatus.Focus();
                //        ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
                //                     "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Selecione o Status.</p></div>";
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

                var cliente = new Cliente()
                {
                    Nome = txtNome.Value.ToUpper(),
                    DataNascto = !txtData.Value.Equals("") ? DateTime.Parse(txtData.Value) : DateTime.MinValue,
                    Email = txtEmail.Value.ToLower(),
                    Facebook = txtFacebook.Value,
                    Instagram = txtInstagram.Value,
                    TelCelular1 = txtTelCel1.Value.Replace("(27)","").Replace("-",""),
                    TelCelular2 = txtTelCel2.Value,
                    TelFixo = txtTelFixo.Value,
                    LoginCadastro = CookieSbe.Recupera("LoginUserLogado"),
                    Observacao = txtObservacao.Value
                };

                var id = Request.QueryString["id"];
                id = Criptografia.Decrypt(id.Replace('_', '+'));
                //Session["idEpi"] = id;

                if (id.Equals("0"))
                {
                    //Cadastro
                    if (!Cliente.Inserir(cliente))
                    {
                        ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                     "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Não foi possível cadastrar CLIENTE!!!</p></div>";
                        return false;
                    }
                    return true;
                }

                //Edição
                cliente.Id = int.Parse(id);
                cliente.IdStatus = int.Parse(ddlStatus.SelectedValue);
                if (!Cliente.Editar(cliente))
                {
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Não foi possível Editar CLIENTE!!!</p></div>";
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


    }
}