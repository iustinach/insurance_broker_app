using System.Collections.Generic;
using System.Linq;
using Modele;

namespace StocareDate
{
    public class AdministratorClienti
    {
        private List<Client> clienti = new List<Client>();

        public void AdaugaClient(Client c)
        {
            clienti.Add(c);
        }

        public List<Client> CautaDupaNume(string nume)
        {
            return clienti
                .Where(c => c.Nume.ToLower() == nume.ToLower())
                .ToList();
        }

        public List<Client> GetAll()
        {
            return clienti;
        }
    }
}
