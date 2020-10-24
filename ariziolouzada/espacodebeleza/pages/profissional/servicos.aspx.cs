using CriptografiaSgpm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Services;
using System.Web.UI;
using espacobeleza_cl;
using System.Web;

namespace ariziolouzada.espacodebeleza.pages.profissional
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
                            hdfIdProfCriptografado.Value = idProfissional;//usado no javascript da página para fazer o refress no metodo doalert
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
                var profissional = ProfissionalEspBel.Pesquisar(id);
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

        //protected void btnSalvarComissao_Click(object sender, EventArgs e)
        //{
        //    var idProf = hdfIdProfissional.Value;
        //}

        private void CarregaTabela(int idProfissional)
        {
            try
            {
                //RECUPERAR O COOKIE
                var idEmpresaContratante = "0";
                HttpCookie cookie = Request.Cookies["idEmpresaContratante"];
                if (cookie != null && cookie.Value != null)
                    idEmpresaContratante = cookie.Value;

                idEmpresaContratante = Criptografia.Decrypt(idEmpresaContratante);

                //colocar o campo para preencher a comissão!!
                var tabelaHtml = new StringBuilder();
                var lista = new List<ServicoEspBel>();

                switch (ddlExibir.SelectedValue)
                {
                    case "1":
                        lista = ServicoEspBel.Lista(false, txtPesquisa.Value, 1, int.Parse(idEmpresaContratante));
                        break;
                    case "2":
                        lista = ServicoEspBel.ListaServicoProfissional(false, idProfissional, 1, txtPesquisa.Value, int.Parse(idEmpresaContratante));
                        break;
                    case "3":
                        lista = ServicoEspBel.ListaServicoNaoSelecionadoProfissional(idProfissional);
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
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Descrição</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Comissão %</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"></th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Valor Serviço</th>");
                    //tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Ações</th>");
                    tabelaHtml.Append("</tr></thead>");
                    tabelaHtml.Append("<tbody>");

                    foreach (var svc in lista)
                    {
                        var idCriotografado = Criptografia.Encrypt(svc.Id.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        //tabelaHtml.Append("<tr class=\"odd gradeX\">");
                        tabelaHtml.Append("<tr>");

                        //Nome   onchange
                        var chekdo = ServicoProfissionalEspBel.Existe(svc.Id, int.Parse(hdfIdProfissional.Value)) ? "checked=\"checked\"" : "";
                        //var link = string.Format("<div class=\"i-checks\"><input type=\"checkbox\" onclick=\"doalert(this, {0})\" {1} /></div>", svc.Id, chekdo);
                        var link = string.Format("<input type=\"checkbox\" onchange=\"doalert(this, {0})\" {1} />", svc.Id, chekdo);

                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\">    {0}</td>", link));

                        //descricao                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", svc.Descricao));

                        //comissao                    
                        var comissao = ServicoProfissionalEspBel.PesquisaComissao(svc.Id, idProfissional);
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", comissao));

                        //Editar Comissão
                        //tratar para exibir somente se o checkbox estver selecionado
                        var editComissao = chekdo != string.Empty ? string.Format("<a href=\"#\" data-toggle=\"modal\" data-target=\"#myModal6\" onclick=\"capturaDadosEditComissao({0});\"><i class=\"fa fa-pencil-square-o fa-2x\"></i></a>", svc.Id) : "";
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", editComissao));

                        //Valor                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: right;\" >{0}</td>", string.Format("{0:C}", svc.Valor)));

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
                if (!ServicoProfissionalEspBel.Existe(int.Parse(idSvc), int.Parse(idProf)))
                {
                    if (!ServicoProfissionalEspBel.Inserir(int.Parse(idSvc), int.Parse(idProf)))
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
                if (ServicoProfissionalEspBel.Existe(int.Parse(idSvc), int.Parse(idProf)))
                {
                    if (!ServicoProfissionalEspBel.Deletar(int.Parse(idSvc), int.Parse(idProf)))
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

        [WebMethod]
        public static string CapturaDadosEditComissao(string idSvc, string idProf)
        {
            try
            {
                var comissao = ServicoProfissionalEspBel.PesquisaComissao(int.Parse(idSvc), int.Parse(idProf));
                return comissao.ToString();
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-danger fade in m-b-15\"><strong>RetirarServicoProfissional-Error: </strong> " +
                       ex.Message + "<span class=\"close\" data-dismiss=\"alert\">X</span></div>";
            }
        }

        [WebMethod]
        public static string SalvarComissao(string idSvc, string idProf, string comissao)
        {
            try
            {
                if(!ServicoProfissionalEspBel.EditarComissao(int.Parse(idSvc), int.Parse(idProf), int.Parse(comissao)))
                {
                    return "<div class=\"alert alert-danger fade in m-b-15\"><strong>Não foi possível editar a comissão para este serviço do Profissional!</strong> "
                                + "<span class=\"close\" data-dismiss=\"alert\">X</span></div>";
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