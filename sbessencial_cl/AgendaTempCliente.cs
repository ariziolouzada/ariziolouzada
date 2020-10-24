using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbessencial_cl
{
    public class AgendaTempCliente
    {

        //Propriedades
        private static readonly string StringConnection;

        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataHoraCadastro { get; set; }
        

        static AgendaTempCliente()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdSbEssencial"].ConnectionString;
        }


        public AgendaTempCliente()
        {

        }


        private AgendaTempCliente(MySqlDataReader leitor)
        {
            Nome = leitor["nome"].ToString();            
            Cpf = leitor["cpf"].ToString();            
            Senha = leitor["senha"].ToString();
            Email = leitor["email"].ToString();
            DataHoraCadastro = Convert.ToDateTime(leitor["dataHoraCadastro"]);
        }
        

        public static bool Inserir(AgendaTempCliente cliente)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO sbessencial.tbl_agenda_temp_cliente ");
                strQuery.Append("(nome, cpf, email, senha, dataCadastro) ");
                strQuery.Append("VALUES (@nome, @cpf, @email, @senha, @dataCadastro ) ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    //comando.Parameters.AddWithValue("@idCliente", cliente.Id);
                    comando.Parameters.AddWithValue("@Nome", cliente.Nome);
                    comando.Parameters.AddWithValue("@email", cliente.Email);
                    comando.Parameters.AddWithValue("@cpf", cliente.Cpf);
                    comando.Parameters.AddWithValue("@senha", cliente.Senha);
                    comando.Parameters.AddWithValue("@dataCadastro", DateTime.Now);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool Editar(AgendaTempCliente cliente)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE sbessencial.tbl_agenda_temp_cliente SET ");
                strQuery.Append("nome = @Nome, email = @email"); 
                strQuery.Append("WHERE cpf  = @cpf ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@email", cliente.Email);
                    comando.Parameters.AddWithValue("@Nome", cliente.Nome);                    

                    var editou = comando.ExecuteNonQuery();
                    resultado = editou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static AgendaTempCliente Pesquisar(string cpf)
        {
            AgendaTempCliente usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_agenda_temp_cliente ");
                strQuery.Append("WHERE cpf = @cpf");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@cpf", cpf);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new AgendaTempCliente(reader);
            }
            return usuario;
        }



    }
}
