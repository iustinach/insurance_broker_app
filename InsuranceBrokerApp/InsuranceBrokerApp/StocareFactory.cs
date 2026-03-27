using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using StocareDate;

namespace InsuranceBrokerApp
{
    public static class StocareFactory
    {
        private const string FORMAT = "FormatSalvare";
        private const string NUME = "NumeFisier";

        public static IStocareClienti GetStocare()
        {
            string format = ConfigurationManager.AppSettings[FORMAT] ?? "";
            string numeFisier = ConfigurationManager.AppSettings[NUME] ?? "clienti";

            string basePath = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName ?? "";
            string cale = Path.Combine(basePath, numeFisier);

            switch (format)
            {
                case "txt":
                    return new AdministrareClienti_FisierText(cale + ".txt");

                default:
                    return new AdministratorClienti();
            }
        }
    }
}