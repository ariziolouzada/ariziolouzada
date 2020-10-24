using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace espacobeleza_cl
{
    public class ComandaItem
    {
        //Propriedades
        private static readonly string StringConnection;

        public long Id { get; set; }
        public long IdComanda { get; set; }
        public int IdProfissional { get; set; }
        public int IdServico { get; set; }
        public decimal Qtde { get; set; }
        public decimal Valor { get; set; }

        //Aux
        public string Servico { get; set; }
        public string Cliente { get; set; }
        public string SituacaoComanda { get; set; }
        public string FormaPgto { get; set; }
        public int IdStatusComanda { get; set; }
        public string Profissional { get; set; }

        public DateTime Data { get; set; }
        public decimal Comissao { get; set; }
        public decimal ValorComissao { get; set; }


        static ComandaItem()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEspacoDeBeleza"].ConnectionString;
        }

        public ComandaItem() { }


        public ComandaItem(string idComanda, string idProfissional, string idServico, string qtde, string valor, int comissaoItem)
        {
            Qtde = Convert.ToDecimal(qtde);
            Valor = Convert.ToDecimal(valor.Replace("R$", "").Trim());
            IdProfissional = Convert.ToInt32(idProfissional);
            IdServico = Convert.ToInt32(idServico);
            IdComanda = Convert.ToInt64(idComanda);
            Comissao = comissaoItem;
        }


        public ComandaItem(MySqlDataReader leitor)
        {
            Id = Convert.ToInt64(leitor["id"]);
            IdComanda = Convert.ToInt64(leitor["idComanda"]);
            IdProfissional = Convert.ToInt32(leitor["idProfissional"]);
            IdServico = Convert.ToInt32(leitor["idServico"]);
            Qtde = Convert.ToDecimal(leitor["qtde"]);
            Valor = Convert.ToDecimal(leitor["valor"]);
            Comissao = Convert.ToInt32(leitor["comissaoItem"]);

            //Aux
            ValorComissao = Convert.ToDecimal(leitor["valorComissao"]);
            Servico = leitor["descricao"].ToString();
            Profissional = leitor["nome"].ToString();
        }


        public static bool Inserir(ComandaItem cdaItem)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO bdespacobeleza.tbl_comanda_item ");
                strQuery.Append("(idComanda, idProfissional, idServico, qtde, valor, comissaoItem) ");
                strQuery.Append("VALUES (@idComanda, @idProfissional, @idServico, @qtde, @valor, @comissaoItem) ");
                //idAgenda @idAgenda, 
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idComanda", cdaItem.IdComanda);
                    comando.Parameters.AddWithValue("@idProfissional", cdaItem.IdProfissional);
                    comando.Parameters.AddWithValue("@idServico", cdaItem.IdServico);
                    comando.Parameters.AddWithValue("@qtde", cdaItem.Qtde);
                    comando.Parameters.AddWithValue("@valor", cdaItem.Valor);
                    comando.Parameters.AddWithValue("@comissaoItem", cdaItem.Comissao);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static int InserirRetornandoId(ComandaItem cdaItem)
        {
            int resultado = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO bdespacobeleza.tbl_comanda_item ");
                strQuery.Append("(idComanda, idProfissional, idServico, qtde, valor, comissaoItem) ");
                strQuery.Append("VALUES (@idComanda, @idProfissional, @idServico, @qtde, @valor, @comissaoItem); ");
                strQuery.Append("SELECT LAST_INSERT_ID(); ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idComanda", cdaItem.IdComanda);
                    comando.Parameters.AddWithValue("@idProfissional", cdaItem.IdProfissional);
                    comando.Parameters.AddWithValue("@idServico", cdaItem.IdServico);
                    comando.Parameters.AddWithValue("@qtde", cdaItem.Qtde);
                    comando.Parameters.AddWithValue("@valor", cdaItem.Valor);
                    comando.Parameters.AddWithValue("@comissaoItem", cdaItem.Comissao);

                    resultado = Convert.ToInt32(comando.ExecuteScalar());
                    //var insert = comando.ExecuteNonQuery();
                    //resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool Editar(ComandaItem cdaItem)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE bdespacobeleza.tbl_comanda_item SET ");
                strQuery.Append("idComanda = @idComanda, idProfissional = @idProfissional ");
                strQuery.Append(", idServico = @idServico, qtde = @qtde , valor = @valor ");
                strQuery.Append(", comissaoItem = @comissaoItem  WHERE id = @id ");
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@id", cdaItem.Id);
                    comando.Parameters.AddWithValue("@idComanda", cdaItem.IdComanda);
                    comando.Parameters.AddWithValue("@idProfissional", cdaItem.IdProfissional);
                    comando.Parameters.AddWithValue("@idServico", cdaItem.IdServico);
                    comando.Parameters.AddWithValue("@qtde", cdaItem.Qtde);
                    comando.Parameters.AddWithValue("@valor", cdaItem.Valor);
                    comando.Parameters.AddWithValue("@comissaoItem", cdaItem.Comissao);

                    var editou = comando.ExecuteNonQuery();
                    resultado = editou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }

        public static bool Apagar(long id)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM bdespacobeleza.tbl_comanda_item ");
                strQuery.Append("WHERE id = @id ");
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@id", id);
                    var excluiu = comando.ExecuteNonQuery();
                    resultado = excluiu == 1;
                }
            }
            return resultado;
        }

        public static bool ApagaItensSemReferencia()
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM bdespacobeleza.tbl_comanda_item ");
                strQuery.Append("WHERE idComanda NOT IN (SELECT id FROM bdespacobeleza.tbl_comanda GROUP BY id) ");
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    var excluiu = comando.ExecuteNonQuery();
                    resultado = excluiu > 0;
                }
            }
            return resultado;
        }


        public static Comanda Pesquisar(int id)
        {
            Comanda comanda = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.vw_comanda_item ");
                strQuery.Append("WHERE id = @id ");
                //strQuery.Append("AND idEmpresaContratante = @idEmpContrat ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    comanda = new Comanda(reader);
            }
            return comanda;
        }


        public static List<ComandaItem> Lista(long idComanda)
        {
            var lista = new List<ComandaItem>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.vw_comanda_item ");
                strQuery.Append("WHERE idComanda = @idComanda ");
                strQuery.Append("ORDER BY id ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idComanda", idComanda);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new ComandaItem(reader));
            }
            return lista;
        }


        public static List<ComandaItem> ListaCarregaTabela(long idComanda)
        {
            var lista = new List<ComandaItem>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT ci.*, svc.descricao, pf.nome ");
                strQuery.Append("FROM bdespacobeleza.tbl_comanda_item ci ");
                strQuery.Append("JOIN bdespacobeleza.tbl_servico svc ON ci.idServico = svc.idServico ");
                strQuery.Append("JOIN bdespacobeleza.tbl_profissional pf ON ci.idProfissional = pf.id ");
                strQuery.Append("WHERE ci.idComanda = @idComanda ");
                strQuery.Append("ORDER BY id ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idComanda", idComanda);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    var cmda = new ComandaItem()
                    {
                        Id = Convert.ToInt64(reader["id"]),
                        Valor = Convert.ToDecimal(reader["valor"]),
                        Servico = reader["descricao"].ToString(),
                        Profissional = reader["nome"].ToString()
                    };
                    lista.Add(cmda);
                }
            }
            return lista;
        }


        public static decimal SomaItensCmda(long idComanda)
        {
            decimal soma = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT SUM(valor) AS totalCmda ");
                strQuery.Append("FROM bdespacobeleza.tbl_comanda_item ");
                strQuery.Append("WHERE idComanda = @idComanda ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idComanda", idComanda);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    soma = reader["totalCmda"] != DBNull.Value ? Convert.ToDecimal(reader["totalCmda"]) : 0;
            }
            return soma;
        }


        public static decimal SomaProfissionalValorServico(int idProfissional, DateTime data)
        {
            decimal soma = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT SUM(valor) AS TOTAL ");
                strQuery.Append("FROM bdespacobeleza.vw_comanda_item  ");
                strQuery.Append("WHERE idProfissional = @idProfissional AND idStatus = 2 ");
                strQuery.Append(string.Format("AND data = '{0}' ", data.ToString("yyyy-MM-dd")));

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idProfissional", idProfissional);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    soma = reader["TOTAL"] != DBNull.Value ? Convert.ToDecimal(reader["TOTAL"]) : 0;
                }
            }
            return soma;
        }

        public static decimal SomaProfissionalValorServico(int idProfissional, DateTime dataInicio, DateTime dataFinal)
        {
            decimal soma = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT SUM(valor) AS TOTAL ");
                strQuery.Append("FROM bdespacobeleza.vw_comanda_item  ");
                strQuery.Append("WHERE idProfissional = @idProfissional AND idStatus = 2 ");
                strQuery.Append(string.Format("AND data >= '{0}' ", dataInicio.ToString("yyyy-MM-dd")));
                strQuery.Append(string.Format("AND data <= '{0}' ", dataFinal.ToString("yyyy-MM-dd")));

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idProfissional", idProfissional);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    soma = reader["TOTAL"] != DBNull.Value ? Convert.ToDecimal(reader["TOTAL"]) : 0;
                }
            }
            return soma;
        }

        public static decimal SomaProfissionalValorComissao(int idProfissional, DateTime data)
        {
            decimal soma = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT SUM(valorComissao) AS TOTAL ");
                strQuery.Append("FROM bdespacobeleza.vw_comanda_item  ");
                strQuery.Append("WHERE idProfissional = @idProfissional AND idStatus = 2 ");
                strQuery.Append(string.Format("AND data = '{0}' ", data.ToString("yyyy-MM-dd")));

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idProfissional", idProfissional);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    soma = reader["TOTAL"] != DBNull.Value ? Convert.ToDecimal(reader["TOTAL"]) : 0;
                }
            }
            return soma;
        }

        public static decimal SomaProfissionalValorComissao(int idProfissional, DateTime dataInicio, DateTime dataFinal)
        {
            decimal soma = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT SUM(valorComissao) AS TOTAL ");
                strQuery.Append("FROM bdespacobeleza.vw_comanda_item  ");
                strQuery.Append("WHERE idProfissional = @idProfissional AND idStatus = 2 ");
                strQuery.Append(string.Format("AND data >= '{0}' ", dataInicio.ToString("yyyy-MM-dd")));
                strQuery.Append(string.Format("AND data <= '{0}' ", dataFinal.ToString("yyyy-MM-dd")));

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idProfissional", idProfissional);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    soma = reader["TOTAL"] != DBNull.Value ? Convert.ToDecimal(reader["TOTAL"]) : 0;
                }
            }
            return soma;
        }


        public static int QtdeItensComanda(int idEmpresaContratante, DateTime dataInicio, DateTime dataFinal)
        {
            int soma = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT COUNT(ci.id) AS QTDE ");
                strQuery.Append("FROM bdespacobeleza.tbl_comanda_item ci ");
                strQuery.Append("JOIN bdespacobeleza.vw_comanda c ON ci.idComanda = c.id ");
                strQuery.Append("WHERE c.idEmpresaContratante = @idEmpresaContratante ");
                strQuery.Append(string.Format("AND c.data >= '{0}' ", dataInicio.ToString("yyyy-MM-dd")));
                strQuery.Append(string.Format("AND c.data <= '{0}' ", dataFinal.ToString("yyyy-MM-dd")));

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idEmpresaContratante", idEmpresaContratante);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    soma = reader["QTDE"] != DBNull.Value ? Convert.ToInt32(reader["QTDE"]) : 0;
                }
            }
            return soma;
        }

        public static int QtdeItensComandaPorProfissional(int idProfissional, DateTime dataInicio, DateTime dataFinal)
        {
            int soma = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT COUNT(id) AS QTDE ");
                strQuery.Append("FROM bdespacobeleza.vw_comanda_item  ");
                strQuery.Append("WHERE idProfissional = @idProfissional AND idStatus = 2 ");
                strQuery.Append(string.Format("AND data >= '{0}' ", dataInicio.ToString("yyyy-MM-dd")));
                strQuery.Append(string.Format("AND data <= '{0}' ", dataFinal.ToString("yyyy-MM-dd")));

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idProfissional", idProfissional);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    soma = reader["QTDE"] != DBNull.Value ? Convert.ToInt32(reader["QTDE"]) : 0;
                }
            }
            return soma;
        }


        public static List<ComandaItem> ListaProfissionalComissao(int idEmpresaContratante, int idProfissao, DateTime dataInicio, DateTime dataFinal)
        {
            var lista = new List<ComandaItem>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT  ci.*, cda.data, svc.comissao, (ci.valor * (svc.comissao / 100)) AS vlrComissao ");
                strQuery.Append(", p.idProfissao, pp.descricao_profissao, p.idEmpresaContratante ");
                strQuery.Append("FROM bdespacobeleza.vw_comanda_item  AS ci ");
                strQuery.Append("JOIN bdespacobeleza.tbl_profissional_servico AS svc ON ci.idServico = svc.idServico ");
                strQuery.Append("JOIN bdespacobeleza.tbl_comanda AS cda ON ci.idComanda = cda.id ");
                strQuery.Append("JOIN bdespacobeleza.tbl_profissional AS p ON ci.idProfissional = p.id ");
                strQuery.Append("JOIN bdespacobeleza.tbl_profissional_profissao AS pp ON p.idProfissao = pp.id_profissao ");

                strQuery.Append("WHERE p.idEmpresaContratante = @idEmpresaContratante ");

                strQuery.Append(string.Format("AND c.data >= '{0}' ", dataInicio.ToString("yyyy-MM-dd")));
                strQuery.Append(string.Format("AND c.data <= '{0}' ", dataFinal.ToString("yyyy-MM-dd")));


                if (idProfissao > 0)
                    strQuery.Append("AND pp.idProfissao = @idProfissao ");


                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idEmpresaContratante", idEmpresaContratante);
                comando.Parameters.AddWithValue("@dataInicio", dataInicio);
                comando.Parameters.AddWithValue("@dataFinal", dataFinal);

                if (idProfissao > 0)
                    comando.Parameters.AddWithValue("@idProfissional", idProfissao);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    var itemCda = new ComandaItem(reader);
                    itemCda.Data = Convert.ToDateTime(reader["data"]);
                    itemCda.Comissao = Convert.ToDecimal(reader["comissao"]);
                    itemCda.ValorComissao = Convert.ToDecimal(reader["vlrComissao"]);

                    lista.Add(itemCda);
                }

            }
            return lista;
        }


        public static List<ComandaItem> ListaServicos(int idProfissional, DateTime dataInicio, DateTime dataFinal)
        {
            var lista = new List<ComandaItem>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT ci.*, cda.nomeCliente, cda.descricaoStatusComanda, cda.formaPgto  ");
                strQuery.Append("FROM bdespacobeleza.vw_comanda_item ci ");
                strQuery.Append("JOIN bdespacobeleza.vw_comanda cda ON ci.idComanda = cda.id ");
                strQuery.Append("WHERE idProfissional = @idProfissional ");
                strQuery.Append(string.Format("AND ci.data >= '{0}' ", dataInicio.ToString("yyyy-MM-dd")));
                strQuery.Append(string.Format("AND ci.data <= '{0}' ", dataFinal.ToString("yyyy-MM-dd")));
                strQuery.Append("ORDER BY cda.id ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idProfissional", idProfissional);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    var itemCda = new ComandaItem(reader);
                    itemCda.Data = Convert.ToDateTime(reader["data"]);
                    //itemCda.Comissao = Convert.ToDecimal(reader["comissao"]);
                    itemCda.ValorComissao = Convert.ToDecimal(reader["valorComissao"]);
                    itemCda.IdStatusComanda = Convert.ToInt32(reader["idStatus"]);
                    itemCda.Cliente = reader["nomeCliente"].ToString();
                    itemCda.SituacaoComanda = reader["descricaoStatusComanda"].ToString();
                    itemCda.FormaPgto = reader["formaPgto"].ToString();
                    lista.Add(itemCda);
                }
            }
            return lista;
        }

        public static List<ComandaItem> ListaServicos(int idProfissional, DateTime data)
        {
            var lista = new List<ComandaItem>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT ci.*, cda.nomeCliente, cda.descricaoStatusComanda, cda.formaPgto  ");
                strQuery.Append("FROM bdespacobeleza.vw_comanda_item ci ");
                strQuery.Append("JOIN bdespacobeleza.vw_comanda cda ON ci.idComanda = cda.id ");
                strQuery.Append("WHERE idProfissional = @idProfissional ");
                strQuery.Append(string.Format("AND ci.data = '{0}' ", data.ToString("yyyy-MM-dd")));
                strQuery.Append("ORDER BY cda.id ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idProfissional", idProfissional);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    var itemCda = new ComandaItem(reader);
                    itemCda.Data = Convert.ToDateTime(reader["data"]);
                    //itemCda.Comissao = Convert.ToDecimal(reader["comissao"]);
                    itemCda.ValorComissao = Convert.ToDecimal(reader["valorComissao"]);
                    itemCda.IdStatusComanda = Convert.ToInt32(reader["idStatus"]);
                    itemCda.Cliente = reader["nomeCliente"].ToString();
                    itemCda.SituacaoComanda = reader["descricaoStatusComanda"].ToString();
                    itemCda.FormaPgto = reader["formaPgto"].ToString();
                    lista.Add(itemCda);
                }
            }
            return lista;
        }


    }
}
