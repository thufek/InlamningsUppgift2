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
            while (true)
            {
                Console.Clear();
                Console.WriteLine("KASSA\n1. Ny kund\n2. Adminverktyg\n3. Avsluta");
                string huvudMenyVal = Console.ReadLine();
                if (huvudMenyVal == "1")
                {
                    var kvitto = new Kvitto();
                    while (true)
                    {
                        Console.Clear();
                        kvitto.VisaKvitto();
                        Kvitto.VisaKommandon();
                        string kommando = Console.ReadLine();
                        var kommandoKoll = new KommandoKoll(kommando, lager, kvitto);
                        Console.WriteLine($"{kommandoKoll.Meddelande}");
                        if (kommandoKoll.Pay)
                        {
                            Thread.Sleep(2000);
                            break;
                        }
                        else if (kommandoKoll.Meddelande != null)
                        {
                            Thread.Sleep(2000);
                        }
                    }
                }
                else if (huvudMenyVal == "2")
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine($"ADMINVERKTYG\n1. Skapa produkt\n2. Redigera produkt\n3. Rea-sätt produkt\n4. Sök kvitto\n5. Tillbaka");
                        Console.Write("Menyval: ");
                        string adminMenyVal = Console.ReadLine();
                        if (adminMenyVal == "1")
                        {
                            Adminverktyg.SkapaNyLagerProdukt(lager);
                        }
                        else if (adminMenyVal == "2")
                        {
                            Adminverktyg.RedigeraLagerProdukt(lager);
                        }
                        else if (adminMenyVal == "3")
                        {
                            Adminverktyg.ReaSättLagerProdukt(lager);
                        }
                        else if (adminMenyVal == "4")
                        {
                            Adminverktyg.SökKvitto();
                        }
                        else if (adminMenyVal == "5")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Ogiltig input!");
                            Thread.Sleep(2000);
                        }
                    }
                }
                else if (huvudMenyVal == "3")
                {
                    Console.WriteLine("Avslutar...");
                    Thread.Sleep(2000);
                    break;
                }
                else
                {
                    Console.WriteLine("Ogiltig input!");
                    Thread.Sleep(2000);
                }
            }
        }
    }
}
