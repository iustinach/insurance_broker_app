using System.Collections.Generic;
using System.Linq;
using Modele;

namespace StocareDate
{
    public class AdministratorClienti
    {
        private List<Client> clienti = new List<Client>();
        int nextId = 1;

        public bool AdaugaClient(Client c)
        {
            if(clienti.Any(x=>x.CNP == c.CNP))
            {
                return false;
            }
            
            if (clienti.Any(x => x.Telefon == c.Telefon))
            {
                return false;
            }
            c.Id = nextId++;
            clienti.Add(c);
            return true;
        }

        public List<Client> CautaDupaNume(string numeFamilie)
        {
            return clienti
                .Where(c => c.Nume.Split(' ')[0].Equals(numeFamilie.Trim(),StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Client> GetAll()
        {
            return clienti;
        }
    }
}
