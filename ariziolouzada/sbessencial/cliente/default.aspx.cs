using System;
using System.Web.UI;
using CriptografiaSgpm;
using sbessencial_cl;
using System.Text;
using System.Web.Services;

namespace ariziolouzada.sbessencial.cliente
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
                var lista = Cliente.Lista(false, txtPesquisa.Value);

                if (lista.Count == 0)
                {
                    tabelaHtml.Append("<div class=\"panel panel-warning\"><div class=\"panel-heading\"> <h4 class=\"panel-title\"><i class=\"icon ion-clock text-warning\">" +
                                      "</i>!! Nenhum Item cadastrado !!</h4></div></div>");
                }
                else
                {
                    //terminar

                    tabelaHtml.Append("<table id=\"tabelaClientes\" class=\"table table-striped table-bordered \"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Nome</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Data Nascto</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Telefones</th>");
                    //tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Email</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Status</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Ações</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"></th>");

                    tabelaHtml.Append("</tr></thead>");
                    tabelaHtml.Append("<tbody>");


                    foreach (var cliente in lista)
                    {
                        var idCriotografado = Criptografia.Encrypt(cliente.Id.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        //tabelaHtml.Append("<tr class=\"odd gradeX\">");
                        tabelaHtml.Append("<tr>");

                        //Nome                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", cliente.Nome));

                        //data nascto                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>",
                                                        cliente.DataNascto != DateTime.MinValue
                                                        ? cliente.DataNascto.ToShortDateString()
                                                        : "Não Cadastrado")
                                                    );

                        //Telefones
                        var telefones = new StringBuilder();
                        if (!cliente.TelCelular1.Equals(""))
                            telefones.Append(cliente.TelCelular1);

                        if (!cliente.TelCelular1.Equals("") && !cliente.TelCelular2.Equals(""))
                            telefones.Append(" / " + cliente.TelCelular2);
                        else
                            if (!cliente.TelCelular2.Equals(""))
                            telefones.Append(cliente.TelCelular2);

                        if (!cliente.TelCelular2.Equals("") && !cliente.TelFixo.Equals(""))
                            telefones.Append(" / " + cliente.TelFixo);
                        else
                            if (!cliente.TelFixo.Equals(""))
                            telefones.Append(cliente.TelFixo);

                        //cliente.TelCelular1, cliente.TelCelular2, cliente.TelFixo);
                        //var telefones = string.Format("{0} / {1} / {2}", cliente.TelCelular1, cliente.TelCelular2, cliente.TelFixo);
                        //var telcliente = string.Format("<button type=\"button\" class=\"btn btn-primary\" data-container=\"body\" data-toggle=\"popover\" data-placement=\"top\" data-content=\"{0}\" data-original-title=\"\" title=\"\"><i class=\"fa fa-fax\"></i></button>", telefones);
                        //tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", telcliente));
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", telefones));

                        ////Email
                        //var email = string.Format("<button type=\"button\" class=\"btn btn-warning\" data-container=\"body\" data-toggle=\"popover\" data-placement=\"top\" data-content=\"{0}\" data-original-title=\"\" title=\"\"><i class=\"fa fa-at\"></i></button>", cliente.Email);
                        //tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", email));

                        //Situação
                        var situacao = string.Format("<span class=\"badge badge-{0}\">{1}</span>"
                                                        , cliente.IdStatus == 1 ? "success" : cliente.IdStatus == 2 ? "warning" : "danger"
                                                        , cliente.IdStatus == 1 ? "Ativo" : cliente.IdStatus == 2 ? "Inativo" : "Excluido"
                                                     );
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", situacao));

                        // ========= AÇÕES ==========

                        //Editar                        
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\"><a href=\"newedit.aspx?id={0}\" class=\"btn btn-xs btn-info\" data-original-title=\"\"  title=\"Editar dados.\" ><i class=\"fa fa-pencil-square\"></i>  Editar</a></td>", idCriotografado));

                        //ResetarSenha                        
                        var btnReseteSenha = string.Format("<button class=\"btn btn-warning btn-circle\" type=\"button\" onclick=\"resetarsenha({0})\"  title=\"Resetar a senha de acesso a agenda web.\"><i class=\"fa fa-unlock\"></i></button>", cliente.Id);
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\">{0}</td>", btnReseteSenha));

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
        

        [WebMethod]
        public static string ResetarSenha(string idCliente)
        {
            try
            {
                var clt = Cliente.Pesquisar(int.Parse(idCliente));
                if (clt.TelCelular1.Length < 9)
                {
                    //return "<div class=\"alert alert-danger fade in m-b-15\"><strong>ResetarSenha-Error: </strong> " +
                    //       "Atualize 1º o número do tel. Celular para resetar a senha do cliente selecionado!!<span class=\"close\" data-dismiss=\"alert\">X</span></div>";

                    return "Atualize 1º o número do tel. Celular para resetar a senha do cliente selecionado!!";
                }

                if (Cliente.ResetarSenha(int.Parse(idCliente)))
                {
                    //return "<div class=\"alert alert-block alert-success fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                    //        "</button><p><i class=\"fa fa-check-square fa-lg\"></i> Senha Resetada com sucesso!</p></div>";

                    return "1";
                }

                //return "<div class=\"alert alert-danger fade in m-b-15\"><strong>ResetarSenha-Error: </strong> " +
                //       "Não foi possível resetar a senha do cliente selecionado!!<span class=\"close\" data-dismiss=\"alert\">X</span></div>";

                return "Não foi possível resetar a senha do cliente selecionado!!";

            }
            catch (Exception ex)
            {
                //return "<div class=\"alert alert-danger fade in m-b-15\"><strong>ResetarSenha-Error: </strong> " +
                //       ex.Message + "<span class=\"close\" data-dismiss=\"alert\">X</span></div>";
                return "ResetarSenha-Error: " + ex.Message ;

            }
        }


    }
}