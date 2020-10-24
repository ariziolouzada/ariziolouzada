using ariziolouzada.classes;
using System;
using System.Web.UI;

namespace ariziolouzada.acaoentreamigos
{
    public partial class exportarexcel : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString.HasKeys())
                {
                    if (Request.QueryString["ids"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["ids"]))
                    {
                        var idStatus = Request.QueryString["ids"];
                        CarregaGridView(int.Parse(idStatus));
                        ExportaExcel(int.Parse(idStatus));
                    }

                }
            }
        }

        private void CarregaGridView(int idStatus)
        {
            try
            {

                ltlMsn.Text = string.Format("<h2>Ação Entre Amigos-Tabela de Números{0}</h2>"
                    , idStatus == -1 ? "" : idStatus == 0 ? ": <b>DISPONÍVEIS</b>" : ": <b>VENDIDOS</b>" 
                    );

                var dt = AcaoEntreAmigos.CarregaGridView(idStatus);
                gwTabelaNumeros.DataSource = dt;
                gwTabelaNumeros.DataBind();

            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                              "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> Page_Load-ERRO:" + ex.Message + "</p></div>";
            }
        }

        private void ExportaExcel(int idStatus)
        {

            try
            {

                var strFileName = string.Format("RelacaoNumeros{0}.xls", idStatus == -1 ? "TODOS" : idStatus == 0 ? "DISPONÍVEIS" : "VENDIDOS" );
                Response.ClearContent();

                Response.AddHeader("content-disposition", "attachment; filename=" + strFileName);
                Response.ContentType = "application/Excel";
                System.IO.StringWriter sw = new System.IO.StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gwTabelaNumeros.RenderControl(htw);
                Response.Write(sw.ToString());

                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //Response.AddHeader("content-disposition", "attachment;filename=\"FileName.xlsx\"");

                //using (var ms = new System.IO.MemoryStream())
                //{
                //    wb.SaveAs(ms);
                //    ms.WriteTo(Response.OutputStream);
                //    ms.Close();
                //}

                Response.Flush();
                Response.End();

            }
            catch (Exception ex)
            {
                //ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                //              "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> Page_Load-ERRO:" + ex.Message + "</p></div>";

            }
        }
    }
}