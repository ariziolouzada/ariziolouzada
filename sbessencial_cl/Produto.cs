using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace sbessencial_cl
{
    public class Produto
    {
        //Propriedades
        private static readonly string StringConnection;
        public int Id { get; set; }
        public int IdStatus { get; set; }
        public string Descricao { get; set; }
        public string DescricaoCompleta { get; set; }
        public int QtdEstoque { get; set; }
        public decimal PrecoCusto { get; set; }
        public decimal PrecoVenda { get; set; }


        static Produto()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdSbEssencial"].ConnectionString;
        }


        public Produto(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }


        public Produto(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["idProduto"]);
            IdStatus = Convert.ToInt32(leitor["idStatus"]);
            Descricao = leitor["descricao"].ToString();
            DescricaoCompleta = leitor["descCompleta"].ToString();
            QtdEstoque = Convert.ToInt32(leitor["qtdEstoque"]);
            PrecoCusto = Convert.ToDecimal(leitor["precoCusto"]);
            PrecoVenda = Convert.ToDecimal(leitor["precoVenda"]);
        }

        public Produto() { }

        //SELECT idProduto, idStatus, descricao, qtdEstoque, precoCusto, precoVenda
        //FROM sbessencial.tbl_produto;

        public static List<Produto> Lista(bool comSelecione, string descricao, int idStatus)
        {
            var lista = new List<Produto>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_produto ");

                strQuery.Append("WHERE descricao LIKE '%" + descricao + "%' ");

                if (idStatus > 0)
                    strQuery.Append("AND idStatus = @idStatus ");


                strQuery.Append("ORDER BY descricao ");

                if (comSelecione)
                    lista.Add(new Produto(0, "Selecione..."));

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                if (idStatus > 0)
                    comando.Parameters.AddWithValue("@idStatus", idStatus);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new Produto(reader));
            }
            return lista;
        }


        public static Produto Pesquisar(int id)
        {
            Produto usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_produto ");
                strQuery.Append("WHERE idProduto = @id");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new Produto(reader);
            }
            return usuario;
        }


        public static int PesquisaQdeEstoqque(int id)
        {
            var qtde = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_produto ");
                strQuery.Append("WHERE idProduto = @id");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    qtde = Convert.ToInt32(reader["qtdEstoque"]);
            }
            return qtde;
        }

        public static Produto Pesquisar(string descricao)
        {
            Produto usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_produto ");
                strQuery.Append("WHERE descricao = @descricao");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@descricao", descricao);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new Produto(reader);
            }
            return usuario;
        }


        public static bool Inserir(Produto pdto)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO sbessencial.tbl_produto ");
                strQuery.Append("(idStatus, descricao, qtdEstoque, precoCusto, precoVenda, descCompleta ) ");
                strQuery.Append("VALUES ( 1, @descricao, @qtdEstoque, @precoCusto, @precoVenda, @descCompleta ) ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    //comando.Parameters.AddWithValue("@idProduto", Produto.Id);
                    comando.Parameters.AddWithValue("@descricao", pdto.Descricao);
                    comando.Parameters.AddWithValue("@qtdEstoque", pdto.QtdEstoque);
                    comando.Parameters.AddWithValue("@precoCusto", pdto.PrecoCusto);
                    comando.Parameters.AddWithValue("@precoVenda", pdto.PrecoVenda);
                    comando.Parameters.AddWithValue("@descCompleta", pdto.DescricaoCompleta);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }

        public static bool Editar(Produto pdto)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE sbessencial.tbl_produto SET ");
                strQuery.Append(" descricao = @descricao, qtdEstoque = @qtdEstoque ");
                strQuery.Append(", idStatus = @idStatus, qtdEstoque = @qtdEstoque ");
                strQuery.Append(", precoCusto = @precoCusto, precoVenda = @precoVenda ");
                strQuery.Append(", descCompleta = @descCompleta ");
                strQuery.Append("WHERE idProduto = @idProduto ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idProduto", pdto.Id);
                    comando.Parameters.AddWithValue("@idStatus", pdto.IdStatus);
                    comando.Parameters.AddWithValue("@descricao", pdto.Descricao);
                    comando.Parameters.AddWithValue("@qtdEstoque", pdto.QtdEstoque);
                    comando.Parameters.AddWithValue("@precoCusto", pdto.PrecoCusto);
                    comando.Parameters.AddWithValue("@precoVenda", pdto.PrecoVenda);
                    comando.Parameters.AddWithValue("@descCompleta", pdto.DescricaoCompleta);

                    var editou = comando.ExecuteNonQuery();
                    resultado = editou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }



        public static bool EditarEstoque(int idPdto, int qtde)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE sbessencial.tbl_produto SET ");
                strQuery.Append(" qtdEstoque = @qtdEstoque ");
                strQuery.Append("WHERE idProduto = @idProduto ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idProduto", idPdto);
                    comando.Parameters.AddWithValue("@qtdEstoque", qtde);

                    var editou = comando.ExecuteNonQuery();
                    resultado = editou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }

        public static bool AtualizaEstoque(int idPdto, int qtde)
        {
            var qtdEatoqueAtual = PesquisaQdeEstoqque(idPdto);
            var qtdNovaEstoque = qtdEatoqueAtual - qtde;
            return EditarEstoque(idPdto, qtdNovaEstoque);
        }

    }
}
