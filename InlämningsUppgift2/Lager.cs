using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlämningsUppgift2
{
    class Lager
    {
        public List<Produkt> LagerProdukter { get; set; }
        public Lager()
        {
            LagerProdukter = new List<Produkt>();
        }
        public void LäggTillProduktILager(Produkt produkt)
        {
            bool produktFinns = KollaOmProduktFinns(produkt);
            if (!produktFinns)
            {
                LagerProdukter.Add(produkt);
            }
        }
        public void TaBortProduktFrånLager(int produktID)
        {
            var lagerProdukt = KollaOmProduktFinns(produktID);
            if (lagerProdukt != null)
            {
                LagerProdukter.Remove(lagerProdukt);
            }
        }
        public void TaBortProduktFrånLager(string produktNamn)
        {
            var lagerProdukt = KollaOmProduktFinns(produktNamn);
            if (lagerProdukt != null)
            {
                LagerProdukter.Remove(lagerProdukt);
            }
        }
        public void ListaAllaProdukterILager()
        {
            for (int i = 0; i < LagerProdukter.Count; i++)
            {
                Console.WriteLine($"ID: {LagerProdukter[i].ProduktID} | {LagerProdukter[i].ProduktNamn} | {LagerProdukter[i].ProduktPris} {LagerProdukter[i].ProduktPrisTyp}");
            }
        }
        public Produkt RedigeraProdukt(int produktID)
        {
            var lagerProdukt = KollaOmProduktFinns(produktID);
            return lagerProdukt;
        }
        private bool KollaOmProduktFinns(Produkt produkt)
        {
            bool produktFinns = false;
            for (int i = 0; i < LagerProdukter.Count; i++)
            {
                if (LagerProdukter[i].ProduktID == produkt.ProduktID || LagerProdukter[i].ProduktNamn == produkt.ProduktNamn)
                {
                    produktFinns = true;
                }
            }
            return produktFinns;
        }
        private Produkt KollaOmProduktFinns(int produktID)
        {
            for (int i = 0; i < LagerProdukter.Count; i++)
            {
                if (LagerProdukter[i].ProduktID == produktID)
                {
                    return LagerProdukter[i];
                }
            }
            return null;
        }
        private Produkt KollaOmProduktFinns(string produktNamn)
        {
            for (int i = 0; i < LagerProdukter.Count; i++)
            {
                if (LagerProdukter[i].ProduktNamn == produktNamn)
                {
                    return LagerProdukter[i];
                }
            }
            return null;
        }
    }
}
