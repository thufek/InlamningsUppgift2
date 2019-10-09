using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlämningsUppgift2
{
    class Lager
    {
        public static string path = @"..\..\Produkter.txt";
        public List<Produkt> Produkter { get; set; }
        public Lager()
        {
            Produkter = new List<Produkt>();
            HämtaLagerProdukterFrånFil();
        }
        private void HämtaLagerProdukterFrånFil()
        {
            IOFunktioner.HämtaLagerProdukter(Produkter, path);
        }
        public bool ProduktFinnsILager(int id)
        {
            return Produkter.Exists(p => p.ID == id);
        }
        public bool ProduktFinnsILager(string name)
        {
            return Produkter.Exists(p => p.Namn == name);
        }
        public Produkt HämtaLagerProdukt(int id)
        {
            return Produkter.FirstOrDefault(p => p.ID == id);
        }
        public void ListaLagerProdukter()
        {
            foreach (var p in Produkter)
            {
                Console.WriteLine($"{p.ID} {p.Namn}  Pris: {p.OrginalPris} ");
            }
        }
        public void ListaLagerProdukterSomEjÄrRea()
        {
            foreach (var p in Produkter.Where(p => p.Rea == false))
            {
                Console.WriteLine($"{p.ID} {p.Namn}  Pris: {p.OrginalPris} ");
            }
        }
    }
}
