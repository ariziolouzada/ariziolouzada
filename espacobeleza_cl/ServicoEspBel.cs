using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace espacobeleza_cl
{
    public class ServicoEspBel
    {
        //Propriedades
        private static readonly string StringConnection;
        public int Id { get; set; }
        public int IdStatus { get; set; }
        public int IdEmpresaContratante { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }

        //Auxiliar
        public string NomeProfissional { get; set; }


        static ServicoEspBel()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEspacoDeBeleza"].ConnectionString;
        }


        public ServicoEspBel(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["idservico"]);
            IdStatus = Convert.ToInt32(leitor["idStatus"]);
            IdEmpresaContratante = Convert.ToInt32(leitor["idEmpresaContratante"]);
            Descricao = leitor["descricao"].ToString();
            Valor = Convert.ToDecimal(leitor["valor"]);
        }


        public ServicoEspBel(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }

        public ServicoEspBel(int id, string descricao, int idEmpContrat)
        {
            Id = id;
            IdEmpresaContratante = idEmpContrat;
            Descricao = descricao;
        }

        public ServicoEspBel(string descricao, decimal valor)
        {
            Valor = valor;
            Descricao = descricao;
        }


        public static List<ServicoEspBel> Lista(bool comSelecione, string descricao, int idStatus, int idEmpContrat)
        {
            var lista = new List<ServicoEspBel>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.tbl_servico ");
                strQuery.Append("WHERE idEmpresaContratante = @idEmpContrat ");

                if(descricao != string.Empty)
                    strQuery.Append("AND descricao LIKE '%" + descricao + "%' ");

                if (idStatus > 0)
                    strQuery.Append("AND idStatus = @idStatus ");

                strQuery.Append("ORDER BY descricao ");

                if (comSelecione)
                    lista.Add(new ServicoEspBel(0, "Selecione..."));

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                comando.Parameters.AddWithValue("@idEmpContrat", idEmpContrat);

                if (idStatus > 0)
                    comando.Parameters.AddWithValue("@idStatus", idStatus);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new ServicoEspBel(reader));
            }
            return lista;
        }


        public static List<ServicoEspBel> ListaServicoProfissional(bool comSelecione, int idProfissional, int idStatus, int idEmpContrat)
        {
            var lista = new List<ServicoEspBel>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT sp.*, s.descricao, s.valor, s.idStatus, p.nome, p.idEmpresaContratante ");
                strQuery.Append("FROM bdespacobeleza.tbl_profissional_servico AS sp ");
                strQuery.Append("JOIN bdespacobeleza.tbl_servico AS s ON sp.idServico = s.idServico ");
                strQuery.Append("JOIN bdespacobeleza.tbl_profissional AS p ON sp.idProfissional = p.id ");
                strQuery.Append("WHERE p.id = @idProfissional ");
                strQuery.Append("AND p.idEmpresaContratante = @idEmpContrat ");

                if (idStatus > 0)
                    strQuery.Append("AND s.idStatus = @idStatus ");

                strQuery.Append("ORDER BY descricao ");

                if (comSelecione)
                    lista.Add(new ServicoEspBel(0, "Selecione..."));
                //erro
                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                comando.Parameters.AddWithValue("@idEmpContrat", idEmpContrat);
                comando.Parameters.AddWithValue("@idProfissional", idProfissional);

                if (idStatus > 0)
                    comando.Parameters.AddWithValue("@idStatus", idStatus);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    var svc = new ServicoEspBel(reader);
                    svc.NomeProfissional = reader["nome"].ToString();
                    lista.Add(svc);
                }
            }
            return lista;
        }


        public static List<ServicoEspBel> ListaServicoProfissional(bool comSelecione, int idProfissional, int idStatus, string filtro, int idEmpContrat)
        {
            var lista = new List<ServicoEspBel>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT sp.*, s.descricao, s.valor, s.idStatus, p.nome, p.idEmpresaContratante ");
                strQuery.Append("FROM bdespacobeleza.tbl_profissional_servico AS sp ");
                strQuery.Append("JOIN bdespacobeleza.tbl_servico AS s ON sp.idServico = s.idServico ");
                strQuery.Append("JOIN bdespacobeleza.tbl_profissional AS p ON sp.idProfissional = p.id ");
                strQuery.Append("WHERE p.id = @idProfissional ");
                strQuery.Append("AND p.idEmpresaContratante = @idEmpContrat ");

                if (!filtro.Equals(""))
                    strQuery.Append("AND p.nome LIKE '%" + filtro + "%' ");

                if (idStatus > 0)
                    strQuery.Append("AND s.idStatus = @idStatus ");

                strQuery.Append("ORDER BY descricao ");

                if (comSelecione)
                    lista.Add(new ServicoEspBel(0, "Selecione..."));

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                comando.Parameters.AddWithValue("@idProfissional", idProfissional);
                comando.Parameters.AddWithValue("@idEmpContrat", idEmpContrat);

                if (idStatus > 0)
                    comando.Parameters.AddWithValue("@idStatus", idStatus);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    var svc = new ServicoEspBel(reader);
                    svc.NomeProfissional = reader["nome"].ToString();
                    lista.Add(svc);
                }
            }
            return lista;
        }


        public static List<ServicoEspBel> ListaServicoNaoSelecionadoProfissional(int idProfissional)
        {
            var lista = new List<ServicoEspBel>();
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
                    var svc = new ServicoEspBel(reader);
                    lista.Add(svc);
                }
            }
            return lista;
        }


        public static ServicoEspBel Pesquisar(int id)
        {
            ServicoEspBel usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.tbl_servico ");
                strQuery.Append("WHERE idServico = @id");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new ServicoEspBel(reader);
            }
            return usuario;
        }

        public static decimal PesquisarValor(int id)
        {
            decimal valor = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.tbl_servico ");
                strQuery.Append("WHERE idServico = @id");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    valor = Convert.ToDecimal(reader["valor"]);
            }
            return valor;
        }


        public static ServicoEspBel Pesquisar(string descricao, int idEmpContrat)
        {
            ServicoEspBel usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.tbl_servico ");
                strQuery.Append("WHERE descricao = @descricao ");
                strQuery.Append("AND idEmpresaContratante = @idEmpContrat ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@descricao", descricao);
                comando.Parameters.AddWithValue("@idEmpContrat", idEmpContrat);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new ServicoEspBel(reader);
            }
            return usuario;
        }


        public static bool Inserir(ServicoEspBel srv)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO bdespacobeleza.tbl_servico ");
                strQuery.Append("(idStatus, descricao, valor, idEmpresaContratante ) ");
                strQuery.Append("VALUES ( 1, @descricao, @valor, @idEmpresaContratante ) ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    //comando.Parameters.AddWithValue("@idServico", Servico.Id);
                    comando.Parameters.AddWithValue("@descricao", srv.Descricao);
                    comando.Parameters.AddWithValue("@valor", srv.Valor);
                    comando.Parameters.AddWithValue("@idEmpresaContratante", srv.IdEmpresaContratante);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool Editar(ServicoEspBel srv)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE bdespacobeleza.tbl_servico SET ");
                strQuery.Append("  descricao = @descricao ");
                strQuery.Append(", valor = @valor ");
                strQuery.Append("WHERE idServico = @idServico ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idServico", srv.Id);
                    comando.Parameters.AddWithValue("@descricao", srv.Descricao);
                    comando.Parameters.AddWithValue("@valor", srv.Valor);

                    var editou = comando.ExecuteNonQuery();
                    resultado = editou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }



    }
}
