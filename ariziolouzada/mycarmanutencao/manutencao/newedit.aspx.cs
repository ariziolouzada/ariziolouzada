using ariziolouzada.classes;
using CriptografiaSgpm;
using Newtonsoft.Json;
using System;
using System.Web.Services;
using System.Web.UI;

namespace ariziolouzada.mycarmanutencao.manutencao
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
                            }
                            hdfIdManutencao.Value = id;
                        }

                        if (Request.QueryString["idmc"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["idmc"]))
                        {
                            var idmc = Request.QueryString["idmc"];
                            if (idmc != null)
                            {
                                idmc = Criptografia.Decrypt(idmc.Replace('_', '+'));
                                hdfIdMyCar.Value = idmc;
                                CarregaDadosVeiculo(int.Parse(idmc));
                            }
                            else
                            {
                                ltlMsn.Text = string.Format("<div class=\"row\"><div class=\"col-sm-12\"><div class=\"alert alert-danger alert-dismissible\">"
                                                            + "<h5><i class=\"icon fas fa-ban\"></i>Page_Load-Erro:</h5>Não foi possívem carregar ID do veículo.</div></div></div>");
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ltlMsn.Text = string.Format("<div class=\"row\"><div class=\"col-sm-12\"><div class=\"alert alert-danger alert-dismissible\">"
                                             + "<h5><i class=\"icon fas fa-ban\"></i>Page_Load-Erro:</h5>{0}</div></div></div>", ex.Message);
            }

        }

        private void CarregaDados(int id)
        {
            try
            {

                var carro = MyCarManutencao.Pesquisar(id);
                if (carro != null)
                {
                    txtData.Value = carro.Data.ToString("yyyy-MM-dd");
                    txtDescricao.Value = carro.Descricao;
                    txtKm.Value = carro.Km.ToString();
                    txtValor.Value = string.Format("{0:C}", carro.Valor);

                    ltlCabecalho.Text = id.Equals("0")
                        ? string.Format("Cadastro de Veículo - <b>{0}</b>", carro.MarcaModel)
                        : string.Format("Editar Veículo - <b>{0}</b>", carro.MarcaModel);

                }
            }
            catch (Exception ex)
            {
                ltlMsn.Text = string.Format("<div class=\"row\"><div class=\"col-sm-12\"><div class=\"alert alert-danger alert-dismissible\">"
                                            //+ "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">X</button>"
                                            + "<h5><i class=\"icon fas fa-ban\"></i>CarregaDados-Erro:</h5>{0}</div></div></div>", ex.Message);
            }

        }

        private void CarregaDadosVeiculo(int idmc)
        {
            try
            {
                var id = Request.QueryString["id"];
                id = Criptografia.Decrypt(id.Replace('_', '+'));

                var carro = MyCar.Pesquisar(idmc);
                if (carro != null)
                {
                    ltlCabecalho.Text = id.Equals("0")
                        ? string.Format("Cadastro Manutenção de Veículo - <b>{0}</b>", carro.MarcaModel)
                        : string.Format("Editar Manutenção de Veículo - <b>{0}</b>", carro.MarcaModel);
                }
            }
            catch (Exception ex)
            {
                ltlMsn.Text = string.Format("<div class=\"row\"><div class=\"col-sm-12\"><div class=\"alert alert-danger alert-dismissible\">"
                                            + "<h5><i class=\"icon fas fa-ban\"></i>CarregaDadosVeículo-Erro:</h5>{0}</div></div></div>", ex.Message);
            }

        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (VerificaCampos())
            {
                if (SalvarDados())
                {
                    LimparCampos();

                    ltlMsn.Text = string.Format("<div class=\"row\"><div class=\"col-sm-12\"><div class=\"alert alert-success alert-dismissible\">"
                                       + "<button >OK</button>"
                        + "<h5><i class=\"far fa-check-circle\"></i>Dados salvos com sucesso!!</h5></div></div></div>");

                }
                else
                {
                    ltlMsn.Text = string.Format("<div class=\"row\"><div class=\"col-sm-12\"><div class=\"alert alert-danger alert-dismissible\">"
                                              + "<h5><i class=\"icon fas fa-ban\"></i>SalvarDados-Erro:</h5>Nao foi possível salvar os dados.</div></div></div>");

                }
            }

        }

        private void LimparCampos()
        {
            txtData.Value = string.Empty;
            txtDescricao.Value = string.Empty;
            txtKm.Value = string.Empty;
            txtValor.Value = string.Empty;
            txtDescricao.Focus();
        }

        private bool SalvarDados()
        {
            var id = Request.QueryString["id"];
            id = id == null ? "0" : Criptografia.Decrypt(id.Replace('_', '+'));

            var manut = new MyCarManutencao()
            {
                Id = int.Parse(id)
                ,
                IdMyCar = int.Parse(hdfIdMyCar.Value)
                ,
                Data = DateTime.Parse(txtData.Value)
                ,
                Descricao = txtDescricao.Value
                ,
                Km = int.Parse(txtKm.Value)
                ,
                Valor = decimal.Parse(txtValor.Value.Replace("R$", "").Trim())
            };

            if (id.Equals("0"))//Cadastro
                return MyCarManutencao.Inserir(manut);

            //Edição
            return MyCarManutencao.Editar(manut);
        }

        private bool VerificaCampos()
        {
            if (txtData.Value == string.Empty)
            {
                ltlMsn.Text = string.Format("<div class=\"row\"><div class=\"col-sm-12\"><div class=\"alert alert-danger alert-dismissible\">"
                                            + "<h5><i class=\"icon fas fa-ban\"></i>CarregaDados-Erro:</h5>" +
                                            "CAMPO <b>DATA</b> OBRIGATÓRIO!!</div></div></div>");

                txtData.Focus();
                return false;
            }

            if (txtKm.Value == string.Empty)
            {
                ltlMsn.Text = string.Format("<div class=\"row\"><div class=\"col-sm-12\"><div class=\"alert alert-danger alert-dismissible\">"
                                            + "<h5><i class=\"icon fas fa-ban\"></i>CarregaDados-Erro:</h5>" +
                                            "CAMPO <b>KM</b> OBRIGATÓRIO!!</div></div></div>");

                txtKm.Focus();
                return false;
            }

            if (txtValor.Value == string.Empty)
            {
                ltlMsn.Text = string.Format("<div class=\"row\"><div class=\"col-sm-12\"><div class=\"alert alert-danger alert-dismissible\">"
                                            + "<h5><i class=\"icon fas fa-ban\"></i>CarregaDados-Erro:</h5>" +
                                            "CAMPO <b>VALOR</b> OBRIGATÓRIO!!</div></div></div>");

                txtValor.Focus();
                return false;
            }
            if (txtDescricao.Value == string.Empty)
            {
                ltlMsn.Text = string.Format("<div class=\"row\"><div class=\"col-sm-12\"><div class=\"alert alert-danger alert-dismissible\">"
                                            + "<h5><i class=\"icon fas fa-ban\"></i>CarregaDados-Erro:</h5>" +
                                            "CAMPO <b>DESCRIÇÃO</b> OBRIGATÓRIO!!</div></div></div>");

                txtDescricao.Focus();
                return false;
            }
            return true;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx?idmc=" + Request.QueryString["idmc"]);
        }

        [WebMethod]
        public static string SalvarDados(string stringJson)
        {
            try
            {
                var dados = JsonConvert.DeserializeObject<MyCarManutencao>(stringJson);
                if (dados.Id == 0)
                {
                    if (!MyCarManutencao.Inserir(dados))
                    {
                        return "Erro: Não foi possível salvar os dados em tela!!";
                    }
                }
                else if (!MyCarManutencao.Editar(dados))
                {
                    return "Erro: Não foi possível Editar os dados em tela!!";
                }
                return "saveOk";
            }
            catch (Exception ex)
            {
                return "Exception-Erro: " + ex.Message;
            }
        }

    }
}




