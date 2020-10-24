using CriptografiaSgpm;
using espacobeleza_cl;
using System;
using System.Text;
using System.Web;
using System.Web.UI;

namespace ariziolouzada.espacodebeleza.pages.financeiro
{
    public partial class colaborador : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Request.QueryString.HasKeys())
                //{
                //    if (Request.QueryString["data"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["data"]))
                //        txtData.Value = Request.QueryString["data"];
                //}
                //else
                //{
                    //carrega sempre o dia atual em tela
                    txtDatainicial.Value = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    ltlCabecalho.Text = string.Format("<h2><b>Entrada - {0}.</b></h2>", DateTime.Now.ToLongDateString());
                ////}

                CarregaDdlProfissiao(true);
                CarregaTabela();
            }
        }
        

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
        //    ltlMsn.Text = string.Empty;
        //    if (txtData.Value != string.Empty)
        //    {
                CarregaTabela();
        //    }
        //    else
        //    {
        //        ltlMsn.Text =
        //         "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
        //               "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ERRO: Digite/Selecione a data para carregar a agenda!!</p></div>";
        //        txtData.Focus();
        //    }
        //    //Response.Redirect("default.aspx?data=" + txtData.Value);
        }


        private void CarregaDdlProfissiao(bool comSelecione)
        {
            var lista = ProfissaoEspBel.Lista(comSelecione);
            ddlProfissao.Items.Clear();
            ddlProfissao.DataSource = lista;
            ddlProfissao.DataValueField = "Id";
            ddlProfissao.DataTextField = "Descricao";
            ddlProfissao.DataBind();

            if (comSelecione)
                ddlProfissao.SelectedIndex = 0;
        }
               

        private void CarregaTabela()
        {
            try
            {
                var tabelaHtml = new StringBuilder();
                var listaHorarios = AgendaHorario.Lista(false);//GeraListaHorarios();

                tabelaHtml.Append("<table id=\"tabelaAgendaHorarios\" class=\"table table-striped table-bordered \"><thead><tr>");
                tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\" ><h3>Profissional</h3></th>");                
                tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\" >Valor</th>");
                tabelaHtml.Append("</tr></thead>");
                tabelaHtml.Append("<tbody>");

                //RECUPERAR O COOKIE
                var idEmpresaContratante = "0";
                HttpCookie cookie = Request.Cookies["idEmpresaContratante"];
                if (cookie != null && cookie.Value != null)
                    idEmpresaContratante = cookie.Value;
                idEmpresaContratante = Criptografia.Decrypt(idEmpresaContratante);

                var listaProfissional = ProfissionalEspBel.Lista("", int.Parse(idEmpresaContratante));

                var data = Convert.ToDateTime(txtDatainicial.Value);

                foreach (var prof in listaProfissional)
                {
                    tabelaHtml.Append("<tr>");
                    //Nome                    
                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", prof.Nome));

                    //Valor
                    var valor = ComandaItem.SomaProfissionalValorComissao(prof.Id, data);
                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format("{0:C}", valor)));
                    
                    tabelaHtml.Append("</tr>");
                }
                tabelaHtml.Append("</tbody></table>");

                ltlTabela.Text = tabelaHtml.ToString();
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaTabela-ERRO:" + ex.Message + "</p></div>";
            }
        }


    }
}