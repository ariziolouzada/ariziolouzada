using ariziolouzada.classes;
using CriptografiaSgpm;
using System;
using System.Text;
using System.Web.UI;

namespace ariziolouzada.mycarmanutencao.veiculo
{
    public partial class _default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregagaTabela();
            }
        }

        private void CarregagaTabela()
        {
            try
            {
                var lista = MyCar.Listar();
                var tabelaHtml = new StringBuilder();

                tabelaHtml.Append("<table id=\"example1\" class=\"table table-bordered table-striped\">");
                if (lista.Count == 0)
                {
                    tabelaHtml.Append("<tr class=\"danger\"><td align=\"center\" colspan=\"8\">Não foi encontrado nenhum registro para o carro selecionado.</td></tr>");
                }
                else
                {
                    //Cabeçalho tabela
                    tabelaHtml.Append("<thead><tr>");
                    tabelaHtml.Append("<th style=\"vertical-align: middle; text-align: center\">ID</th>");
                    tabelaHtml.Append("<th style=\"vertical-align: middle; text-align: center\">MARCA/MODELO</th>");
                    tabelaHtml.Append("<th style=\"vertical-align: middle; text-align: center\">ANO</th>");
                    tabelaHtml.Append("<th style=\"vertical-align: middle; text-align: center\">PLACAS</th>");
                    tabelaHtml.Append("<th style=\"vertical-align: middle; text-align: center\">COR</th>");
                    tabelaHtml.Append("<th style=\"vertical-align: middle; text-align: center\">CHASSI</th>");
                    tabelaHtml.Append("<th style=\"vertical-align: middle; text-align: center\">SITUAÇÃO</th>");
                    tabelaHtml.Append("<th></th>");
                    tabelaHtml.Append("</tr></thead><tbody>");

                    //linhas
                    foreach (var item in lista)
                    {
                        tabelaHtml.Append("<tr>");
                        //ID                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\">{0}</td>", item.Id));
                        //Marca/Modelo                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\">{0}</td>", item.MarcaModel));
                        //AnoModeloFabricacao
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\">{0}</td>", item.AnoModeloFabricacao));
                        //Placas                     
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\">{0}</td>", item.Placas));
                        //Cor                   
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\">{0}</td>", item.Cor));
                        //Chassi                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\">{0}</td>", item.Chassi));

                        //Situação
                        var situacao = string.Format("<span class=\"badge badge-{0}\">{1}</span>"
                                                        , item.IdStatus == 1 ? "success" : item.IdStatus == 2 ? "warning" : "danger"
                                                        , item.IdStatus == 1 ? "Ativo" : item.IdStatus == 2 ? "Inativo" : "Excluido"
                                                     );
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", situacao));

                        // ============ AÇÕES ===============
                        var idCriotografado = Criptografia.Encrypt(item.Id.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        //Editar                        
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\"><a href=\"newedit.aspx?id={0}\" " +
                            "class=\"btn btn-xs btn-info\" data-original-title=\"\"  title=\"Editar dados.\" ><i class=\"fas fa-edit fa-lg\"></i>" +
                            "  Editar</a></td>", idCriotografado));


                        tabelaHtml.Append("</tr>");
                    }
                    tabelaHtml.Append(" </tbody></table>");
                }
                ltlTabela.Text = tabelaHtml.ToString();
            }
            catch (Exception ex)
            {
                ltlMsn.Text = string.Format("<div class=\"row\"><div class=\"col-sm-12\"><div class=\"alert alert-danger alert-dismissible\">"
                                            //+ "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">X</button>"
                                            + "<h5><i class=\"icon fas fa-ban\"></i> Erro:</h5>{0}</div></div></div>"
                                            );
            }

        }
   
    }
}