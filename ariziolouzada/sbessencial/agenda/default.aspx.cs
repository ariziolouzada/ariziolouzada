using CriptografiaSgpm;
using sbessencial_cl;
using System;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;

namespace ariziolouzada.sbessencial.agenda
{
    public partial class _default : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var data = DateTime.Now.ToString("yyyy-MM-dd");
                if (Session["hdfIdProfissional"] != null)
                {
                    if (Session["hdfIdProfissional"].ToString().Equals(""))
                        Session["hdfIdProfissional"] = "0";
                }
                else
                {
                    Session["hdfIdProfissional"] = "0";
                }

                if (Request.QueryString.HasKeys())
                {
                    if (Request.QueryString["dia"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["dia"]))
                    {
                        data = Request.QueryString["dia"];
                    }
                }
                txtData.Value = data;
                CarregaDdlProfissonal(true);
                CarregaTabela();
            }
        }


        private void CarregaDdlProfissonal(bool comSelecione)
        {
            var lista = Profissional.Lista(comSelecione);
            ddlProfissional.Items.Clear();
            ddlProfissional.DataSource = lista;
            ddlProfissional.DataValueField = "Id";
            ddlProfissional.DataTextField = "Nome";
            ddlProfissional.DataBind();

            if (comSelecione)
                ddlProfissional.SelectedIndex = 0;
        }


        private void CarregaTabela()
        {
            try
            {
                var data = txtData.Value.Equals(string.Empty) ? DateTime.Now.ToShortDateString() : txtData.Value;

                var tabelaHtml = new StringBuilder();

                var lista = Agenda.ListaDiaria(data, int.Parse(Session["hdfIdProfissional"].ToString()));

                if (lista.Count == 0)
                {
                    tabelaHtml.Append("<div class=\"panel panel-warning\"><div class=\"panel-heading\"> <h4 class=\"panel-title\"><i class=\"icon ion-clock text-warning\">" +
                                      "</i>!! Nenhum Item cadastrado !!</h4></div></div>");
                }
                else
                {
                    tabelaHtml.Append("<table id=\"tabelaAgenda\" class=\"table table-striped table-bordered\"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Hora</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Situação</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Cliente</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Serviço</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Profissional</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"></th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Ações</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"></th>");

                    tabelaHtml.Append("</tr></thead>");
                    tabelaHtml.Append("<tbody>");

                    var dataHoraAtual = DateTime.Now;

                    foreach (var agenda in lista)
                    {
                        var idCriotografado = Criptografia.Encrypt(agenda.IdRegistroAgenda.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        //tabelaHtml.Append("<tr class=\"odd gradeX\">");
                        tabelaHtml.Append("<tr>");

                        //tratar a questão de exibir somente a hora e coloacar a cor na situação

                        //Hora    
                        var cssHora = dataHoraAtual > agenda.DataHora ? "danger" : agenda.DataHora == dataHoraAtual ? "warning" : "primary";
                        var hora = string.Format("{0}h{1}min", string.Format("{0:00}", agenda.DataHora.Hour), string.Format("{0:00}", agenda.DataHora.Minute));
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" ><span class=\"badge badge-{1}\">{0}</span></td>", hora, cssHora));


                        //Situacao   1-AGENDADO  2-CONFIRMADO 3-CANCELADO 4-EXECUTADO 5-NAO EXECUTADO
                        var cssStatus = agenda.IdStatus == 1 ? "primary" : agenda.IdStatus == 2 ? "info" : agenda.IdStatus == 3 ? "danger" : agenda.IdStatus == 4 ? "success" : "warning";
                        //CAso não seja editado fica na condição de ID 6 - AGUARDANDO FINALIZAÇÃO
                        if (dataHoraAtual > agenda.DataHora && agenda.IdStatus == 1)
                        {
                            cssStatus = "default";
                            agenda.Status = "AGUARDANDO FINALIZAÇÃO";
                        }

                        if (agenda.IdStatus == 7)
                        {
                            cssStatus = "danger";
                            agenda.Status = "AGENDA NÃO DISPONÍVEL";
                        }

                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" ><span class=\"label label-{1}\">{0}</span></td>", agenda.Status, cssStatus));

                        //Cliente                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", agenda.Cliente));

                        //Servico   
                        var listaServicos = AgendaServicos.Lista(agenda.IdRegistroAgenda);
                        var servicos = new StringBuilder("--");
                        for (int i = 0; i < listaServicos.Count; i++)
                        {
                            servicos.Append(string.Format(" {0};", listaServicos[i].DescricaoServico));
                            if (i + 1 < listaServicos.Count)
                                servicos.Append("<br />--");
                        }
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", servicos));

                        //Profissional
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", agenda.NomeProfissional));


                        // ========= AÇÕES ==========
                        //CONFIRMAÇÃO
                        //tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\"><a href=\"#\" class=\"btn btn-warning\" onclick=\"confirmacaoAgenda({0})\" data-original-title=\"\"  title=\"Excluir registro.\" ><i class=\"fa fa-check-square-o\"></i>  </a></td>", agenda.IdRegistroAgenda));
                        if (agenda.IdStatus <= 2)
                            tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\"><a href=\"confirmacao.aspx?id={0}\" class=\"btn btn-warning\" data-original-title=\"\"  title=\"Confirmar Agenda.\" ><i class=\"fa fa-check-square-o\"></i>  Confirmar</a></td>", idCriotografado));
                        else
                            tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\"></td>"));

                        //Editar                        
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\"><a href=\"newedit.aspx?id={0}\" class=\"btn btn-info\" data-original-title=\"\"  title=\"Editar dados.\" ><i class=\"fa fa-pencil-square\"></i>  Editar</a></td>", idCriotografado));

                        //Excluir                        
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\"><a href=\"excluir.aspx?id={0}\" class=\"btn btn-danger\" data-original-title=\"\"  title=\"Excluir registro.\" ><i class=\"fa fa-eraser\"></i>  Excluir</a></td>", idCriotografado));
                        //tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\"><a href=\"#\" data-toggle=\"modal\" data-target=\"#myModal4\" class=\"btn btn-danger\" onclick=\"capturarId({0})\" data-original-title=\"\"  title=\"Excluir registro.\" ><i class=\"fa fa-eraser\"></i>  Excluir</a></td>", agenda.IdRegistroAgenda));

                        //<button type="button" class="btn btn-danger" data-toggle="modal" data-target="#myModal4">Excluir</ button >

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

        protected void BtnSimDelReg_Click(object sender, EventArgs e)
        {
            try
            {
                var id = HttpContext.Current.Session["idAgendaExcluir"].ToString();
                if (Agenda.Excluir(long.Parse(id)))
                {
                    if (Session["paremetrosDefault"].ToString() != string.Empty)
                    {
                        var paremetrosDefault = Session["paremetrosDefault"].ToString();
                        Response.Redirect(string.Format("default.aspx?dia={0}", paremetrosDefault));
                    }
                    else
                    {
                        Response.Redirect("default.aspx");
                    }
                }


            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaTabela-ERRO:" + ex.Message + "</p></div>";
            }
        }

        protected void btnCarregarLista_Click(object sender, EventArgs e)
        {
            ltlMsn.Text = "";
            var data = txtData.Value;
            Session["paremetrosDefault"] = string.Format("{0}", data);
            Response.Redirect(string.Format("default.aspx?dia={0}", data));
        }


        [WebMethod]
        public static string SelecaoProfissional(string id)
        {
            try
            {
                HttpContext.Current.Session["hdfIdProfissional"] = id;
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-danger fade in m-b-15\"><strong>CapturarId-Error: </strong> " +
                       ex.Message + "<span class=\"close\" data-dismiss=\"alert\">X</span></div>";
            }
        }

        [WebMethod]
        public static string ConfirmacaoAgenda(string id)
        {
            try
            {
                if (Agenda.EditarStatus(long.Parse(id), 2))
                {
                    return HttpContext.Current.Session["paremetrosDefault"].ToString();
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-danger fade in m-b-15\"><strong>CapturarId-Error: </strong> " +
                       ex.Message + "<span class=\"close\" data-dismiss=\"alert\">X</span></div>";
            }
        }


        [WebMethod]
        public static string CapturarId(string id)
        {
            try
            {
                HttpContext.Current.Session["idAgendaExcluir"] = id;
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-danger fade in m-b-15\"><strong>CapturarId-Error: </strong> " +
                       ex.Message + "<span class=\"close\" data-dismiss=\"alert\">X</span></div>";
            }
        }

        [WebMethod]
        public static string ExcluirId()
        {
            try
            {
                //erro não está excluindo!!
                var id = HttpContext.Current.Session["idAgendaExcluir"].ToString();
                if (Agenda.Excluir(long.Parse(id)))
                {
                    return HttpContext.Current.Session["paremetrosDefault"].ToString();
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-danger fade in m-b-15\"><strong>ddlAno_SelectedIndexChanged-Error: </strong> " +
                       ex.Message + "<span class=\"close\" data-dismiss=\"alert\">X</span></div>";
            }
        }


        //protected void btnSimDelReg_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var id = Session["idAgendaExcluir"].ToString();
        //        if (Agenda.e(int.Parse(id)))
        //        {
        //            btnCarregarLista_Click(sender, e);
        //        }
        //        else
        //        {
        //            ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
        //                          "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO ao excluir o registro!!</p></div>";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
        //                     "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaTabelaEpi-ERRO:" + ex.Message + "</p></div>";
        //    }
        //}

    }
}