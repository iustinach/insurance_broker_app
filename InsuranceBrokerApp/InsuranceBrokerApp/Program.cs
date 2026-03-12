using System;
using InsuranceBrokerApp.Models;

class Program
{
    static void Main()
    {
        Client[] clienti = new Client[100];
        int nrClienti = 0;

        int optiune;

        do
        {
            Console.WriteLine("\n===== MENIU =====");
            Console.WriteLine("1. Adauga client");
            Console.WriteLine("2. Afiseaza clienti");
            Console.WriteLine("3. Cauta client dupa nume");
            Console.WriteLine("0. Iesire");

            Console.Write("Alege optiunea: ");
            optiune = int.Parse(Console.ReadLine());

            switch (optiune)
            {
                case 1:
                    clienti[nrClienti++] = CitesteClient();
                    break;

                case 2:
                    AfiseazaClienti(clienti, nrClienti);
                    break;

                case 3:
                    CautaClient(clienti, nrClienti);
                    break;
            }

        } while (optiune != 0);
    }

    static Client CitesteClient()
    {
        Client c = new Client();

        Console.Write("ID client: ");
        c.Id = int.Parse(Console.ReadLine());

        Console.Write("Nume client: ");
        c.Nume = Console.ReadLine();

        Console.Write("CNP: ");
        c.CNP = Console.ReadLine();

        Console.Write("Telefon: ");
        c.Telefon = Console.ReadLine();

        return c;
    }

    static void AfiseazaClienti(Client[] clienti, int n)
    {
        Console.WriteLine("\nLista clienti:");

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine(clienti[i]);
        }
    }

    static void CautaClient(Client[] clienti, int n)
    {
        Console.Write("Introdu numele clientului: ");
        string nume = Console.ReadLine();

        bool gasit = false;

        for (int i = 0; i < n; i++)
        {
            if (clienti[i].Nume.Equals(nume, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(clienti[i]);
                gasit = true;
            }
        }

        if (!gasit)
            Console.WriteLine("Clientul nu a fost gasit.");
    }
}