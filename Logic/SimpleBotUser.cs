using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot
{
    public class SimpleBotUser
    {
        static Dictionary<string, UserProfile> _perfil = new Dictionary<string, UserProfile>();
        //static IUserRepo _userRepo = new UserMemRepo();
        static IUserRepo _userRepo = new UserSqlRepo(System.Configuration.ConfigurationManager.ConnectionStrings["ConnStringDb1"].ConnectionString);
        public static string Reply(Message message)
        {
            string userId = message.Id;
            var perfil = GetProfile(userId);
            perfil.Visitas += 1;
            SetProfile(userId, perfil);

            _userRepo.SalvaHistorico(message);

            //return $"{message.User} disse '{message.Text}'";z
            return $"{message.User} conversou {perfil.Visitas} vezes";
        }

        public static UserProfile GetProfile(string id)
        {
            //Buscar o usuario 
            var usuario = new MongoClient();
            var db = usuario.GetDatabase("bot");
            var col = db.GetCollection<UserProfile>("usuario");
            // Procurar no banco se não existe cria em memoria
            var listaUsuario = col.Find(new BsonDocument("_id", id)).FirstOrDefault();
            if (listaUsuario != null)
                return listaUsuario;

            return new UserProfile
            {
                Id = id,
                Visitas = 0
            };
        }

        public static void SetProfile(string id, UserProfile profile)
        {
            //Mongo db
            //var usuario = new MongoClient();
            //var db = usuario.GetDatabase("bot");
            //var col = db.GetCollection<UserProfile>("usuario");
            // Procurar no banco se não existe cria, se sim atualiza.
            //col.ReplaceOne(new BsonDocument("_id", id), profile, new UpdateOptions() { IsUpsert = true });
            //

        }
    }
}