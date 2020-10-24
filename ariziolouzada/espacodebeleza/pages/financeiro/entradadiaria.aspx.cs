using CriptografiaSgpm;
using espacobeleza_cl;
using System;
using System.Text;
using System.Web;
using System.Web.UI;


namespace ariziolouzada.espacodebeleza.pages.financeiro
{
    public partial class entradadiaria : Page
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
                txtData.Value = DateTime.Now.Date.ToString("yyyy-MM-dd");
                ltlCabecalho.Text = string.Format("<h2><b>Entrada - {0}.</b></h2>", DateTime.Now.ToLongDateString());
                ////}

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


        private void CarregaTabela()
        {
            try
            {
                var tabelaHtml = new StringBuilder();

                tabelaHtml.Append("<table id=\"tabelaEntradas\" class=\"table table-striped table-bordered \"><thead><tr>");
                tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\" ><h3>Profissional</h3></th>");
                tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\" ></th>");
                tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\" >Valor Comissão</th>");
                tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\" >Valor Serviço</th>");
                tabelaHtml.Append("</tr></thead>");
                tabelaHtml.Append("<tbody>");

                //RECUPERAR O COOKIE
                var idEmpresaContratante = "0";
                HttpCookie cookie = Request.Cookies["idEmpresaContratante"];
                if (cookie != null && cookie.Value != null)
                    idEmpresaContratante = cookie.Value;
                idEmpresaContratante = Criptografia.Decrypt(idEmpresaContratante);

                var listaProfissional = ProfissionalEspBel.Lista("", int.Parse(idEmpresaContratante));

                var data = Convert.ToDateTime(txtData.Value);
                decimal totalComissao = 0;
                decimal totalSerico = 0;
                foreach (var prof in listaProfissional)
                {
                    tabelaHtml.Append("<tr>");
                    //Nome                    
                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", prof.Nome));

                    //Nome                    
                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", ""));

                    //Valor
                    var valor = ComandaItem.SomaProfissionalValorComissao(prof.Id, data);
                    totalComissao = totalComissao + valor;
                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format("{0:C}", valor)));

                    //VAlor Serico
                    var valorServico = ComandaItem.SomaProfissionalValorServico(prof.Id, data);
                    totalSerico = totalSerico + valorServico;
                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format("{0:C}", valorServico)));

                    tabelaHtml.Append("</tr>");
                }
                //Somatório colspan=\"2\"
                tabelaHtml.Append("<tr style=\"font-size: 14pt;\">");
                tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: right\" >{0}</td>", ""));
                tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: right\" >{0}</td>", "T O T A L"));
                tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format("{0:C}", totalComissao)));
                tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center; color: red;\" ><b>{0}</b></td>", string.Format("{0:C}", totalSerico)));
                tabelaHtml.Append("</tr>");
                //style="color: red;"
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