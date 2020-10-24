using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace espacobeleza_cl
{
    public class ComandaFormaPgto
    {
        //Propriedades
        private static readonly string StringConnection;

        public long Id { get; set; }
        public long IdComanda { get; set; }
        public int IdFormaPgto { get; set; }
        public int IdEmpresaContratante { get; set; }
        public decimal Valor { get; set; }

        //AUX
        public string Cliente { get; set; }
        public string FormaPgto { get; set; }
        public string Status { get; set; }
        public DateTime Data { get; set; }
        public int IdStatusComanda { get; set; }


        static ComandaFormaPgto()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEspacoDeBeleza"].ConnectionString;
        }

        public ComandaFormaPgto()
        {

        }

        public ComandaFormaPgto(string idCmda, string idFormaPgto, string valor)
        {
            IdComanda = long.Parse(idCmda);
            IdFormaPgto = int.Parse(idFormaPgto);
            Valor = decimal.Parse(valor);
        }

        public ComandaFormaPgto(MySqlDataReader leitor)
        {
            Id = Convert.ToInt64(leitor["id"]);
            IdComanda = Convert.ToInt64(leitor["idComanda"]);
            IdFormaPgto = Convert.ToInt32(leitor["idFormaPgto"]);
            Valor = Convert.ToDecimal(leitor["valor"]);

            //aux
            FormaPgto = leitor["formaPgto"].ToString();
            Cliente = leitor["nomeCliente"].ToString();
            Data = Convert.ToDateTime(leitor["data"]);
            IdStatusComanda = Convert.ToInt32(leitor["idStatusComanda"]);
            //Status = leitor["descricaoStatusComanda"].ToString();
        }


        public static bool Inserir(ComandaFormaPgto comanda)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO bdespacobeleza.tbl_comanda_forma_pgto ");
                strQuery.Append("(idComanda, idFormaPgto, valor) ");
                strQuery.Append("VALUES (@idComanda, @idFormaPgto, @valor) ");
                //idAgenda @idAgenda, 
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idComanda", comanda.IdComanda);
                    comando.Parameters.AddWithValue("@idFormaPgto", comanda.IdFormaPgto);
                    comando.Parameters.AddWithValue("@valor", comanda.Valor);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool Editar(ComandaFormaPgto comanda)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE bdespacobeleza.tbl_comanda_forma_pgto SET ");
                strQuery.Append("idFormaPgto = @idFormaPgto ");
                strQuery.Append(", idComanda = @idComanda, valor = @valor ");
                strQuery.Append("WHERE id = @id ");
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@id", comanda.Id);
                    comando.Parameters.AddWithValue("@valor", comanda.Valor);
                    comando.Parameters.AddWithValue("@idComanda", comanda.IdComanda);
                    comando.Parameters.AddWithValue("@idFormaPgto", comanda.IdFormaPgto);

                    var editou = comando.ExecuteNonQuery();
                    resultado = editou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool Apagar(long id)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM bdespacobeleza.tbl_comanda_forma_pgto ");
                strQuery.Append("WHERE id = @id ");
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

        public static bool ApagarRegistroFormaPgtoComanda(long idComanda)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM bdespacobeleza.tbl_comanda_forma_pgto ");
                strQuery.Append("WHERE idComanda = @id ");
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@id", idComanda);
                    var excluiu = comando.ExecuteNonQuery();
                    resultado = excluiu > 0;
                }
            }
            return resultado;
        }


        public static bool ApagaItensSemReferencia()
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM bdespacobeleza.tbl_comanda_forma_pgto ");
                strQuery.Append("WHERE idComanda NOT IN (SELECT id FROM bdespacobeleza.tbl_comanda GROUP BY id) ");
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    var excluiu = comando.ExecuteNonQuery();
                    resultado = excluiu > 0;
                }
            }
            return resultado;
        }


        public static ComandaFormaPgto Pesquisar(long id)
        {
            ComandaFormaPgto comanda = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.vw_comanda_forma_pgto ");
                strQuery.Append("WHERE id = @id ");
                //strQuery.Append("AND idEmpresaContratante = @idEmpContrat ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    comanda = new ComandaFormaPgto(reader);
            }
            return comanda;
        }


        public static List<ComandaFormaPgto> Lista(long idComanda)
        {
            var lista = new List<ComandaFormaPgto>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.vw_comanda_forma_pgto ");
                strQuery.Append("WHERE idComanda = @idComanda ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idComanda", idComanda);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new ComandaFormaPgto(reader));
            }
            return lista;
        }

        public static List<ComandaFormaPgto> ListaDaTabela(long idComanda)
        {
            var lista = new List<ComandaFormaPgto>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT cfp.*, fp.descricao AS FormaPgto ");
                strQuery.Append("FROM bdespacobeleza.tbl_comanda_forma_pgto cfp ");
                strQuery.Append("JOIN bdespacobeleza.tbl_forma_pagto fp ON cfp.idFormaPgto = fp.id ");
                strQuery.Append("WHERE idComanda = @idComanda ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idComanda", idComanda);

                var leitor = comando.ExecuteReader();
                while (leitor.Read())
                    lista.Add(
                                    new ComandaFormaPgto()
                                    {
                                        Id = Convert.ToInt64(leitor["id"])
                                        ,
                                        IdComanda = Convert.ToInt64(leitor["idComanda"])
                                        ,
                                        IdFormaPgto = Convert.ToInt32(leitor["idFormaPgto"])
                                        ,
                                        Valor = Convert.ToDecimal(leitor["valor"])
                                        ,
                                        FormaPgto = leitor["FormaPgto"].ToString()
                                    }
                             );
            }
            return lista;
        }

        public static List<ComandaFormaPgto> Lista(long idComanda, int idEmpContrat)
        {
            var lista = new List<ComandaFormaPgto>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.vw_comanda_forma_pgto ");
                strQuery.Append("WHERE idComanda = @idComanda ");
                strQuery.Append("AND idEmpresaContratante = @idEmpContrat ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idComanda", idComanda);
                comando.Parameters.AddWithValue("@idEmpContrat", idEmpContrat);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new ComandaFormaPgto(reader));
            }
            return lista;
        }

        //SELECT id, idComanda, idFormaPgto, valor FROM bdespacobeleza.tbl_comanda_forma_pgto;

    }
}
