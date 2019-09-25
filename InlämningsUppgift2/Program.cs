using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace InlämningsUppgift2
{
    class Program
    {
        static void Main(string[] args)
        {
            var lager = new Lager();
            IOfunktioner.LaddaInLagerProdukter(lager);
            while (true)
            {
                Console.Clear();
                Console.WriteLine("KASSA\n1. Ny kund\n2. Adminverktyg\n3. Avsluta");
                string huvudMenyVal = Console.ReadLine();
                if (huvudMenyVal == "1")
                {
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
                            Kundvagn.KvittoNummer++;
                            IOfunktioner.SkrivUtKvitto(kundvagn);
                            Thread.Sleep(3000);
                            break;
                        }
                        else if (kommandoKoll.RättKommando)
                        {
                            var lagerProdukt = lager.HämtaProdukt(kommandoKoll.ProduktID);
                            var nyProdukt = new Produkt(lagerProdukt);
                            kundvagn.LäggTillNyaProdukterIKundvagn(lagerProdukt, nyProdukt, kommandoKoll.Antal);
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
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("ADMINVERKTYG\n1. Lägg till ny produkt\n2. Redigera produkt\n3. Sök kvitto");
                        string adminMenyVal = Console.ReadLine();
                        if (adminMenyVal == "1")
                        {
                            var nyLagerProdukt = lager.SkapaNyProdukt();
                            if (lager.KollaOmProduktFinnsILager(nyLagerProdukt.ProduktID))
                            {
                                Console.WriteLine("Produkt med vald ID existerar redan! Lägger ej till ny produkt!");
                            }
                            else
                            {
                                lager.LäggTillProduktILager(nyLagerProdukt);
                                Console.WriteLine($"Lägger till {nyLagerProdukt.ProduktNamn} i lager!");
                            }
                            Thread.Sleep(3000);
                        }
                        else if(adminMenyVal == "2")
                        {
                            
                        }
                        else if (adminMenyVal =="3")
                        {
                            IOfunktioner.SökKvitto();
                        }
                        else
                        {
                            Console.WriteLine("Ogiltig inmatning. Välj 1, 2 eller 3!");
                            Thread.Sleep(2000);
                        }
                    }
                }
                else if (huvudMenyVal == "3")
                {
                    Console.WriteLine("Avslutar...");
                    IOfunktioner.SparaLagerProdukter(lager);
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
