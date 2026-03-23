using System.Collections.Generic;
using System.Linq;
using Modele;

namespace StocareDate
{
    public class AdministratorClienti
    {
        private List<Client> clienti = new List<Client>();

        public bool AdaugaClient(Client c)
        {
            if(clienti.Any(x=>x.CNP == c.CNP))
            {
                Console.WriteLine("CNP deja existent!");
                return false;
            }
            if (clienti.Any(x => x.Id == c.Id))
            {
                Console.WriteLine("ID deja existent!");
                return false;
            }
            if (clienti.Any(x => x.Telefon == c.Telefon))
            {
                Console.WriteLine("Telefon deja existent!");
                return false;
            }
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
