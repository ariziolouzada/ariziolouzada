using CriptografiaSgpm;
using sbessencial_cl;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;

namespace ariziolouzada.sbessencial.profissional
{
    public partial class servicos : Page
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
                            var idProfissional = Request.QueryString["id"];
                            idProfissional = Criptografia.Decrypt(idProfissional.Replace('_', '+'));

                            CarregaDados(int.Parse(idProfissional));
                            CarregaTabela(int.Parse(idProfissional));
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
                var profissional = Profissional.Pesquisar(id);
                if (profissional != null)
                {
                    lblCabecalho.Text = "SERVIÇOS REALIZADOS PELO PROFISSIONAL " + profissional.Nome;
                    hdfIdProfissional.Value = id.ToString();
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
            //Response.Redirect("~/sbessencial/cliente");
            if (Session["paremetrosDefault"].ToString() != string.Empty)
            {
                var paremetrosDefault = Session["paremetrosDefault"].ToString();
                Response.Redirect(string.Format("default.aspx?nome={0}", paremetrosDefault));
            }
            else
            {
                Response.Redirect("default.aspx");
            }


        }


        private void CarregaTabela(int idProfissional)
        {
            try
            {
                var tabelaHtml = new StringBuilder();
                var lista = new List<Servico>();

                switch (ddlExibir.SelectedValue)
                {
                    case "1":
                        lista = Servico.Lista(false, txtPesquisa.Value, 1);
                        break;
                    case "2":
                        lista = Servico.ListaServicoProfissional(false, idProfissional, 1, txtPesquisa.Value);
                        break;
                    case "3":
                        lista = Servico.ListaServicoNaoSelecionadoProfissional(idProfissional);
                        break;
                }


                if (lista.Count == 0)
                {
                    tabelaHtml.Append("<div class=\"panel panel-warning\"><div class=\"panel-heading\"> <h4 class=\"panel-title\"><i class=\"icon ion-clock text-warning\">" +
                                      "</i>!! Nenhum Item cadastrado !!</h4></div></div>");
                }
                else
                {
                    //terminar

                    tabelaHtml.Append("<table id=\"tabelaClientes\" class=\"table table-striped table-bordered \"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"></th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Descriçao</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Valor</th>");
                    //tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Ações</th>");
                    tabelaHtml.Append("</tr></thead>");
                    tabelaHtml.Append("<tbody>");


                    foreach (var svc in lista)
                    {
                        var idCriotografado = Criptografia.Encrypt(svc.Id.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        //tabelaHtml.Append("<tr class=\"odd gradeX\">");
                        tabelaHtml.Append("<tr>");

                        //Nome   
                        var chekdo = ServicoProfissional.Existe(svc.Id, int.Parse(hdfIdProfissional.Value)) ? "checked=\"checked\"" : "";
                        var link = string.Format("<input type=\"checkbox\" onchange=\"doalert(this, {0})\" {1}>", svc.Id, chekdo);

                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\">    {0}</td>", link));

                        //descricao                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", svc.Descricao));

                        //Valor                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", string.Format("{0:C}", svc.Valor)));

                        // ========= AÇÕES ==========

                        //Serviços realizados pelo profissional
                        //tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\">{0}</td>", ));



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
            var idProfissional = Request.QueryString["id"];
            idProfissional = Criptografia.Decrypt(idProfissional.Replace('_', '+'));

            CarregaTabela(int.Parse(idProfissional));
        }


        [WebMethod]
        public static string AdicionarServicoProfissional(string idSvc, string idProf)
        {
            try
            {
                if (!ServicoProfissional.Existe(int.Parse(idSvc), int.Parse(idProf)))
                {
                    if (!ServicoProfissional.Inserir(int.Parse(idSvc), int.Parse(idProf)))
                    {
                        return "<div class=\"alert alert-danger fade in m-b-15\"><strong>Não foi possível Adicionar Servico para este Profissional!</strong> "
                                + "<span class=\"close\" data-dismiss=\"alert\">X</span></div>";
                    }
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-danger fade in m-b-15\"><strong>AdicionarServicoProfissional-Error: </strong> " +
                       ex.Message + "<span class=\"close\" data-dismiss=\"alert\">X</span></div>";
            }
        }


        [WebMethod]
        public static string RetirarServicoProfissional(string idSvc, string idProf)
        {
            try
            {
                if (ServicoProfissional.Existe(int.Parse(idSvc), int.Parse(idProf)))
                {
                    if (!ServicoProfissional.Deletar(int.Parse(idSvc), int.Parse(idProf)))
                    {
                        return "<div class=\"alert alert-danger fade in m-b-15\"><strong>Não foi possível retirar Servico para este Profissional!</strong> "
                                + "<span class=\"close\" data-dismiss=\"alert\">X</span></div>";
                    }
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-danger fade in m-b-15\"><strong>RetirarServicoProfissional-Error: </strong> " +
                       ex.Message + "<span class=\"close\" data-dismiss=\"alert\">X</span></div>";
            }
        }


    }
}