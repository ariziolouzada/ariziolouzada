using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace ferrita_Cl
{
    public class ColaboradorComanda
    {

        private static readonly string stringConnection;

        public long NumeroComanda { get; set; }
        public int IdColaborador { get; set; }
        public DateTime DataCmda { get; set; }
        public decimal ValorTotal { get; set; }
        public int IdStatus { get; set; }
        public int IdFormaPgto { get; set; }

        public string FormaPgto { get; set; }


        static ColaboradorComanda()
        {
            stringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEpcVrd"].ConnectionString;
        }


        public ColaboradorComanda() {}


        // numero_comanda, id_colaborador, data_cmda, valor_total, id_status
        public ColaboradorComanda(MySqlDataReader leitor)
        {
            NumeroComanda   = Convert.ToInt64(leitor["numero_comanda"]);
            IdColaborador   = Convert.ToInt32(leitor["id_colaborador"]);
            DataCmda        = Convert.ToDateTime(leitor["data_cmda"]);
            ValorTotal      = Convert.ToDecimal(leitor["valor_total"]);
            IdStatus        = Convert.ToInt32(leitor["id_status"]);
            IdFormaPgto     = Convert.ToInt32(leitor["id_forma_pgto"]);
            FormaPgto     = leitor["desc_forma_pgto"].ToString();
        }


        public static List<ColaboradorComanda> ListaPorIdColaborador(int idColab)
        {
            var resultado = new List<ColaboradorComanda>();
            using (var conexao = new MySqlConnection(stringConnection))
            {
                var sql = new StringBuilder();
                sql.AppendLine("SELECT cmda.*, fp.desc_forma_pgto ");
                sql.AppendLine("FROM tbl_ferrita_colaborador_cmda  AS cmda ");
                sql.AppendLine("JOIN tbl_ferrita_cmda_forma_pgto AS fp ON cmda.id_forma_pgto = fp.id_forma_pgto ");
                sql.AppendLine("WHERE id_colaborador = @idColab ");

                using (var comando = new MySqlCommand(sql.ToString(), conexao))
                {
                    using (conexao)
                    {
                        conexao.Open();//Abrir a conexão com o BD
                        comando.Parameters.AddWithValue("@idColab", idColab);
                        MySqlDataReader drEvento = comando.ExecuteReader();
                        while (drEvento.Read())
                        {
                            resultado.Add(new ColaboradorComanda(drEvento));
                        }
                    }
                }
            }
            return resultado;
        }


        public static ColaboradorComanda Pesquisa(long numero)
        {
            ColaboradorComanda resultado = null;
            using (var conexao = new MySqlConnection(stringConnection))
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("SELECT cmda.*, fp.desc_forma_pgto ");
                sql.AppendLine("FROM tbl_ferrita_colaborador_cmda  AS cmda ");
                sql.AppendLine("JOIN tbl_ferrita_cmda_forma_pgto AS fp ON cmda.id_forma_pgto = fp.id_forma_pgto ");
                sql.AppendLine("WHERE numero_comanda = @numero");

                using (var comando = new MySqlCommand(sql.ToString(), conexao))
                {
                    using (conexao)
                    {
                        conexao.Open();//Abrir a conexão com o BD
                        comando.Parameters.AddWithValue("@numero", numero);
                        MySqlDataReader drEvento = comando.ExecuteReader();
                        while (drEvento.Read())
                        {
                            resultado = new ColaboradorComanda(drEvento);
                        }
                    }
                }
            }
            return resultado;

        }


        public static bool Inserir(ColaboradorComanda colabCmda)
        {
            bool ok = false;
            using (var conexao = new MySqlConnection(stringConnection))
            {
                var sql = new StringBuilder();
                sql.Append("INSERT INTO tbl_ferrita_colaborador_cmda ");
                sql.Append("(numero_comanda, id_colaborador, data_cmda, valor_total, id_status, id_forma_pgto )");
                sql.Append("VALUES (@numero_comanda, @id_colaborador, @data_cmda, @valor_total, 1, @id_forma_pgto)");

                using (var comando = new MySqlCommand(sql.ToString(), conexao))
                {
                    using (conexao)
                    {
                        conexao.Open();
                        comando.Parameters.AddWithValue("@numero_comanda", colabCmda.NumeroComanda);
                        comando.Parameters.AddWithValue("@id_colaborador", colabCmda.IdColaborador);
                        comando.Parameters.AddWithValue("@data_cmda", colabCmda.DataCmda);
                        comando.Parameters.AddWithValue("@valor_total", colabCmda.ValorTotal);
                        comando.Parameters.AddWithValue("@id_forma_pgto", colabCmda.IdFormaPgto);
                        var inseriu = comando.ExecuteNonQuery();
                        ok = inseriu == 1;
                    }
                }
            }
            return ok;
        }


        public static bool Update(ColaboradorComanda colabCmda)
        {
            bool ok = false;
            using (var conexao = new MySqlConnection(stringConnection))
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE tbl_ferrita_colaborador_cmda SET ");
                sql.AppendLine("data_cmda = @data_cmda, id_colaborador = @id_colaborador ");
                sql.AppendLine(", id_status = @id_status, valor_total = @valor_total ");
                sql.AppendLine(", id_forma_pgto = @id_forma_pgto ");
                sql.AppendLine("WHERE numero_comanda = @numero_comanda  ");

                using (var comando = new MySqlCommand(sql.ToString(), conexao))
                {
                    using (conexao)
                    {
                        conexao.Open();
                        comando.Parameters.AddWithValue("@numero_comanda", colabCmda.NumeroComanda);
                        comando.Parameters.AddWithValue("@id_colaborador", colabCmda.IdColaborador);
                        comando.Parameters.AddWithValue("@data_cmda", colabCmda.DataCmda);
                        comando.Parameters.AddWithValue("@valor_total", colabCmda.ValorTotal);
                        comando.Parameters.AddWithValue("@id_status", colabCmda.IdStatus);
                        comando.Parameters.AddWithValue("@id_forma_pgto", colabCmda.IdFormaPgto);                        
                        var update = comando.ExecuteNonQuery();
                        if (update == 1)
                            ok = true;
                    }
                }
            }
            return ok;
        }


        public static bool Apagar(long numeroCmda)
        {
            bool ok = false;
            using (var conexao = new MySqlConnection(stringConnection))
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("DELETE FROM tbl_ferrita_colaborador_cmda ");
                sql.AppendLine("WHERE numero_comanda = @numero ");
                using (var comando = new MySqlCommand(sql.ToString(), conexao))
                {
                    using (conexao)
                    {
                        conexao.Open();
                        comando.Parameters.AddWithValue("@numero", numeroCmda);            
                        var apagar = comando.ExecuteNonQuery();
                        ok =  apagar == 1;
                    }
                }
            }
            return ok;
        }


    }
}
