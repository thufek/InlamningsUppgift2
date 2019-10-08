using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace InlämningsUppgift2
{
    static class IOFunktioner
    {
        public static string HämtaKvittoText(DateTime datum)
        {
            string kvittoInnehåll;
            string path = $"..\\..\\RECIEPT_{datum.ToString("yyyyMMdd")}.txt";
            if (File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    kvittoInnehåll = sr.ReadToEnd();
                }
                return kvittoInnehåll;
            }
            else
            {
                return null;
            }
        }
        public static int HämtaKvittoNummer(string path)
        {
            string kvittoInnehåll;
            if (!File.Exists(path))
            {
                return 1;
            }
            else
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    kvittoInnehåll = sr.ReadToEnd();
                }
                string[] antalKvitton = kvittoInnehåll.Split('#');
                return antalKvitton.Length;
            }
        }
        public static void SkrivUtKvittoTillFil(Kvitto kvitto)
        {
            if (!File.Exists(kvitto.Path))
            {
                using (StreamWriter sw = File.CreateText(kvitto.Path))
                {

                }
            }
            using (StreamWriter sw = File.AppendText(kvitto.Path))
            {
                sw.WriteLine($"#{kvitto.Nummer}¨    {kvitto.Datum.Hour}:{kvitto.Datum.Minute}:{kvitto.Datum.Second}");
                foreach (var p in kvitto.Produkter)
                {
                    sw.WriteLine($"{p.Namn} {p.Antal} {p.HämtaPrisTyp()} * {p.OrginalPris} = {p.TotalPris}");
                }
                if (kvitto.Rabatt)
                {
                    sw.WriteLine($"Items total: {kvitto.TotalPris}\nRabatt: {kvitto.RabattPris - kvitto.TotalPris}\nTotal:¨{kvitto.RabattPris}");
                }
                else
                {
                    sw.WriteLine($"Total:¨{kvitto.TotalPris}");
                }
            }
        }
        public static void HämtaLagerProdukter(List<Produkt> lagerProdukter, string path)
        {
            int id;
            string namn;
            int maxAntal;
            string prisTyp;
            decimal orginalPris;
            decimal reaPris;
            bool rea;
            DateTime reaStart;
            DateTime reaSlut;
            using (StreamReader sr = File.OpenText(path))
            {
                while (!sr.EndOfStream)
                {
                    id = int.Parse(sr.ReadLine());
                    namn = sr.ReadLine();
                    maxAntal = int.Parse(sr.ReadLine());
                    prisTyp = sr.ReadLine();
                    orginalPris = Convert.ToDecimal(sr.ReadLine());
                    reaPris = Convert.ToDecimal(sr.ReadLine());
                    rea = Convert.ToBoolean(sr.ReadLine());
                    reaStart = DateTime.Parse(sr.ReadLine());
                    reaSlut = DateTime.Parse(sr.ReadLine());
                    if (prisTyp == "st")
                    {
                        lagerProdukter.Add(new Produkt(id, namn, maxAntal, Produkt.Typ.St, orginalPris, reaPris, reaStart, reaSlut));
                    }
                    else if (prisTyp == "kg")
                    {
                        lagerProdukter.Add(new Produkt(id, namn, maxAntal, Produkt.Typ.Kg, orginalPris, reaPris, reaStart, reaSlut));
                    }
                }
            }
        }
        public static void SparaLagerProdukterIFil(List<Produkt> lagerProdukter, string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (StreamWriter sw = File.CreateText(path))
            {
                foreach (var p in lagerProdukter)
                {
                    sw.WriteLine(p.ID);
                    sw.WriteLine(p.Namn);
                    sw.WriteLine(p.MaxAntal);
                    if (p.PrisTyp == Produkt.Typ.St)
                    {
                        sw.WriteLine("st");
                    }
                    if (p.PrisTyp == Produkt.Typ.Kg)
                    {
                        sw.WriteLine("kg");
                    }
                    sw.WriteLine(p.OrginalPris);
                    sw.WriteLine(p.ReaPris);
                    sw.WriteLine(p.Rea);
                    sw.WriteLine($"{p.ReaStart.Year}-{p.ReaStart.Month}-{p.ReaStart.Day}");
                    sw.WriteLine($"{p.ReaSlut.Year}-{p.ReaSlut.Month}-{p.ReaSlut.Day}");
                }
            }
        }
    }
}
