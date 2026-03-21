using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele
{
    public enum TipPolita
    {
        RCA,
        CASCO,
        Locuinta,
        Viata
    }

    [Flags]
    public enum OptiuniPolita
    {
        None = 0,
        Urgenta=1,
        Suport24_7=2,
        AsistentaRutiera=4
    }
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