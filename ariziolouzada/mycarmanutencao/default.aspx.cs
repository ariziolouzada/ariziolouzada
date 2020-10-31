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
                var dataVenda = carro.DataVenda != DateTime.MinValue ? carro.DataVenda : DateTime.Now;
                var periodo = new CalculaDiferencaDatas(carro.DataCompra, dataVenda);
                var valorManutencao = MyCarManutencao.SomaValor(carro.Id);

                lblValorCompra.Text = string.Format("{0:C}", carro.ValorCompra);
                lblGastoManutencao.Text = string.Format("{0:C}", valorManutencao);
                lblTempoPropriedade.Text = string.Format("{0} ano(s) {1} mes(es) dia(s) {2}", periodo.Anos, periodo.Meses, periodo.Dias );
            
                lblValorVenda.Text = string.Format("{0:C}", carro.ValorVenda);
                decimal porcentagemVenda = 0;
                if (carro.ValorVenda > 0)
                {
                    porcentagemVenda = (carro.ValorVenda / carro.ValorCompra) * 100;
                }
                lblPorcentagemVenda.Text = string.Format("{0}%", string.Format("{0:#.##}", porcentagemVenda));
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
            CarregaDadosCarro();
        }
    }
}