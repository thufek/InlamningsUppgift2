using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlämningsUppgift2
{
    class KommandoCheck
    {
        public bool RättKommando { get; private set; }
        public int ProduktID { get; private set; }
        public int Antal { get; private set; }
        public string Meddelande { get; private set; }
        public string KommandoInput { get; private set; }
        public List<Produkt> ProduktLista { get; private set; }
        public KommandoCheck(string kommandoInput, List<Produkt> produktLista)
        {
            KommandoInput = kommandoInput;
            ProduktLista = new List<Produkt>();
            ProduktLista = produktLista;
            KommandoStringParse();
        }
        private void KommandoStringParse() //Validerar kommando input och kollar om produkt id'n existerar i lager
        {
            string[] splittad = KommandoInput.Split(' ');
            int produktid;
            int antal;
            bool hittadeProdukt = false;

            if (splittad.Length == 2 && int.TryParse(splittad[0], out produktid) && int.TryParse(splittad[1], out antal))
            {
                for (int i = 0; i < ProduktLista.Count && antal > 0; i++)
                {
                    if (ProduktLista[i].ProduktID == produktid)
                    {
                        ProduktID = produktid;
                        Antal = antal;
                        RättKommando = true;
                        hittadeProdukt = true;
                    }
                }
                if (hittadeProdukt == false)
                {
                    Meddelande = "Hittade ej produkt. Testa annat ID!";
                    RättKommando = false;
                }
            }
            else
            {
                RättKommando = false;
                Meddelande = "Ogiltigt kommando!";
            }
        }
    }
}
