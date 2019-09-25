using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace InlämningsUppgift2
{
    static class IOfunktioner
    {
        static string lagerPath = "..\\..\\LagerProdukter.txt";
        public static void LaddaInLagerProdukter(Lager lager)
        {
            int id;
            string namn;
            decimal pris;
            string enu;
            using (StreamReader sr = File.OpenText(lagerPath))
            {
                while (!sr.EndOfStream)
                {
                    id = int.Parse(sr.ReadLine());
                    namn = sr.ReadLine();
                    pris = Convert.ToDecimal(sr.ReadLine());
                    enu = sr.ReadLine();
                    if (enu == "st")
                    {
                        lager.LagerProdukter.Add(new Produkt(id, namn, pris, Produkt.PrisTyp.st));
                    }
                    else if (enu == "kg")
                    {
                        lager.LagerProdukter.Add(new Produkt(id, namn, pris, Produkt.PrisTyp.kg));
                    }
                }
            }
        }
        public static void SparaLagerProdukter(Lager lager)
        {
            if (File.Exists(lagerPath))
            {
                File.Delete(lagerPath);
            }
            using (StreamWriter sw = File.CreateText(lagerPath))
            {
                for (int i = 0; i < lager.LagerProdukter.Count; i++)
                {
                    sw.WriteLine(lager.LagerProdukter[i].ProduktID);
                    sw.WriteLine(lager.LagerProdukter[i].ProduktNamn);
                    sw.WriteLine(lager.LagerProdukter[i].ProduktPris);
                    if (lager.LagerProdukter[i].ProduktPrisTyp == Produkt.PrisTyp.kg)
                    {
                        sw.WriteLine("kg");
                    }
                    else if (lager.LagerProdukter[i].ProduktPrisTyp == Produkt.PrisTyp.st)
                    {
                        sw.WriteLine("st");
                    }
                }
            }
        }
        public static void SkrivUtKvitto(Kundvagn kundvagn)
        {
            string dagensDatum = $"{kundvagn.DatumKvitto.Year}{kundvagn.DatumKvitto.Month}{kundvagn.DatumKvitto.Day}";
            string path = $"..\\..\\RECEIPT_{dagensDatum}.txt";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine($"*{Kundvagn.KvittoNummer} {kundvagn.DatumKvitto.TimeOfDay}");
                    foreach (var item in kundvagn.Produkter)
                    {
                        sw.WriteLine($"{item.ProduktNamn} {item.ProduktAntal} {item.ProduktPrisTyp} {item.ProduktPris}");
                    }
                    sw.WriteLine($"Totalt: #{kundvagn.TotalPris} kr");
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine($"*{Kundvagn.KvittoNummer} {kundvagn.DatumKvitto.TimeOfDay}");
                    foreach (var item in kundvagn.Produkter)
                    {
                        sw.WriteLine($"{item.ProduktNamn} {item.ProduktAntal} {item.ProduktPrisTyp} {item.ProduktPris}");
                    }
                    sw.WriteLine($"Totalt: #{kundvagn.TotalPris} kr");
                }
            }
        }
        public static void SökKvitto()
        {
            
            string helaKvittot = "";
            bool hittatKvitto = false;
            while (!hittatKvitto)
            {
                Console.Clear();
                Console.Write("Skriv in datum på kvittot du söker i detta format (YYYY-MM-DD): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime datum))
                {
                    string dagensDatum = $"{datum.Year}{datum.Month}{datum.Day}";
                    string path = $"..\\..\\RECEIPT_{dagensDatum}.txt";
                    if (File.Exists(path))
                    {
                        Console.WriteLine("Hittade kvitto!");
                        hittatKvitto = true;
                        using (StreamReader sr = File.OpenText(path))
                        {
                            helaKvittot = sr.ReadToEnd();
                        }
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Hittade ej kvitto för angivet datum!");
                        Thread.Sleep(3000);
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltig inmating. Mata in datum enligt format!");
                    Thread.Sleep(3000);
                }
            }
            string[] kvitton = helaKvittot.Split('*');
        }
    }
}
