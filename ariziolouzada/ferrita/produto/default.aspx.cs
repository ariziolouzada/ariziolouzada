using CriptografiaSgpm;
using ferrita_Cl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ariziolouzada.ferrita.produto
{
    public partial class _default : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                CarregaTabela();

            }
        }


        private void CarregaTabela()
        {
            try
            {
                var tabelaHtml = new StringBuilder();

                var lista = ServicoProduto.ListaServicoProduto(txtPesquisaValue.Value);

                ////Ajuste do valor do lucro
                //foreach (var svcprod in lista)
                //{
                //    var lucro = svcprod.PrecoVenda - svcprod.PrecoCusto;
                //    ServicoProduto.EditarValorLucro(svcprod.IdProduto, lucro);
                //}
                //lista = ServicoProduto.ListaServicoProduto(txtPesquisaValue.Value);
                //// -=============================

                if (lista.Count == 0)
                {
                    tabelaHtml.Append("<div class=\"panel panel-warning\"><div class=\"panel-heading\"> <h4 class=\"panel-title\"><i class=\"icon ion-clock text-warning\">" +
                                      "</i>!! Nenhum PRODUTO cadastrado !!</h4></div></div>");
                }
                else
                {

                    tabelaHtml.Append("<table id=\"tabela\" class=\"table table-bordered table-hover\"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Código</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Descrição</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Pç Custo R$</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Mg Lucro %</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Lucro R$</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Pç Venda R$</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">TAM</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Qtde. Estoque</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Situação</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Ação</th>");
                    tabelaHtml.Append("</tr></thead><tbody>");

                    foreach (var svc in lista)
                    {
                        var idCriotografado = Criptografia.Encrypt(svc.IdProduto.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        var cssLinha = svc.QtdeEstoque == 0 ? "class=\"danger\"  style=\"color: red; font-weight: bold; \"" : "";
                        tabelaHtml.Append(string.Format("<tr {0}>", cssLinha));
                        //codigo                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", svc.CodigoProduto));
                        //Descrição                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", svc.DescricaoProduto));
                        //Pço Custo                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: right;\" >{0}</td>", svc.PrecoCusto));
                        //margem Lucro                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", svc.MargemLucro));
                        //Lucro                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: right;\" >{0}</td>", svc.Lucro));
                        //Pç Venda                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: right;\" >{0}</td>", svc.PrecoVenda));
                        //tamanho                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", svc.TamanhoUnico.Equals("SIM") ? "S" : "N"));

                        if (svc.TamanhoUnico.Equals("SIM"))
                            //Qtde Estoque                    
                            tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", svc.QtdeEstoque));
                        else
                        {
                            var htmlQtds = new StringBuilder();
                            htmlQtds.Append("<a href=\"#\" title=\"");                            
                            htmlQtds.Append(string.Format("P:{0} - M:{1}", svc.QtdeEstqTamanhoP, svc.QtdeEstqTamanhoM));
                            htmlQtds.Append(string.Format(" - G:{0} - GG:{1}", svc.QtdeEstqTamanhoG, svc.QtdeEstqTamanhoGG));
                            htmlQtds.Append(string.Format(" - EG:{0}", svc.QtdeEstqTamanhoEG));
                            htmlQtds.Append(string.Format("\">{0}</a>", svc.QtdeEstoque));
                            tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", htmlQtds));
                        }


                        //Situação
                        var situacao = string.Format("<span class=\"label label-{0}\">{1}</span>"
                                                        , svc.IdStatus == 1 ? "success" : svc.IdStatus == 2 ? "warning" : "danger"
                                                        , svc.IdStatus == 1 ? "Ativo" : svc.IdStatus == 2 ? "Inativo" : "Excluido"
                                                     );
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", situacao));

                        // ========= AÇÕES ==========
                        //Editar                        
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\"><a href=\"newedit.aspx?id={0}\" class=\"btn btn-xs btn-info\" data-original-title=\"\"  title=\"Editar dados.\" ><i class=\"fa fa-pencil-square\"></i>  Editar</a></td>", idCriotografado));

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

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                CarregaTabela();
            }
            catch (Exception ex)
            {
                ltlMsn.Text = string.Format("<div class=\"alert alert-danger alert-dismissible\">" +
                                            "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">X</button>" +
                                            "<h4><i class=\"icon fa fa-ban\"></i> btnPesquisar_Click-Erro:</h4>{0}</div>", ex.Message);
            }
        }

        protected void btnAlterLucro_Click(object sender, EventArgs e)
        {

        }
    }
}