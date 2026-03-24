using System;
using System.Runtime.ConstrainedExecution;
using Modele;
using StocareDate;

class Program
{
    static void Main()
    {
       List<Client> clienti=new List<Client>();

        AdministratorClienti admin = new AdministratorClienti();



        int optiune;

        do
        {
            Console.WriteLine("\n===== MENIU =====");
            Console.WriteLine("1. Adauga client");
            Console.WriteLine("2. Afiseaza clienti");
            Console.WriteLine("3. Cauta client dupa nume");
            Console.WriteLine("0. Iesire");

            Console.Write("Alege optiunea: ");

            if (!int.TryParse(Console.ReadLine(), out optiune))
            {
                Console.WriteLine("Optiune inexistenta!");
                optiune = -1;
                continue;
            }

            switch (optiune)
            {
                case 1:
                    Client c = CitesteClient();
                    if (admin.AdaugaClient(c))
                    {
                        clienti.Add(c);
                        Console.WriteLine("Client adaugat cu succes!");
                        Polita p = CitestePolita();
                        c.Polite.Add(p);
                    }
                    else
                    {
                        Console.WriteLine("CNP sau telefon deja existent!");
                    }
                        break;

                case 2:
                    AfiseazaClienti(clienti);
                    break;

                case 3:
                    Console.WriteLine("Introdu numele: ");
                    string nume=Console.ReadLine();
                    var rez=admin.CautaDupaNume(nume);

                    if (rez.Count == 0)
                    {
                        Console.WriteLine("Nu s-a gasit niciun client.");
                    }
                    else
                    {
                        foreach (var val in rez)
                        { Console.WriteLine(val); }
                    }
                    break;
                default:
                    Console.WriteLine("Optiune inexistenta!");
                    break;
            }

        } while (optiune != 0);
    }

    static Client CitesteClient()
    {
        Client c = new Client();

        Console.Write("Nume client: ");
        c.Nume = Console.ReadLine();

        string cnp;
        do
        {
            Console.WriteLine("CNP: ");
            cnp = Console.ReadLine().Trim();

            if(cnp.Length!=13||!cnp.All(char.IsDigit))
            {
                Console.WriteLine("CNP invalid!");
            }
        }while(cnp.Length !=13 || !cnp.All(char.IsDigit));
        c.CNP = cnp;

        Console.Write("Telefon: ");
        c.Telefon = Console.ReadLine();

        return c;
    }

    static void AfiseazaClienti(List<Client> clienti)
    {
        Console.WriteLine("\nLista clienti:");

        foreach (var client in clienti)
        {
            Console.WriteLine(client);
        }
    }

    static void CautaClient(List<Client> clienti)
    {
        Console.Write("Introdu numele clientului: ");
        string nume = Console.ReadLine();

        bool gasit = false;

        foreach (var client in clienti)
        {
            if (client.Nume.Equals(nume, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(client);
                gasit = true;
            }
        }
        if (!gasit)
            Console.WriteLine("Clientul nu a fost gasit.");
    }
    static Polita CitestePolita()
    {
        Polita p = new Polita();

        Console.WriteLine("Alege tip polita:");
        Console.WriteLine("1. RCA");
        Console.WriteLine("2. CASCO");
        Console.WriteLine("3. Locuinta");
        Console.WriteLine("4. Viata");

        int opt;

        while (!int.TryParse(Console.ReadLine(), out opt) || opt < 1 || opt > 4)
        {
            Console.WriteLine("Optiune invalida!");
        }

        p.Tip = (TipPolita)(opt - 1); 

        Console.WriteLine("Alege optiuni (0 - fara):");
        Console.WriteLine("1. Urgenta");
        Console.WriteLine("2. Suport 24/7");
        Console.WriteLine("3. Asistenta rutiera");

        int opt2;
        p.Optiuni = OptiuniPolita.None;

        while (true)
        {
            if (!int.TryParse(Console.ReadLine(), out opt2))
            {
                Console.WriteLine("Optiune invalida!");
                continue;
            }

            if (opt2 == 0) break;

            switch (opt2)
            {
                case 1: p.Optiuni |= OptiuniPolita.Urgenta; break;
                case 2: p.Optiuni |= OptiuniPolita.Suport24_7; break;
                case 3: p.Optiuni |= OptiuniPolita.AsistentaRutiera; break;
                default:
                    Console.WriteLine("Optiune invalida!");
                    break;
            }
        }

        return p;
    }
}