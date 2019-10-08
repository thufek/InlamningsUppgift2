using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Globalization;

namespace InlämningsUppgift2
{
    class Kvitto
    {
        public string Path { get; private set; }
        public int Nummer { get; private set; }
        public decimal TotalPris { get; private set; }
        public decimal RabattPris { get; private set; }
        public bool Rabatt { get; private set; }
        public DateTime Datum { get; private set; }
        public List<Produkt> Produkter { get; set; }
        public Kvitto()
        {
            Produkter = new List<Produkt>();
            Datum = DateTime.Now;
            Path = $@"..\..\RECIEPT_{Datum.ToString("yyyyMMdd")}.txt";
            Nummer = IOFunktioner.HämtaKvittoNummer(Path);
        }
        public void RäknaPris()
        {
            TotalPris = Produkter.Sum(p => p.TotalPris);
            RäknaRabatt();
        }
        private void RäknaRabatt()
        {
            if (TotalPris < 1000)
            {
                Rabatt = false;
            }
            else if (TotalPris > 1000 && TotalPris < 2000)
            {
                Rabatt = true;
                RabattPris = TotalPris * 0.98m; //2% rabatt
            }
            else
            {
                Rabatt = true;
                RabattPris = TotalPris * 0.97m; //3% rabatt
            }
        }
        private void ListaProdukterIKvitto()
        {
            foreach (var p in Produkter)
            {
                p.RäknaTotalPris();
                if (p.Rea)
                {
                    Console.WriteLine($"{p.Namn} {p.Antal} {p.HämtaPrisTyp()} * {p.ReaPris,0:C} = {p.TotalPris,0:C}");
                }
                else
                {
                    Console.WriteLine($"{p.Namn} {p.Antal} {p.HämtaPrisTyp()} * {p.OrginalPris,0:C} = {p.TotalPris,0:C}");
                }
            }
        }
        public void VisaKvitto()
        {
            Console.WriteLine($"KVITTO      {Datum}");
            ListaProdukterIKvitto();
            RäknaPris();
            if (Rabatt)
            {
                Console.WriteLine($"Items total: {TotalPris,0:C}\nRabatt: {(RabattPris - TotalPris),0:C}\nTotal: {RabattPris,0:C}");
            }
            else
            {
                Console.WriteLine($"Total: {TotalPris,0:C}");
            }
        }
        public static void VisaKommandon()
        {
            Console.WriteLine($"kommandon:\n<produktid> <antal>\nRETURN <produktid>\nPAY");
            Console.Write("kommando: ");
        }
        public bool ProduktFinnsIKvitto(int id)
        {
            return Produkter.Exists(p => p.ID == id);
        }
        public void TaBortProdukt(Produkt produkt)
        {
            Produkter.Remove(produkt);
        }
        public Produkt HämtaProduktIKvitto(int id)
        {
            return Produkter.First(p => p.ID == id);
        }
    }
}
