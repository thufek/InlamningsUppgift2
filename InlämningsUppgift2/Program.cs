using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace InlämningsUppgift2
{
    class Program
    {
        static void Main(string[] args)
        {
            var lager = new Lager();
            lager.LagerProdukter.Add(new Produkt(100, "Snus", 55.90m, Produkt.PrisTyp.krSt));
            lager.LagerProdukter.Add(new Produkt(200, "Öl", 9.90m, Produkt.PrisTyp.krSt));
            lager.LagerProdukter.Add(new Produkt(300, "Ferrari", 1995000m, Produkt.PrisTyp.krSt));
            lager.LagerProdukter.Add(new Produkt(400, "Kaststjärna", 149.90m, Produkt.PrisTyp.krSt));
            lager.LagerProdukter.Add(new Produkt(500, "Kebab", 69.90m, Produkt.PrisTyp.krSt));
            while (true)
            {
                Console.WriteLine("KASSA\n1. Ny kund\n2. Adminverktyg\n3. Avsluta");
                string huvudMenyVal = Console.ReadLine();
                if (huvudMenyVal == "1")
                {
                    Console.Clear();
                    var kundvagn = new Kundvagn();
                    while (true)
                    {
                        Console.WriteLine($"KASSA\nKVITTO    {kundvagn.DatumKvitto}");
                        kundvagn.ListaAllaProdukterIKundvagn();
                        kundvagn.RäknaTotalPris();
                        Console.WriteLine($"Total: {kundvagn.TotalPris}");
                        Console.WriteLine("kommandon:\n<produkt id> <antal>\nPAY");
                        string kommando = Console.ReadLine();
                        var kommandoKoll = new KommandoCheck(kommando, lager.LagerProdukter); 
                        if (kommando == "PAY")
                        {
                            Console.WriteLine("Skriver ut kvitto...");
                            Thread.Sleep(3000);
                        }           
                        else if (kommandoKoll.RättKommando)
                        {
                            var nyprodukt = lager.HämtaProdukt(kommandoKoll.ProduktID);
                            if (kundvagn.Produkter.Contains(nyprodukt))
                            {
                                kundvagn.Produkter.IndexOf(nyprodukt);
                            }
                            else
                            {
                                nyprodukt.ProduktAntal = kommandoKoll.Antal;
                                nyprodukt.ProduktPris *= kommandoKoll.Antal;
                                kundvagn.Produkter.Add(nyprodukt);
                            }

                        }
                        else
                        {
                            Console.WriteLine($"{kommandoKoll.Meddelande}");
                            Thread.Sleep(2000);
                        }
                        Console.Clear();
                    }
                }
                else if (huvudMenyVal == "2")
                {

                }
                else if (huvudMenyVal == "3")
                {

                }
                else
                {
                    Console.WriteLine("Ogiltig inmatning. Mata in 1, 2 eller 3!");
                    Thread.Sleep(2000);
                    Console.Clear();
                }
            }
        }
    }
}
