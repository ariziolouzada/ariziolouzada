using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ferrita_Cl
{
    public class ColaboradorComandaItem
    {

        private static readonly string stringConnection;
        public long NumeroComanda { get; set; }
        public int Item { get; set; }
        public int IdProduto { get; set; }
        public int IdSituacao { get; set; }
        public int Qtde { get; set; }
        public Decimal ValorUnitario { get; set; }
        public Decimal ValorTotal { get; set; }
        
        public int QtdeTamanhoP { get; set; }
        public int QtdeTamanhoM { get; set; }
        public int QtdeTamanhoG { get; set; }
        public int QtdeTamanhoGG { get; set; }
        public int QtdeTamanhoEG { get; set; }
        public string TamanhoUnico { get; set; }

        //View
        public string DescricaoProduto { get; set; }
        public string CodigoProduto    { get; set; }
        public int    IdStatusProduto  { get; set; }


        static ColaboradorComandaItem()
        {
            stringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEpcVrd"].ConnectionString;
        }


        public ColaboradorComandaItem() {}


        public ColaboradorComandaItem(MySqlDataReader leitor)
        {
            NumeroComanda = Convert.ToInt64(leitor["numero_cmda"]);
            IdProduto = Convert.ToInt32(leitor["id_produto"]);
            Item = Convert.ToInt32(leitor["item"]);
            IdSituacao = Convert.ToInt32(leitor["id_situacao"]);
            Qtde = Convert.ToInt32(leitor["qtde"]);
            ValorUnitario = Convert.ToDecimal(leitor["valor_unit"]);
            ValorTotal = Convert.ToDecimal(leitor["valor_total"]);

            QtdeTamanhoP = leitor["qtd_tam_p"] != DBNull.Value ? Convert.ToInt32(leitor["qtd_tam_p"]) : 0;
            QtdeTamanhoM = leitor["qtd_tam_m"] != DBNull.Value ? Convert.ToInt32(leitor["qtd_tam_m"]) : 0;
            QtdeTamanhoG = leitor["qtd_tam_g"] != DBNull.Value ? Convert.ToInt32(leitor["qtd_tam_g"]) : 0;
            QtdeTamanhoGG = leitor["qtd_tam_gg"] != DBNull.Value ? Convert.ToInt32(leitor["qtd_tam_gg"]) : 0;
            QtdeTamanhoEG = leitor["qtd_tam_eg"] != DBNull.Value ? Convert.ToInt32(leitor["qtd_tam_eg"]) : 0;

            if (leitor["tamanho_unico"] != DBNull.Value)
                TamanhoUnico = leitor["tamanho_unico"].ToString();


            //vw_ferrita_colab_cmda_itens
            DescricaoProduto = leitor["descricao"].ToString();
            CodigoProduto = leitor["codigo"].ToString();
            IdStatusProduto = Convert.ToInt32(leitor["id_status"]);

        }


        public static bool Inserir(ColaboradorComandaItem cmdaItem)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(stringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO tbl_ferrita_colaborador_cmda_item ");
                strQuery.Append("(item, numero_cmda, id_produto, id_situacao, qtde, valor_unit, valor_total  ");
                strQuery.Append(" , Tamanho_Unico, qtd_tam_p, qtd_tam_m, qtd_tam_g, qtd_tam_gg, qtd_tam_eg ) ");
                strQuery.Append("VALUES (@item, @numero_cmda, @id_produto , @id_situacao, @qtde, @valor_unit, @valor_total ");
                strQuery.Append(" , @Tamanho_Unico, @qtd_tam_p, @qtd_tam_m, @qtd_tam_g, @qtd_tam_gg, @qtd_tam_eg ) ");


                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@item", cmdaItem.Item);
                    comando.Parameters.AddWithValue("@numero_cmda", cmdaItem.NumeroComanda);
                    comando.Parameters.AddWithValue("@id_produto", cmdaItem.IdProduto);
                    comando.Parameters.AddWithValue("@id_situacao", cmdaItem.IdSituacao);
                    comando.Parameters.AddWithValue("@qtde", cmdaItem.Qtde);
                    comando.Parameters.AddWithValue("@valor_unit", cmdaItem.ValorUnitario);
                    comando.Parameters.AddWithValue("@valor_total", cmdaItem.ValorTotal);

                    comando.Parameters.AddWithValue("@Tamanho_Unico", cmdaItem.TamanhoUnico);
                    comando.Parameters.AddWithValue("@qtd_tam_p",  cmdaItem.QtdeTamanhoP);
                    comando.Parameters.AddWithValue("@qtd_tam_m",  cmdaItem.QtdeTamanhoM);
                    comando.Parameters.AddWithValue("@qtd_tam_g",  cmdaItem.QtdeTamanhoG);
                    comando.Parameters.AddWithValue("@qtd_tam_gg", cmdaItem.QtdeTamanhoGG);
                    comando.Parameters.AddWithValue("@qtd_tam_eg", cmdaItem.QtdeTamanhoEG);
                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool Editar(ColaboradorComandaItem cmdaItem)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(stringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE tbl_ferrita_colaborador_cmda_item SET ");
                strQuery.Append(" id_produto = @id_produto, id_situacao = @id_situacao ");
                strQuery.Append(", qtde = @qtde, valor_unit = @valor_unit, valor_total = @valor_total ");
                strQuery.Append("WHERE numero_cmda = @numero_cmda AND item = @item");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@item", cmdaItem.Item);
                    comando.Parameters.AddWithValue("@numero_cmda", cmdaItem.NumeroComanda);
                    comando.Parameters.AddWithValue("@id_produto", cmdaItem.IdProduto);
                    comando.Parameters.AddWithValue("@id_situacao", cmdaItem.IdSituacao);
                    comando.Parameters.AddWithValue("@qtde", cmdaItem.Qtde);
                    comando.Parameters.AddWithValue("@valor_unit", cmdaItem.ValorUnitario);
                    comando.Parameters.AddWithValue("@valor_total", cmdaItem.ValorTotal);
                    var editou = comando.ExecuteNonQuery();
                    resultado = editou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool ValidarItensTemporarios(long numeroCmda)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(stringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE tbl_ferrita_colaborador_cmda_item SET ");
                strQuery.Append("id_situacao = 1 ");
                strQuery.Append("WHERE numero_cmda = @numero_cmda");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@numero_cmda", numeroCmda);
                    //comando.Parameters.AddWithValue("@id_situacao", cmdaItem.IdSituacao);
                    var editou = comando.ExecuteNonQuery();
                    resultado = editou > 0;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool ApagarItensTemporarios()
        {
            bool resultado;
            using (var conexao = new MySqlConnection(stringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM tbl_ferrita_colaborador_cmda_item ");
                strQuery.Append("WHERE id_situacao = 0 ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    var apagou = comando.ExecuteNonQuery();
                    resultado = apagou > 0;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool EditarNumeroItem(long numeroCmda, int item, int novoItem)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(stringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE tbl_ferrita_colaborador_cmda_item SET ");
                strQuery.Append("item = @novoItem ");
                strQuery.Append("WHERE numero_cmda = @numeroComanda AND item = @item ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@numeroComanda", numeroCmda);
                    comando.Parameters.AddWithValue("@item", item);
                    comando.Parameters.AddWithValue("@novoItem", novoItem);
                    var apagou = comando.ExecuteNonQuery();
                    resultado = apagou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool ApagarItem(long numeroCmda, int item)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(stringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM tbl_ferrita_colaborador_cmda_item ");
                strQuery.Append("WHERE numero_cmda = @numeroComanda AND item = @item ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@numeroComanda", numeroCmda);
                    comando.Parameters.AddWithValue("@item", item);
                    var apagou = comando.ExecuteNonQuery();
                    resultado = apagou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static List<ColaboradorComandaItem> Lista(long numeroComanda)
        {
            var resultado = new List<ColaboradorComandaItem>();
            using (var conexao = new MySqlConnection(stringConnection))
            {
                var sql = new StringBuilder();
                sql.Append("SELECT cmdaItens.*, sp.descricao, sp.codigo, sp.id_status ");
                sql.Append("FROM  tbl_ferrita_colaborador_cmda_item AS cmdaItens ");
                sql.Append("JOIN  tbl_ferrita_svc_prod AS sp  ON cmdaItens.id_produto = sp.ID_SVC_PROD ");
                sql.Append("WHERE numero_cmda = @numeroComanda ");

                using (var comando = new MySqlCommand(sql.ToString(), conexao))
                {
                    using (conexao)
                    {
                        conexao.Open();//Abrir a conexão com o BD
                        comando.Parameters.AddWithValue("@numeroComanda", numeroComanda);
                        var drEvento = comando.ExecuteReader();
                        while (drEvento.Read())
                        {
                            resultado.Add(new ColaboradorComandaItem(drEvento));
                        }
                    }
                }
            }
            return resultado;
        }


        public static ColaboradorComandaItem Pesquisa(long numeroComanda, int item)
        {
            ColaboradorComandaItem  resultado = null;
            using (var conexao = new MySqlConnection(stringConnection))
            {
                var sql = new StringBuilder();
                //sql.Append("SELECT * ");
                //sql.Append("FROM  vw_ferrita_colab_cmda_itens");
                //sql.Append("WHERE numero_cmda = @numeroComanda ");

                sql.Append("SELECT cmdaItens.*, sp.descricao, sp.codigo, sp.id_status ");
                sql.Append("FROM  tbl_ferrita_colaborador_cmda_item AS cmdaItens ");
                sql.Append("JOIN  tbl_ferrita_svc_prod AS sp  ON cmdaItens.id_produto = sp.ID_SVC_PROD ");

                sql.Append("AND item = @item ");

                using (var comando = new MySqlCommand(sql.ToString(), conexao))
                {
                    using (conexao)
                    {
                        conexao.Open();//Abrir a conexão com o BD
                        comando.Parameters.AddWithValue("@numero_cmda", numeroComanda);
                        comando.Parameters.AddWithValue("@item", item);
                        var drEvento = comando.ExecuteReader();
                        while (drEvento.Read())
                        {
                            resultado = new ColaboradorComandaItem(drEvento);
                        }
                    }
                }
            }
            return resultado;
        }

    }
}
