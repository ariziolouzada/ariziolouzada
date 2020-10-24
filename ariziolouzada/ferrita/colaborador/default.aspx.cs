using CriptografiaSgpm;
using ferrita_Cl;
using System;
using System.Text;
using System.Web.UI;

namespace ariziolouzada.ferrita.colaborador
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

                var lista = Colaborador.Lista(txtPesquisaValue.Value);

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
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"></th>");
                    tabelaHtml.Append("</tr></thead><tbody>");

                    foreach (var svc in lista)
                    {
                        var idCriotografado = Criptografia.Encrypt(svc.Id.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        tabelaHtml.Append("<tr>");
                        //codigo                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", svc.Id));
                        //Descrição                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", svc.Nome));
                        //Pço Custo                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", svc.Telefone));
                        //Situação
                        var situacao = string.Format("<span class=\"label label-{0}\">{1}</span>"
                                                        , svc.IdStatus == 1 ? "success" : svc.IdStatus == 2 ? "warning" : "danger"
                                                        , svc.IdStatus == 1 ? "Ativo" : svc.IdStatus == 2 ? "Inativo" : "Excluido"
                                                     );
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", situacao));

                        // ========= AÇÕES ==========
                        //Editar                        
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\"><a href=\"newedit.aspx?id={0}\" class=\"btn btn-xs btn-info\" data-original-title=\"\"  title=\"Editar dados.\" ><i class=\"fa fa-pencil-square\"></i>  Editar</a></td>", idCriotografado));
                        //Comanda                        
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\">" +
                            "<a href=\"listacmdacolab.aspx?id={0}\" class=\"btn btn-xs btn-default\" data-original-title=\"\"  title=\"Comandas deste Colaborador.\" ><i class=\"fa fa-shopping-cart\"></i>  Comandas</a>" +
                            "&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"neweditcomanda.aspx?id={0}&nc=X5A1oqTnjBE=\" class=\"btn btn-xs btn-default\" data-original-title=\"\"  title=\"Comandas deste Colaborador.\" ><i class=\"fa fa-plus-square\"></i>  Adicionar</a>" +
                            "</td>", idCriotografado));

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

    }
}