using ariziolouzada.classes;
using CriptografiaSgpm;
using System;
using System.Text;
using System.Web.UI;

namespace ariziolouzada.mycarmanutencao.manutencao
{
    public partial class _default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ltlMsn.Text = string.Empty;
                CarregaDdlCarro();

                if (Request.QueryString.HasKeys())
                {
                    if (Request.QueryString["idmc"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["idmc"]))
                    {
                        var idmc = Request.QueryString["idmc"];
                        if (idmc != null)
                        {
                            idmc = Criptografia.Decrypt(idmc.Replace('_', '+'));
                            ddlCarro.SelectedValue = idmc;
                        }
                    }
                }
                CarregaLinkNovaManutencao();
                CarregaTabela();
            }
        }

        private void CarregaLinkNovaManutencao()
        {
            var idMyCar = Criptografia.Encrypt(ddlCarro.SelectedValue);
            idMyCar = idMyCar.Replace('+', '_');//class=\"btn btn-block bg-gradient-secondary\"&nbsp;&nbsp;&nbsp;&nbsp;Nova
            ltlBtnNovaManutacao.Text = string.Format("<a href=\"newedit.aspx?id=X5A1oqTnjBE=&idmc={0}\" title=\"Nova Manutenção.\">" +
                                                    "<i class=\"fas fa-plus-circle fa-lg\"></i></a>", idMyCar);
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

        private void CarregaTabela()
        {
            try
            {
                var idCarro = int.Parse(ddlCarro.SelectedValue);

                var lista = MyCarManutencao.Listar(idCarro);
                var tabelaHtml = new StringBuilder();

                tabelaHtml.Append("<table id=\"example1\" class=\"table table-bordered table-striped\">");
                if (lista.Count == 0)
                {
                    tabelaHtml.Append("</tbody><tr style=\"font-size: 12pt; color: red; font-weight: bold;\"><td align=\"center\">Não foi encontrado nenhum registro para o carro selecionado.</td></tr>");
                }
                else
                {
                    //Cabeçalho tabela
                    tabelaHtml.Append("<thead><tr>");
                    //tabelaHtml.Append("<th style=\"vertical-align: middle; text-align: center\">ID</th>");
                    tabelaHtml.Append("<th style=\"vertical-align: middle; text-align: center\">DATA</th>");
                    tabelaHtml.Append("<th style=\"vertical-align: middle; text-align: center\">DESCRIÇÃO</th>");
                    tabelaHtml.Append("<th style=\"vertical-align: middle; text-align: center\">KM</th>");
                    tabelaHtml.Append("<th style=\"vertical-align: middle; text-align: center\">VALOR</th>");
                    tabelaHtml.Append("<th></th>");
                    tabelaHtml.Append("<th></th>");
                    tabelaHtml.Append("</tr></thead><tbody>");

                    //linhas
                    foreach (var item in lista)
                    {
                        tabelaHtml.Append("<tr>");
                        //ID                    
                        //tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\">{0}</td>", item.Id));
                        //dATA                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\">{0}</td>", item.Data.ToShortDateString()));
                        //Descricao
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\">{0}</td>", item.Descricao));
                        //\km                     
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\">{0}</td>", item.Km));
                        //Valor                  
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\">{0}</td>", string.Format("{0:C}", item.Valor)));

                        // ============ AÇÕES ===============
                        var idCriotografado = Criptografia.Encrypt(item.Id.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        var idMyCar = Criptografia.Encrypt(ddlCarro.SelectedValue);
                        idMyCar = idMyCar.Replace('+', '_');

                        //Editar        class=\"btn btn-xs btn-info\" Editar                
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\"><a href=\"newedit.aspx?id={0}&idmc={1}\" " +
                            "data-original-title=\"\"  title=\"Editar dados.\" ><i class=\"fas fa-edit fa-2x\"></i>" +
                            "</a></td>", idCriotografado, idMyCar));

                        //Excluir   class=\"btn btn-xs btn-info\"   Excluir                  
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\"><a href=\"javascript:;\" onclick=\"guardaIdExcluir('{0}')\" " +
                            "title=\"Apagar Manutenção.\" data-toggle=\"modal\" data-target=\"#modal-delete\" class=\"text-danger\"><i class=\"far fa-trash-alt fa-2x\"></i>" +
                            "</a></td>", idCriotografado));


                        tabelaHtml.Append("</tr>");
                    }
                }
                tabelaHtml.Append("</tbody></table>");
                ltlTabela.Text = tabelaHtml.ToString();
            }
            catch (Exception ex)
            {
                ltlMsn.Text = string.Format("<div class=\"row\"><div class=\"col-sm-12\"><div class=\"alert alert-danger alert-dismissible\">"
                                            //+ "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">X</button>"
                                            + "<h5><i class=\"icon fas fa-ban\"></i>CarregaTabela-Erro:</h5>{0}</div></div></div>", ex.Message);
            }

        }

        protected void ddlCarro_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaLinkNovaManutencao();
            CarregaTabela();
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                var id = Criptografia.Decrypt(hdfIdDel.Value.Replace('_', '+'));
                if (MyCarManutencao.Excluir(int.Parse(id)))
                {
                    Response.Redirect("default.aspx?idmc=" + Request.QueryString["idmc"]);
                }
                else
                {
                    ltlMsn.Text = string.Format("<div class=\"row\"><div class=\"col-sm-12\"><div class=\"alert alert-danger alert-dismissible\">"
                                                + "<h5><i class=\"icon fas fa-ban\"></i>BtnConfirmar-Erro:</h5>" +
                                                 "Não foi possível apagar a manutenção em tela.</div></div></div>");

                }
            }
            catch (Exception ex)
            {
                ltlMsn.Text = string.Format("<div class=\"row\"><div class=\"col-sm-12\"><div class=\"alert alert-danger alert-dismissible\">"
                                              + "<h5><i class=\"icon fas fa-ban\"></i>CarregaTabela-Erro:</h5>{0}</div></div></div>", ex.Message);
            }
        }
    }
}