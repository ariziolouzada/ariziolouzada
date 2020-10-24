using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Text;

namespace sbessencial_cl
{
    public class AgendaTemp
    {
        //Propriedades
        private static readonly string StringConnection;
        public int Id { get; set; }
        public int IdProfissional { get; set; }
        public int IdCliente { get; set; }
        public int IdStatus { get; set; }
        public DateTime DataHora { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }

        //vw_agenda_temp
        public string DescricaoStatus { get; set; }

        static AgendaTemp()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdSbEssencial"].ConnectionString;
        }


        public AgendaTemp(string data, string hora, string idProfissional, string nome, string tel)
        {
            DataHora = DataHoraAgenda(data, hora);
            IdProfissional = int.Parse(idProfissional);
            Nome = nome;
            Telefone = tel;
        }


        public AgendaTemp(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["id_agenda_temp"]);
            IdProfissional = Convert.ToInt32(leitor["id_profissional"]);
            IdCliente = Convert.ToInt32(leitor["id_cliente"]);
            IdStatus = Convert.ToInt32(leitor["id_status_agenda_temp"]);
            DataHora = Convert.ToDateTime(leitor["dataHora"]);
            Nome = leitor["nome"].ToString();
            Telefone = leitor["telefone"].ToString();
            DescricaoStatus = leitor["desc_status_agenda_temp"].ToString();
        }


        public DateTime DataHoraAgenda(string data, string hora)
        {
            var dataStr = string.Format("{0} {1}:00:00", data, hora);
            return Convert.ToDateTime(dataStr);
        }


        public static bool Inserir(AgendaTemp agd)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                //terminar colocar idstatus
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO sbessencial.tbl_agenda_temp ");
                strQuery.Append("(dataHora, id_profissional, id_cliente, nome, telefone, id_status_agenda_temp) ");
                strQuery.Append("VALUES (@dataHora, @id_profissional, @id_cliente, @nome, @telefone, 1) ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD 
                    comando.Parameters.AddWithValue("@dataHora", agd.DataHora);
                    comando.Parameters.AddWithValue("@id_profissional", agd.IdProfissional);
                    comando.Parameters.AddWithValue("@id_cliente", agd.IdCliente);
                    comando.Parameters.AddWithValue("@nome", agd.Nome);
                    comando.Parameters.AddWithValue("@telefone", agd.Telefone);
                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;
                }
            }
            return resultado;
        }

        public static int InserirRetornndoId(AgendaTemp agd)
        {
            int resultado = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO sbessencial.tbl_agenda_temp ");
                strQuery.Append("(dataHora, id_profissional, id_cliente, nome, telefone, id_status_agenda_temp) ");
                strQuery.Append("VALUES (@dataHora, @id_profissional, @id_cliente, @nome, @telefone, 1); ");
                strQuery.Append("SELECT LAST_INSERT_ID(); ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD 
                    comando.Parameters.AddWithValue("@dataHora", agd.DataHora);
                    comando.Parameters.AddWithValue("@id_profissional", agd.IdProfissional);
                    comando.Parameters.AddWithValue("@id_cliente", agd.IdCliente);
                    comando.Parameters.AddWithValue("@nome", agd.Nome);
                    comando.Parameters.AddWithValue("@telefone", agd.Telefone);

                    resultado = Convert.ToInt32(comando.ExecuteScalar());
                    //var insert = comando.ExecuteNonQuery();
                    //resultado = insert == 1;
                }
            }
            return resultado;
        }


        public static bool ExcluirPeloId(long idRegistroAgenda)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM sbessencial.tbl_agenda_temp ");
                strQuery.Append("WHERE id_agenda_temp = @idRegistroAgenda ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idRegistroAgenda", idRegistroAgenda);

                    var excluiu = comando.ExecuteNonQuery();
                    resultado = excluiu == 1;
                }
            }
            return resultado;
        }

        public static bool Excluir(DateTime data, int hora, int idProfissional)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM sbessencial.tbl_agenda_temp ");
                strQuery.Append(string.Format("WHERE DATE(dataHora) = '{0}' ", data.ToString("yyyy-MM-dd")));
                strQuery.Append("AND HOUR(dataHora) = @hora ");
                strQuery.Append("AND id_Profissional = @idProfissional ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@hora", hora);
                    comando.Parameters.AddWithValue("@idProfissional", idProfissional);

                    var excluiu = comando.ExecuteNonQuery();
                    resultado = excluiu == 1;
                }
            }
            return resultado;
        }

        public static bool Excluir( int id)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM sbessencial.tbl_agenda_temp ");
                strQuery.Append("WHERE id_agenda_temp = @id ");

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

        public static bool EditarStatus(int idStatus, int idAgdTmp)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE sbessencial.tbl_agenda_temp SET ");
                strQuery.Append("id_status_agenda_temp = @idStatus ");
                strQuery.Append("WHERE id_agenda_temp = @id ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@id", idAgdTmp);
                    comando.Parameters.AddWithValue("@idStatus", idStatus);
                    var editou = comando.ExecuteNonQuery();
                    resultado = editou == 1;
                }
            }
            return resultado;
        }

        public static bool PesquisaDataHora(DateTime data, int hora, int idProfissional)
        {
            var resultado = false;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.vw_agenda_temp ");
                strQuery.Append(string.Format("WHERE DATE(dataHora) = '{0}' ", data.ToString("yyyy-MM-dd")));
                strQuery.Append("AND HOUR(dataHora) = @hora ");
                strQuery.Append("AND id_Profissional = @idProfissional ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@hora", hora);
                comando.Parameters.AddWithValue("@idProfissional", idProfissional);
                var reader = comando.ExecuteReader();
                resultado = reader.Read();
            }
            return resultado;
        }

        public static AgendaTemp Pesquisar(DateTime data, int hora, int idProfissional)
        {
            AgendaTemp resultado = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.vw_agenda_temp ");
                strQuery.Append(string.Format("WHERE DATE(dataHora) = '{0}' ", data.ToString("yyyy-MM-dd")));
                strQuery.Append("AND HOUR(dataHora) = @hora ");
                strQuery.Append("AND id_Profissional = @idProfissional ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@hora", hora);
                comando.Parameters.AddWithValue("@idProfissional", idProfissional);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    resultado = new AgendaTemp(reader);
            }
            return resultado;
        }


        public static AgendaTemp Pesquisar(int idAgdTmp)
        {
            AgendaTemp resultado = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.vw_agenda_temp ");
                strQuery.Append("WHERE id_agenda_temp = @idAgdTmp ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idAgdTmp", idAgdTmp);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    resultado = new AgendaTemp(reader);
            }
            return resultado;
        }
    }
}
