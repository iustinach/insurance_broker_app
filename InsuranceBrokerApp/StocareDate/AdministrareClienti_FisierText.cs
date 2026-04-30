using System;
using System.Collections.Generic;
using System.IO;
using Modele;
using static System.Net.Mime.MediaTypeNames;

namespace StocareDate
{
    public class AdministrareClienti_FisierText : IStocareClienti
    {
        private string numeFisier;

        public AdministrareClienti_FisierText(string numeFisier)
        {
            this.numeFisier = numeFisier;

            if (!File.Exists(numeFisier))
            {
                File.Create(numeFisier).Close();
            }
        }

        public bool AdaugaClient(Client c)
        {
            var clienti = GetAll();

            if (clienti.Any(x => x.CNP == c.CNP))
                return false;

            if (clienti.Any(x => x.Telefon == c.Telefon))
                return false;

            c.Id = clienti.Count + 1;

            using (StreamWriter sw = new StreamWriter(numeFisier, true))
            {
                string linie = $"{c.Id};{c.Nume};{c.CNP};{c.Telefon}";

                foreach (var p in c.Polite)
                {
                    linie += $"|{p.Tip},{p.Optiuni}";
                }

                sw.WriteLine(linie);
            }

            return true;
        }

        public List<Client> GetAll()
        {
            List<Client> clienti = new List<Client>();

            foreach (var linie in File.ReadAllLines(numeFisier))
            {
                if (string.IsNullOrWhiteSpace(linie))
                    continue;

                var parti = linie.Split('|');
                var dateClient = parti[0].Split(';');

                Client c = new Client
                {
                    Id = int.Parse(dateClient[0]),
                    Nume = dateClient[1],
                    CNP = dateClient[2],
                    Telefon = dateClient[3],
                    Polite = new List<Polita>()
                };

                for (int i = 1; i < parti.Length; i++)
                {
                    var pData = parti[i].Split(',');

                    Polita p = new Polita
                    {
                        Tip = (TipPolita)Enum.Parse(typeof(TipPolita), pData[0]),
                        Optiuni = (OptiuniPolita)Enum.Parse(typeof(OptiuniPolita), pData[1])
                    };

                    c.Polite.Add(p);
                }

                clienti.Add(c);
            }

            return clienti;
        }

        public List<Client> CautaDupaNume(string nume)
        {
            return GetAll()
             .Where(c => c.Nume
        .ToLower()
        .Contains(nume.ToLower()))
        .ToList();
        }

        public void ModificaTelefon(int id, string telefonNou)
        {
            var clienti = GetAll();

            foreach (var c in clienti)
            {
                if (c.Id == id)
                    c.Telefon = telefonNou;
            }

            using (StreamWriter sw = new StreamWriter(numeFisier))
            {
                foreach (var c in clienti)
                {
                    string linie = $"{c.Id};{c.Nume};{c.CNP};{c.Telefon}";

                    foreach (var p in c.Polite)
                    {
                        linie += $"|{p.Tip},{p.Optiuni}";
                    }

                    sw.WriteLine(linie);
                }
            }
        }
        public void ModificaClient(Client clientModificat)
        {
            var clienti = GetAll();

            for (int i = 0; i < clienti.Count; i++)
            {
                if (clienti[i].Id == clientModificat.Id)
                {
                    clienti[i] = clientModificat;
                    break;
                }
            }

            using (StreamWriter sw = new StreamWriter(numeFisier))
            {
                foreach (var c in clienti)
                {
                    string linie = $"{c.Id};{c.Nume};{c.CNP};{c.Telefon}";

                    foreach (var p in c.Polite)
                    {
                        linie += $"|{p.Tip},{p.Optiuni}";
                    }

                    sw.WriteLine(linie);
                }
            }
        }
    }
}