using CriptografiaSgpm;
using ferrita_Cl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ariziolouzada.ferrita.colaborador
{
    public partial class listacmdacolab : Page
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

                            CarregaDados(int.Parse(id));
                            CarregaTabela(int.Parse(id));
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
            var colab = Colaborador.PesquisaPeloId(id);
            lblNomeColaborador.Text = colab.Nome;

        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("neweditcomanda.aspx?id={0}&nc=X5A1oqTnjBE=", Request.QueryString["id"]));
        }


        private void CarregaTabela(int idColab)
        {
            try
            {
                var tabelaHtml = new StringBuilder();
                var lista = ColaboradorComanda.ListaPorIdColaborador(idColab);
                if (lista.Count == 0)
                {
                    tabelaHtml.Append("<div class=\"panel panel-warning\"><div class=\"panel-heading\"> <h4 class=\"panel-title\"><i class=\"icon ion-clock text-warning\">" +
                                      "</i>!! Nenhuma comanda cadastrada !!</h4></div></div>");
                }
                else
                {
                    tabelaHtml.Append("<table id=\"tabela\" class=\"table table-bordered table-hover\"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Número</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Data</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Valor</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Forma Pgto</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Situação</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Ação</th>");
                    tabelaHtml.Append("</tr></thead><tbody>");

                    foreach (var cmda in lista)
                    {
                        var idCriotografado = Criptografia.Encrypt(cmda.NumeroComanda.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        tabelaHtml.Append("<tr>");
                        //numero                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", cmda.NumeroComanda));
                        //Colaborador                    
                        //tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", cmda.));
                        //Data                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", cmda.DataCmda.ToShortDateString()));
                        //Valor                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", cmda.ValorTotal));
                        //Forma Pgto                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", cmda.FormaPgto));

                        //Situação
                        var situacao = string.Format("<span class=\"label label-{0}\">{1}</span>"
                                                        , cmda.IdStatus == 1 ? "success" : cmda.IdStatus == 2 ? "warning" : "danger"
                                                        , cmda.IdStatus == 1 ? "Em Aberto" : cmda.IdStatus == 2 ? "Pendente" : "Fechada"
                                                     );
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", situacao));

                        // ========= AÇÕES ==========
                        //Editar                        
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\"><a href=\"neweditcomanda.aspx?id={0}&nc={1}\" class=\"btn btn-xs btn-info\" data-original-title=\"\"  title=\"Editar dados.\" ><i class=\"fa fa-pencil-square\"></i>  Editar</a></td>", Request.QueryString["id"], idCriotografado));

                        tabelaHtml.Append("</tr>");
                    }
                }
                tabelaHtml.Append("</tbody></table>");//Fim da tabela
                ltlTabela.Text = tabelaHtml.ToString();
            }
            catch (Exception ex)
            {
                ltlMsn.Text = string.Format("<div class=\"alert alert-danger alert-dismissible\">" +
                                            "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">X</button>" +
                                            "<h4><i class=\"icon fa fa-ban\"></i> CarregaTabela-Erro:</h4>{0}</div>", ex.Message);
            }
        }


    }
}