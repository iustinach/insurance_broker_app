using System.Collections.Generic;
using System.Linq;
using Modele;

namespace StocareDate
{
    public class AdministratorClienti : IStocareClienti
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
        public void ModificaTelefon(int id, string telefonNou)
        {
            foreach (var c in clienti)
            {
                if (c.Id == id)
                {
                    c.Telefon = telefonNou;
                }
            }
        }
        public void StergeClient(int id)
        {
            var client = clienti.FirstOrDefault(c => c.Id == id);

            if (client != null)
            {
                clienti.Remove(client);
            }
        }
    }
}
