using sbessencial_cl;
using System;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;

namespace ariziolouzada.sbessencial.minhagenda
{
    public partial class _default : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString.HasKeys())
                {
                    string dt, idp;
                    if (Request.QueryString["dt"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["dt"]))
                    {
                        dt = Request.QueryString["dt"];
                        txtData.Value = dt;
                    }

                    if (Request.QueryString["idp"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["idp"]))
                    {
                        CarregaDdlProfissonal(true);
                        idp = Request.QueryString["idp"];
                        ddlProfissional.SelectedValue = idp;
                    }

                    CarregarAgenda();
                }
                else
                {
                    txtData.Value = DateTime.Now.ToString("yyyy-MM-dd");
                    CarregaDdlProfissonal(true);
                }

            }
        }

        protected void btnCarregar_Click(object sender, EventArgs e)
        {
            if (VerificaBtnCarrega())
            {
                CarregarAgenda();
            }
        }


        private bool VerificaBtnCarrega()
        {
            ltlMsn.Text = string.Empty;

            if (txtData.Value == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                            "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Selecione o dia da agenda a ser carregada!!</p></div>";
                txtData.Focus();
                return false;
            }

            var data = Convert.ToDateTime(txtData.Value);
            if (data < DateTime.Now.Date)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                              "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Selecione uma data maior ou igual a hoje para ser carregada a agenda!!</p></div>";
                txtData.Focus();
                return false;
            }

            if (ddlProfissional.SelectedIndex == 0)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                            "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Selecione o profissiona!!</p></div>";
                ddlProfissional.Focus();
                return false;
            }


            return true;
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


        private void CarregarAgenda()
        {
            try
            {
                //erro CarregaTabelaAgenda-ERRO:Could not find specified column in results: nomeCleinte

                var tabelaHtml = new StringBuilder();
                tabelaHtml.Append("<div id=\"ltlTabelaHorarios\"><table class=\"table table-striped table-bordered \"><thead><tr>");
                tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle; width: 30%;\">Horário</th>");
                tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Situação</th>");
                tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Ações</th>");
                tabelaHtml.Append("</tr></thead>");
                tabelaHtml.Append("<tbody>");

                for (int hora = 8; hora < 21; hora++)
                {
                    //ver pq não está aparecendo a agenda cadastrada!!

                    tabelaHtml.Append("<tr>");
                    var horario = string.Format("De {0} às {1} horas.", hora, hora + 1);
                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left; \" >{0}</td>", horario));

                    var sitAgendaHtml = "<span class=\"badge badge-primary\">Disponível</span>"; //                       
                    //var agendaDisponivel = true;
                    var horaAtual = DateTime.Now.Hour;
                    var btnAgendar = string.Format("<button type=\"button\" onclick=\"capturaHorarioAgenda({0})\" class=\"btn btn-success btn-xs\" ><i class=\"fa fa-calendar-o\"></i>&nbsp;&nbsp;<span class=\"bold\">Agendar</span></button>", hora);

                    //Pesquisa na tbl_Agenda
                    var agd = Agenda.Pesquisa(DateTime.Parse(txtData.Value), hora, int.Parse(ddlProfissional.SelectedValue));

                    if (DateTime.Parse(txtData.Value).Date == DateTime.Now.Date)
                    {
                        if (horaAtual <= hora)
                        {
                            //Pesquisa na tbl_Agenda
                            //if (Agenda.PesquisaDataHora(DateTime.Parse(txtData.Value), hora, int.Parse(ddlProfissional.SelectedValue)) == false)
                            if (agd != null)
                            {
                                if (agd.IdStatus == 7)
                                {
                                    sitAgendaHtml = "<span class=\"badge badge-danger\">Profisssional Indisponível</span>";
                                    btnAgendar = string.Empty;

                                    ////Pesquisa na tbl_Agenda_temp
                                    //if (AgendaTemp.PesquisaDataHora(DateTime.Parse(txtData.Value), hora, int.Parse(ddlProfissional.SelectedValue)))
                                    //{
                                    //    sitAgendaHtml = "<span class=\"badge badge-warning\">Aguardando confirmação</span>";
                                    //    btnAgendar = string.Format("<button type=\"button\" onclick=\"capturaHorarioAgendaExcluir({0})\" class=\"btn btn-danger btn-xs\" data-toggle=\"modal\" data-target=\"#myModalExcluir\"><i class=\"fa fa-eraser\"></i>&nbsp;&nbsp;<span class=\"bold\">Excluir</span></button>", hora);
                                    //}

                                }
                                else
                                {
                                    sitAgendaHtml = "<span class=\"badge badge-success\">Horário NÃO Disponível</span>";
                                    btnAgendar = string.Empty;

                                }


                            }
                            else
                            {

                                //Pesquisa na tbl_Agenda_temp se existe alguma agenda temporaria cadastrada
                                if (AgendaTemp.PesquisaDataHora(DateTime.Parse(txtData.Value), hora, int.Parse(ddlProfissional.SelectedValue)))
                                {
                                    sitAgendaHtml = "<span class=\"badge badge-warning\">Aguardando confirmação</span>";
                                    //btnAgendar = string.Format("<button type=\"button\" onclick=\"capturaHorarioAgendaExcluir({0})\" class=\"btn btn-danger btn-xs\" data-toggle=\"modal\" data-target=\"#myModalExcluir\"><i class=\"fa fa-eraser\"></i>&nbsp;&nbsp;<span class=\"bold\">Excluir</span></button>", hora);

                                    btnAgendar = string.Empty;
                                }

                                //
                            }


                        }
                        else
                        {
                            sitAgendaHtml = "<span class=\"badge badge-danger\">NÃO Disponível</span>";
                            btnAgendar = string.Empty;
                        }

                        //tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", sitAgendaHtml));
                        //tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", btnAgendar));

                    }
                    else
                    { //data maior que a atual

                        //Pesquisa na tbl_Agenda
                        //var agd = Agenda.Pesquisa(DateTime.Parse(txtData.Value), hora, int.Parse(ddlProfissional.SelectedValue));
                        //if (Agenda.PesquisaDataHora(DateTime.Parse(txtData.Value), hora, int.Parse(ddlProfissional.SelectedValue)) == false)
                        if (agd != null)
                        {
                            if (agd.IdStatus != 7)
                            {
                                //Pesquisa na tbl_Agenda_temp
                                if (AgendaTemp.PesquisaDataHora(DateTime.Parse(txtData.Value), hora, int.Parse(ddlProfissional.SelectedValue)))
                                {
                                    sitAgendaHtml = "<span class=\"badge badge-warning\">Aguardando confirmação</span>";
                                    btnAgendar = string.Format("<button type=\"button\" onclick=\"capturaHorarioAgendaExcluir({0})\" class=\"btn btn-danger btn-xs\" data-toggle=\"modal\" data-target=\"#myModalExcluir\"><i class=\"fa fa-eraser\"></i>&nbsp;&nbsp;<span class=\"bold\">Excluir</span></button>", hora);

                                }
                                else
                                {
                                    sitAgendaHtml = "<span class=\"badge badge-primary\">Disponível</span>";
                                    //agendaDisponivel = false;
                                }
                            }
                            else
                            {
                                //agendaDisponivel = false;
                            }

                        }
                        else
                        {
                            //agendaDisponivel = true;
                        }

                        //tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", sitAgendaHtml));
                        //tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", btnAgendar));


                    }

                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", sitAgendaHtml));
                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", btnAgendar));

                    //if (DateTime.Parse(txtData.Value).Date == DateTime.Now.Date)
                    //{
                    //    if (horaAtual <= hora)
                    //    {
                    //        btnAgendar = agendaDisponivel
                    //                ? string.Format("<button type=\"button\" onclick=\"capturaHorarioAgenda({0})\" class=\"btn btn-success btn-xs\" ><i class=\"fa fa-calendar-o\"></i>&nbsp;&nbsp;<span class=\"bold\">Agendar</span></button>", hora)
                    //                : string.Format("<button type=\"button\" onclick=\"capturaHorarioAgendaExcluir({0})\" class=\"btn btn-danger btn-xs\" data-toggle=\"modal\" data-target=\"#myModalExcluir\"><i class=\"fa fa-eraser\"></i>&nbsp;&nbsp;<span class=\"bold\">Excluir</span></button>", hora);

                    //    }


                    //}
                    //else
                    //{
                    //   btnAgendar = agendaDisponivel
                    //                ? string.Format("<button type=\"button\" onclick=\"capturaHorarioAgenda({0})\" class=\"btn btn-success btn-xs\" ><i class=\"fa fa-calendar-o\"></i>&nbsp;&nbsp;<span class=\"bold\">Agendar</span></button>", hora)
                    //                : string.Format("<button type=\"button\" onclick=\"capturaHorarioAgendaExcluir({0})\" class=\"btn btn-danger btn-xs\" data-toggle=\"modal\" data-target=\"#myModalExcluir\"><i class=\"fa fa-eraser\"></i>&nbsp;&nbsp;<span class=\"bold\">Excluir</span></button>", hora);

                    //}
                    //tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", btnAgendar));


                    tabelaHtml.Append("</tr>");
                }
                tabelaHtml.Append("</tbody></table></div>");
                ltlTabelaHorarios.Text = tabelaHtml.ToString();
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaTabelaAgenda-ERRO:" + ex.Message + "</p></div>";
            }
        }


        private static string CarregarAgendaStatic(string data, string idProfissional)
        {
            try
            {
                var tabelaHtml = new StringBuilder();
                tabelaHtml.Append("<div id=\"ltlTabelaHorarios\"><table class=\"table table-striped table-bordered \"><thead><tr>");
                tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Horário</th>");
                tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Situação</th>");
                tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Ações</th>");
                tabelaHtml.Append("</tr></thead>");
                tabelaHtml.Append("<tbody>");

                for (int hora = 8; hora < 21; hora++)
                {
                    tabelaHtml.Append("<tr>");
                    var horario = string.Format("De {0} às {1} horas.", hora, hora + 1);
                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left; width: 20%;\" >{0}</td>", horario));

                    var sitAgendaHtml = "<span class=\"badge badge-danger\">NÃO Disponível</span>";
                    var agendaDisponivel = true;
                    //Pesquisa na tbl_Agenda
                    if (Agenda.PesquisaDataHora(DateTime.Parse(data), hora, int.Parse(idProfissional)) == false)
                    {
                        //Pesquisa na tbl_Agenda_temp
                        if (AgendaTemp.PesquisaDataHora(DateTime.Parse(data), hora, int.Parse(idProfissional)))
                        {
                            sitAgendaHtml = "<span class=\"badge badge-warning\">Aguardando confirmação</span>";
                        }
                        else
                        {
                            sitAgendaHtml = "<span class=\"badge badge-primary\">Disponível</span>";
                            agendaDisponivel = false;
                        }
                    }
                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", sitAgendaHtml));

                    var btnAgendar = agendaDisponivel
                                    ? string.Format("<button type=\"button\" onclick=\"capturaHorarioAgendaExcluir({0})\" class=\"btn btn-danger btn-xs\" data-toggle=\"modal\" data-target=\"#myModalExcluir\"><i class=\"fa fa-eraser\"></i>&nbsp;&nbsp;<span class=\"bold\">Excluir</span></button>", hora)
                                    : string.Format("<button type=\"button\" onclick=\"capturaHorarioAgenda({0})\" class=\"btn btn-success btn-xs\" ><i class=\"fa fa-calendar-o\"></i>&nbsp;&nbsp;<span class=\"bold\">Agendar</span></button>", hora);
                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", btnAgendar));

                    tabelaHtml.Append("</tr>");
                }
                tabelaHtml.Append("</tbody></table></div>");
                return tabelaHtml.ToString();
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaTabelaAgenda-ERRO:" + ex.Message + "</p></div>";
            }
        }

        [WebMethod]
        public static void CapturaHorarioAgenda(string data, string hora, string idProfissional)
        {
            try
            {
                //HttpContext.Current.Session["IdSrvExcluir"] = id;
                var url = string.Format("agendar.aspx?dt={0}&hr={1}&idp={2}", data, hora, idProfissional);
                HttpContext.Current.Response.Redirect(url);


            }
            catch (Exception ex)
            {

            }
        }


        [WebMethod]
        public static string AcaoBtnAgendar(
                                                string data
                                                , string hora
                                                , string idProfissional
                                                , string nome
                                                , string tel
                                            )
        {
            try
            {
                if (AgendaTemp.Inserir(new AgendaTemp(data, hora, idProfissional, nome, tel)))
                {
                    return CarregarAgendaStatic(data, idProfissional);
                }

                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                       "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Não foi possivel salvar a sua solicitação do horário na agenda!</p></div>";
                ;
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> AcaoBtnAgendar-ERRO:" + ex.Message + "</p></div>";
                ;
            }

        }



        [WebMethod]
        public static string AcaoBtnExcluirAgenda(
                                                        string data
                                                        , string hora
                                                        , string idProfissional
                                            )
        {
            try
            {

                var agdTmp = AgendaTemp.Pesquisar(DateTime.Parse(data), int.Parse(hora), int.Parse(idProfissional));
                if (agdTmp != null)
                {
                    //apagar os serviços temporarios selecionados
                    if (AgendaServicosTemp.AgagarPeloIdAgenda(agdTmp.Id))
                    {
                        //Apagar a agenda temporaria
                        if (AgendaTemp.Excluir(DateTime.Parse(data), int.Parse(hora), int.Parse(idProfissional)))
                        {
                            return CarregarAgendaStatic(data, idProfissional);
                        }
                    }
                    else
                    {
                        return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                               "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Não foi possivel excluir a sua solicitação do horário na agenda!</p></div>";
                    }

                }
                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                       "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Não foi possivel excluir a sua solicitação do horário na agenda!</p></div>";


            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> AcaoBtnExcluirAgenda-ERRO:" + ex.Message + "</p></div>";
            }

        }



    }
}