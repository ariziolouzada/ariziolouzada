using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace sbessencial_cl
{
    public class Servico
    {
        //Propriedades
        private static readonly string StringConnection;
        public int Id { get; set; }
        public int IdStatus { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }

        //Auxiliar
        public string NomeProfissional { get; set; }



        static Servico()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdSbEssencial"].ConnectionString;
        }


        public Servico(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["idservico"]);
            IdStatus = Convert.ToInt32(leitor["idStatus"]);
            Descricao = leitor["descricao"].ToString();
            Valor = Convert.ToDecimal(leitor["valor"]);
        }


        public Servico(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }

        public Servico(string descricao, decimal valor)
        {
            Valor = valor;
            Descricao = descricao;
        }


        public static List<Servico> Lista(bool comSelecione, string descricao, int idStatus)
        {
            var lista = new List<Servico>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_servico ");
                strQuery.Append("WHERE descricao LIKE '%" + descricao + "%' ");

                if (idStatus > 0)
                    strQuery.Append("AND idStatus = @idStatus ");

                strQuery.Append("ORDER BY descricao ");

                if (comSelecione)
                    lista.Add(new Servico(0, "Selecione..."));

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                if (idStatus > 0)
                    comando.Parameters.AddWithValue("@idStatus", idStatus);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new Servico(reader));
            }
            return lista;
        }

        public static List<Servico> CarregaTabelaServico(bool comSelecione, string descricao, int idStatus)
        {
            var lista = new List<Servico>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_servico ");
                strQuery.Append("WHERE descricao LIKE '%" + descricao + "%' ");
                if (idStatus != 3 && idStatus > 0)
                    strQuery.Append("AND idStatus < 3 ");

                if (idStatus > 0)
                    strQuery.Append("AND idStatus = @idStatus ");

                strQuery.Append("ORDER BY descricao ");

                if (comSelecione)
                    lista.Add(new Servico(0, "Selecione..."));

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                if (idStatus > 0)
                    comando.Parameters.AddWithValue("@idStatus", idStatus);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new Servico(reader));
            }
            return lista;
        }


        public static List<Servico> ListaServicoProfissional(bool comSelecione, int idProfissional, int idStatus)
        {
            var lista = new List<Servico>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT sp.*, s.descricao, s.valor, s.idStatus, p.nome ");
                strQuery.Append("FROM sbessencial.tbl_profissional_servico AS sp ");
                strQuery.Append("JOIN sbessencial.tbl_servico AS s ON sp.idServico = s.idServico ");
                strQuery.Append("JOIN sbessencial.tbl_profissional AS p ON sp.idProfissional = p.id ");
                strQuery.Append("WHERE p.id = @idProfissional ");

                if (idStatus > 0)
                    strQuery.Append("AND s.idStatus = @idStatus ");

                strQuery.Append("ORDER BY descricao ");

                if (comSelecione)
                    lista.Add(new Servico(0, "Selecione..."));

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                comando.Parameters.AddWithValue("@idProfissional", idProfissional);

                if (idStatus > 0)
                    comando.Parameters.AddWithValue("@idStatus", idStatus);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    var svc = new Servico(reader);
                    svc.NomeProfissional = reader["nome"].ToString();
                    lista.Add(svc);
                }
            }
            return lista;
        }


        public static List<Servico> ListaServicoProfissional(bool comSelecione, int idProfissional, int idStatus, string filtro)
        {
            var lista = new List<Servico>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT sp.*, s.descricao, s.valor, s.idStatus, p.nome ");
                strQuery.Append("FROM sbessencial.tbl_profissional_servico AS sp ");
                strQuery.Append("JOIN sbessencial.tbl_servico AS s ON sp.idServico = s.idServico ");
                strQuery.Append("JOIN sbessencial.tbl_profissional AS p ON sp.idProfissional = p.id ");
                strQuery.Append("WHERE p.id = @idProfissional ");

                if (!filtro.Equals(""))
                    strQuery.Append("AND p.nome LIKE '%" + filtro + "%' ");

                if (idStatus > 0)
                    strQuery.Append("AND s.idStatus = @idStatus ");

                strQuery.Append("ORDER BY descricao ");

                if (comSelecione)
                    lista.Add(new Servico(0, "Selecione..."));

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                comando.Parameters.AddWithValue("@idProfissional", idProfissional);

                if (idStatus > 0)
                    comando.Parameters.AddWithValue("@idStatus", idStatus);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    var svc = new Servico(reader);
                    svc.NomeProfissional = reader["nome"].ToString();
                    lista.Add(svc);
                }
            }
            return lista;
        }


        public static List<Servico> ListaServicoNaoSelecionadoProfissional(int idProfissional)
        {
            var lista = new List<Servico>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * FROM tbl_servico ");
                strQuery.Append("WHERE  idServico NOT IN ( ");
                strQuery.Append("SELECT idServico FROM tbl_profissional_servico  ");
                strQuery.Append("WHERE  idProfissional = @idProfissional ) ");
                strQuery.Append("AND idStatus = 1 ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                comando.Parameters.AddWithValue("@idProfissional", idProfissional);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    var svc = new Servico(reader);
                    lista.Add(svc);
                }
            }
            return lista;
        }


        public static Servico Pesquisar(int id)
        {
            Servico usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_servico ");
                strQuery.Append("WHERE idServico = @id");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new Servico(reader);
            }
            return usuario;
        }


        public static Servico Pesquisar(string descricao)
        {
            Servico usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_servico ");
                strQuery.Append("WHERE descricao = @descricao");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@descricao", descricao);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new Servico(reader);
            }
            return usuario;
        }


        public static bool Inserir(Servico srv)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO sbessencial.tbl_servico ");
                strQuery.Append("(idStatus, descricao, valor ) ");
                strQuery.Append("VALUES ( 1, @descricao, @valor ) ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    //comando.Parameters.AddWithValue("@idServico", Servico.Id);
                    comando.Parameters.AddWithValue("@descricao", srv.Descricao);
                    comando.Parameters.AddWithValue("@valor", srv.Valor);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool Editar(Servico srv)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE sbessencial.tbl_servico SET ");
                strQuery.Append("  descricao = @descricao ");
                strQuery.Append(", valor = @valor, idStatus = @idStatus ");
                strQuery.Append("WHERE idServico = @idServico ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idServico", srv.Id);
                    comando.Parameters.AddWithValue("@descricao", srv.Descricao);
                    comando.Parameters.AddWithValue("@valor", srv.Valor);
                    comando.Parameters.AddWithValue("@idStatus", srv.IdStatus);

                    var editou = comando.ExecuteNonQuery();
                    resultado = editou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool EditarStatus(int idServico, int idStatus)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE sbessencial.tbl_servico SET ");
                strQuery.Append("idStatus = @idStatus ");
                strQuery.Append("WHERE idServico = @idServico ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idServico", idServico);
                    comando.Parameters.AddWithValue("@idStatus", idStatus);

                    var editou = comando.ExecuteNonQuery();
                    resultado = editou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }



    }
}
