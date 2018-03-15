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


        public UserProfile BuscarUsuario(string id)
        {
            using (var conn = new SqlConnection(_connectionstring))
            {
                UserProfile usr = new UserProfile();
                var cmd = new SqlCommand("SELECT * FROM tbUsuarios where Id = @id", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usr = new UserProfile
                        {
                            Id = reader.GetString(0),
                            Visitas = reader.GetInt32(1)
                        };
                    }
                }
                return usr;
            }
        }




        public void AtualizarUsuario(UserProfile userProfile, string id)
        {
            using (var conn = new SqlConnection(_connectionstring))
            {
                var cmd = new SqlCommand("UPDATE tbUsuarios SET visitas = @visitas WHERE Id = @id", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@visistas", userProfile.Visitas);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }


        public void SalvaHistorico(Message message)
        {
            using (var conn = new SqlConnection(_connectionstring))
            {
                var cmd = new SqlCommand("INSERT tbMensagens VALUES( @UserName, @texto)", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@UserName", message.User);
                cmd.Parameters.AddWithValue("@texto", message.Text);
                cmd.ExecuteNonQuery();
            }
        }

    }
}