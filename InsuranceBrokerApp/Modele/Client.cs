using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele
{
    public class Client
    {
        public int Id { get; set; }

        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string NumeComplet => $"{Nume} {Prenume}";
        public string CNP { get; set; }

        public string Telefon { get; set; }

        public List<Polita> Polite { get; set; } = new List<Polita>();

        public void AdaugaPolita(Polita polita)
        {
            Polite.Add(polita);
        }

        public override string ToString()
        {
            return $"{Id} - {Nume} - {Telefon}";
        }
    }
}
