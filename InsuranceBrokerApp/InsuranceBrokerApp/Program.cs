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
        admin.AdaugaClient(new Client { Id = 1, Nume = "Ana", Telefon = "0790567876" });
        var rezultate = admin.CautaDupaNume("Ana");
        foreach(var c in rezultate)
        { Console.WriteLine(c); }


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
                    Client c = CitesteClient();
                    clienti.Add(c);
                    admin.AdaugaClient(c);
                    break;

                case 2:
                    AfiseazaClienti(clienti);
                    break;

                case 3:
                    Console.WriteLine("Introdu numele: ");
                    string nume=Console.ReadLine();
                    var rez=admin.CautaDupaNume(nume);

                    foreach (var val in rez)
                    { Console.WriteLine(val); }
                    if(rez.Count==0)
                    { Console.WriteLine("Nu s-a gasit niciun client."); }
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
}