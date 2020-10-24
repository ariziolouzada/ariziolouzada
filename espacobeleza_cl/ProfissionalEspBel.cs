using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace espacobeleza_cl
{
    public class ProfissionalEspBel
    {
        //Propriedades
        private static readonly string StringConnection;
        public int Id { get; set; }
        public int IdProfissao { get; set; }
        public int IdEmpresaContratante { get; set; }
        public int IdStatus { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        //Aux
        public string Profissao { get; set; }

        static ProfissionalEspBel()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEspacoDeBeleza"].ConnectionString;
        }

        public ProfissionalEspBel()
        {

        }

        public ProfissionalEspBel(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }


        public ProfissionalEspBel(int id, int idProf, string nome, int idEmpContrat)
        {
            Id = id;
            IdProfissao = idProf;
            IdEmpresaContratante = idEmpContrat;
            Nome = nome;
        }


        public ProfissionalEspBel(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["id"]);
            IdProfissao = Convert.ToInt32(leitor["idProfissao"]);
            IdEmpresaContratante = Convert.ToInt32(leitor["idEmpresaContratante"]);
            IdStatus = Convert.ToInt32(leitor["idStatus"]);
            Nome = leitor["nome"].ToString();
            Telefone = leitor["telefone"].ToString();
            Profissao = leitor["descricao_profissao"].ToString();
        }


        public static List<ProfissionalEspBel> Lista(bool comSelecione, int idEmpContrat)
        {
            var lista = new List<ProfissionalEspBel>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.vw_profissional ");
                strQuery.Append("WHERE idStatus = 1 ");
                strQuery.Append("AND idEmpresaContratante = @idEmpContrat ");

                strQuery.Append("ORDER BY nome ");

                if (comSelecione)
                    lista.Add(new ProfissionalEspBel(0, "Selecione..."));

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idEmpContrat", idEmpContrat);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new ProfissionalEspBel(reader));
            }
            return lista;
        }

        public static List<ProfissionalEspBel> Lista(bool comSelecione, int idProfissao, int idEmpContrat)
        {
            var lista = new List<ProfissionalEspBel>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.vw_profissional ");
                strQuery.Append("WHERE idStatus = 1 ");
                strQuery.Append("AND idEmpresaContratante = @idEmpContrat ");
                strQuery.Append("AND idProfissao = @idProfissao ");

                strQuery.Append("ORDER BY nome ");

                if (comSelecione)
                    lista.Add(new ProfissionalEspBel(0, "Selecione..."));

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idEmpContrat", idEmpContrat);
                comando.Parameters.AddWithValue("@idProfissao", idProfissao);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new ProfissionalEspBel(reader));
            }
            return lista;
        }


        public static List<ProfissionalEspBel> Lista(int idStatus, int idEmpContrat)
        {
            var lista = new List<ProfissionalEspBel>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.vw_profissional ");
                strQuery.Append("WHERE idEmpresaContratante = @idEmpContrat ");

                if (idStatus > 0)
                    strQuery.Append("AND idStatus = @idStatus ");

                strQuery.Append("ORDER BY nome ");


                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                comando.Parameters.AddWithValue("@idStatus", idStatus);
                comando.Parameters.AddWithValue("@idEmpContrat", idEmpContrat);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new ProfissionalEspBel(reader));
            }
            return lista;
        }


        public static List<ProfissionalEspBel> Lista(string nome, int idEmpContrat)
        {
            var lista = new List<ProfissionalEspBel>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.vw_profissional ");
                strQuery.Append("WHERE idEmpresaContratante = @idEmpContrat ");

                if (nome != string.Empty)
                    strQuery.Append("AND nome LIKE '%" + nome + "%' ");

                strQuery.Append("ORDER BY nome ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idEmpContrat", idEmpContrat);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new ProfissionalEspBel(reader));
            }
            return lista;
        }


        public static List<ProfissionalEspBel> Lista(string nome, int idEmpContrat, int idProfissao)
        {
            var lista = new List<ProfissionalEspBel>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.vw_profissional ");
                strQuery.Append("WHERE idEmpresaContratante = @idEmpContrat ");

                if(idProfissao > 0)
                    strQuery.Append("AND idProfissao = @idProfissao ");

                if (nome != string.Empty)
                    strQuery.Append("AND nome LIKE '%" + nome + "%' ");

                strQuery.Append("ORDER BY nome ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idEmpContrat", idEmpContrat);
                comando.Parameters.AddWithValue("@idProfissao", idProfissao);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new ProfissionalEspBel(reader));
            }
            return lista;
        }


        public static ProfissionalEspBel Pesquisar(int id)
        {
            ProfissionalEspBel usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.vw_profissional ");
                strQuery.Append("WHERE id = @id ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new ProfissionalEspBel(reader);
            }
            return usuario;
        }


        public static ProfissionalEspBel Pesquisar(string nome, int idEmpContrat)
        {
            ProfissionalEspBel usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.vw_profissional ");
                strQuery.Append("WHERE nome = @nome ");
                strQuery.Append("AND idEmpresaContratante = @idEmpContrat ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@nome", nome);
                comando.Parameters.AddWithValue("@idEmpContrat", idEmpContrat);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new ProfissionalEspBel(reader);
            }
            return usuario;
        }


        public static string PesquisaNome(int id)
        {
            string usuario = string.Empty;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.tbl_profissional ");
                strQuery.Append("WHERE id = @id ");
                //strQuery.Append("AND idEmpresaContratante = @idEmpContrat ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = reader["nome"].ToString();
            }
            return usuario;
        }


        public static bool Inserir(ProfissionalEspBel ProfissionalEspBel)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO bdespacobeleza.tbl_profissional ");
                strQuery.Append("( idStatus, nome, telefone, idEmpresaContratante, idProfissao ) ");
                strQuery.Append("VALUES ( 1, @nome, @telefone, @idEmpresaContratante, @idProfissao ) ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@telefone", ProfissionalEspBel.Telefone);
                    comando.Parameters.AddWithValue("@nome", ProfissionalEspBel.Nome);
                    comando.Parameters.AddWithValue("@idEmpresaContratante", ProfissionalEspBel.IdEmpresaContratante);
                    comando.Parameters.AddWithValue("@idProfissao", ProfissionalEspBel.IdProfissao);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static int InserirNomeRetornndoId(string nome, int idEmpContrat, int idProfissao)
        {
            int resultado = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO bdespacobeleza.tbl_profissional ");
                strQuery.Append("( idStatus, nome, idEmpresaContratante, idProfissional) ");
                strQuery.Append("VALUES ( 1, @Nome, @idEmpresaContratante, @idProfissional); ");
                strQuery.Append("SELECT LAST_INSERT_ID(); ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@Nome", nome);
                    comando.Parameters.AddWithValue("@idEmpresaContratante", idEmpContrat);
                    comando.Parameters.AddWithValue("@idProfissional", idProfissao);

                    resultado = Convert.ToInt32(comando.ExecuteScalar());

                    //var insert = comando.ExecuteNonQuery();
                    //if(insert == 1)
                    //  resultado = comando.LastInsertedId;

                }
            }
            return resultado;
        }


        public static bool Editar(ProfissionalEspBel ProfissionalEspBel)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE bdespacobeleza.tbl_profissional SET ");
                strQuery.Append(" Nome = @Nome, idProfissional = @idProfissional ");
                strQuery.Append(", idStatus = @idStatus ");
                strQuery.Append(", telefone = @telefone ");
                strQuery.Append("WHERE id = @id ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@id", ProfissionalEspBel.Id);
                    comando.Parameters.AddWithValue("@idStatus", ProfissionalEspBel.IdStatus);
                    comando.Parameters.AddWithValue("@Nome", ProfissionalEspBel.Nome);
                    comando.Parameters.AddWithValue("@telefone", ProfissionalEspBel.Telefone);
                    comando.Parameters.AddWithValue("@idProfissional", ProfissionalEspBel.IdProfissao);

                    var editou = comando.ExecuteNonQuery();
                    resultado = editou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


    }
}
