using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Text;

namespace espacobeleza_cl
{
    public class Agenda
    {

        //Propriedades
        private static readonly string StringConnection;

        public DateTime DataAgenda       { get; set; }
        public int      IdAgenda         { get; set; }
        public int      IdHorarioInicial { get; set; }
        public int      IdHorarioFinal   { get; set; }
        public int      IdProfissional   { get; set; }
        public int      IdServico        { get; set; }
        public int      IdCliente        { get; set; }
        public int      IdStatusAgenda { get; set; }
        public DateTime DataCadastro     { get; set; }
        public int IdEmpresaContratante { get; set; }
        public long IdComanda { get; set; }

        //Aux
        public string Servico        { get; set; }
        public string Cliente { get; set; }
        public string Profissional        { get; set; }
        public string HorarioInicial        { get; set; }
        public string HorarioFinal        { get; set; }
        public string Status        { get; set; }
        public int    IdProfissao   { get; set; }
        public decimal Valor { get; set; }


        static Agenda()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEspacoDeBeleza"].ConnectionString;
        }

        public Agenda(string dataAgenda, string idHoraInicial, string idHoraFinal, string idProf, string idServico, string idCliente)
        {
            DataAgenda = Convert.ToDateTime(dataAgenda);
            //IdAgenda = Convert.ToInt32();
            IdHorarioInicial = Convert.ToInt32(idHoraInicial);
            IdHorarioFinal = Convert.ToInt32(idHoraFinal);
            IdProfissional = Convert.ToInt32(idProf);
            IdServico = Convert.ToInt32(idServico);
            IdCliente = Convert.ToInt32(idCliente);
        }

        public Agenda(MySqlDataReader leitor)
        {
            DataAgenda = Convert.ToDateTime(leitor["dataAgenda"]);
            IdAgenda = Convert.ToInt32(leitor["idAgenda"]);
            IdHorarioInicial  = Convert.ToInt32(leitor["idHorarioInicial"]);
            IdHorarioFinal    = Convert.ToInt32(leitor["idHorarioFinal"]);
            IdProfissional    = Convert.ToInt32(leitor["idProfissional"]);
            IdServico = Convert.ToInt32(leitor["idServico"]);
            IdCliente = Convert.ToInt32(leitor["idCliente"]);
            DataCadastro = Convert.ToDateTime(leitor["dataCadastro"]);
            IdStatusAgenda = Convert.ToInt32(leitor["idStatusAgenda"]);
            IdEmpresaContratante = Convert.ToInt32(leitor["idEmpresaContratante"]);

            //Aux
            Status = leitor["statusAgenda"].ToString();
            Servico = leitor["servico"].ToString();
            Cliente = leitor["cliente"].ToString();
            Profissional = leitor["nome"].ToString();
            IdProfissao    = Convert.ToInt32(leitor["idProfissao"]);
            HorarioInicial = leitor["horaInicial"].ToString();
            HorarioFinal = leitor["horaFinial"].ToString();
            Valor = Convert.ToDecimal(leitor["valorServico"]);
        }

        
        public static bool Inserir(Agenda agenda)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO bdespacobeleza.tbl_agenda ");
                strQuery.Append("(dataAgenda ,idHorarioInicial, idHorarioFinal, idProfissional, idServico, idCliente, dataCadastro, idStatusAgenda) ");
                strQuery.Append("VALUES (@dataAgenda, @idHorarioInicial, @idHorarioFinal, @idProfissional, @idServico, @idCliente, @dataCadastro, 1) ");
                //idAgenda @idAgenda, 
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@dataAgenda", agenda.DataAgenda);
                    comando.Parameters.AddWithValue("@idHorarioInicial", agenda.IdHorarioInicial);
                    comando.Parameters.AddWithValue("@idHorarioFinal", agenda.IdHorarioFinal);
                    comando.Parameters.AddWithValue("@idProfissional", agenda.IdProfissional);
                    comando.Parameters.AddWithValue("@idServico", agenda.IdServico);
                    comando.Parameters.AddWithValue("@idCliente", agenda.IdCliente);
                    comando.Parameters.AddWithValue("@dataCadastro", DateTime.Now);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static int InserirRetornandoId(Agenda agenda)
        {
            int resultado = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO bdespacobeleza.tbl_agenda ");
                strQuery.Append("(dataAgenda ,idHorarioInicial, idHorarioFinal, idProfissional, idServico, idCliente, dataCadastro, idStatusAgenda) ");
                strQuery.Append("VALUES (@dataAgenda, @idHorarioInicial, @idHorarioFinal, @idProfissional, @idServico, @idCliente, @dataCadastro, 1); ");
                strQuery.Append("SELECT LAST_INSERT_ID(); ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@dataAgenda", agenda.DataAgenda);
                    comando.Parameters.AddWithValue("@idHorarioInicial", agenda.IdHorarioInicial);
                    comando.Parameters.AddWithValue("@idHorarioFinal", agenda.IdHorarioFinal);
                    comando.Parameters.AddWithValue("@idProfissional", agenda.IdProfissional);
                    comando.Parameters.AddWithValue("@idServico", agenda.IdServico);
                    comando.Parameters.AddWithValue("@idCliente", agenda.IdCliente);
                    comando.Parameters.AddWithValue("@dataCadastro", DateTime.Now);

                    resultado = Convert.ToInt32(comando.ExecuteScalar());
                    //var insert = comando.ExecuteNonQuery();
                    //resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool Editar(Agenda agenda)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE bdespacobeleza.tbl_agenda SET ");
                strQuery.Append("idHorarioInicial = @idHorarioInicial, idHorarioFinal = @idHorarioFinal ");
                strQuery.Append(", idProfissional = @idProfissional, idServico = @idServico, idCliente = @idCliente ");
                strQuery.Append(", dataAgenda = @dataAgenda  WHERE idAgenda = @idAgenda "); 
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idAgenda", agenda.IdAgenda);
                    comando.Parameters.AddWithValue("@dataAgenda", agenda.DataAgenda);
                    comando.Parameters.AddWithValue("@idHorarioInicial", agenda.IdHorarioInicial);
                    comando.Parameters.AddWithValue("@idHorarioFinal", agenda.IdHorarioFinal);
                    comando.Parameters.AddWithValue("@idProfissional", agenda.IdProfissional);
                    comando.Parameters.AddWithValue("@idServico", agenda.IdServico);
                    comando.Parameters.AddWithValue("@idCliente", agenda.IdCliente);
                    comando.Parameters.AddWithValue("@dataCadastro", DateTime.Now);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool EditarStatus(int idAgenda, int idStatus)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE bdespacobeleza.tbl_agenda SET ");
                strQuery.Append("idStatusAgenda = @idStatusAgenda ");
                strQuery.Append("WHERE idAgenda = @idAgenda "); 
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idAgenda", idAgenda);
                    comando.Parameters.AddWithValue("@idStatusAgenda", idStatus);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool EditarIdComanda(int idAgenda, long idComanda)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE bdespacobeleza.tbl_agenda SET ");
                strQuery.Append("idComanda = @idComanda ");
                strQuery.Append("WHERE idAgenda = @idAgenda "); 
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idAgenda", idAgenda);
                    comando.Parameters.AddWithValue("@idComanda", idComanda);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool Apagar(int idAgenda)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM bdespacobeleza.tbl_agenda ");
                strQuery.Append("WHERE idAgenda = @idAgenda "); 
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idAgenda", idAgenda);
                    var excluiu = comando.ExecuteNonQuery();
                    resultado = excluiu == 1;
                }
            }
            return resultado;
        }


        public static Agenda Pesquisar(int idAgenda)
        {
            Agenda agenda = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.vw_agenda ");
                strQuery.Append("WHERE idAgenda = @idAgenda ");
                //strQuery.Append("AND idEmpresaContratante = @idEmpContrat ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idAgenda", idAgenda);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    agenda = new Agenda(reader);
            }
            return agenda;
        }

    }
}
