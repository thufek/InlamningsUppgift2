using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace InlämningsUppgift2
{
    class Kundvagn
    {
        public static int KvittoNummer = 0;
        public List<Produkt> Produkter { get; set; }
        public DateTime DatumKvitto { get; set; }
        public decimal ItemsTotal { get; set; }
        public decimal Rabatt { get; set; }
        public decimal TotalPris { get; set; }
        public Kundvagn()
        {
            Produkter = new List<Produkt>();
            DatumKvitto = DateTime.Now;
        }
        public void RäknaTotalPris()
        {
            TotalPris = 0;
            for (int i = 0; i < Produkter.Count; i++)
            {
                if (Produkter[i].ProduktRea)
                {
                    TotalPris += Produkter[i].ProduktReaPris;
                }
                else
                {
                    TotalPris += Produkter[i].ProduktPris;
                }      
            }
        }
        public void ListaAllaProdukterIKundvagn()
        {
            for (int i = 0; i < Produkter.Count; i++)
            {
                string prisTyp = "";
                if (Produkter[i].ProduktPrisTyp == Produkt.PrisTyp.kg) prisTyp = "kilo";
                if (Produkter[i].ProduktPrisTyp == Produkt.PrisTyp.st) prisTyp = "styck";
                if (Produkter[i].ProduktRea)
                {
                    Console.WriteLine($"{Produkter[i].ProduktNamn} {Produkter[i].ProduktAntal} {prisTyp} {Produkter[i].ProduktReaPris} REA-vara");
                }
                else
                {
                    Console.WriteLine($"{Produkter[i].ProduktNamn} {Produkter[i].ProduktAntal} {prisTyp} {Produkter[i].ProduktPris}");
                }
            }
        }
        public void LäggTillNyaProdukterIKundvagn(Produkt lagerProdukt, Produkt nyProdukt, int antal)
        {
            for (int i = 0; i < antal - 1; i++)
            {
                nyProdukt.ProduktAntal++;
                nyProdukt.ProduktPris += lagerProdukt.ProduktPris;
                nyProdukt.ProduktReaPris += lagerProdukt.ProduktReaPris;
            }
            bool produktFinns = false;
            for (int i = 0; i < Produkter.Count; i++)
            {
                if (Produkter[i].ProduktID == nyProdukt.ProduktID)
                {
                    Produkter[i].ProduktAntal += nyProdukt.ProduktAntal;
                    Produkter[i].ProduktPris += nyProdukt.ProduktPris;
                    Produkter[i].ProduktReaPris += nyProdukt.ProduktReaPris;
                    produktFinns = true;
                }
            }
            if (produktFinns == false)
            {
                Produkter.Add(nyProdukt);
            }
        }
    }
}
