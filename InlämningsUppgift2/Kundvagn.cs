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
        public List<Produkt> Produkter { get; set; }
        public DateTime DatumKvitto { get; set; }
        public int TotaltAntalProdukter { get; set; }
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
        public bool FinnsProduktIKvitto(Produkt produkt)
        {
            for (int i = 0; i < Produkter.Count; i++)
            {
                if (Produkter[i].ProduktID == produkt.ProduktID)
                {
                    return true;
                }
            }
            return false;
        }
        public void ListaAllaProdukterIKundvagn()
        {
            for (int i = 0; i < Produkter.Count; i++)
            {
                if (Produkter[i].ProduktRea)
                {
                    Console.WriteLine($"{Produkter[i].ProduktNamn} {Produkter[i].ProduktAntal} {Produkter[i].ProduktReaPris}");
                }
                else
                {
                    Console.WriteLine($"{Produkter[i].ProduktNamn} {Produkter[i].ProduktAntal} {Produkter[i].ProduktPris}");
                }
            }
        }
        public void SkrivUtKvitto()
        {
 
        }
    }
}
