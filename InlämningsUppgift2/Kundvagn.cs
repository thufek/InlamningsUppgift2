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
        public void SkrivUtKvitto()
        {
            string dagensDatum = $"{DatumKvitto.Year}{DatumKvitto.Month}{DatumKvitto.Day}";
            string path = $"..\\..\\RECEIPT_{dagensDatum}.txt";
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine($"***** {DatumKvitto.TimeOfDay} *****");
                    foreach (var item in Produkter)
                    {
                        sw.WriteLine($"ID: {item.ProduktID} : {item.ProduktNamn} : {item.ProduktAntal} {item.ProduktPrisTyp} : {item.ProduktPris}");
                    }
                    sw.WriteLine($"Totalt: {TotalPris} kr");
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine($"***** {DatumKvitto.TimeOfDay} *****");
                    foreach (var item in Produkter)
                    {
                        sw.WriteLine($"ID: {item.ProduktID} : {item.ProduktNamn} : {item.ProduktAntal} {item.ProduktPrisTyp} : {item.ProduktPris}");
                    }
                    sw.WriteLine($"Totalt: {TotalPris} kr");
                }
            }
        }
    }
}
