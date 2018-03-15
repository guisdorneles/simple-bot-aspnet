using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot
{
    public class UserMongoRepo: IUserRepo
    {
        private MongoClient cliente;
        private IMongoDatabase _db;
        public UserMongoRepo()
        {
            _db = cliente.GetDatabase("bot");
        }

        public void AtualizarUsuario(UserProfile userProfile, string id)
        {
            var col = _db.GetCollection<BsonDocument>("historico");
            col.ReplaceOne(new BsonDocument("_id", id),userProfile.ToBsonDocument<UserProfile>(), new UpdateOptions() { IsUpsert = true });
        }

        public UserProfile BuscarUsuario(string id)
        {
            var usuario = _db.GetCollection<BsonDocument>("usuario").Find(Builders<BsonDocument>.Filter.Eq("Id", id)).FirstOrDefault();
            var col = _db.GetCollection<BsonDocument>("usuario");
            if (usuario != null)
            {
                UserProfile user = new UserProfile
                {
                    Id = usuario["Id"].ToString(),
                    Visitas = usuario["Visitas"].ToInt32()
                };
                return user;
            }

            col.InsertOne(new BsonDocument
            {
                { "Id", id },
                {"Visitas", 0 }
            });

            return new UserProfile
            {
                Id = id,
                Visitas = 0
            };
        }


        public void InserirUsuario(UserProfile userProfile)
        {
            var col = _db.GetCollection<BsonDocument>("usuario");

            var doc = new BsonDocument()
            {
                {"Id", userProfile.Id},
                {"Visitas", userProfile.Visitas},
            };
            col.InsertOne(doc);
        }

        public  void SalvaHistorico(Message message)
        {

            var col = _db.GetCollection<BsonDocument>("historico");
 
            var doc = new BsonDocument()
            {
                {"Nome_Usuario", message.User },
                {"Texto", message.Text},
            };
            col.InsertOne(doc);
        }
    }
}