using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ferrita_Cl
{
    public class ServicoProduto
    {

        private static readonly string stringConnection;
        public int IdProduto { get; set; }
        public int IdStatus { get; set; }
        public string DescricaoProduto { get; set; }
        public string CodigoProduto { get; set; }
        public Decimal PrecoVenda { get; set; }
        public Decimal PrecoCusto { get; set; }
        public Decimal Lucro { get; set; }
        //public Decimal Desconto { get; set; }
        public Decimal MargemLucro { get; set; }
        //public string CodigoDeBarra { get; set; }
        //public string Referencia { get; set; }
        public int IdTipoServProd { get; set; }
        //public int ParticipacaoColaorador { get; set; }
        public int QtdeEstoque { get; set; }
        //public int QtdeLimiteEstoque { get; set; }
        public int IdFornecedor { get; set; }
        //public DateTime DataValidade { get; set; }
        public int QtdeEstqTamanhoP { get; set; }
        public int QtdeEstqTamanhoM { get; set; }
        public int QtdeEstqTamanhoG { get; set; }
        public int QtdeEstqTamanhoGG { get; set; }
        public int QtdeEstqTamanhoEG { get; set; }
        public string TamanhoUnico { get; set; }

        static ServicoProduto()
        {
            stringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEpcVrd"].ConnectionString;
        }

        public ServicoProduto() { }

        public ServicoProduto(int id, string desc)
        {
            IdProduto = id;
            DescricaoProduto = desc;
        }

        public ServicoProduto(MySqlDataReader leitor)
        {
            //  id_svc_prod, id_status, id_fornecedor, id_tipo_svc_prod, codigo, descricao, 
            //preco_custo, margem_lucro, qtde_estoque, qtde_estoque_tam_p, qtde_estoque_tam_m, qtde_estoque_tam_g, 
            //qtde_estoque_tam_gg, qtde_estoque_tam_eg, tamanho_unico

            IdProduto = Convert.ToInt32(leitor["id_svc_prod"]);
            IdStatus = Convert.ToInt32(leitor["id_status"]);


            if (leitor["id_tipo_svc_prod"] != DBNull.Value)
                IdTipoServProd = Convert.ToInt32(leitor["id_tipo_svc_prod"]);

            if (leitor["descricao"] != DBNull.Value)
                DescricaoProduto = leitor["descricao"].ToString();

            if (leitor["codigo"] != DBNull.Value)
                CodigoProduto = leitor["codigo"].ToString();

            IdFornecedor = Convert.ToInt32(leitor["id_fornecedor"]);

            if (leitor["qtde_estoque"] != DBNull.Value)
                QtdeEstoque = Convert.ToInt32(leitor["qtde_estoque"]);

            if (leitor["margem_lucro"] != DBNull.Value)
                MargemLucro = Convert.ToDecimal(leitor["margem_lucro"]);

            if (leitor["lucro"] != DBNull.Value)
                Lucro = Convert.ToDecimal(leitor["lucro"]);

            if (leitor["preco_custo"] != DBNull.Value)
                PrecoCusto = Convert.ToDecimal(leitor["preco_custo"]);

            if (leitor["preco_venda"] != DBNull.Value)
                PrecoVenda = Convert.ToDecimal(leitor["preco_venda"]);

            QtdeEstqTamanhoP = leitor["qtde_estoque_tam_p"] != DBNull.Value ? Convert.ToInt32(leitor["qtde_estoque_tam_p"]) : 0;

            QtdeEstqTamanhoM = leitor["qtde_estoque_tam_m"] != DBNull.Value ? Convert.ToInt32(leitor["qtde_estoque_tam_m"]) : 0;

            QtdeEstqTamanhoG = leitor["qtde_estoque_tam_g"] != DBNull.Value ? Convert.ToInt32(leitor["qtde_estoque_tam_g"]) : 0;

            QtdeEstqTamanhoGG = leitor["qtde_estoque_tam_gg"] != DBNull.Value ? Convert.ToInt32(leitor["qtde_estoque_tam_gg"]) : 0;

            QtdeEstqTamanhoEG = leitor["qtde_estoque_tam_eg"] != DBNull.Value ? Convert.ToInt32(leitor["qtde_estoque_tam_eg"]) : 0;

            if (leitor["tamanho_unico"] != DBNull.Value)
                TamanhoUnico = leitor["tamanho_unico"].ToString();


            //if (leitor["REF_SERV_PROD"] != DBNull.Value)
            //    Referencia = leitor["REF_SERV_PROD"].ToString();

            //PrecoProduto = Convert.ToDecimal(leitor[""]);

            //if (leitor["DESCONTO"] != DBNull.Value)
            //    Desconto = Convert.ToDecimal(leitor["DESCONTO"]);

            //if (leitor["COD_BARRA"] != DBNull.Value)
            //    CodigoDeBarra = Convert.ToInt32(leitor["COD_BARRA"]);
            //CodigoDeBarra = leitor["COD_BARRA"].ToString();


            //if (leitor["PARTICIPACAO_COLABORADOR"] != DBNull.Value)
            //    ParticipacaoColaorador = Convert.ToInt32(leitor["PARTICIPACAO_COLABORADOR"]);

            //if (leitor["QTDE_LIMITE_ESTOQUE"] != DBNull.Value)
            //    QtdeLimiteEstoque = Convert.ToInt32(leitor["QTDE_LIMITE_ESTOQUE"]);

            //if (leitor["DATA_VALIDADE"] != DBNull.Value)
            //    DataValidade = Convert.ToDateTime(leitor["DATA_VALIDADE"]);

        }


        public static bool Inserir(ServicoProduto svcprod)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(stringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO tbl_ferrita_svc_prod ");
                strQuery.Append("(id_status, id_fornecedor, id_tipo_svc_prod, codigo, descricao, ");
                strQuery.Append("preco_custo, preco_venda, margem_lucro, lucro, tamanho_unico, ");

                if (svcprod.TamanhoUnico.Equals("SIM"))
                    strQuery.Append("qtde_estoque ) ");
                else
                    strQuery.Append("qtde_estoque_tam_p, qtde_estoque_tam_m, qtde_estoque_tam_g, qtde_estoque_tam_gg, qtde_estoque_tam_eg )");

                strQuery.Append("VALUES (1, @id_fornecedor, @id_tipo_svc_prod, @codigo, @descricao, ");
                strQuery.Append("@preco_custo, @preco_venda, @margem_lucro, @lucro,  @tamanho_unico, ");

                if (svcprod.TamanhoUnico.Equals("SIM"))
                    strQuery.Append("@qtde_estoque ) ");
                else
                    strQuery.Append("@qtde_estoque_tam_p, @qtde_estoque_tam_m, @qtde_estoque_tam_g, @qtde_estoque_tam_gg, @qtde_estoque_tam_eg )");


                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@id_fornecedor", svcprod.IdFornecedor);
                    comando.Parameters.AddWithValue("@id_tipo_svc_prod", svcprod.IdTipoServProd);
                    comando.Parameters.AddWithValue("@codigo", svcprod.CodigoProduto);
                    comando.Parameters.AddWithValue("@descricao", svcprod.DescricaoProduto.ToUpper());
                    comando.Parameters.AddWithValue("@preco_custo", svcprod.PrecoCusto);
                    comando.Parameters.AddWithValue("@preco_venda", svcprod.PrecoVenda);
                    comando.Parameters.AddWithValue("@margem_lucro", svcprod.MargemLucro);
                    comando.Parameters.AddWithValue("@lucro", svcprod.Lucro);
                    comando.Parameters.AddWithValue("@tamanho_unico", svcprod.TamanhoUnico);

                    if (svcprod.TamanhoUnico.Equals("SIM"))
                        comando.Parameters.AddWithValue("@qtde_estoque", svcprod.QtdeEstoque);
                    else
                    {
                        comando.Parameters.AddWithValue("@qtde_estoque_tam_p", svcprod.QtdeEstqTamanhoP);
                        comando.Parameters.AddWithValue("@qtde_estoque_tam_m", svcprod.QtdeEstqTamanhoM);
                        comando.Parameters.AddWithValue("@qtde_estoque_tam_g", svcprod.QtdeEstqTamanhoG);
                        comando.Parameters.AddWithValue("@qtde_estoque_tam_gg", svcprod.QtdeEstqTamanhoGG);
                        comando.Parameters.AddWithValue("@qtde_estoque_tam_eg", svcprod.QtdeEstqTamanhoEG);
                    }

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool AtualizarEstoque(ServicoProduto svcprod)
        {

            var svc = ServicoProduto.Pesquisa(svcprod.IdProduto);
            var novaQted = svc.QtdeEstoque - svcprod.QtdeEstoque;

            var novaQtedTamP = 0;
            var novaQtedTamM = 0;
            var novaQtedTamG = 0;
            var novaQtedTamGG = 0;
            var novaQtedTamEG = 0;

            if (!svcprod.TamanhoUnico.Equals("SIM"))
            {
                novaQtedTamP = svc.QtdeEstqTamanhoP - svcprod.QtdeEstqTamanhoP;
                novaQtedTamM = svc.QtdeEstqTamanhoM - svcprod.QtdeEstqTamanhoM;
                novaQtedTamG = svc.QtdeEstqTamanhoG - svcprod.QtdeEstqTamanhoG;
                novaQtedTamGG = svc.QtdeEstqTamanhoGG - svcprod.QtdeEstqTamanhoGG;
                novaQtedTamEG = svc.QtdeEstqTamanhoEG - svcprod.QtdeEstqTamanhoEG;
            }

            bool resultado;
            using (var conexao = new MySqlConnection(stringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE tbl_ferrita_svc_prod SET ");

                if (svcprod.TamanhoUnico.Equals("SIM"))
                    strQuery.Append(" qtde_estoque = @qtde_estoque ");
                else
                {
                    strQuery.Append(" qtde_estoque = @qtde_estoque ");
                    strQuery.Append(", qtde_estoque_tam_p = @qtde_estoque_tam_p, qtde_estoque_tam_m = @qtde_estoque_tam_m ");
                    strQuery.Append(", qtde_estoque_tam_g = @qtde_estoque_tam_g, qtde_estoque_tam_gg = @qtde_estoque_tam_gg ");
                    strQuery.Append(", qtde_estoque_tam_eg = @qtde_estoque_tam_eg ");
                }

                strQuery.Append("WHERE id_svc_prod = @id_svc_prod ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@id_svc_prod", svcprod.IdProduto);

                    if (svcprod.TamanhoUnico.Equals("SIM"))
                        comando.Parameters.AddWithValue("@qtde_estoque", novaQted );
                    else
                    {
                        comando.Parameters.AddWithValue("@qtde_estoque",        novaQted    );
                        comando.Parameters.AddWithValue("@qtde_estoque_tam_p",  novaQtedTamP);
                        comando.Parameters.AddWithValue("@qtde_estoque_tam_m",  novaQtedTamM);
                        comando.Parameters.AddWithValue("@qtde_estoque_tam_g",  novaQtedTamG);
                        comando.Parameters.AddWithValue("@qtde_estoque_tam_gg", novaQtedTamGG );
                        comando.Parameters.AddWithValue("@qtde_estoque_tam_eg", novaQtedTamEG );

                    }
                    var editou = comando.ExecuteNonQuery();
                    resultado = editou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }

        public static bool Editar(ServicoProduto svcprod)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(stringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE tbl_ferrita_svc_prod SET ");
                strQuery.Append(" id_fornecedor = @id_fornecedor, id_tipo_svc_prod = @id_tipo_svc_prod, lucro = @lucro");
                strQuery.Append(", descricao = @descricao, preco_custo = @preco_custo, preco_venda = @preco_venda");
                strQuery.Append(", codigo = @codigo, margem_lucro = @margem_lucro, tamanho_unico = @tamanho_unico ");

                if (svcprod.TamanhoUnico.Equals("SIM"))
                    strQuery.Append(", qtde_estoque = @qtde_estoque ");
                else
                {
                    strQuery.Append(", qtde_estoque = @qtde_estoque ");
                    strQuery.Append(", qtde_estoque_tam_p = @qtde_estoque_tam_p, qtde_estoque_tam_m = @qtde_estoque_tam_m ");
                    strQuery.Append(", qtde_estoque_tam_g = @qtde_estoque_tam_g, qtde_estoque_tam_gg = @qtde_estoque_tam_gg ");
                    strQuery.Append(", qtde_estoque_tam_eg = @qtde_estoque_tam_eg ");
                }

                strQuery.Append("WHERE id_svc_prod = @id_svc_prod ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@id_svc_prod", svcprod.IdProduto);
                    comando.Parameters.AddWithValue("@id_fornecedor", svcprod.IdFornecedor);
                    comando.Parameters.AddWithValue("@id_tipo_svc_prod", svcprod.IdTipoServProd);
                    comando.Parameters.AddWithValue("@codigo", svcprod.CodigoProduto);
                    comando.Parameters.AddWithValue("@descricao", svcprod.DescricaoProduto);
                    comando.Parameters.AddWithValue("@preco_custo", svcprod.PrecoCusto);
                    comando.Parameters.AddWithValue("@preco_venda", svcprod.PrecoVenda);
                    comando.Parameters.AddWithValue("@margem_lucro", svcprod.MargemLucro);
                    comando.Parameters.AddWithValue("@lucro", svcprod.Lucro);
                    comando.Parameters.AddWithValue("@tamanho_unico", svcprod.TamanhoUnico);

                    if (svcprod.TamanhoUnico.Equals("SIM"))
                        comando.Parameters.AddWithValue("@qtde_estoque", svcprod.QtdeEstoque);
                    else
                    {
                        comando.Parameters.AddWithValue("@qtde_estoque", svcprod.QtdeEstoque);
                        comando.Parameters.AddWithValue("@qtde_estoque_tam_p", svcprod.QtdeEstqTamanhoP);
                        comando.Parameters.AddWithValue("@qtde_estoque_tam_m", svcprod.QtdeEstqTamanhoM);
                        comando.Parameters.AddWithValue("@qtde_estoque_tam_g", svcprod.QtdeEstqTamanhoG);
                        comando.Parameters.AddWithValue("@qtde_estoque_tam_gg", svcprod.QtdeEstqTamanhoGG);
                        comando.Parameters.AddWithValue("@qtde_estoque_tam_eg", svcprod.QtdeEstqTamanhoEG);

                    }

                    var editou = comando.ExecuteNonQuery();
                    resultado = editou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }

        public static bool EditarValorLucro(int idProd, decimal valor)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(stringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE tbl_ferrita_svc_prod SET ");
                strQuery.Append("lucro = @lucro ");                                   
                strQuery.Append("WHERE id_svc_prod = @id_svc_prod ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@id_svc_prod", idProd);
                    comando.Parameters.AddWithValue("@lucro", valor);

                    var editou = comando.ExecuteNonQuery();
                    resultado = editou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static List<ServicoProduto> ListaServicoProduto()
        {
            List<ServicoProduto> resultado = new List<ServicoProduto>();

            using (var conexao = new MySqlConnection(stringConnection))
            {
                StringBuilder sql = new StringBuilder();

                sql.Append("SELECT * ");
                sql.Append("FROM tbl_ferrita_svc_prod ");
                //sql.AppendLine("JOIN TBL_TIPO_SERV_PROD AS TTSP ON TSP.ID_TIPO_SERV_PROD = TTSP.ID_TIPO_SERV_PROD ");
                //sql.AppendLine("WHERE TTSP.DESC_TIPO_SERV_PROD = @DESC_TIPO_SERV_PROD");

                using (var comando = new MySqlCommand(sql.ToString(), conexao))
                {

                    using (conexao)
                    {
                        conexao.Open();//Abrir a conexão com o BD
                        //comando.Parameters.AddWithValue(@"DESC_TIPO_SERV_PROD", pDescServProd);
                        var drEvento = comando.ExecuteReader();
                        while (drEvento.Read())
                        {
                            resultado.Add(new ServicoProduto(drEvento));
                        }
                    }
                }
            }

            return resultado;

        }

        public static List<ServicoProduto> ListaDdlServicoProduto(bool comSelecione)
        {
            List<ServicoProduto> resultado = new List<ServicoProduto>();

            using (var conexao = new MySqlConnection(stringConnection))
            {
                StringBuilder sql = new StringBuilder();

                sql.Append("SELECT * ");
                sql.Append("FROM tbl_ferrita_svc_prod ");
                sql.Append("WHERE ID_STATUS = 1");

                if (comSelecione)
                    resultado.Add(new ServicoProduto(0, "Selecione..."));

                using (var comando = new MySqlCommand(sql.ToString(), conexao))
                {
                    using (conexao)
                    {
                        conexao.Open();//Abrir a conexão com o BD
                        //comando.Parameters.AddWithValue(@"DESC_TIPO_SERV_PROD", pDescServProd);
                        var drEvento = comando.ExecuteReader();
                        while (drEvento.Read())
                        {
                            var descricao = drEvento["descricao"].ToString();
                            if (!descricao.Equals(""))//Somente os q possue descrição cadastrada
                                resultado.Add(new ServicoProduto(drEvento));
                        }
                    }
                }
            }
            return resultado;
        }


        public static List<ServicoProduto> ListaServicoProduto(string value)
        {
            var resultado = new List<ServicoProduto>();

            using (var conexao = new MySqlConnection(stringConnection))
            {
                StringBuilder sql = new StringBuilder();

                sql.Append("SELECT * ");
                sql.Append("FROM tbl_ferrita_svc_prod ");
                sql.AppendLine(string.Format("WHERE CODIGO like '%{0}%' or DESCRICAO LIKE '%{0}%'", value));

                using (var comando = new MySqlCommand(sql.ToString(), conexao))
                {
                    using (conexao)
                    {
                        conexao.Open();//Abrir a conexão com o BD
                        //comando.Parameters.AddWithValue(@"DESC_TIPO_SERV_PROD", pDescServProd);
                        var drEvento = comando.ExecuteReader();
                        while (drEvento.Read())
                        {
                            resultado.Add(new ServicoProduto(drEvento));
                        }
                    }
                }
            }
            return resultado;
        }


        public static ServicoProduto Pesquisa(int idSvcProd)
        {
            ServicoProduto resultado = null;
            using (var conexao = new MySqlConnection(stringConnection))
            {
                var sql = new StringBuilder();
                sql.Append("SELECT * ");
                sql.Append("FROM  tbl_ferrita_svc_prod ");
                sql.Append("WHERE id_svc_prod = @idSvcProd ");

                using (var comando = new MySqlCommand(sql.ToString(), conexao))
                {
                    using (conexao)
                    {
                        conexao.Open();//Abrir a conexão com o BD
                        comando.Parameters.AddWithValue(@"idSvcProd", idSvcProd);
                        var drEvento = comando.ExecuteReader();
                        while (drEvento.Read())
                        {
                            resultado = new ServicoProduto(drEvento);
                        }
                    }
                }
            }
            return resultado;

        }


        //public static List<ServicoProduto> ListaServicoProduto(int pTipoPesq, string pValue, bool pTamUnc)
        //{
        //    List<ServicoProduto> resultado = new List<ServicoProduto>();

        //    using (var conexao = new MySqlConnection(stringConnection))
        //    {
        //        StringBuilder sql = new StringBuilder();

        //        sql.AppendLine("SELECT * ");
        //        sql.AppendLine("FROM TBL_SERVICO_PRODUTO ");

        //        if (pTipoPesq == 1)//filtra pelo Código
        //            sql.AppendLine("WHERE ID_SERV_PROD = " + pValue);

        //        if (pTipoPesq == 2)//filtra pela Descrição
        //            sql.AppendLine("WHERE DESC_SERV_PROD LIKE '%" + pValue + "%' ");

        //        if (pTipoPesq == 3)//filtra pela Referencia
        //            sql.AppendLine("WHERE REF_SERV_PROD LIKE '%" + pValue + "%' ");

        //        if (pTipoPesq == 4)//filtra pelo Fornecedor
        //        {
        //            //sql.AppendLine("JOIN TBL_FORNECEDOR ON ID_SERV_PROD = ID_FORNECEDOR ");
        //            sql.AppendLine("WHERE ID_FORNECEDOR_SERV_PROD = " + pValue);
        //        }

        //        if (pTamUnc)//filtra pela Referencia
        //            if (pTipoPesq == 0)
        //                sql.AppendLine("WHERE TAMANHO_UNICO = 1 ");
        //            else
        //                sql.AppendLine("AND TAMANHO_UNICO = 1 ");

        //        else
        //            if (pTipoPesq == 0)
        //            sql.AppendLine("WHERE TAMANHO_UNICO = 0 ");
        //        else
        //            sql.AppendLine("AND TAMANHO_UNICO = 0 ");


        //        using (var comando = new MySqlCommand(sql.ToString(), conexao))
        //        {

        //            using (conexao)
        //            {
        //                conexao.Open();//Abrir a conexão com o BD
        //                //comando.Parameters.AddWithValue(@"DESC_TIPO_SERV_PROD", pDescServProd);
        //                var drEvento = comando.ExecuteReader();
        //                while (drEvento.Read())
        //                {
        //                    resultado.Add(new ServicoProduto(drEvento));
        //                }
        //            }
        //        }
        //    }

        //    return resultado;

        //}


        //public static List<string> ListaDescServicoProduto()
        //{
        //    List<string> resultado = new List<string>();

        //    using (var conexao = new MySqlConnection(stringConnection))
        //    {
        //        StringBuilder sql = new StringBuilder();

        //        sql.AppendLine("SELECT DESC_SERV_PROD ");
        //        sql.AppendLine("FROM TBL_SERVICO_PRODUTO ");
        //        sql.AppendLine("ORDER BY DESC_SERV_PROD ");

        //        using (var comando = new MySqlCommand(sql.ToString(), conexao))
        //        {
        //            using (conexao)
        //            {
        //                conexao.Open();//Abrir a conexão com o BD
        //                var drEvento = comando.ExecuteReader();
        //                while (drEvento.Read())
        //                {
        //                    resultado.Add(drEvento["DESC_SERV_PROD"].ToString());
        //                }
        //            }
        //        }
        //    }

        //    return resultado;

        //}



    }
}
