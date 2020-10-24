using CriptografiaSgpm;
using espacobeleza_cl;
using System;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;

namespace ariziolouzada.espacodebeleza.pages.comanda
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
                }


                CarregaTabela();
            }
        }


        private void CarregaTabela()
        {
            try
            {
                //RECUPERAR O COOKIE
                var idEmpresaContratante = "0";
                HttpCookie cookie = Request.Cookies["idEmpresaContratante"];
                if (cookie != null && cookie.Value != null)
                    idEmpresaContratante = cookie.Value;

                idEmpresaContratante = Criptografia.Decrypt(idEmpresaContratante);

                var tabelaHtml = new StringBuilder();
                var lista = Comanda.Lista(DateTime.Parse(txtData.Value), int.Parse(idEmpresaContratante), int.Parse(ddlTipo.SelectedValue));

                decimal total = 0;
                if (lista.Count == 0)
                {
                    tabelaHtml.Append("<div class=\"panel panel-warning\"><div class=\"panel-heading\"> <h4 class=\"panel-title\"><i class=\"icon ion-clock text-warning\">" +
                                      "</i>!! Nenhum Item cadastrado !!</h4></div></div>");
                }
                else
                {
                    //terminar

                    tabelaHtml.Append("<table id=\"tabelaClientes\" class=\"table table-striped table-bordered \"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Número</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Cliente</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Serviço(s)</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Produto(s)</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Valor</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Status</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Forma Pgto</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\"></th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Ações</th>");

                    tabelaHtml.Append("</tr></thead>");
                    tabelaHtml.Append("<tbody>");

                    foreach (var comanda in lista)
                    {
                        var idCriotografado = Criptografia.Encrypt(comanda.Id.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        //tabelaHtml.Append("<tr class=\"odd gradeX\">");
                        tabelaHtml.Append("<tr>");

                        //Numero                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format("{0:000}", comanda.Id)));

                        //Cliente                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left;\" >{0}</td>", comanda.Cliente));

                        //Servicos realizados
                        var btnSvc = string.Format("<button type=\"button\" onclick=\"carregaServicoCmdaModal('{0}');\" class=\"btn btn-outline btn-success  dim\" data-toggle=\"modal\" data-target=\"#myModal2\" title=\"Serviços da comanda.\"> <i class=\"fa fa-cogs fa-2x\"></i> </button>", idCriotografado);
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", btnSvc));

                        //Produtos
                        var btnPdtos = string.Format("<button type=\"button\" onclick=\"carregaProdutoCmdaModal('{0}');\" class=\"btn btn-outline btn-warning  dim\" data-toggle=\"modal\" data-target=\"#myModal3\" title=\"Produtos da comanda.\"> <i class=\"fa fa-paypal fa-2x\"></i> </button>", idCriotografado);
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", btnPdtos));

                        //Valor                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format("{0:C}", comanda.Valor)));
                        total = total + comanda.Valor;
                        //Situação
                        var situacao = string.Format("<span class=\"badge badge-{0}\">{1}</span>", comanda.IdStatus == 1 ? "primary" : "danger", comanda.Status);
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", situacao));

                        //Forma Pgto                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", comanda.FormaPgto));

                        // ========= AÇÕES ==========

                        //Fechar Comanda
                        if (comanda.IdStatus == 1)
                        {
                            var btnFecharComanda = string.Format("<button type=\"button\" onclick=\"carregaDadosCmdaModal('{0}');\" class=\"btn btn-outline btn-warning  dim\" data-toggle=\"modal\" data-target=\"#modalFecharComanda\" title=\"Fechar comanda.\"> <i class=\"fa fa-money fa-2x\"></i> </button>", idCriotografado);
                            tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", btnFecharComanda));
                        }
                        else
                            tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center;\" >{0}</td>", ""));

                        //Editar                        
                        //tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\"><a href=\"newedit.aspx?id={0}\" class=\"btn btn-info btn-xs\" data-original-title=\"\"  title=\"Editar dados comanda.\" ><i class=\"fa fa-pencil fa-2x\"></i> </a></td>", idCriotografado));
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\"><button type=\"button\" onclick=\"editarDadosComanda('{0}');\" class=\"btn btn-outline btn-info  dim\" title=\"Editar dados comanda.\"> <i class=\"fa fa-pencil fa-2x\"></i> </button></td>", idCriotografado));

                        tabelaHtml.Append("</tr>");
                    }
                    tabelaHtml.Append("</tbody></table>");
                }
                lblTotal.Text = string.Format("{0:C}", total);
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
            if (txtData.Value != string.Empty)
            {
                CarregaTabela();

            }
            else
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ERRO: Entre com a data para carregar a lista de comandas!</p></div>";
                txtData.Focus();
            }
            //var nome = txtPesquisa.Value;
            //Session["paremetrosDefault"] = string.Format("{0}", nome);
            //Response.Redirect(string.Format("default.aspx?nome={0}", nome));

        }


        private static string CarregaTblSvcComandaModal(string idComanda)
        {
            try
            {
                var tabelaHtml = new StringBuilder();
                idComanda = Criptografia.Decrypt(idComanda.Replace('_', '+'));
                var lista = ComandaItem.Lista(long.Parse(idComanda));

                if (lista.Count == 0)
                {
                    tabelaHtml.Append("<div class=\"panel panel-warning\"><div class=\"panel-heading\"> <h4 class=\"panel-title\"><i class=\"icon ion-clock text-warning\">" +
                                      "</i>!! Nenhum Item cadastrado !!</h4></div></div>");
                }
                else
                {
                    tabelaHtml.Append("<table id=\"tabelaClientes\" class=\"table table-striped table-bordered \"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Item</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Profissional</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Serviço</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Valor</th>");
                    tabelaHtml.Append("</tr></thead>");
                    tabelaHtml.Append("<tbody>");

                    decimal totalServico = 0;
                    var item = 1;
                    foreach (var cdaItem in lista)
                    {
                        var idCriotografado = Criptografia.Encrypt(cdaItem.Id.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        tabelaHtml.Append("<tr>");

                        //Item                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format(@"{0:00}", item)));

                        //Profissional                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", cdaItem.Profissional));

                        //Serviço                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", cdaItem.Servico));
                        totalServico = totalServico + cdaItem.Valor;

                        //Valor                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format("{0:C}", cdaItem.Valor)));

                        item++;
                        tabelaHtml.Append("</tr>");
                    }
                    ////Total   bgcolor=\"#CFCFCF\"
                    //tabelaHtml.Append("<tr style=\"font-size: 14pt;\">");
                    //tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: right\" colspan=\"3\"><b>TOTAL</b></td>"));
                    //tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\"  colspan=\"2\"><b>{0}</b></td>", string.Format("{0:C}", totalServico)));
                    //tabelaHtml.Append("</tr>");

                    tabelaHtml.Append("</tbody></table>");
                }
                return tabelaHtml.ToString();

            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaTabela-ERRO:" + ex.Message + "</p></div>";
            }

        }


        private static string CarregaTblProdComandaModal(string idComanda)
        {
            try
            {
                var tabelaHtml = new StringBuilder();
                idComanda = Criptografia.Decrypt(idComanda.Replace('_', '+'));
                var lista = ComandaItemProduto.ListaCarregaTabela(long.Parse(idComanda));

                if (lista.Count == 0)
                {
                    tabelaHtml.Append("<div class=\"panel panel-warning\"><div class=\"panel-heading\"> <h4 class=\"panel-title\"><i class=\"icon ion-clock text-warning\">" +
                                      "</i>!! Nenhum Item cadastrado !!</h4></div></div>");
                }
                else
                {
                    tabelaHtml.Append("<table id=\"tabelaClientes\" class=\"table table-striped table-bordered \"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Item</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Produto</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Qtde</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Valor</th>");
                    tabelaHtml.Append("</tr></thead>");
                    tabelaHtml.Append("<tbody>");

                    decimal totalServico = 0;
                    var item = 1;
                    foreach (var cdaItem in lista)
                    {
                        var idCriotografado = Criptografia.Encrypt(cdaItem.Id.ToString());
                        idCriotografado = idCriotografado.Replace('+', '_');

                        tabelaHtml.Append("<tr>");

                        //Item                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format(@"{0:00}", item)));
                        //Produto                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: left\" >{0}</td>", cdaItem.DescricaoProduto));
                        //Qtde                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", cdaItem.Qtde));

                        totalServico = totalServico + cdaItem.Valor;
                        //Valor                    
                        tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format("{0:C}", cdaItem.Valor)));

                        item++;
                        tabelaHtml.Append("</tr>");
                    }
                    ////Total   bgcolor=\"#CFCFCF\"
                    //tabelaHtml.Append("<tr style=\"font-size: 14pt;\">");
                    //tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: right\" colspan=\"3\"><b>TOTAL</b></td>"));
                    //tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\"  colspan=\"2\"><b>{0}</b></td>", string.Format("{0:C}", totalServico)));
                    //tabelaHtml.Append("</tr>");

                    tabelaHtml.Append("</tbody></table>");
                }
                return tabelaHtml.ToString();

            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaTabelaProduto-ERRO:" + ex.Message + "</p></div>";
            }

        }


        private static string CarregaTbldadosComandaModal(string idComanda)
        {
            try
            {
                var tabelaHtml = new StringBuilder();
                idComanda = Criptografia.Decrypt(idComanda.Replace('_', '+'));
                var comanda = Comanda.Pesquisar(long.Parse(idComanda));
                if (comanda != null)
                {

                    tabelaHtml.Append("<table id=\"tabelaClientes\" class=\"table table-striped table-bordered \"><thead><tr>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Nº</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Cliente</th>");
                    tabelaHtml.Append("<th style=\"text-align: center; vertical-align: middle;\">Valor</th>");
                    tabelaHtml.Append("</tr></thead>");
                    tabelaHtml.Append("<tbody>");
                    tabelaHtml.Append("<tr>");
                    //numero                    
                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", comanda.Id));
                    //Cliente                    
                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", comanda.Cliente));
                    //Valor                    
                    tabelaHtml.Append(string.Format("<td style=\"vertical-align: middle; text-align: center\" >{0}</td>", string.Format("{0:C}", comanda.Valor)));
                    tabelaHtml.Append("</tr></tbody></table>");
                }
                else
                {
                    tabelaHtml.Append("<div class=\"panel panel-warning\"><div class=\"panel-heading\"> <h4 class=\"panel-title\"><i class=\"icon ion-clock text-warning\">" +
                                        "</i>!! Nenhum dado cadastrado !!</h4></div></div>");
                }
                return tabelaHtml.ToString();
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaTabela-ERRO:" + ex.Message + "</p></div>";
            }

        }


        [WebMethod]
        public static string CarregaDadosCmdaModal(string idComanda)
        {
            try
            {
                return CarregaTbldadosComandaModal(idComanda);
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-danger fade in m-b-15\"><strong>CarregaDadosCmdaModal-Error: </strong> " +
                        ex.Message + "<span class=\"close\" data-dismiss=\"alert\">X</span></div>";
            }
        }

        [WebMethod]
        public static string CarregaServicoCmdaModal(string idComanda)
        {
            try
            {
                return CarregaTblSvcComandaModal(idComanda);
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-danger fade in m-b-15\"><strong>CarregaServicoCmdaModal-Error: </strong> " +
                        ex.Message + "<span class=\"close\" data-dismiss=\"alert\">X</span></div>";
            }
        }

        [WebMethod]
        public static string CarregaProdutoCmdaModal(string idComanda)
        {
            try
            {
                return CarregaTblProdComandaModal(idComanda);
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-danger fade in m-b-15\"><strong>carregaProdutoCmdaModal-Error: </strong> " +
                        ex.Message + "<span class=\"close\" data-dismiss=\"alert\">X</span></div>";
            }
        }
        [WebMethod]
        public static string FecharComanda(string idComanda, string idFormaPgto)
        {
            try
            {
                idComanda = Criptografia.Decrypt(idComanda.Replace('_', '+'));
                if (!Comanda.FecharComanda(long.Parse(idComanda), int.Parse(idFormaPgto)))
                {
                    return "<div class=\"alert alert-danger fade in m-b-15\"><strong>FecharComanda-Erro: </strong> " +
                            "Não foi possível fechar a comanda de nº " + idComanda + ".<span class=\"close\" data-dismiss=\"alert\">X</span></div>";
                }
                return "";
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-danger fade in m-b-15\"><strong>CarregaServicoCmdaModal-Error: </strong> " +
                        ex.Message + "<span class=\"close\" data-dismiss=\"alert\">X</span></div>";
            }
        }

        //[WebMethod]
        //public static string ResetarSenha(string idCliente)
        //{
        //    try
        //    {
        //        var clt = ClienteEspBel.Pesquisar(int.Parse(idCliente));
        //        if (clt.TelCelular1.Length < 9)
        //        {
        //            //return "<div class=\"alert alert-danger fade in m-b-15\"><strong>ResetarSenha-Error: </strong> " +
        //            //       "Atualize 1º o número do tel. Celular para resetar a senha do cliente selecionado!!<span class=\"close\" data-dismiss=\"alert\">X</span></div>";

        //            return "Atualize 1º o número do tel. Celular para resetar a senha do cliente selecionado!!";
        //        }

        //        if (ClienteEspBel.ResetarSenha(int.Parse(idCliente)))
        //        {
        //            //return "<div class=\"alert alert-block alert-success fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
        //            //        "</button><p><i class=\"fa fa-check-square fa-lg\"></i> Senha Resetada com sucesso!</p></div>";

        //            return "1";
        //        }

        //        //return "<div class=\"alert alert-danger fade in m-b-15\"><strong>ResetarSenha-Error: </strong> " +
        //        //       "Não foi possível resetar a senha do cliente selecionado!!<span class=\"close\" data-dismiss=\"alert\">X</span></div>";

        //        return "Não foi possível resetar a senha do cliente selecionado!!";

        //    }
        //    catch (Exception ex)
        //    {
        //        //return "<div class=\"alert alert-danger fade in m-b-15\"><strong>ResetarSenha-Error: </strong> " +
        //        //       ex.Message + "<span class=\"close\" data-dismiss=\"alert\">X</span></div>";
        //        return "ResetarSenha-Error: " + ex.Message;

        //    }
        //}

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTabela();
        }

    }
}