using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SimpleBot
{
    public class UserSqlRepo : IUserRepo
    {
        string _connectionstring;
        public UserSqlRepo(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public void InserirUsuario(UserProfile userProfile)
        {
            using (var conn = new SqlConnection(_connectionstring))
            {
                var cmd = new SqlCommand("INSERT INTO tbUsuarios values (@visitas)", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@visistas", userProfile.Visitas);
                cmd.ExecuteNonQuery();
            }
        }


        public UserProfile BuscarUsuario(UserProfile userProfile)
        {
            using (var conn = new SqlConnection(_connectionstring))
            {
                var cmd = new SqlCommand("SELECT * FROM tbUsuarios where Id = @id", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@id", userProfile.Id);
                cmd.ExecuteNonQuery();
                return 
            }
        }




        public void AtualizarUsuario(UserProfile userProfile)
        {
            using (var conn = new SqlConnection(_connectionstring))
            {
                var cmd = new SqlCommand("UPDATE tbUsuarios SET visitas = @visitas WHERE Id = @id", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@visistas", userProfile.Visitas);
                cmd.Parameters.AddWithValue("@id", userProfile.Id);
                cmd.ExecuteNonQuery();
            }
        }


        public void SalvaHistorico(Message message)
        {
            using (var conn = new SqlConnection(_connectionstring))
            {
                var cmd = new SqlCommand("INSERT ...", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}