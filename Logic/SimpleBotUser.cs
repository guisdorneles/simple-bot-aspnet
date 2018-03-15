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
        static IUserRepo _userRepo = new UserSqlRepo(System.Configuration.ConfigurationManager.ConnectionStrings["ConnStringDb1"].ConnectionString);
        //static IUserRepo _userRepo = new UserMongoRepo();
        public static string Reply(Message message)
        {
            string userId = message.Id;
            var perfil = _userRepo.BuscarUsuario(userId);
            perfil.Visitas += 1;
            _userRepo.AtualizarUsuario(perfil, userId);
            _userRepo.SalvaHistorico(message);
            return $"{message.User} conversou {perfil.Visitas} vezes";
        }
    }
}