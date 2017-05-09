using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using EFSamuari.Data;
using EFSamurai.Domain;
using Microsoft.EntityFrameworkCore;

namespace EFSamurai.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            ClearDatabase();
            AddOneSamurai("Prutt", 1, 2, "Prutt-san", Haircut.Western);
            AddSomeBattles();

            AddOneSamuraiWithRelatedData();
            Console.WriteLine("All samurais");
            ListAllSamuraiNames();
            Console.WriteLine();
            Console.WriteLine("All samurais ordered by name");
            ListAllSamuraiNames_OrderByName();
            Console.WriteLine();
            Console.WriteLine("All samurais ordered by id descending");
            ListAllSamuraiNames_OrderByIdDescending();
            //AddSomeSamurais();

            Console.WriteLine("\nDone!");
            Console.ReadLine();
        }

        private static void ListAllSamuraiNames_OrderByIdDescending()
        {
            using (var context = new SamuraiContext())
            {
                var samurais = context.Samurais;

                var orederedSamurais = samurais.OrderByDescending(x => x.Id);
                foreach (var samurai in orederedSamurais)
                {
                    Console.WriteLine(samurai.Id + ", " + samurai.Name);
                }
            }
        }

        private static void ListAllSamuraiNames_OrderByName()
        {
            using (var context = new SamuraiContext())
            {
                var samurais = context.Samurais;

                var orederedSamurais = samurais.OrderBy(x => x.Name);
                foreach (var samurai in orederedSamurais)
                {
                    Console.WriteLine(samurai.Id + ", " + samurai.Name);
                }
            }
        }

        private static void ListAllSamuraiNames()
        {
            using (var context = new SamuraiContext())
            {
                var samurais = context.Samurais;

                foreach (var samurai in samurais)
                {
                    Console.WriteLine(samurai.Id + ", " + samurai.Name);
                }
            }
        }

        private static void AddOneSamuraiWithRelatedData()
        {
            var samurai = new Samurai()
            {
                Name = "Greta",
                Agility = 2,
                Strength = 4,
                Alias = new SecretIdentity {RealName = "Gretsson"},
                Haircut = Haircut.Chonmage
            };
            var samuraiQuote = new Quote
            {
                IsFunny = false,
                Samurai = samurai,
                Text = "Bra väder idag",
                Type = QuoteType.Lame
            };
            samurai.Quotes.Add(samuraiQuote);

            var battleEvent = new BattleEvent { Description = "Battle SamuraiRelatedData Event", EventSummary = "Everyone dies", Time = DateTime.UtcNow };
            var battleLog = new BattleLog { Name = "Battle SamuraiRelatedData log" };
            battleLog.Events.Add(battleEvent);
            var battle = new Battle { BattleLog = battleLog };
            var samuraiBattleOne = new SamuraiBattles { Battle = battle, Samurai = samurai};

            using (var context = new SamuraiContext())
            {
                context.Add(samurai);
                context.Battles.Add(battle);
                context.SamuraiBattles.Add(samuraiBattleOne);
                context.Quotes.Add(samuraiQuote);
                context.SaveChanges();
            }
        }

        private static void ClearDatabase()
        {
            using (var context = new SamuraiContext())
            {
                context.Database.ExecuteSqlCommand("DELETE FROM  [dbo].[BattleEvent]");
                context.Database.ExecuteSqlCommand("DELETE FROM  [dbo].[Samurais]");
                context.Database.ExecuteSqlCommand("DELETE FROM  [dbo].[Battles]");
                context.Database.ExecuteSqlCommand("DELETE FROM  [dbo].[BattleLog]");
                context.Database.ExecuteSqlCommand("DELETE FROM  [dbo].[Quotes]");
                context.Database.ExecuteSqlCommand("DELETE FROM  [dbo].[SamuraiBattles]");
                context.Database.ExecuteSqlCommand("DELETE FROM  [dbo].[SecretIdentity]");
                context.SaveChanges();
            }
        }

        private static void AddSomeBattles()
        {
            var battlesToAdd = new List<Battle>();
            var battleOneEvents = new BattleEvent {Description = "Battle One Event", EventSummary = "Everyone dies", Time = DateTime.UtcNow};
            var battleOneLog = new BattleLog { Name = "Battle one log"};
            battleOneLog.Events.Add(battleOneEvents);
            var battleOne = new Battle { BattleLog = battleOneLog };
            var samuraiOne = AddOneSamurai("Daniel", 1, 2, "Daniel-san", Haircut.Western);
            var samuraiBattleOne = new SamuraiBattles {Battle = battleOne, SamuraiId = samuraiOne.Id };


            var battleTwoEvents = new BattleEvent { Description = "Battle Two Event", EventSummary = "Noone dies", Time = DateTime.UtcNow.AddDays(2) };
            var battleTwoLog = new BattleLog { Name = "Battle two log" };
            battleOneLog.Events.Add(battleTwoEvents);
            var battleTwo = new Battle { BattleLog = battleTwoLog };
            var samuraiTwo = AddOneSamurai("Sven", 1, 2, "Svenne-san", Haircut.Oicho);
            var samuraiBattleTwo = new SamuraiBattles { Battle = battleTwo, SamuraiId = samuraiTwo.Id };
            battlesToAdd.Add(battleOne);
            battlesToAdd.Add(battleTwo);
            


            using (var context = new SamuraiContext())
            {
                context.Battles.AddRange(battlesToAdd);
                context.SamuraiBattles.Add(samuraiBattleOne);
                context.SamuraiBattles.Add(samuraiBattleTwo);
                context.SaveChanges();
            }
        }

        private static void AddSomeSamurais()
        {
            var samuraisToAdd = new List<Samurai>();
            Samurai samurai = new Samurai();
            bool keepRunning = true;
            bool samuraiExists = false;

            while (keepRunning)
            {
                Console.Write("Enter name: ");
                var input = Console.ReadLine();
                samurai.Name = input;
                Console.Write("Enter agility: ");
                input = Console.ReadLine();
                samurai.Agility = Convert.ToInt32(input);
                Console.Write("Enter strength: ");
                input = Console.ReadLine();
                samurai.Strength = Convert.ToInt32(input);
                Console.Write("Enter secret identity: ");
                input = Console.ReadLine();
                samurai.Alias = new SecretIdentity { RealName = input};
                samuraisToAdd.Add(samurai);
                Console.Write("Add one more?(Y/N) ");
                var consoleKey = Console.ReadKey();


                using (var context = new SamuraiContext())
                {
                    samuraiExists = context.Samurais.Any(x => x.Name == samurai.Name);
                }

                switch (consoleKey.Key)
                {
                    case ConsoleKey.Y:
                        Console.WriteLine();
                        continue;
                    case ConsoleKey.N:
                        keepRunning = false;
                        break;
                    default:
                        break;
                }
            }

            if (!samuraiExists)
            {
                using (var context = new SamuraiContext())
                {
                    context.Samurais.AddRange(samuraisToAdd);
                    context.SaveChanges();
                }
            }

        }

        private static Samurai AddOneSamurai(string name, int agility, int strength, string alias, Haircut haircut)
        {
            var secretIdentity = new SecretIdentity { RealName = alias };

            var samurai = new Samurai
            {
                Name = name, Agility = agility, Strength = strength, Alias = secretIdentity, Haircut = haircut
            };
            using (var context = new SamuraiContext())
            {
                context.Samurais.Add(samurai);
                context.SaveChanges();
            }
            return samurai;
        }
    }
}