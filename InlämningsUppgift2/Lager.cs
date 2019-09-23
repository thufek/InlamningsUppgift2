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
        public Produkt HämtaProdukt(int produktID)
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
        public bool KollaOmProduktFinnsILager(int produktID)
        {
            for (int i = 0; i < LagerProdukter.Count; i++)
            {
                if (LagerProdukter[i].ProduktID == produktID)
                {
                    return true;
                }
            }
            return false;
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
        public Produkt SkapaNyProdukt()
        {
            Produkt nyProdukt;
            int id = 0;
            string namn = "";
            decimal pris = 0;
            string prisTyp = "";
            bool okPrisTyp = false;
            do
            {
                if (id == 0)
                {
                    while (true)
                    {
                        Console.Write("Mata in ID: ");
                        if (int.TryParse(Console.ReadLine(), out id))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Ogiltig inmatning!");
                        }
                    }
                }
                if (namn == "")
                {
                    Console.Write("Mata in produktnamn: ");
                    namn = Console.ReadLine();
                }
                if (pris == 0)
                {
                    while (true)
                    {
                        Console.Write("Mata in pris: ");
                        if (decimal.TryParse(Console.ReadLine(), out pris))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Ogiltig inmatning!");
                        }
                    }

                }
                if (!okPrisTyp)
                {
                    Console.Write("Mata in pristyp (kg eller st)");
                    prisTyp = Console.ReadLine();
                    if (prisTyp == "kg" || prisTyp == "st")
                    {
                        okPrisTyp = true;
                    }
                }
            } while (id == 0 || namn == "" || pris == 0 || okPrisTyp == false);
            if (prisTyp == "st")
            {
                return nyProdukt = new Produkt(id, namn, pris, Produkt.PrisTyp.st);
            }
            else if (prisTyp == "kg")
            {
                return nyProdukt = new Produkt(id, namn, pris, Produkt.PrisTyp.kg);
            }

            return null;

        }

    }
}
