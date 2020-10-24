using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace espacobeleza_cl
{
    public class AgendaHorario
    {
        //Propriedades
        private static readonly string StringConnection;

        public int Id { get; set; }
        public string Descricao { get; set; }

        static AgendaHorario()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEspacoDeBeleza"].ConnectionString;
        }


        public AgendaHorario(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["id_agenda_horario"]);
            Descricao = leitor["descricao_agenda_horario"].ToString();
        }

        public AgendaHorario(int id, string desc)
        {
            Id = id;
            Descricao = desc;
        }

        public static List<AgendaHorario> Lista(bool comSelecione)
        {
            var lista = new List<AgendaHorario>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.tbl_agenda_horario ");
                //strQuery.Append("WHERE idStatus = 1 ");

                //strQuery.Append("ORDER BY nome ");

                if (comSelecione)
                    lista.Add(new AgendaHorario(0, "Selecione..."));

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new AgendaHorario(reader));
            }
            return lista;
        }


        public static AgendaHorario Pesquisa(int id)
        {
            AgendaHorario lista = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.tbl_agenda_horario ");
                strQuery.Append("WHERE id_agenda_horario = @id ");
                
                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                   lista = new AgendaHorario(reader);
            }
            return lista;
        }

    }
}
