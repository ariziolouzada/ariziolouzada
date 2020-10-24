using CriptografiaSgpm;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace espacobeleza_cl
{
  public  class ProdutoEspBel
    {
        //Propriedades
        private static readonly string StringConnection;
        public int Id { get; set; }
        public int IdStatus { get; set; }
        public int IdEmpresaContratante { get; set; }
        public string Descricao { get; set; }
        public string DescricaoCompleta { get; set; }
        public int QtdEstoque { get; set; }
        public decimal PrecoCusto { get; set; }
        public decimal PrecoVenda { get; set; }


        static ProdutoEspBel()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEspacoDeBeleza"].ConnectionString;
        }


        public ProdutoEspBel(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }


        public ProdutoEspBel(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["idProduto"]);
            IdStatus = Convert.ToInt32(leitor["idStatus"]);
            IdEmpresaContratante = Convert.ToInt32(leitor["idEmpresaContratante"]);
            Descricao = leitor["descricao"].ToString();
            DescricaoCompleta = leitor["descCompleta"].ToString();
            QtdEstoque = Convert.ToInt32(leitor["qtdEstoque"]);
            PrecoCusto = Convert.ToDecimal(leitor["precoCusto"]);
            PrecoVenda = Convert.ToDecimal(leitor["precoVenda"]);
        }

        public ProdutoEspBel() { }

        //SELECT idProduto, idStatus, descricao, qtdEstoque, precoCusto, precoVenda
        //FROM bdespacobeleza.tbl_produto;

        public static List<ProdutoEspBel> Lista(bool comSelecione, int idStatus)
        {
            var lista = new List<ProdutoEspBel>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.tbl_produto ");

                if (idStatus > 0)
                    strQuery.Append("WHERE idStatus = @idStatus ");

                strQuery.Append("ORDER BY descricao ");

                if (comSelecione)
                    lista.Add(new ProdutoEspBel(0, "Selecione..."));

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                if (idStatus > 0)
                    comando.Parameters.AddWithValue("@idStatus", idStatus);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new ProdutoEspBel(reader));
            }
            return lista;
        }

        public static List<ProdutoEspBel> Lista(bool comSelecione, string descricao, int idStatus)
        {
            var lista = new List<ProdutoEspBel>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.tbl_produto ");

                strQuery.Append("WHERE descricao LIKE '%" + descricao + "%' ");

                if (idStatus > 0)
                    strQuery.Append("AND idStatus = @idStatus ");


                strQuery.Append("ORDER BY descricao ");

                if (comSelecione)
                    lista.Add(new ProdutoEspBel(0, "Selecione..."));

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                if (idStatus > 0)
                    comando.Parameters.AddWithValue("@idStatus", idStatus);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new ProdutoEspBel(reader));
            }
            return lista;
        }


        public static ProdutoEspBel Pesquisar(int id)
        {
            ProdutoEspBel usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.tbl_produto ");
                strQuery.Append("WHERE idProduto = @id");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new ProdutoEspBel(reader);
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
                strQuery.Append("FROM bdespacobeleza.tbl_produto ");
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

        public static ProdutoEspBel Pesquisar(string descricao)
        {
            ProdutoEspBel usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.tbl_produto ");
                strQuery.Append("WHERE descricao = @descricao");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@descricao", descricao);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new ProdutoEspBel(reader);
            }
            return usuario;
        }


        public static bool Inserir(ProdutoEspBel pdto)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO bdespacobeleza.tbl_produto ");
                strQuery.Append("(idStatus, idEmpresaContratante, descricao, qtdEstoque, precoCusto, precoVenda, descCompleta ) ");
                strQuery.Append("VALUES ( 1, @idEmpresaContratante, @descricao, @qtdEstoque, @precoCusto, @precoVenda, @descCompleta ) ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idEmpresaContratante", pdto.IdEmpresaContratante);
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

        public static bool Editar(ProdutoEspBel pdto)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE bdespacobeleza.tbl_produto SET ");
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
                strQuery.Append("UPDATE bdespacobeleza.tbl_produto SET ");
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


        public static decimal PesquisarValor(int id)
        {
            decimal valor = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.tbl_produto ");
                strQuery.Append("WHERE idProduto = @id");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    valor = Convert.ToDecimal(reader["precoVenda"]);
            }
            return valor;
        }


    }
}
