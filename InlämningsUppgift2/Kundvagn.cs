using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlämningsUppgift2
{
    class Kundvagn
    {
        public List<Produkt> Produkter { get; set; }
        public int AntalAvProdukt { get; set; }
        public int TotaltAntalProdukter { get; set; }
        public decimal TotalPris { get; set; }
        public Kundvagn()
        {
            Produkter = new List<Produkt>();
        }
        public void RäknaTotalPris()
        {
            for (int i = 0; i < Produkter.Count; i++)
            {
                TotalPris += Produkter[i].ProduktPris;
            }
        }
    }
}
