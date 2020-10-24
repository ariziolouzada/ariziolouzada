using CriptografiaSgpm;
using sbessencial_cl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ariziolouzada.sbessencial.usuario
{
    public partial class permissao : Page
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
            try
            {
                var usr = UsuarioSbe.Pesquisar(id);
                if (usr != null)
                {
                    lblNomeUsuario.Text = string.Format("Permissões do usuário {0}", usr.Nome);

                }
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaDados-ERRO:" + ex.Message + "</p></div>";
            }
        }


        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }


        private void CarregaTabela(int idUser)
        {
            try
            {
                var tabelaHtml = new StringBuilder();
                var lista = PermissaoSbe.Lista("SITE.ESSENCIAL_SALAO.");

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
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Permissão</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Ações</th>");
                    //tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"></th>");

                    tabelaHtml.Append("</tr></thead>");
                    tabelaHtml.Append("<tbody>");


                    foreach (var permissao in lista)
                    {
                        var idCriotografado = Criptografia.Encrypt(permissao.Id.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        //tabelaHtml.Append("<tr class=\"odd gradeX\">");
                        tabelaHtml.Append("<tr>");

                        //ID                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", permissao.Id));

                        //Permissão                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", permissao.Descricao));

                        // ========= AÇÕES ==========

                        //Permissões                        
                        //tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\"><a href=\"permissao.aspx?id={0}\" class=\"btn btn-outline btn-default\" data-original-title=\"\"  title=\"Permissões do usuário.\" ><i class=\"fa fa-gears\"></i>  Permissões</a></td>", idCriotografado));
                        var enableAdd = "";
                        var enableDel = "";
                        var temPermissao = UsuarioSbePermissao.Possui(idUser, permissao.Id);

                        if (temPermissao)
                        {
                            //RETIRAR 
                            enableAdd = "invisible";

                            //tabelaHtml.Append(string.Format("<td style=\"text-align: center\"><a href=\"javascript:;\" onclick=\"capturarIdPerfil({0}, 1, {1}, {2})\" class=\"btn btn-xs btn-danger\" data-original-title=\"\"  title=\"Retirar o perfil\" ><i class=\"fa fa-minus-square\"></i>  Retirar</a></td>", idPerfil, idOme, idUsuario));
                        }
                        else
                        { // CONCEDER   
                            enableDel = "invisible";
                            //tabelaHtml.Append(string.Format("<td style=\"text-align: center\"><a href=\"javascript:;\" onclick=\"capturarIdPerfil({0}, 2, {1}, {2})\"  class=\"btn btn-xs btn-success\" data-original-title=\"\"  title=\"Conceder o perfil.\" ><i class=\"fa fa-plus-square\"></i>  Conceder</a></td>", idPerfil, idOme, idUsuario));
                        }

                        tabelaHtml.Append(string.Format("<td style=\"text-align: center\">" +
                                                          "<a href=\"javascript:;\" onclick=\"capturarIdPerfil({0}, 1, {1})\" id-perfil={0} class=\"btn btn-xs btn-danger {2}\" data-original-title=\"\"  title=\"Retirar o perfil\" ><i class=\"fa fa-minus-square\"></i>  Retirar</a>" +
                                                          "<a href =\"javascript:;\" onclick=\"capturarIdPerfil({0}, 2, {1})\"  id-perfil={0} class=\"btn btn-xs btn-primary {3}\" data-original-title=\"\"  title=\"Conceder o perfil.\" ><i class=\"fa fa-plus-square\"></i>  Conceder</a>" +
                                                          "</td>", permissao.Id, idUser, enableDel, enableAdd));

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



        [WebMethod]
        public static string CapturarIdPerfil(string idPermissao, string acao, string idUsuario)
        {
            try
            {
                 
                if (acao.Equals("1"))
                {
                    //Retirar Perfil
                    UsuarioSbePermissao.Apagar(int.Parse(idUsuario), int.Parse(idPermissao));

                }
                else
                {
                    //adiciona
                    UsuarioSbePermissao.Inserir(int.Parse(idUsuario), int.Parse(idPermissao));

                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CapturarIdPerfil-ERRO:" + ex.Message + "</p></div>";
            }

        }

    }
}