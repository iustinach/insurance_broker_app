using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modele;

namespace StocareDate
{
    public interface IStocareClienti
    {
        bool AdaugaClient(Client c);
        List<Client> GetAll();
        List<Client> CautaDupaNume(string nume);
        void ModificaTelefon(int id, string telefonNou);
        void ModificaClient(Client c);

    }
}
