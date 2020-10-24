using CriptografiaSgpm;
using espacobeleza_cl;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;

namespace ariziolouzada.espacodebeleza.pages.agenda
{
    public partial class _default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.HasKeys())
                {
                    if (Request.QueryString["data"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["data"]))
                        txtData.Value = Request.QueryString["data"];
                }
                else
                {
                    //carrega sempre o dia atual em tela
                    txtData.Value = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    ltlCabecalho.Text = string.Format("<h2><b>Agenda - {0}.</b></h2>", DateTime.Now.ToLongDateString());
                }

                //CarregaDdlServico(true);
                CarregaDdlProfissiao(true);
                //CarregaTabela();
            }
        }


        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            ltlMsn.Text = string.Empty;
            if (txtData.Value != string.Empty)
            {
                CarregaTabela();
            }
            else
            {
                ltlMsn.Text =
                 "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                       "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ERRO: Digite/Selecione a data para carregar a agenda!!</p></div>";
                txtData.Focus();
            }
            //Response.Redirect("default.aspx?data=" + txtData.Value);
        }

        private void CarregaDdlProfissiao(bool comSelecione)
        {
            var lista = ProfissaoEspBel.Lista(comSelecione);
            ddlProfissao.Items.Clear();
            ddlProfissao.DataSource = lista;
            ddlProfissao.DataValueField = "Id";
            ddlProfissao.DataTextField = "Descricao";
            ddlProfissao.DataBind();

            if (comSelecione)
                ddlProfissao.SelectedIndex = 0;
        }

        //private void CarregaDdlServico(bool comSelecione)
        //{
        //    //RECUPERAR O COOKIE
        //    var idEmpresaContratante = "0";
        //    HttpCookie cookie = Request.Cookies["idEmpresaContratante"];
        //    if (cookie != null && cookie.Value != null)
        //        idEmpresaContratante = cookie.Value;

        //    idEmpresaContratante = Criptografia.Decrypt(idEmpresaContratante);

        //    var lista = ServicoEspBel.ListaServicoProfissional(comSelecione, int.Parse(hdfIdProfissional.Value), 1, int.Parse(idEmpresaContratante));
        //    ddlServico.Items.Clear();
        //    ddlServico.DataSource = lista;
        //    ddlServico.DataValueField = "Id";
        //    ddlServico.DataTextField = "Descricao";
        //    ddlServico.DataBind();

        //    if (comSelecione)
        //        ddlServico.SelectedIndex = 0;
        //}

        private void CarregaTabela()
        {
            try
            {
                var tabelaHtml = new StringBuilder();
                var listaHorarios = AgendaHorario.Lista(false);//GeraListaHorarios();

                tabelaHtml.Append("<table id=\"tabelaAgendaHorarios\" class=\"table table-striped table-bordered \"><thead><tr>");
                tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\" rowspan=\"2\"><h3>Profissional</h3></th>");
                tabelaHtml.Append(string.Format("<th style=\"text-align: center; vertical-align: middle;\" colspan=\"{0}\"><h3>Horário</h3></th>", listaHorarios.Count));
                tabelaHtml.Append("</tr><tr>");
                foreach (var linha in listaHorarios)
                {
                    tabelaHtml.Append(string.Format("<th style=\"text-align: center; vertical-align: middle;\">{0}</th>", linha.Descricao));
                }
                tabelaHtml.Append("</tr></thead>");
                tabelaHtml.Append("<tbody>");

                //RECUPERAR O COOKIE
                var idEmpresaContratante = "0";
                HttpCookie cookie = Request.Cookies["idEmpresaContratante"];
                if (cookie != null && cookie.Value != null)
                    idEmpresaContratante = cookie.Value;
                idEmpresaContratante = Criptografia.Decrypt(idEmpresaContratante);

                var listaProfissional = ProfissionalEspBel.Lista(txtPesqNomeProf.Value, int.Parse(idEmpresaContratante));

                foreach (var prof in listaProfissional)
                {
                    tabelaHtml.Append("<tr>");
                    //Nome                    
                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", prof.Nome));

                    //linhas dos Horarios
                    foreach (var linha in listaHorarios)
                    {
                        ////ocupado <div class="text-danger"><a href="#"><i class="fa fa-female"></i> fa-female</a></div>
                        ////livre <div class="text-navy"><a href="#"><i class="fa fa-square-o"></i> fa-square-o</a></div>
                        ////var agenda = string.Format("<div class=\"text-primary\"><a href=\"#\"><i class=\"fa fa-square-o fa-2x\"></i></a></div>");

                        var dataHoraAtual = DateTime.Now;
                        var data = DateTime.Parse(txtData.Value);
                        var dataHoraStr = string.Format("{0} {1}:00", data.ToShortDateString(), linha.Descricao);
                        var dataHoraAgenda = DateTime.Parse(dataHoraStr);

                        //if (dataHoraAgenda > dataHoraAtual)
                        //{//Só preenche a hora que ainda não aconteceu
                        var agendaLivre = string.Empty;
                        var agendaOcupada = string.Empty;
                        var agenda = AgendaHorarioProfissional.Pesquisar(txtData.Value, linha.Id, prof.Id);
                        if (agenda != null)
                        {
                            var toltip = string.Format(" title=\"Cliente: {0}\" ", agenda.Cliente);
                            agendaOcupada = string.Format("<button class=\"btn btn-primary dim\" type=\"button\"  data-toggle=\"modal\" data-target=\"#myModal7\" onclick=\"capturaDadosAgenda({0});\" {1}><i class=\"fa fa-female\"></i></button>", agenda.IdAgenda, toltip);

                            tabelaHtml.Append(string.Format("<td style=\"text-align: center; vertical-align: middle;\">{0}</td>", agendaOcupada));
                        }


                        //    if (agenda == null)//agenda livre
                        //    {
                        //        agendaLivre = string.Format("<button class=\"btn btn-primary dim\" type=\"button\"  data-toggle=\"modal\" data-target=\"#myModal6\" onclick=\"capturaHoraInicialAgenda({0},{1});\"><i class=\"fa fa-check\"></i></button>", linha.Id, prof.Id);
                        //    }
                        //    else
                        //    {
                        //        //var toltip = string.Format("data-toggle=\"tooltip\" data-placement=\"top\" title=\"\" data-original-title=\"Cliente: {0}\"", agenda.Cliente);
                        //        var toltip = string.Format(" title=\"Cliente: {0}\" ", agenda.Cliente);
                        //        agendaOcupada = string.Format("<button class=\"btn btn-danger dim\" type=\"button\"  data-toggle=\"modal\" data-target=\"#myModal7\" onclick=\"capturaDadosAgenda({0});\" {1}><i class=\"fa fa-female\"></i></button>", agenda.IdAgenda, toltip);
                        //    }
                        //    tabelaHtml.Append(string.Format("<td style=\"text-align: center; vertical-align: middle;\">{0}</td>", agenda == null ? agendaLivre : agendaOcupada));

                        //}
                        else
                        {
                            tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" ></td>"));
                        }

                    }
                    tabelaHtml.Append("</tr>");
                }
                tabelaHtml.Append("</tbody></table>");

                ltlTabela.Text = tabelaHtml.ToString();
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaTabela-ERRO:" + ex.Message + "</p></div>";
            }
        }

        private List<string> GeraListaHorarios()
        {
            var lista = new List<string>();
            for (int i = 8; i < 19; i++)
            {
                lista.Add(string.Format("{0}:00", string.Format(@"{0:00}", i)));
                if (i < 18)
                    lista.Add(string.Format("{0}:30", string.Format(@"{0:00}", i)));
            }
            return lista;
        }


        [WebMethod]
        public static string SalvarDadosAgenda(string dataAgenda, string idHoraInicial, string idHoraFinal, string idProf, string idServico, string idCliente)
        {
            try
            {
                var agenda = new Agenda(dataAgenda, idHoraInicial, idHoraFinal, idProf, idServico, idCliente);
                var idAgendaCadastrado = Agenda.InserirRetornandoId(agenda);

                if (idAgendaCadastrado > 0)
                {
                    //Inserir o registro na  tbl_agenda_horario_profisonal
                    var listaHorario = AgendaHorario.Lista(false);
                    foreach (var item in listaHorario)
                    {
                        if (item.Id >= agenda.IdHorarioInicial && item.Id <= agenda.IdHorarioFinal)
                        {
                            if (AgendaHorarioProfissional.Inserir(new AgendaHorarioProfissional(dataAgenda, item.Id.ToString(), idProf, idAgendaCadastrado.ToString(), idServico)))
                            {

                            }
                        }
                    }

                    return string.Empty;
                }


                //if (Agenda.Inserir(agenda))                
                //    return string.Empty;

                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                       "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ERRO: Não foi possível salvar os dados da agenda!!</p></div>";
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> SalvarDadosAgenda-ERRO:" + ex.Message + "</p></div>";
            }
        }


        [WebMethod]
        public static string ApagarDadosAgenda(string idAgenda)
        {
            try
            {
                var listaHorarioAgenda = AgendaHorarioProfissional.Lista(int.Parse(idAgenda));
                //Apagar registro tabela tbl_agenda_horario_profisonal
                if (AgendaHorarioProfissional.Apagar(int.Parse(idAgenda)))
                {
                    var agenda = Agenda.Pesquisar(int.Parse(idAgenda));
                    //Apagar agenda
                    if (Agenda.Apagar(int.Parse(idAgenda)))
                    {
                        return string.Empty;
                    }
                    else
                    {
                        //em caso de erro insiro novente os dados da tbl_agenda_horario_profisonal
                        foreach (var item in listaHorarioAgenda)
                        {
                            AgendaHorarioProfissional.Inserir(item);
                        }
                    }
                }

                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                       "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ERRO: Não foi possível Apagar os dados da agenda!!</p></div>";
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ApagarDadosAgenda-ERRO:" + ex.Message + "</p></div>";
            }
        }

        [WebMethod]
        public static string FinalizarAgenda(string idAgenda)
        {
            try
            {
                var idCriptografado = Criptografia.Encrypt(idAgenda);
                idCriptografado = idCriptografado.Replace('+', '_');
                return idCriptografado;

                //return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                //       "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ERRO: Não foi possível Apagar os dados da agenda!!</p></div>";
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ApagarDadosAgenda-ERRO:" + ex.Message + "</p></div>";
            }
        }


        [WebMethod]
        public static string CapturaHoraInicialAgenda(string idHoraAgenda, string idProf)
        {
            try
            {
                var profiss = ProfissionalEspBel.Pesquisar(int.Parse(idProf));
                var agenda = AgendaHorario.Pesquisa(int.Parse(idHoraAgenda));

                var htmlModalAgenda = new StringBuilder();

                htmlModalAgenda.Append("<div class=\"form-group\"><label class=\"control-label\">Profissional</label>");
                htmlModalAgenda.Append(string.Format("<input type=\"text\" class=\"form-control\" id=\"txtProfAgenda\" disabled=\"\" value=\"{0}\" /></div>", profiss.Nome));

                htmlModalAgenda.Append("<div class=\"form-group\"><label class=\"control-label\">Horário Inicial</label>");
                htmlModalAgenda.Append(string.Format("<input type=\"text\" class=\"form-control\" id=\"txtHoraInicialAgenda\" disabled=\"\" value=\"{0}\" /></div>", agenda.Descricao));

                htmlModalAgenda.Append("<div class=\"form-group\"><label class=\"control-label\">Horário Final</label>");
                htmlModalAgenda.Append("<select class=\"form-control\" id=\"ddlHoraFinal\" >");
                htmlModalAgenda.Append(string.Format("<option value=\"0\" >Selecione...</option>"));
                var listaHorarios = AgendaHorario.Lista(false);
                foreach (var linha in listaHorarios)
                {
                    if (linha.Id > int.Parse(idHoraAgenda))
                    {
                        htmlModalAgenda.Append(string.Format("<option value=\"{0}\" >{1}</option>", linha.Id, linha.Descricao));
                    }
                }
                htmlModalAgenda.Append("</select></div>");

                htmlModalAgenda.Append("<div class=\"form-group\"><label class=\"control-label\">Serviço</label>");
                htmlModalAgenda.Append("<select class=\"form-control\" id=\"ddlServico\">");
                htmlModalAgenda.Append(string.Format("<option value=\"0\" >Selecione...</option>"));
                var listaSvcProf = ServicoEspBel.ListaServicoProfissional(false, profiss.Id, 1, profiss.IdEmpresaContratante);
                foreach (var item in listaSvcProf)
                {
                    htmlModalAgenda.Append(string.Format("<option value=\"{0}\" >{1}</option>", item.Id, item.Descricao));
                }
                htmlModalAgenda.Append("</select></div >");

                htmlModalAgenda.Append("<div class=\"form-group\"><label class=\"control-label\">Cliente</label>");
                htmlModalAgenda.Append("<select class=\"form-control\" id=\"ddlCliente\">");
                htmlModalAgenda.Append(string.Format("<option value=\"0\" >Selecione...</option>"));
                var listaCliente = ClienteEspBel.Lista(false, "", profiss.IdEmpresaContratante);
                foreach (var item in listaCliente)
                {
                    htmlModalAgenda.Append(string.Format("<option value=\"{0}\" >{1}</option>", item.Id, item.Nome));
                }
                htmlModalAgenda.Append("</select></div >");


                return htmlModalAgenda.ToString();
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> CapturaHoraInicialAgenda-ERRO:" + ex.Message + "</p></div>";
            }
        }


        [WebMethod]
        public static string CapturaDadosAgenda(string idAgenda)
        {
            try
            {
                var agenda = Agenda.Pesquisar(int.Parse(idAgenda));

                var htmlModalAgenda = new StringBuilder();

                htmlModalAgenda.Append(string.Format("<div class=\"form-group\"><h3>{0}</h3></div>", agenda.DataAgenda.ToShortDateString()));

                htmlModalAgenda.Append("<div class=\"form-group\"><label class=\"control-label\">Cliente</label>");
                htmlModalAgenda.Append(string.Format("<input type=\"text\" class=\"form-control\" id=\"txtCleinte\" disabled=\"\" value=\"{0}\" /></div>", agenda.Cliente));

                htmlModalAgenda.Append("<div class=\"form-group\"><label class=\"control-label\">Profissional</label>");
                htmlModalAgenda.Append(string.Format("<input type=\"text\" class=\"form-control\" id=\"txtProfAgenda\" disabled=\"\" value=\"{0}\" /></div>", agenda.Profissional));

                htmlModalAgenda.Append("<div class=\"form-group\"><label class=\"control-label\">Serviço</label>");
                htmlModalAgenda.Append(string.Format("<input type=\"text\" class=\"form-control\" id=\"txtServicoAgenda\" disabled=\"\" value=\"{0}\" /></div>", agenda.Servico));

                htmlModalAgenda.Append("<div class=\"form-group\"><div class=\"col-lg-6 col-md-6 col-sm-6\"><label class=\"control-label\">Hora Inicio</label>");
                htmlModalAgenda.Append(string.Format("<input type=\"text\" class=\"form-control\" id=\"txtHoraInicialAgenda\" disabled=\"\" value=\"{0}\" /></div>", agenda.HorarioInicial));
                htmlModalAgenda.Append("<div class=\"col-lg-6 col-md-6 col-sm-6\"><label class=\"control-label\">Hora Final</label>");
                htmlModalAgenda.Append(string.Format("<input type=\"text\" class=\"form-control\" id=\"txtHoraFinalAgenda\" disabled=\"\" value=\"{0}\" /></div></div>", agenda.HorarioFinal));

                //htmlModalAgenda.Append("<select class=\"form-control\" id=\"ddlHoraFinal\">");
                //htmlModalAgenda.Append(string.Format("<option value=\"0\" >Selecione...</option>"));

                //var listaHorarios = AgendaHorario.Lista(false);
                //foreach (var linha in listaHorarios)
                //{
                //    if (linha.Id > int.Parse(idHoraAgenda))
                //    {
                //        htmlModalAgenda.Append(string.Format("<option value=\"{0}\" >{1}</option>", linha.Id, linha.Descricao));
                //    }
                //}
                //htmlModalAgenda.Append("</select></div>");
                //htmlModalAgenda.Append("<div class=\"form-group\"><label class=\"control-label\">Serviço</label>");
                //htmlModalAgenda.Append("<select class=\"form-control\" id=\"ddlServico\">");
                //htmlModalAgenda.Append(string.Format("<option value=\"0\" >Selecione...</option>"));

                //var listaSvcProf = ServicoEspBel.ListaServicoProfissional(false, profiss.Id, 1, profiss.IdEmpresaContratante);
                //foreach (var item in listaSvcProf)
                //{
                //    htmlModalAgenda.Append(string.Format("<option value=\"{0}\" >{1}</option>", item.Id, item.Descricao));
                //}
                //htmlModalAgenda.Append("</select></div >");


                return htmlModalAgenda.ToString();
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> CapturaHoraInicialAgenda-ERRO:" + ex.Message + "</p></div>";
            }
        }


        [WebMethod]
        public static string CarregaDdlServico(string idProfissional)
        {
            try
            {
                var profiss = ProfissionalEspBel.Pesquisar(int.Parse(idProfissional));
                var htmlModalAgenda = new StringBuilder();
                htmlModalAgenda.Append(" <select class=\"form-control\" id=\"sltServico\">");
                htmlModalAgenda.Append(string.Format("<option value=\"0\" >Selecione...</option>"));
                var listaSvcProf = ServicoEspBel.ListaServicoProfissional(false, profiss.Id, 1, profiss.IdEmpresaContratante);
                foreach (var item in listaSvcProf)
                    htmlModalAgenda.Append(string.Format("<option value=\"{0}\" >{1}</option>", item.Id, item.Descricao));

                htmlModalAgenda.Append("</select>");
                return htmlModalAgenda.ToString();
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> CarregaDdlServico-ERRO:" + ex.Message + "</p></div>";
            }
        }


        [WebMethod]
        public static string CarregaDdlHorarioFinal(string idHoraAgenda)
        {
            try
            {
                var htmlModalAgenda = new StringBuilder();
                //htmlModalAgenda.Append("<div class=\"form-group\"><label class=\"control-label\">Horário Final</label>");
                htmlModalAgenda.Append(" <select class=\"form-control\" id=\"sltHoraFinal\">");
                htmlModalAgenda.Append(string.Format("<option value=\"0\" >Selecione...</option>"));
                var listaHorarios = AgendaHorario.Lista(false);
                foreach (var linha in listaHorarios)
                {
                    if (linha.Id >= int.Parse(idHoraAgenda))
                    {
                        htmlModalAgenda.Append(string.Format("<option value=\"{0}\" >{1}</option>", linha.Id, linha.Descricao));
                    }
                }
                htmlModalAgenda.Append("</select>");

                return htmlModalAgenda.ToString();
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> CarregaDdlHorarioFinal-ERRO:" + ex.Message + "</p></div>";
            }
        }

        [WebMethod]
        public static string CriarAgenda(string data)
        {
            try
            {
                //RECUPERAR O COOKIE
                var idEmpresaContratante = "0";
                HttpCookie cookie = HttpContext.Current.Request.Cookies["idEmpresaContratante"];
                if (cookie != null && cookie.Value != null)
                    idEmpresaContratante = cookie.Value;
                idEmpresaContratante = Criptografia.Decrypt(idEmpresaContratante);

                var htmlModalAgenda = new StringBuilder();

                htmlModalAgenda.Append(string.Format("<div class=\"form-group centro\"><h2><b>{0}</b></h2></div>", DateTime.Parse(data).ToLongDateString()));

                //htmlModalAgenda.Append("<div class=\"form-group\"><label class=\"control-label\">Data</label>");
                //htmlModalAgenda.Append(string.Format("<input type=\"text\" class=\"form-control\" id=\"txtdataAgenda\" value=\"{0}\"  disabled=\"\"/></div>", DateTime.Parse(data).ToShortDateString()));

                htmlModalAgenda.Append("<div class=\"form-group\"><label class=\"control-label\">Cliente</label>");
                htmlModalAgenda.Append("<select class=\"form-control\" id=\"ddlCliente\">");
                htmlModalAgenda.Append(string.Format("<option value=\"0\" >Selecione...</option>"));
                var listaClientes = ClienteEspBel.Lista(false, "", int.Parse(idEmpresaContratante));
                foreach (var linha in listaClientes)
                    htmlModalAgenda.Append(string.Format("<option value=\"{0}\" >{1}</option>", linha.Id, linha.Nome));
                htmlModalAgenda.Append("</select></div>");

                htmlModalAgenda.Append("<div class=\"form-group\"><label class=\"control-label\">Profissional</label>");
                htmlModalAgenda.Append("<select class=\"form-control\" id=\"ddlProfissional\" onchange=\"carregaDdlServico();\">");
                htmlModalAgenda.Append(string.Format("<option value=\"0\" >Selecione...</option>"));
                var listaProf = ProfissionalEspBel.Lista(false, int.Parse(idEmpresaContratante));
                foreach (var linha in listaProf)
                    htmlModalAgenda.Append(string.Format("<option value=\"{0}\" >{1}</option>", linha.Id, linha.Nome));
                htmlModalAgenda.Append("</select></div>");

                htmlModalAgenda.Append("<div class=\"form-group\"><label class=\"control-label\">Serviço</label>");
                htmlModalAgenda.Append("<div id=\"ddlServico\"><select class=\"form-control\" >");
                htmlModalAgenda.Append("</select></div></div>");

                htmlModalAgenda.Append("<div class=\"form-group\"><label class=\"control-label\">Horário Inicial</label>");
                htmlModalAgenda.Append("<select class=\"form-control\" id=\"ddlHoraInicial\" onchange=\"carregaDdlHorarioFinal();\" >");
                htmlModalAgenda.Append(string.Format("<option value=\"0\" >Selecione...</option>"));
                var dataHoraAtual = DateTime.Now;
                var dataH = DateTime.Parse(data);
                var listaHorarios = AgendaHorario.Lista(false);
                foreach (var linha in listaHorarios)
                {
                    var dataHoraStr = string.Format("{0} {1}:00", dataH.ToShortDateString(), linha.Descricao);
                    var dataHoraAgenda = DateTime.Parse(dataHoraStr);
                    if (dataHoraAgenda >= dataHoraAtual)
                    {//Só preenche a hora que ainda não aconteceu
                        htmlModalAgenda.Append(string.Format("<option value=\"{0}\" >{1}</option>", linha.Id, linha.Descricao));
                    }
                }
                htmlModalAgenda.Append("</select></div>");


                htmlModalAgenda.Append("<div class=\"form-group\"><label class=\"control-label\">Horário Final</label>");
                htmlModalAgenda.Append("<div id=\"ddlHoraFinal\"> <select class=\"form-control\">");
                //htmlModalAgenda.Append(string.Format("<option value=\"0\" >Selecione...</option>"));
                //foreach (var linha in listaHorarios)
                //    htmlModalAgenda.Append(string.Format("<option value=\"{0}\" >{1}</option>", linha.Id, linha.Descricao));
                htmlModalAgenda.Append("</select></div></div>");





                //htmlModalAgenda.Append("<select class=\"form-control\" id=\"ddlHoraFinal\">");
                //htmlModalAgenda.Append(string.Format("<option value=\"0\" >Selecione...</option>"));

                //var listaHorarios = AgendaHorario.Lista(false);
                //foreach (var linha in listaHorarios)
                //{
                //    if (linha.Id > int.Parse(idHoraAgenda))
                //    {
                //        htmlModalAgenda.Append(string.Format("<option value=\"{0}\" >{1}</option>", linha.Id, linha.Descricao));
                //    }
                //}
                //htmlModalAgenda.Append("</select></div>");


                //htmlModalAgenda.Append("<div class=\"form-group\"><label class=\"control-label\">Serviço</label>");
                //htmlModalAgenda.Append("<select class=\"form-control\" id=\"ddlServico\">");
                //htmlModalAgenda.Append(string.Format("<option value=\"0\" >Selecione...</option>"));

                //var listaSvcProf = ServicoEspBel.ListaServicoProfissional(false, profiss.Id, 1, profiss.IdEmpresaContratante);
                //foreach (var item in listaSvcProf)
                //{
                //    htmlModalAgenda.Append(string.Format("<option value=\"{0}\" >{1}</option>", item.Id, item.Descricao));
                //}
                //htmlModalAgenda.Append("</select></div >");


                return htmlModalAgenda.ToString();
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> CapturaHoraInicialAgenda-ERRO:" + ex.Message + "</p></div>";
            }
        }


        [WebMethod]
        public static string CarregarAgenda(string dataAgenda, string idProfissao, string txtPesqNomeProf)
        {
            try
            {
                var tabelaHtml = new StringBuilder();
                var listaHorarios = AgendaHorario.Lista(false);//GeraListaHorarios();

                tabelaHtml.Append("<table id=\"tabelaAgendaHorarios\" class=\"table table-striped table-bordered \"><thead><tr>");
                tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\" rowspan=\"2\"><h3>Profissional</h3></th>");
                tabelaHtml.Append(string.Format("<th style=\"text-align: center; vertical-align: middle;\" colspan=\"{0}\"><h3>Horário</h3></th>", listaHorarios.Count));
                tabelaHtml.Append("</tr><tr>");
                foreach (var linha in listaHorarios)
                {
                    tabelaHtml.Append(string.Format("<th style=\"text-align: center; vertical-align: middle;\">{0}</th>", linha.Descricao));
                }
                tabelaHtml.Append("</tr></thead>");
                tabelaHtml.Append("<tbody>");

                //RECUPERAR O COOKIE
                var idEmpresaContratante = "0";
                HttpCookie cookie = HttpContext.Current.Request.Cookies["idEmpresaContratante"];
                if (cookie != null && cookie.Value != null)
                    idEmpresaContratante = cookie.Value;
                idEmpresaContratante = Criptografia.Decrypt(idEmpresaContratante);

                var listaProfissional = ProfissionalEspBel.Lista(txtPesqNomeProf, int.Parse(idEmpresaContratante), int.Parse(idProfissao));

                foreach (var prof in listaProfissional)
                {
                    tabelaHtml.Append("<tr>");
                    //Nome                    
                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", prof.Nome));

                    //linhas dos Horarios
                    foreach (var linha in listaHorarios)
                    {
                        ////ocupado <div class="text-danger"><a href="#"><i class="fa fa-female"></i> fa-female</a></div>
                        ////livre <div class="text-navy"><a href="#"><i class="fa fa-square-o"></i> fa-square-o</a></div>
                        ////var agenda = string.Format("<div class=\"text-primary\"><a href=\"#\"><i class=\"fa fa-square-o fa-2x\"></i></a></div>");

                        var dataHoraAtual = DateTime.Now;
                        var data = DateTime.Parse(dataAgenda);
                        var dataHoraStr = string.Format("{0} {1}:00", data.ToShortDateString(), linha.Descricao);
                        var dataHoraAgenda = DateTime.Parse(dataHoraStr);

                        //if (dataHoraAgenda > dataHoraAtual)
                        //{//Só preenche a hora que ainda não aconteceu
                        var agendaLivre = string.Empty;
                        var agendaOcupada = string.Empty;
                        var agenda = AgendaHorarioProfissional.Pesquisar(dataAgenda, linha.Id, prof.Id);
                        if (agenda != null)
                        {
                            agendaLivre = agenda.IdComanda == 0 ? "primary" : "danger";
                           var enableBtns = agenda.IdComanda == 0 ? "1" : "0";
                            var toltip = string.Format(" title=\"Cliente: {0}\" ", agenda.Cliente);
                            agendaOcupada = string.Format("<button class=\"btn btn-{2} dim\" type=\"button\"  data-toggle=\"modal\" data-target=\"#myModal7\" onclick=\"capturaDadosAgenda({0}, {3});\" {1}><i class=\"fa fa-female\"></i></button>", agenda.IdAgenda, toltip, agendaLivre, enableBtns);

                            tabelaHtml.Append(string.Format("<td style=\"text-align: center; vertical-align: middle;\">{0}</td>", agendaOcupada));
                        }


                        //    if (agenda == null)//agenda livre
                        //    {
                        //        agendaLivre = string.Format("<button class=\"btn btn-primary dim\" type=\"button\"  data-toggle=\"modal\" data-target=\"#myModal6\" onclick=\"capturaHoraInicialAgenda({0},{1});\"><i class=\"fa fa-check\"></i></button>", linha.Id, prof.Id);
                        //    }
                        //    else
                        //    {
                        //        //var toltip = string.Format("data-toggle=\"tooltip\" data-placement=\"top\" title=\"\" data-original-title=\"Cliente: {0}\"", agenda.Cliente);
                        //        var toltip = string.Format(" title=\"Cliente: {0}\" ", agenda.Cliente);
                        //        agendaOcupada = string.Format("<button class=\"btn btn-danger dim\" type=\"button\"  data-toggle=\"modal\" data-target=\"#myModal7\" onclick=\"capturaDadosAgenda({0});\" {1}><i class=\"fa fa-female\"></i></button>", agenda.IdAgenda, toltip);
                        //    }
                        //    tabelaHtml.Append(string.Format("<td style=\"text-align: center; vertical-align: middle;\">{0}</td>", agenda == null ? agendaLivre : agendaOcupada));

                        //}
                        else
                        {
                            tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" ></td>"));
                        }

                    }
                    tabelaHtml.Append("</tr>");
                }
                tabelaHtml.Append("</tbody></table>");

                return tabelaHtml.ToString();
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> CarregarAgenda-ERRO:" + ex.Message + "</p></div>";
            }
        }


    }
}