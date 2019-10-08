using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace InlämningsUppgift2
{
    static class Adminverktyg
    {
        public static void SökKvitto()
        {
            DateTime datum;
            string kvittoInnehåll;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Sök kvitto");
                Console.Write("Mata in datum på kvitto i detta format (ÅÅÅÅ-MM-DD): ");
                if (DateTime.TryParse(Console.ReadLine(), out datum))
                {
                    kvittoInnehåll = IOFunktioner.HämtaKvittoText(datum);
                    if (kvittoInnehåll == null)
                    {
                        Meddelande("Hittade ej kvitto!");
                        break;
                    }
                    else
                    {
                        string[] kvitton = kvittoInnehåll.Split('#');
                        int kvittoNummer;
                        Console.WriteLine("Hittade kvitton!");
                        for (int i = 1; i < kvitton.Length; i++)
                        {
                            string[] splittadkvitto = kvitton[i].Split('¨');
                            decimal pris = Convert.ToDecimal(splittadkvitto[2]);
                            Console.WriteLine($"Kvitto: {splittadkvitto[0]} ----- Total: {pris,0:C}");
                        }
                        while (true)
                        {
                            Console.Write("Vilket kvitto vill du se? Mata in kvittonummer!: ");
                            if (int.TryParse(Console.ReadLine(), out kvittoNummer))
                            {

                                if (kvittoNummer > 0 && kvittoNummer < kvitton.Length)
                                {
                                    string[] splittadkvitto = kvitton[kvittoNummer].Split('¨');
                                    decimal pris = Convert.ToDecimal(splittadkvitto[2]);
                                    Console.Write(splittadkvitto[0].Replace('¨', ' '));
                                    Console.Write(splittadkvitto[1].Replace('¨', ' '));
                                    Console.WriteLine($" {pris,0:C}");
                                    Console.ReadLine();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Kvittonummer finns ej!");
                                    continue;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Kvittonummer finns ej!");
                                continue;
                            }
                        }
                        break;
                    }
                }
                else
                {
                    Meddelande("Ogiltig input! Mata in datum enligt format!");
                }
            }
        }
        public static void SkapaNyLagerProdukt(Lager lager)
        {
            Console.Clear();
            Console.WriteLine("Skapa produkt");
            int id = MataInNyId(lager);
            string namn = MataInNamn(lager);
            decimal pris = MataInOrginalPris();
            string prisTyp = MataInPrisTyp();
            if (prisTyp == "st")
            {
                lager.Produkter.Add(new Produkt(id, namn, Produkt.Typ.St, pris));
            }
            else if (prisTyp == "kg")
            {
                lager.Produkter.Add(new Produkt(id, namn, Produkt.Typ.Kg, pris));
            }
            Meddelande($"Lägger till produk     ID: {id} Namn: {namn} Pris: {pris} {prisTyp}");
            IOFunktioner.SparaLagerProdukterIFil(lager.Produkter, Lager.path);
        }
        private static int MataInNyId(Lager lager)
        {
            int id;
            while (true)
            {
                Console.Write("Mata in ID: ");
                if (int.TryParse(Console.ReadLine(), out id))
                {
                    if (lager.ProduktFinnsILager(id))
                    {
                        Console.WriteLine($"Det finns redan en produkt med ID: {id}");
                        continue;
                    }
                    else if (id < 1)
                    {
                        Console.WriteLine($"Ogiltigt ID");
                        continue;
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Ogiltig inmatning!");
                }
            }
            return id;
        }
        private static string MataInNamn(Lager lager)
        {
            string namn;
            while (true)
            {
                Console.Write("Mata in produktnamn: ");
                namn = Console.ReadLine();
                if (lager.ProduktFinnsILager(namn))
                {
                    Console.WriteLine($"Det finns redan en produkt som heter: {namn}!");
                    continue;
                }
                else if (namn == "")
                {
                    Console.WriteLine($"Du måste mata in ett namn!");
                    continue;
                }
                break;
            }
            return namn;
        }
        private static string MataInPrisTyp()
        {
            string prisTyp;
            while (true)
            {
                Console.Write("Mata in pristyp (kg eller st): ");
                prisTyp = Console.ReadLine();
                if (prisTyp == "kg" || prisTyp == "st")
                {
                    break;
                }
                Console.WriteLine("Ogiltig pristyp!");
            }
            return prisTyp;
        }
        private static decimal MataInOrginalPris()
        {
            decimal pris;
            while (true)
            {
                Console.Write("Mata in pris: ");
                if (decimal.TryParse(Console.ReadLine(), out pris))
                {
                    if (pris < 1)
                    {
                        Console.WriteLine("Priset kan ej vara mindre än 1!");
                        continue;
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Ogiltig inmatning!");
                }
            }
            return pris;
        }
        public static void RedigeraLagerProdukt(Lager lager)
        {
            int id;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Redigera produkt");
                lager.ListaLagerProdukter();
                Console.Write("Välj produkt <id> : ");
                if (!int.TryParse(Console.ReadLine(), out id) || !lager.ProduktFinnsILager(id))
                {
                    Meddelande("Denna produkt id finns ej!");
                    continue;
                }
                var produkt = lager.HämtaLagerProdukt(id);
                Console.WriteLine($"Redigera produkt med id: {produkt.ID} ");
                produkt.BytNamnPåProdukt(MataInNamn(lager));
                produkt.BytPrisPåProdukt(MataInOrginalPris());
                Meddelande($"ID: {produkt.ID} heter nu {produkt.Namn} och kostar {produkt.OrginalPris}");
                IOFunktioner.SparaLagerProdukterIFil(lager.Produkter, Lager.path);
                break;
            }
        }
        public static void ReaSättLagerProdukt(Lager lager)
        {
            int id;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Reasätt produkt\nListar alla produkter som inte är rea: ");
                lager.ListaLagerProdukterSomEjÄrRea();
                Console.Write("Välj produkt <id> : ");
                if (!int.TryParse(Console.ReadLine(), out id) || !lager.ProduktFinnsILager(id))
                {
                    Meddelande("Denna produkt id finns ej!");
                    continue;
                }
                var produkt = lager.HämtaLagerProdukt(id);
                Console.WriteLine($"Reasätter produkt med id: {produkt.ID} ");
                MataInReaStart(produkt);
                MataInReaSlut(produkt);
                MataInReaPris(produkt);
                MataInMaxAntal(produkt);
                Meddelande($"{produkt.Namn} är nu reasatt med start: {produkt.ReaStart.Date} och slut {produkt.ReaSlut.Date} och reapris på {produkt.ReaPris}");
                IOFunktioner.SparaLagerProdukterIFil(lager.Produkter, Lager.path);
                break;
            }
        }
        private static void MataInMaxAntal(Produkt produkt)
        {
            int maxAntal;
            while (true)
            {
                Console.Write("Mata in begränsad antal per kund (0 ifall ingen begränsning): ");
                if (!int.TryParse(Console.ReadLine(), out maxAntal))
                {
                    Console.WriteLine("Ogiltig inmatning! Mata in en siffra!");
                    continue;
                }
                else if (maxAntal < 0)
                {
                    Console.WriteLine("Begränsat antal kan ej vara mindre än 0!");
                    continue;
                }
                produkt.SättMaxAntal(maxAntal);
                break;
            }
        }
        private static void MataInReaPris(Produkt produkt)
        {
            decimal reaPris;
            while (true)
            {
                Console.Write($"Orginalpris på denna vara är: {produkt.OrginalPris} Mata in reapris: ");
                if (!decimal.TryParse(Console.ReadLine(), out reaPris))
                {
                    Console.WriteLine("Ogiltig inmatning!");
                    continue;
                }
                else if (reaPris >= produkt.OrginalPris)
                {
                    Console.WriteLine("Reapriset kan inte vara högre än orginalpriset!");
                    continue;
                }
                else if (reaPris < 1)
                {
                    Console.WriteLine("Reapriset kan inte vara mindre än 1!");
                    continue;
                }
                produkt.SättReaPris(reaPris);
                break;
            }
        }
        private static void MataInReaStart(Produkt produkt)
        {
            DateTime reaStart;
            while (true)
            {
                Console.Write("Mata in startdatum för rea i detta format (ÅÅÅÅ-MM-DD): ");
                if (DateTime.TryParse(Console.ReadLine(), out reaStart))
                {
                    produkt.SättReaStart(reaStart);
                    break;
                }
                Console.WriteLine("Ogiltig input! Mata in datum enligt format!");
            }
        }
        private static void MataInReaSlut(Produkt produkt)
        {
            DateTime reaSlut;
            while (true)
            {
                Console.Write("Mata in slutdatum för rea i detta format (ÅÅÅÅ-MM-DD): ");
                if (DateTime.TryParse(Console.ReadLine(), out reaSlut))
                {
                    if (reaSlut < produkt.ReaStart)
                    {
                        Console.WriteLine("Slutdatumet för rea kan inte vara före startdatum!");
                        continue;
                    }
                    produkt.SättReaSlut(reaSlut);
                    break;
                }
                Console.WriteLine("Ogiltig input! Mata in datum enligt format!");
            }
        }
        private static void Meddelande(string meddelande)
        {
            Console.WriteLine(meddelande);
            Thread.Sleep(2000);
        }
    }
}
