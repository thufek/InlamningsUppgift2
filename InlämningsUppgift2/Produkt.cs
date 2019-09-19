using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlämningsUppgift2
{
    class Produkt
    {
        public enum PrisTyp
        {
            None,
            krKg,
            krSt
        }
        public int ProduktID { get; set; }
        public string ProduktNamn { get; set; }
        public decimal ProduktPris { get; set; }
        public PrisTyp ProduktPrisTyp { get; set; }
        public int ProduktAntal { get; set; }
        public bool ProduktRea { get; set; }
        public DateTime ProduktReaStart { get; set; }
        public DateTime ProduktReaSlut { get; set; }
        public decimal ProduktReaPris { get; set; }
        public Produkt(int _produktID, string _produktNamn, decimal _produktPris, decimal _produktReaPris, PrisTyp _produktPrisTyp)
        {
            ProduktID = _produktID;
            ProduktNamn = _produktNamn;
            ProduktPris = _produktPris;
            ProduktReaPris = _produktReaPris;
            ProduktPrisTyp = _produktPrisTyp;
            ProduktAntal++;
        }
        public Produkt(Produkt produktKopia)
        {
            ProduktID = produktKopia.ProduktID;
            ProduktNamn = produktKopia.ProduktNamn;
            ProduktPris = produktKopia.ProduktPris;
            ProduktPrisTyp = produktKopia.ProduktPrisTyp;
            ProduktAntal = produktKopia.ProduktAntal;
            ProduktRea = produktKopia.ProduktRea;
            ProduktReaStart = produktKopia.ProduktReaStart;
            ProduktReaSlut = produktKopia.ProduktReaSlut;
            ProduktReaPris = produktKopia.ProduktReaPris;
        }
    }
}
