using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ferrita_Cl
{
   public class Fornecedor
    {
        private static readonly string stringConnection;

        public int      IdFornecedor        { get; set; }
        public string   NomeFornecedor      { get; set; }
        public string   CnpjFornecedor      { get; set; }
        public string   Telefone            { get; set; }
        public int      IdStatusFornecedor  { get; set; }


        static Fornecedor()
        {
            stringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEpcVrd"].ConnectionString;
        }

        public Fornecedor() { }

        public Fornecedor(MySqlDataReader leitor)
        {
            IdFornecedor = Convert.ToInt32(leitor["ID_FORNECEDOR"]);
            NomeFornecedor = leitor["NOME_FORNECEDOR"].ToString();
            Telefone = leitor["TELEFONE"].ToString();

            if (leitor["CNPJ_FORNECEDOR"] != DBNull.Value)
                CnpjFornecedor = leitor["CNPJ_FORNECEDOR"].ToString();

            IdStatusFornecedor = Convert.ToInt32(leitor["ID_STATUS_FORNECEDOR"]);
        }


        public Fornecedor(string NomeFornecedor, string CnpjFornecedor, int IdStatusFornecedor)
        {
            this.NomeFornecedor = NomeFornecedor;
            this.CnpjFornecedor = CnpjFornecedor;
            this.IdStatusFornecedor = IdStatusFornecedor;
        }


        public Fornecedor(int IdFornecedor, string NomeFornecedor, string CnpjFornecedor, int IdStatusFornecedor)
        {
            this.IdFornecedor = IdFornecedor;
            this.NomeFornecedor = NomeFornecedor;
            this.CnpjFornecedor = CnpjFornecedor;
            this.IdStatusFornecedor = IdStatusFornecedor;
        }

        public Fornecedor(int IdFornecedor, string NomeFornecedor)
        {
            this.IdFornecedor = IdFornecedor;
            this.NomeFornecedor = NomeFornecedor;
        }


        public static List<Fornecedor> ListaFornecedores()
        {
            List<Fornecedor> resultado = new List<Fornecedor>();
            using (var conexao = new MySqlConnection(stringConnection))
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * ");
                sql.AppendLine("FROM tbl_ferrita_fornecedor ");

                using (var comando = new MySqlCommand(sql.ToString(), conexao))
                {
                    using (conexao)
                    {
                        conexao.Open();//Abrir a conexão com o BD
                        //comando.Parameters.AddWithValue(@"DESC_TIPO_SERV_PROD", pDescServProd);
                        MySqlDataReader drEvento = comando.ExecuteReader();
                        while (drEvento.Read())
                        {
                            resultado.Add(new Fornecedor(drEvento));
                        }
                    }
                }
            }
            return resultado;
        }


        public static List<Fornecedor> ListaFornecedores(bool comSelecione)
        {
            List<Fornecedor> resultado = new List<Fornecedor>();
            using (var conexao = new MySqlConnection(stringConnection))
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * ");
                sql.AppendLine("FROM tbl_ferrita_fornecedor ");

                if (comSelecione)
                    resultado.Add(new Fornecedor(0, "Selecione..."));

                using (var comando = new MySqlCommand(sql.ToString(), conexao))
                {
                    using (conexao)
                    {
                        conexao.Open();//Abrir a conexão com o BD
                        //comando.Parameters.AddWithValue(@"DESC_TIPO_SERV_PROD", pDescServProd);
                        MySqlDataReader drEvento = comando.ExecuteReader();
                        while (drEvento.Read())
                        {
                            resultado.Add(new Fornecedor(drEvento));
                        }
                    }
                }
            }
            return resultado;
        }


        public static Fornecedor PesquisaFornecedorPeloId(int pIdFornecedor)
        {
            Fornecedor resultado = null;
            using (var conexao = new MySqlConnection(stringConnection))
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * ");
                sql.AppendLine("FROM tbl_ferrita_fornecedor ");
                sql.AppendLine("WHERE ID_FORNECEDOR = @ID_FORNECEDOR");

                using (var comando = new MySqlCommand(sql.ToString(), conexao))
                {
                    using (conexao)
                    {
                        conexao.Open();//Abrir a conexão com o BD
                        comando.Parameters.AddWithValue("@ID_FORNECEDOR", pIdFornecedor);
                        MySqlDataReader drEvento = comando.ExecuteReader();
                        while (drEvento.Read())
                        {
                            resultado = new Fornecedor(drEvento);
                        }
                    }
                }
            }
            return resultado;

        }


        public static Fornecedor PesquisaFornecedorPeloNome(string pNomeFornecedor)
        {
            Fornecedor resultado = null;
            using (var conexao = new MySqlConnection(stringConnection))
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * ");
                sql.AppendLine("FROM tbl_ferrita_fornecedor ");
                sql.AppendLine("WHERE NOME_FORNECEDOR = @NOME_FORNECEDOR");

                using (var comando = new MySqlCommand(sql.ToString(), conexao))
                {
                    using (conexao)
                    {
                        conexao.Open();//Abrir a conexão com o BD
                        comando.Parameters.AddWithValue("@NOME_FORNECEDOR", pNomeFornecedor);
                        MySqlDataReader drEvento = comando.ExecuteReader();
                        while (drEvento.Read())
                        {
                            resultado = new Fornecedor(drEvento);
                        }
                    }
                }
            }
            return resultado;
        }


        public static Fornecedor PesquisaFornecedorPeloCnpj(string pCnpjFornecedor)
        {
            Fornecedor resultado = null;
            using (var conexao = new MySqlConnection(stringConnection))
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * ");
                sql.AppendLine("FROM tbl_ferrita_fornecedor ");
                sql.AppendLine("WHERE CNPJ_FORNECEDOR = @CNPJ_FORNECEDOR");

                using (var comando = new MySqlCommand(sql.ToString(), conexao))
                {
                    using (conexao)
                    {
                        conexao.Open();//Abrir a conexão com o BD
                        comando.Parameters.AddWithValue("@CNPJ_FORNECEDOR", pCnpjFornecedor);
                        MySqlDataReader drEvento = comando.ExecuteReader();
                        while (drEvento.Read())
                        {
                            resultado = new Fornecedor(drEvento);
                        }
                    }
                }
            }
            return resultado;
        }


        public static int PesquisaIdFornecedor(string pNomeFornecedor)
        {
            int resultado = 0;
            using (var conexao = new MySqlConnection(stringConnection))
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * ");
                sql.AppendLine("FROM tbl_ferrita_fornecedor ");
                sql.AppendLine("WHERE NOME_FORNECEDOR = @NOME_FORNECEDOR");

                using (var comando = new MySqlCommand(sql.ToString(), conexao))
                {
                    using (conexao)
                    {
                        conexao.Open();//Abrir a conexão com o BD
                        comando.Parameters.AddWithValue("@NOME_FORNECEDOR", pNomeFornecedor);
                        MySqlDataReader drEvento = comando.ExecuteReader();
                        while (drEvento.Read())
                        {
                            resultado = Convert.ToInt32(drEvento["ID_FORNECEDOR"]);
                        }
                    }
                }
            }
            return resultado;
        }

        public static string PesquisaNomeFornecedor(int pIdFornecedor)
        {
            string resultado = string.Empty;
            using (var conexao = new MySqlConnection(stringConnection))
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * ");
                sql.AppendLine("FROM tbl_ferrita_fornecedor ");
                sql.AppendLine("WHERE ID_FORNECEDOR = @ID_FORNECEDOR");

                using (var comando = new MySqlCommand(sql.ToString(), conexao))
                {
                    using (conexao)
                    {
                        conexao.Open();//Abrir a conexão com o BD
                        comando.Parameters.AddWithValue("@ID_FORNECEDOR", pIdFornecedor);
                        MySqlDataReader drEvento = comando.ExecuteReader();
                        while (drEvento.Read())
                        {
                            resultado = drEvento["NOME_FORNECEDOR"].ToString();
                        }
                    }
                }
            }
            return resultado;
        }


        public static bool InserirFornecedor(Fornecedor pFncdr)
        {
            bool ok = false;
            using (var conexao = new MySqlConnection(stringConnection))
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine("INSERT INTO tbl_ferrita_fornecedor ");
                sql.AppendLine("(NOME_FORNECEDOR, CNPJ_FORNECEDOR, ID_STATUS_FORNECEDOR, TELEFONE) ");
                sql.AppendLine("VALUES (@NOME_FORNECEDOR, @CNPJ_FORNECEDOR, @ID_STATUS_FORNECEDOR, @TELEFONE )");

                using (var comando = new MySqlCommand(sql.ToString(), conexao))
                {
                    using (conexao)
                    {
                        conexao.Open();
                        comando.Parameters.AddWithValue("@NOME_FORNECEDOR", pFncdr.NomeFornecedor);
                        comando.Parameters.AddWithValue("@CNPJ_FORNECEDOR", pFncdr.CnpjFornecedor);
                        comando.Parameters.AddWithValue("@ID_STATUS_FORNECEDOR", pFncdr.IdStatusFornecedor);
                        comando.Parameters.AddWithValue("@TELEFONE", pFncdr.Telefone);
                        var inseriu = comando.ExecuteNonQuery();
                        if (inseriu == 1)
                            ok = true;
                    }
                }
            }
            return ok;
        }


        public static bool UpdateFornecedor(Fornecedor pFncdr)
        {
            bool ok = false;
            using (var conexao = new MySqlConnection(stringConnection))
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE tbl_ferrita_fornecedor SET ");
                sql.AppendLine("NOME_FORNECEDOR = @NOME_FORNECEDOR, CNPJ_FORNECEDOR = @CNPJ_FORNECEDOR, ");
                sql.AppendLine("ID_STATUS_FORNECEDOR = @ID_STATUS_FORNECEDOR, TELEFONE = @TELEFONE ");
                sql.AppendLine("WHERE ID_FORNECEDOR = @ID_FORNECEDOR");

                using (var comando = new MySqlCommand(sql.ToString(), conexao))
                {
                    using (conexao)
                    {
                        conexao.Open();
                        comando.Parameters.AddWithValue("@NOME_FORNECEDOR", pFncdr.NomeFornecedor);
                        comando.Parameters.AddWithValue("@CNPJ_FORNECEDOR", pFncdr.CnpjFornecedor);
                        comando.Parameters.AddWithValue("@ID_STATUS_FORNECEDOR", pFncdr.IdStatusFornecedor);
                        comando.Parameters.AddWithValue("@TELEFONE", pFncdr.Telefone);
                        comando.Parameters.AddWithValue("@ID_FORNECEDOR", pFncdr.IdFornecedor);
                        var update = comando.ExecuteNonQuery();
                        if (update == 1)
                            ok = true;
                    }
                }
            }
            return ok;
        }


        public static bool UpdateNomeFornecedor(int pIdFornecedor, string pNomeFornecedor)
        {
            bool ok = false;
            using (var conexao = new MySqlConnection(stringConnection))
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE tbl_ferrita_fornecedor SET ");
                sql.AppendLine("NOME_FORNECEDOR = @NOME_FORNECEDOR ");
                sql.AppendLine("WHERE ID_FORNECEDOR = @ID_FORNECEDOR");

                using (var comando = new MySqlCommand(sql.ToString(), conexao))
                {
                    using (conexao)
                    {
                        conexao.Open();
                        comando.Parameters.AddWithValue("@NOME_FORNECEDOR", pNomeFornecedor);
                        comando.Parameters.AddWithValue("@ID_FORNECEDOR", pIdFornecedor);
                        var update = comando.ExecuteNonQuery();
                        if (update == 1)
                            ok = true;
                    }
                }
            }
            return ok;
        }



    }
}
