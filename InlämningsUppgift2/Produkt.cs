using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlämningsUppgift2
{
    class Produkt
    {
        public enum Typ
        {
            None,
            Kg,
            St
        }
        public int ID { get; private set; }
        public string Namn { get; private set; }
        public int Antal { get; set; }
        public int MaxAntal { get; private set; }
        public Typ PrisTyp { get; private set; }
        public decimal TotalPris { get; private set; }
        public decimal OrginalPris { get; private set; }
        public decimal ReaPris { get; private set; }
        public bool Rea { get; private set; }
        public DateTime ReaStart { get; private set; }
        public DateTime ReaSlut { get; private set; }
        public Produkt(int id, string namn, Typ prisTyp, decimal pris)
        {
            ID = id;
            Namn = namn;
            PrisTyp = prisTyp;
            OrginalPris = pris;
        }
        public Produkt(int id, string namn, int maxAntal, Typ prisTyp, decimal orginalPris, decimal reaPris, DateTime reaStart, DateTime reaSlut)
        {
            ID = id;
            Namn = namn;
            MaxAntal = maxAntal;
            PrisTyp = prisTyp;
            OrginalPris = orginalPris;
            ReaPris = reaPris;
            ReaStart = reaStart;
            ReaSlut = reaSlut;
            KollaOmRea();
        }
        public Produkt(Produkt produkt)
        {
            ID = produkt.ID;
            Namn = produkt.Namn;
            Antal = produkt.Antal;
            MaxAntal = produkt.MaxAntal;
            PrisTyp = produkt.PrisTyp;
            TotalPris = produkt.TotalPris;
            OrginalPris = produkt.OrginalPris;
            ReaPris = produkt.ReaPris;
            Rea = produkt.Rea;
            ReaStart = produkt.ReaStart;
            ReaSlut = produkt.ReaSlut;
            KollaOmRea();
        }
        public void RäknaTotalPris()
        {
            if (Rea)
            {
                TotalPris = ReaPris * Antal;
            }
            else
            {
                TotalPris = OrginalPris * Antal;
            }
        }
        public void SättMaxAntal(int maxAntal)
        {
            if (maxAntal > 0)
            {
                MaxAntal = maxAntal;
            }
        }
        public void SättReaStart(DateTime reaStart)
        {
            ReaStart = reaStart;
        }
        public void SättReaSlut(DateTime reaSlut)
        {
            ReaSlut = reaSlut;
            KollaOmRea();
        }
        private void KollaOmRea()
        {
            if (DateTime.Now.Date >= ReaStart.Date && DateTime.Now.Date <= ReaSlut.Date && ReaPris > 0)
            {
                Rea = true;
            }
            else
            {
                Rea = false;
            }
            RäknaTotalPris();
        }
        public string HämtaPrisTyp()
        {
            string prisTyp;
            if (PrisTyp == Typ.Kg)
            {
                prisTyp = "kg";
            }
            else if (PrisTyp == Typ.St)
            {
                prisTyp = "st";
            }
            else
            {
                return null;
            }
            return prisTyp;
        }
        public bool AntalBlirÖverMax(int antal)
        {
            if (MaxAntal != 0 && (antal + Antal) > MaxAntal)
            {
                return true;
            }
            return false;
        }
        public void BytNamnPåProdukt(string nyttNamn)
        {
            Namn = nyttNamn;
        }
        public void BytPrisPåProdukt(decimal nyttPris)
        {
            OrginalPris = nyttPris;
        }
        public void SättReaPris(decimal reaPris)
        {
            ReaPris = reaPris;
        }
    }
}
