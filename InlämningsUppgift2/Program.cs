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
            lager.LagerProdukter.Add(new Produkt(100, "Snus", 55.90m, 0, Produkt.PrisTyp.krSt));
            lager.LagerProdukter.Add(new Produkt(200, "Öl", 9.90m, 0, Produkt.PrisTyp.krSt));
            lager.LagerProdukter.Add(new Produkt(300, "Ferrari", 1995000m, 0, Produkt.PrisTyp.krSt));
            lager.LagerProdukter.Add(new Produkt(400, "Kaststjärna", 149.90m, 0, Produkt.PrisTyp.krSt));
            lager.LagerProdukter.Add(new Produkt(500, "Kebab", 69.90m, 0, Produkt.PrisTyp.krKg));
            while (true)
            {
                Console.Clear();
                Console.WriteLine("KASSA\n1. Ny kund\n2. Adminverktyg\n3. Avsluta");
                string huvudMenyVal = Console.ReadLine();
                if (huvudMenyVal == "1")
                {
                    Console.Clear();
                    var kundvagn = new Kundvagn();
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine($"KASSA\nKVITTO    {kundvagn.DatumKvitto}");
                        kundvagn.ListaAllaProdukterIKundvagn();
                        kundvagn.RäknaTotalPris();
                        Console.WriteLine($"Total: {kundvagn.TotalPris}\nkommandon:\n<produkt id> <antal>\nPAY");
                        string kommando = Console.ReadLine();
                        var kommandoKoll = new KommandoCheck(kommando, lager.LagerProdukter); 
                        if (kommando == "PAY")
                        {
                            if (kundvagn.Produkter.Count < 1)
                            {
                                Console.WriteLine("Inga varor inlagda! Skriver ej ut kvitto!");
                                Thread.Sleep(2000);
                                continue;
                            }
                            Console.WriteLine("Skriver ut kvitto...");
                            Thread.Sleep(3000);
                            break;
                        }           
                        else if (kommandoKoll.RättKommando)
                        {
                            var lagerProdukt = lager.HämtaProdukt(kommandoKoll.ProduktID);
                            var nyProdukt = new Produkt(lagerProdukt.ProduktID, lagerProdukt.ProduktNamn, 0, 0, lagerProdukt.ProduktPrisTyp);
                            for (int i = 0; i < kommandoKoll.Antal; i++)
                            {
                                nyProdukt.ProduktAntal++;
                                nyProdukt.ProduktPris += lagerProdukt.ProduktPris;
                                nyProdukt.ProduktReaPris += lagerProdukt.ProduktReaPris;
                            }
                            bool produktFinns = false;
                            for (int i = 0; i < kundvagn.Produkter.Count; i++)
                            {
                                if (kundvagn.Produkter[i].ProduktID == nyProdukt.ProduktID)
                                {
                                    kundvagn.Produkter[i].ProduktAntal += nyProdukt.ProduktAntal;
                                    kundvagn.Produkter[i].ProduktPris += nyProdukt.ProduktPris;
                                    kundvagn.Produkter[i].ProduktReaPris += nyProdukt.ProduktReaPris;
                                    produktFinns = true;
                                }
                            }
                            if (produktFinns == false)
                            {
                                kundvagn.Produkter.Add(nyProdukt);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"{kommandoKoll.Meddelande}");
                            Thread.Sleep(2000);
                        }
                    }
                }
                else if (huvudMenyVal == "2")
                {

                }
                else if (huvudMenyVal == "3")
                {
                    Console.WriteLine("Avslutar...");
                    Thread.Sleep(2000);
                    break;
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
