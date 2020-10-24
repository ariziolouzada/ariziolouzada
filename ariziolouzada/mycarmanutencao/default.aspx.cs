using ariziolouzada.classes;
using System;
using System.Web.UI;

namespace ariziolouzada.mycarmanutencao
{
    public partial class _default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ltlMsn.Text = string.Empty;
                CarregaDdlCarro();

                CarregaDadosCarro();
            }
        }

        private void CarregaDadosCarro()
        {
            var idMyCar = ddlCarro.SelectedValue;
            var carro = MyCar.Pesquisar(int.Parse(idMyCar));
            if (carro != null)
            {
                var periodo = new CalculaDiferencaDatas(carro.DataCompra, carro.DataVenda);

            }
        }

        private void CarregaDdlCarro()
        {
            try
            {
                ddlCarro.DataSource = MyCar.Listar();
                ddlCarro.DataValueField = "Id";
                ddlCarro.DataTextField = "MarcaModel";
                ddlCarro.DataBind();
            }
            catch (Exception ex)
            {
                ltlMsn.Text = string.Format("<div class=\"row\"><div class=\"col-sm-12\"><div class=\"alert alert-danger alert-dismissible\">"
                                            //+ "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">X</button>"
                                            + "<h5><i class=\"icon fas fa-ban\"></i>CarregaDdlCarro-Erro:</h5>{0}</div></div></div>", ex.Message);
            }

        }

        protected void ddlCarro_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}