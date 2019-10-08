using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlämningsUppgift2
{
    class KommandoKoll
    {
        public string Meddelande { get; private set; }
        public bool Pay { get; private set; }
        public KommandoKoll(string kommandoInput, Lager lager, Kvitto kvitto)
        {
            KommandoStringParse(kommandoInput, lager, kvitto);
        }
        private void KommandoStringParse(string kommandoInput, Lager lager, Kvitto kvitto) //Validerar kommando input och kollar om produkt id'n existerar i lager
        {
            string[] splittad = kommandoInput.Split(' ');
            int produktid;
            int antal;
            if (splittad.Length == 2 && int.TryParse(splittad[0], out produktid) && int.TryParse(splittad[1], out antal))
            {
                if (!lager.ProduktFinnsILager(produktid))
                {
                    Meddelande = "Produkten existerar ej!";
                }
                else if (antal < 1)
                {
                    Meddelande = "Antal måste vara 1 eller mer!";
                }
                else
                {
                    var nyProdukt = new Produkt(lager.HämtaLagerProdukt(produktid));
                    if (!kvitto.ProduktFinnsIKvitto(nyProdukt.ID) && !nyProdukt.AntalBlirÖverMax(antal))
                    {
                        nyProdukt.Antal = antal;
                        kvitto.Produkter.Add(new Produkt(nyProdukt));
                    }
                    else if (kvitto.ProduktFinnsIKvitto(nyProdukt.ID))
                    {
                        var kvittoProdukt = kvitto.HämtaProduktIKvitto(nyProdukt.ID);
                        if (!kvittoProdukt.AntalBlirÖverMax(antal))
                        {
                            kvittoProdukt.Antal += antal;
                        }
                        else
                        {
                            kvittoProdukt.Antal = kvittoProdukt.MaxAntal;
                            Meddelande = $"Max {kvittoProdukt.MaxAntal} {kvittoProdukt.HämtaPrisTyp()} per kund!";
                        }
                    }
                    else
                    {
                        nyProdukt.Antal = nyProdukt.MaxAntal;
                        kvitto.Produkter.Add(new Produkt(nyProdukt));
                        Meddelande = $"Max {nyProdukt.MaxAntal} {nyProdukt.HämtaPrisTyp()} per kund!";
                    }
                }
            }
            else if (splittad.Length == 2 && splittad[0] == "RETURN" && int.TryParse(splittad[1], out produktid))
            {
                if (!kvitto.ProduktFinnsIKvitto(produktid))
                {
                    Meddelande = "Produkt finns ej i kvitto";
                }
                else
                {
                    var kvittoProdukt = kvitto.HämtaProduktIKvitto(produktid);
                    kvitto.TaBortProdukt(kvittoProdukt);
                }
            }
            else if (kommandoInput == "PAY")
            {
                if (kvitto.Produkter.Count < 1)
                {
                    Meddelande = "Lägg till produkter först";
                }
                else
                {
                    Pay = true;
                    IOFunktioner.SkrivUtKvittoTillFil(kvitto);
                    Meddelande = "Skriver ut kvitto...";
                }
            }
            else
            {
                Meddelande = "Ogiltigt kommando!";
            }
        }
    }
}
