using CriptografiaSgpm;
using espacobeleza_cl;
using System;
using System.Web.UI;

namespace ariziolouzada.espacodebeleza.pages.financeiro
{
    public partial class pgtocomanda : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString.HasKeys())
                    {
                        if (Request.QueryString["idag"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["idag"]))
                        {
                            var id = Request.QueryString["idag"];
                            id = Criptografia.Decrypt(id.Replace('_', '+'));

                            ////RECUPERAR O COOKIE
                            //var idEmpresaContratante = "0";
                            //HttpCookie cookie = Request.Cookies["idEmpresaContratante"];
                            //if (cookie != null && cookie.Value != null)
                            //    idEmpresaContratante = cookie.Value;

                            //idEmpresaContratante = Criptografia.Decrypt(idEmpresaContratante);


                            CarregaDadosAgenda(int.Parse(id));
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


        private void CarregaDdlManicure1(int idProfissao, bool comSelecione, int idEmpresaContratante)
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


        private void CarregaDadosAgenda(int idAgenda)
        {
            var agenda = Agenda.Pesquisar(idAgenda);
            if (agenda != null)
            {
                txtData.Value = agenda.DataAgenda.ToString("yyyy-MM-dd");
                txtCliente.Value = agenda.Cliente;


                ///continuar a imementação para as outros paineis
            }
        }

    }
}