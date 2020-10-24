using sbessencial_cl;
using System;
using System.Web.UI;

namespace ariziolouzada.sbessencial.minhagenda
{
    public partial class agendar : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {

                    if (Request.QueryString.HasKeys())
                    {
                        string data, hr = "0", idp = "0";
                        if (Request.QueryString["dt"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["dt"]))
                        {
                            data = Request.QueryString["dt"];
                        }

                        if (Request.QueryString["hr"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["hr"]))
                        {
                            hr = Request.QueryString["hr"];
                        }

                        if (Request.QueryString["idp"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["idp"]))
                        {
                            idp = Request.QueryString["idp"];
                        }
                        var nome = Profissional.PesquisaNome(int.Parse(idp));
                        lblHorarioEscolhido.Text = string.Format("Agendar o Horário Escolhido De {0} às {1} Horas<br /> Profissional {2}",
                                                    hr, (int.Parse(hr) + 1), nome);


                        CarregaDdlServico(false, int.Parse(idp));
                    }
                    CarregaDadosUserLogado();

                }
                catch (Exception ex)
                {
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>Page_Load-ERRO:" + ex.Message + "</p></div>";
                }

            }

        }


        private void CarregaDadosUserLogado()
        {
            try
            {
                var idUserAgendaLogado = CookieSbe.Recupera("idUserAgendaLogado");
                var clt = Cliente.Pesquisar(int.Parse(idUserAgendaLogado));
                if (clt != null)
                {
                    txtNome.Value = clt.Nome;
                    txtTelefone.Value = clt.TelCelular1;
                }
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>Page_Load-ERRO:" + ex.Message + "</p></div>";


            }
        }


        private void CarregaDdlServico(bool comSelecione, int idProfissional)
        {
            var lista = Servico.ListaServicoProfissional(comSelecione, idProfissional, 1);
            ddlServico.Items.Clear();
            ddlServico.DataSource = lista;
            ddlServico.DataValueField = "Id";
            ddlServico.DataTextField = "Descricao";
            ddlServico.DataBind();

            //if (comSelecione)
            //    ddlServico.SelectedIndex = 0;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            var data = Request.QueryString["dt"];
            var idp = Request.QueryString["idp"];
            var url = string.Format("default.aspx?dt={0}&idp={1}", data, idp);
            Response.Redirect(url);
        }

        protected void btnAgendar_Click(object sender, EventArgs e)
        {
            if (VerificaBtnAgendar())
            {
                if (SalvarAgenda())
                {
                    btnCancelar_Click(sender, e);
                }
                else
                {
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Não foi possível salvar o agendamento.</p></div>";

                }
            }
        }

        private bool SalvarAgenda()
        {
            var data = Request.QueryString["dt"];
            var idp = Request.QueryString["idp"];
            var hora = Request.QueryString["hr"];
            var agdTmp = new AgendaTemp(data, hora, idp, txtNome.Value.ToUpper(), txtTelefone.Value);

            var idUserAgendaLogado = CookieSbe.Recupera("idUserAgendaLogado");
            agdTmp.IdCliente = int.Parse(idUserAgendaLogado);

            var idAgdTmp = AgendaTemp.InserirRetornndoId(agdTmp);
            if (idAgdTmp > 0)
            {
                var arrayIdScvAgdTemp = hdfSvcSelecionados.Value.Split(';');
                var itensSalvos = 0;
                for (int i = 0; i < arrayIdScvAgdTemp.Length; i++)
                {
                    if (AgendaServicosTemp.Inserir(new AgendaServicosTemp(int.Parse(arrayIdScvAgdTemp[i]), idAgdTmp)))
                        itensSalvos++;
                }
                return itensSalvos == arrayIdScvAgdTemp.Length;
            }
            return false;
        }


        private bool VerificaBtnAgendar()
        {
            ltlMsn.Text = string.Empty;

            if (txtNome.Value == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Digite seu NOME para realizar o agendamento.</p></div>";
                txtNome.Focus();
                return false;
            }

            if (txtTelefone.Value == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Digite seu TELEFONE para realizar o agendamento.</p></div>";
                txtTelefone.Focus();
                return false;
            }


            if (hdfSvcSelecionados.Value == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Selecione ao menos um serviço para realizar o agendamento.</p></div>";
                ddlServico.Focus();
                return false;
            }


            return true;
        }


    }
}