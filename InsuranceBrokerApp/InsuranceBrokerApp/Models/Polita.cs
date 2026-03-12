using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceBrokerApp.Models
{
    public class Polita
    {
        public int Id { get; set; }

        public string TipAsigurare { get; set; }

        public double Pret { get; set; }

        public DateTime DataExpirare { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, Tip: {TipAsigurare}, Pret: {Pret}, Expira: {DataExpirare.ToShortDateString()}";
        }
    }
}