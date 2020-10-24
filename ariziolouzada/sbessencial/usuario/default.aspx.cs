using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CriptografiaSgpm;
using sbessencial_cl;
using System.Text;
using System.Web.Services;

namespace ariziolouzada.sbessencial.usuario
{
    public partial class _default : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString.HasKeys())
                {
                    if (Request.QueryString["nome"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["nome"]))
                        txtPesquisa.Value = Request.QueryString["nome"];

                }
                
                CarregaTabela();
            }
        }


        private void CarregaTabela()
        {
            try
            {
                var tabelaHtml = new StringBuilder();
                var lista = UsuarioSbe.Lista(txtPesquisa.Value);

                if (lista.Count == 0)
                {
                    tabelaHtml.Append("<div class=\"panel panel-warning\"><div class=\"panel-heading\"> <h4 class=\"panel-title\"><i class=\"icon ion-clock text-warning\">" +
                                      "</i>!! Nenhum Item cadastrado !!</h4></div></div>");
                }
                else
                {
                    //terminar

                    tabelaHtml.Append("<table id=\"tabelaClientes\" class=\"table table-striped table-bordered \"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">ID</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Nome</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Login</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Status</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Ações</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"></th>");

                    tabelaHtml.Append("</tr></thead>");
                    tabelaHtml.Append("<tbody>");


                    foreach (var user in lista)
                    {
                        var idCriotografado = Criptografia.Encrypt(user.Id.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        //tabelaHtml.Append("<tr class=\"odd gradeX\">");
                        tabelaHtml.Append("<tr>");

                        //ID                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", user.Id));

                        //Nome                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", user.Nome));

                        //Login                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", user.Login));
                                                
                        //Status
                        var situacao = string.Format("<span class=\"badge badge-{0}\">{1}</span>"
                                                        , user.IdStatus == 1 ? "success" : user.IdStatus == 2 ? "warning" : "danger"
                                                        , user.IdStatus == 1 ? "Ativo" : user.IdStatus == 2 ? "Inativo" : "Excluido"
                                                     );
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", situacao));

                        // ========= AÇÕES ==========

                        //Editar                        
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\"><a href=\"newedit.aspx?id={0}\" class=\"btn btn-xs btn-info\" data-original-title=\"\"  title=\"Editar dados.\" ><i class=\"fa fa-pencil-square\"></i>  Editar</a></td>", idCriotografado));

                        //Permissões                        
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\"><a href=\"permissao.aspx?id={0}\" class=\"btn btn-outline btn-default\" data-original-title=\"\"  title=\"Permissões do usuário.\" ><i class=\"fa fa-gears\"></i>  Permissões</a></td>", idCriotografado));


                        tabelaHtml.Append("</tr>");
                    }
                    tabelaHtml.Append("</tbody></table>");
                }
                ltlTabela.Text = tabelaHtml.ToString();

            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaTabela-ERRO:" + ex.Message + "</p></div>";
            }

        }


        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            ltlMsn.Text = "";
            var nome = txtPesquisa.Value;
            Session["paremetrosDefault"] = string.Format("{0}", nome);
            Response.Redirect(string.Format("default.aspx?nome={0}", nome));

            //if (ddlMes.SelectedIndex > 0)
            //{
            //    CarregaTabela();
            //}
            //else
            //{
            //    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
            //                    "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Selecione o mês.</p></div>";
            //    ddlMes.Focus();
            //}
        }

    }
}