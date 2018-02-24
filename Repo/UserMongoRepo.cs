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
        public  void SalvaHistorico(Message message)
        {
            var cliente = new MongoClient();
            var db = cliente.GetDatabase("bot");
            var col = db.GetCollection<BsonDocument>("historico");
 
            var doc = new BsonDocument()
            {
                {"Nome Usuario", message.User },
                {"Texto", message.Text},
            };
            col.InsertOne(doc);
        }
    }
}