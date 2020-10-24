using sbessencial_cl;
using System;
using System.Text;
using System.Web.Services;
using System.Web.UI;

namespace ariziolouzada.sbessencial.agenda.minhaagenda
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

                CarregaDdlCliente(true);

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

            //var data = Convert.ToDateTime(txtData.Value);
            //if (data < DateTime.Now.Date)
            //{
            //    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
            //                  "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Selecione uma data maior ou igual a hoje para ser carregada a agenda!!</p></div>";
            //    txtData.Focus();
            //    return false;
            //}

            if (ddlProfissional.SelectedIndex == 0)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                            "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Selecione o profissiona!!</p></div>";
                ddlProfissional.Focus();
                return false;
            }

            return true;
        }


        private void CarregaDdlCliente(bool comSelecione)
        {
            var lista = Cliente.Lista(comSelecione, "");
            ddlCliente.Items.Clear();
            ddlCliente.DataSource = lista;
            ddlCliente.DataValueField = "Id";
            ddlCliente.DataTextField = "Nome";
            ddlCliente.DataBind();

            if (comSelecione)
                ddlCliente.SelectedIndex = 0;
        }



        private void CarregarAgenda()
        {
            try
            {
                var tabelaHtml = new StringBuilder();
                tabelaHtml.Append("<div id=\"ltlTabelaHorarios\"><table class=\"table table-striped table-bordered \"><thead><tr>");
                tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle; width: 20%;\">Horário</th>");
                tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Cliente / Tel. Contato</th>");
                tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Serviço</th>");
                tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Situação</th>");
                tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Ações</th>");
                tabelaHtml.Append("</tr></thead>");
                tabelaHtml.Append("<tbody>");

                for (int hora = 8; hora < 21; hora++)
                {
                    tabelaHtml.Append("<tr>");
                    var horario = string.Format("De {0} às {1} horas.", hora, hora + 1);

                    var agdTmp = AgendaTemp.Pesquisar(DateTime.Parse(txtData.Value), hora, int.Parse(ddlProfissional.SelectedValue));
                    if (agdTmp != null)
                    { //Existe agendamente temporário

                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left; \" ><b>{0}</b></td>", horario));

                        //Cliente
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0} / Tel.: {1}</td>"
                                                        , agdTmp.Nome
                                                        , agdTmp.Telefone.Equals("") ? "Não cadastrado" : agdTmp.Telefone));
                        //Serviço
                        //listar os serviços temporários
                        var listaSvc = AgendaServicosTemp.Lista(agdTmp.Id);
                        var svcsHtml = new StringBuilder();
                        for (int i = 0; i < listaSvc.Count; i++)
                        {
                            svcsHtml.Append(listaSvc[i].DescricaoServico);
                            if (i < listaSvc.Count + 1)
                                svcsHtml.Append("; ");
                        }

                        var svctmp = string.Format("<button type=\"button\" class=\"btn btn-outline btn-primary btn-xs\" data-container=\"body\" data-toggle=\"popover\" "
                                                 + " data-placement=\"top\" data-content=\"{0}\" data-original-title=\"\" title=\"\"><i class=\"fa fa-search\"> </i> Visualizar</button>", svcsHtml);

                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left;\" >{0}</td>", svctmp));

                        //Situação
                        var sitAgendaHtml = "<span class=\"badge badge-warning\">Aguardando confirmação</span>";
                        if (agdTmp.IdStatus == 2)
                            sitAgendaHtml = "<span class=\"badge badge-primary\">Confimada Agenda</span>";
                        else if (agdTmp.IdStatus == 3)
                            sitAgendaHtml = "<span class=\"badge badge-danger\">NÃO Confirmado</span>";

                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", sitAgendaHtml));

                        //Ações
                        // o btn da ação nao funciona 
                        var btnAcoes = string.Empty;
                        if (agdTmp.IdStatus == 1)
                        {
                            btnAcoes = string.Format("<button onclick=\"capturaIdAgendaTemp({0}, {1}, {2})\" class=\"btn btn-primary dim\" type=\"button\" data-toggle=\"modal\" data-target=\"#myModalConfirmar\" data-original-title=\"\" title=\"\">" +
                                "<i class=\"fa fa-check-square-o\"></i></button>&nbsp;&nbsp;"
                                 + "<button class=\"btn btn-danger dim\" type=\"button\"><i class=\"fa fa-times-circle-o\"></i></button>", agdTmp.Id, hora, agdTmp.IdCliente);

                            ////
                        }
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", btnAcoes));

                    }
                    else
                    {
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left; \" >{0}</td>", horario));

                        var btnAcoes = string.Empty;
                        if (Agenda.PesquisaDataHora(DateTime.Parse(txtData.Value), hora, int.Parse(ddlProfissional.SelectedValue)))
                        {
                            tabelaHtml.Append("<td style=\"vertical-align: middle; text-align: center\" ></td>");
                            tabelaHtml.Append("<td style=\"vertical-align: middle; text-align: center\" ></td>");
                            tabelaHtml.Append("<td style=\"vertical-align: middle; text-align: center\" ><span class=\"badge badge-danger\">NÃO DISPONÍVEL</span> </td>");

                            //implementar acoa do btn
                            btnAcoes = string.Format("<button type=\"button\" class=\"btn btn-outline btn-primary\" onclick=\"abrirAgenda({0})\" ><i class=\"fa fa-wrench\"></i> Abrir</button>", hora);
                            //tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", btnAcoes));

                            //tabelaHtml.Append("<td style=\"vertical-align: middle; text-align: center\" ></td>");
                        }
                        else
                        {
                            tabelaHtml.Append("<td style=\"vertical-align: middle; text-align: center\" ></td>");
                            tabelaHtml.Append("<td style=\"vertical-align: middle; text-align: center\" ></td>");
                            tabelaHtml.Append("<td style=\"vertical-align: middle; text-align: center\" ></td>");

                            btnAcoes = string.Format("<button type=\"button\" class=\"btn btn-outline btn-warning\" onclick=\"fecharAgenda({0})\" ><i class=\"fa fa-wrench\"></i> Fechar</button>", hora);
                        }
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", btnAcoes));
                    }

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


        [WebMethod]
        public static string FecharAgenda(string data, string hora, string idProfissional)
        {
            try
            {
                var datahora = Agenda.DataHoraAgenda(data, hora, "00");
                //inserir na agenda principal
                var agd = new Agenda()
                {
                    IdCliente = 8, //ID do Salão Essencial
                    DataHora = datahora,
                    IdProfissional = int.Parse(idProfissional),
                    Observacao = "Agenda Não disponível",
                    IdRegistroAgenda = long.Parse(FluxoCaixa.GerarID()),
                    IdStatus = 7 //nÃO DISPONÍVEL
                };

                if (Agenda.Inserir(agd))
                {
                    return "";
                }

                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                       "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Não foi possivel Fechar o horário na agenda!</p></div>";

            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> FecharAgenda-ERRO:" + ex.Message + "</p></div>";
            }

        }


        [WebMethod]
        public static string AbrirAgenda(string data, string hora, string idProfissional)
        {
            try
            {                
                if (Agenda.Excluir(DateTime.Parse(data), int.Parse(hora), int.Parse(idProfissional) ))
                {
                    return "";
                }

                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                       "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Não foi possivel Abrir o horário na agenda!</p></div>";

            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> FecharAgenda-ERRO:" + ex.Message + "</p></div>";
            }

        }


        [WebMethod]
        public static string ConfirmarAgenda(string idAgdTmp, string idCliente)
        {
            try
            {
                if (AgendaTemp.EditarStatus(2, int.Parse(idAgdTmp)))
                {
                    var at = AgendaTemp.Pesquisar(int.Parse(idAgdTmp));

                    //inserir na agenda principal
                    var agd = new Agenda()
                    {
                        IdCliente = int.Parse(idCliente),
                        DataHora = at.DataHora,
                        IdProfissional = at.IdProfissional,
                        Observacao = "Agendado pela Internet",
                        IdRegistroAgenda = long.Parse(FluxoCaixa.GerarID()),
                        IdStatus = 2
                    };

                    if (Agenda.Inserir(agd))
                    {
                        //inserir os serviços
                        var listaSvc = AgendaServicosTemp.Lista(int.Parse(idAgdTmp));
                        foreach (var svc in listaSvc)
                        {
                            var agdSvc = new AgendaServicos(agd.IdRegistroAgenda, svc.IdServicoTemp, svc.Valor);
                            AgendaServicos.Inserir(agdSvc);
                        }
                    }

                    return "";
                }

                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                       "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Não foi possivel Confirmar a solicitação do horário na agenda!</p></div>";


            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> AcaoBtnExcluirAgenda-ERRO:" + ex.Message + "</p></div>";
            }

        }


    }
}