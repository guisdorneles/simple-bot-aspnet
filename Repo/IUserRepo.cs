using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBot
{
    public interface IUserRepo
    {
        void SalvaHistorico(Message message);
        void InserirUsuario(UserProfile userProfile);
        void AtualizarUsuario(UserProfile userProfile, string id);
        UserProfile BuscarUsuario(string id);

    }
}
