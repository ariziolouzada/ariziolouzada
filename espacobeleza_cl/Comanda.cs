using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace espacobeleza_cl
{
    public class Comanda
    {
        //Propriedades
        private static readonly string StringConnection;

        public long Id { get; set; }
        public int IdCliente { get; set; }
        public int IdFormaPgto { get; set; }
        public int IdStatus { get; set; }
        public DateTime Data { get; set; }
        public int IdEmpresaContratante { get; set; }
        public decimal Valor { get; set; }

        //AUX
        public string Cliente { get; set; }
        public string FormaPgto { get; set; }
        public string Status { get; set; }


        static Comanda()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEspacoDeBeleza"].ConnectionString;
        }

        public Comanda(string data, string idCliente, long id)
        {
            Data = Convert.ToDateTime(data);
            IdCliente = Convert.ToInt32(idCliente);
            Id = id;
            //Numero = Convert.ToInt32(numero);
        }

        public Comanda(MySqlDataReader leitor)
        {
            Id = Convert.ToInt64(leitor["id"]);
            IdCliente = Convert.ToInt32(leitor["idCliente"]);
            IdFormaPgto = Convert.ToInt32(leitor["idFormaPgto"]);
            IdStatus = Convert.ToInt32(leitor["idStatus"]);
            Data = Convert.ToDateTime(leitor["data"]);
            Valor = Convert.ToDecimal(leitor["valor"]);
            //IdEmpresaContratante = Convert.ToInt32(leitor["idEmpresaContratante"]);

            //aux
            FormaPgto = leitor["formaPgto"].ToString();
            Cliente = leitor["nomeCliente"].ToString();
            Status = leitor["descricaoStatusComanda"].ToString();
        }

        public Comanda()
        {
        }

        public static string GeraIdTemp()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssffff");
        }

        public static bool Inserir(Comanda comanda)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO bdespacobeleza.tbl_comanda ");
                strQuery.Append("(id, data, idCliente, idFormaPgto, valor, idStatus) ");
                strQuery.Append("VALUES (@id, @data, @idCliente, @idFormaPgto, @valor, @idStatus) ");
                //idAgenda @idAgenda, 
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@data", comanda.Data);
                    comando.Parameters.AddWithValue("@id", comanda.Id);
                    comando.Parameters.AddWithValue("@idCliente", comanda.IdCliente);
                    comando.Parameters.AddWithValue("@idFormaPgto", comanda.IdFormaPgto);
                    comando.Parameters.AddWithValue("@idStatus", comanda.IdStatus);
                    comando.Parameters.AddWithValue("@valor", comanda.Valor);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static int InserirRetornandoId(Comanda comanda)
        {
            int resultado = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO bdespacobeleza.tbl_comanda ");
                strQuery.Append("(data, idCliente, idFormaPgto, valor, idStatus) ");
                strQuery.Append("VALUES (@data, @idCliente, @idFormaPgto, @valor, @idStatus) ");
                strQuery.Append("SELECT LAST_INSERT_ID(); ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@data", comanda.Data);
                    //comando.Parameters.AddWithValue("@numero", comanda.Numero);
                    comando.Parameters.AddWithValue("@idCliente", comanda.IdCliente);
                    comando.Parameters.AddWithValue("@idFormaPgto", comanda.IdFormaPgto);
                    comando.Parameters.AddWithValue("@idStatus", comanda.IdStatus);
                    comando.Parameters.AddWithValue("@valor", comanda.Valor);

                    resultado = Convert.ToInt32(comando.ExecuteScalar());
                    //var insert = comando.ExecuteNonQuery();
                    //resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool Editar(Comanda comanda)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE bdespacobeleza.tbl_comanda SET ");
                strQuery.Append("idFormaPgto = @idFormaPgto, idStatus = @idStatus ");
                strQuery.Append(", idCliente = @idCliente, valor = @valor  ");
                strQuery.Append(", data = @data WHERE id = @id ");
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@id", comanda.Id);
                    comando.Parameters.AddWithValue("@data", comanda.Data);
                    //comando.Parameters.AddWithValue("@numero", comanda.Numero);
                    comando.Parameters.AddWithValue("@valor", comanda.Valor);
                    comando.Parameters.AddWithValue("@idCliente", comanda.IdCliente);
                    comando.Parameters.AddWithValue("@idStatus", comanda.IdStatus);
                    comando.Parameters.AddWithValue("@idFormaPgto", comanda.IdFormaPgto);

                    var editou = comando.ExecuteNonQuery();
                    resultado = editou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }

        public static bool FecharComanda(long idComanda, int idFormaPgto)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE bdespacobeleza.tbl_comanda SET ");
                strQuery.Append("idFormaPgto = @idFormaPgto, idStatus = 2 ");
                strQuery.Append("WHERE id = @id ");
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@id", idComanda);
                    //comando.Parameters.AddWithValue("@idStatus", 2);
                    comando.Parameters.AddWithValue("@idFormaPgto", idFormaPgto);

                    var editou = comando.ExecuteNonQuery();
                    resultado = editou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }

        public static bool Apagar(int id)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM bdespacobeleza.tbl_comanda ");
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


        public static Comanda Pesquisar(long id)
        {
            Comanda comanda = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.vw_comanda ");
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


        public static List<Comanda> Lista(DateTime data, int idEmpContrat)
        {
            var lista = new List<Comanda>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.vw_comanda ");
                strQuery.Append(string.Format("WHERE data = '{0}' ", data.ToString("yyyy-MM-dd")));
                strQuery.Append("AND idEmpresaContratante = @idEmpContrat ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idEmpContrat", idEmpContrat);
                //comando.Parameters.AddWithValue("@data", data);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new Comanda(reader));
            }
            return lista;
        }


        public static List<Comanda> Lista(DateTime data, int idEmpContrat, int idStatus)
        {
            var lista = new List<Comanda>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.vw_comanda ");
                //strQuery.Append("JOIN bdespacobeleza.tbl_cliente AS clt ON cda.idCliente = clt.idCliente ");
                strQuery.Append(string.Format("WHERE data = '{0}' ", data.ToString("yyyy-MM-dd")));
                strQuery.Append("AND idEmpresaContratante = @idEmpContrat ");

                if (idStatus > 0)
                    strQuery.Append("AND idStatus = @idStatus ");

                //strQuery.Append("ORDER BY numero ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idEmpContrat", idEmpContrat);
                //comando.Parameters.AddWithValue("@data", data);

                if (idStatus > 0)
                    comando.Parameters.AddWithValue("@idStatus", idStatus);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new Comanda(reader));
            }
            return lista;
        }


        public static List<Comanda> ListaPorProfissional(DateTime data, int idProfissional, int idStatus)
        {
            var lista = new List<Comanda>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT DISTINCT cda.id, ci.idProfissional,cda.* ");
                strQuery.Append("FROM bdespacobeleza.tbl_comanda_item ci ");
                strQuery.Append("JOIN bdespacobeleza.vw_comanda cda ON ci.idComanda = cda.id ");
                strQuery.Append(string.Format("WHERE data = '{0}' ", data.ToString("yyyy-MM-dd")));
                strQuery.Append("AND ci.idProfissional = @idProfissional ");

                if (idStatus > 0)
                    strQuery.Append("AND idStatus = @idStatus ");

                //strQuery.Append("ORDER BY numero ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idProfissional", idProfissional);
                //comando.Parameters.AddWithValue("@data", data);

                if (idStatus > 0)
                    comando.Parameters.AddWithValue("@idStatus", idStatus);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new Comanda(reader));
            }
            return lista;
        }


        public static decimal TotalPorProfissional(DateTime data, int idProfissional, int idStatus)
        {
            decimal soma = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT DISTINCT cda.id, ci.idProfissional, cda.valor ");
                strQuery.Append("FROM bdespacobeleza.tbl_comanda_item ci ");
                strQuery.Append("JOIN bdespacobeleza.vw_comanda cda ON ci.idComanda = cda.id ");
                strQuery.Append(string.Format("WHERE data = '{0}' ", data.ToString("yyyy-MM-dd")));

                if (idProfissional > 0)
                    strQuery.Append("AND ci.idProfissional = @idProfissional ");

                if (idStatus > 0)
                    strQuery.Append("AND idStatus = @idStatus ");

                //strQuery.Append("ORDER BY numero ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                if (idProfissional > 0)
                    comando.Parameters.AddWithValue("@idProfissional", idProfissional);
                //comando.Parameters.AddWithValue("@data", data);

                if (idStatus > 0)
                    comando.Parameters.AddWithValue("@idStatus", idStatus);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    soma = soma + (reader["valor"] != DBNull.Value ? Convert.ToDecimal(reader["valor"]) : 0);
            }
            return soma;
        }
               

        public static int QtdeTotalComanda(int idEmpresaContratante, DateTime dataInicio, DateTime dataFinal, int idStatus)
        {
            int qtde = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT count(id) AS qtde ");
                strQuery.Append("FROM bdespacobeleza.vw_comanda ");
                strQuery.Append("WHERE idEmpresaContratante = @idEmpresaContratante ");
                strQuery.Append(string.Format("AND data >= '{0}' ", dataInicio.ToString("yyyy-MM-dd")));
                strQuery.Append(string.Format("AND data <= '{0}' ", dataFinal.ToString("yyyy-MM-dd")));

                if (idStatus > 0)
                    strQuery.Append("AND idStatus = @idStatus ");

                //strQuery.Append("ORDER BY numero ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                    comando.Parameters.AddWithValue("@idEmpresaContratante", idEmpresaContratante);

                if (idStatus > 0)
                    comando.Parameters.AddWithValue("@idStatus", idStatus);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    qtde = reader["qtde"] != DBNull.Value ? Convert.ToInt32(reader["qtde"]) : 0;
            }
            return qtde;
        }
              
        public static int QtdeComandaPorProfissional(int idProfissional, DateTime dataInicio, DateTime dataFinal,  int idStatus)
        {
            int qtde = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT count(c.id) AS qtde ");
                strQuery.Append("FROM bdespacobeleza.vw_comanda c ");
                strQuery.Append("JOIN bdespacobeleza.tbl_comanda_item ci ON c.id = ci.idComanda ");
                strQuery.Append("WHERE ci.idProfissional = @idProfissional ");
                strQuery.Append(string.Format("AND data >= '{0}' ", dataInicio.ToString("yyyy-MM-dd")));
                strQuery.Append(string.Format("AND data <= '{0}' ", dataFinal.ToString("yyyy-MM-dd")));

                if (idStatus > 0)
                    strQuery.Append("AND idStatus = @idStatus ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                    comando.Parameters.AddWithValue("@idProfissional", idProfissional);

                if (idStatus > 0)
                    comando.Parameters.AddWithValue("@idStatus", idStatus);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    qtde = reader["qtde"] != DBNull.Value ? Convert.ToInt32(reader["qtde"]) : 0;
            }
            return qtde;
        }
               
        public static decimal Soma(DateTime data, int idEmpContrat, int idStatus)
        {
            decimal soma = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT SUM(valor) as TOTAL ");
                strQuery.Append("FROM bdespacobeleza.vw_comanda ");
                //strQuery.Append("JOIN bdespacobeleza.tbl_cliente AS clt ON cda.idCliente = clt.idCliente ");
                strQuery.Append(string.Format("WHERE data = '{0}' ", data.ToString("yyyy-MM-dd")));
                strQuery.Append("AND idEmpresaContratante = @idEmpContrat ");

                if (idStatus > 0)
                    strQuery.Append("AND idStatus = @idStatus ");

                //strQuery.Append("ORDER BY numero ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idEmpContrat", idEmpContrat);
                //comando.Parameters.AddWithValue("@data", data);

                if (idStatus > 0)
                    comando.Parameters.AddWithValue("@idStatus", idStatus);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    soma = (reader["TOTAL"] != DBNull.Value ? Convert.ToDecimal(reader["TOTAL"]) : 0);
            }
            return soma;
        }

        //public static decimal Soma(DateTime data, int idEmpresaContratante, int idStatus)
        //{
        //    decimal soma = 0;
        //    using (var conexao = new MySqlConnection(StringConnection))
        //    {
        //        var strQuery = new StringBuilder();
        //        strQuery.Append("SELECT DISTINCT cda.id, ci.idProfissional, cda.valor ");
        //        strQuery.Append("FROM bdespacobeleza.tbl_comanda_item ci ");
        //        strQuery.Append("JOIN bdespacobeleza.vw_comanda cda ON ci.idComanda = cda.id ");
        //        strQuery.Append(string.Format("WHERE data = '{0}' ", data.ToString("yyyy-MM-dd")));

        //        if (idEmpresaContratante > 0)
        //            strQuery.Append("AND cda.idEmpresaContratante = @idEmpresaContratante ");

        //        if (idStatus > 0)
        //            strQuery.Append("AND idStatus = @idStatus ");

        //        //strQuery.Append("ORDER BY numero ");

        //        conexao.Open();//Abrir a conexão com o BD
        //        var comando = new MySqlCommand(strQuery.ToString(), conexao);

        //        if (idEmpresaContratante > 0)
        //            comando.Parameters.AddWithValue("@idEmpresaContratante", idEmpresaContratante);
        //        //comando.Parameters.AddWithValue("@data", data);

        //        if (idStatus > 0)
        //            comando.Parameters.AddWithValue("@idStatus", idStatus);

        //        var reader = comando.ExecuteReader();
        //        while (reader.Read())
        //            soma = soma + (reader["valor"] != DBNull.Value ? Convert.ToDecimal(reader["valor"]) : 0);
        //    }
        //    return soma;
        //}

    }
}
