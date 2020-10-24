using CriptografiaSgpm;
using ferrita_Cl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ariziolouzada.ferrita.fornecedor
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

                var lista = Fornecedor.ListaFornecedores();

                if (lista.Count == 0)
                {
                    tabelaHtml.Append("<div class=\"panel panel-warning\"><div class=\"panel-heading\"> <h4 class=\"panel-title\"><i class=\"icon ion-clock text-warning\">" +
                                      "</i>!! Nenhum FORNECEDOR cadastrado !!</h4></div></div>");
                }
                else
                {

                    tabelaHtml.Append("<table id=\"tabela\" class=\"table table-bordered table-hover\"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">ID</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Nome</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Telefone</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Situação</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Ação</th>");
                    tabelaHtml.Append("</tr></thead><tbody>");

                    foreach (var svc in lista)
                    {
                        var idCriotografado = Criptografia.Encrypt(svc.IdFornecedor.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        tabelaHtml.Append("<tr>");
                        //codigo                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", svc.IdFornecedor));
                        //Descrição                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", svc.NomeFornecedor));
                        //Pço Custo                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: right;\" >{0}</td>", svc.Telefone));                       
                        //Situação
                        var situacao = string.Format("<span class=\"label label-{0}\">{1}</span>"
                                                        , svc.IdStatusFornecedor == 1 ? "success" : svc.IdStatusFornecedor == 2 ? "warning" : "danger"
                                                        , svc.IdStatusFornecedor == 1 ? "Ativo" : svc.IdStatusFornecedor == 2 ? "Inativo" : "Excluido"
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

    }
}