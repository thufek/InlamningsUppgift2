﻿using System;
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
        public bool ProduktRea { get; set; }
        public DateTime ProduktReaStart { get; set; }
        public DateTime ProduktReaSlut { get; set; }
        public int ProduktReaPris { get; set; }
        public Produkt(int _produktID, string _produktNamn, decimal _produktPris, PrisTyp _produktPrisTyp)
        {
            ProduktID = _produktID;
            ProduktNamn = _produktNamn;
            ProduktPris = _produktPris;
            ProduktPrisTyp = _produktPrisTyp;
        }
    }
}
