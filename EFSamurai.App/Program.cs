using System;
using System.Collections.Generic;
using System.Linq;
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
            AddOneSamurai("Prutt", 1, 2, "Prutt-san", Haircut.Western, "Som man bäddar får man ligga", QuoteType.Cheesy);
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
            Console.WriteLine();
            Console.WriteLine("List all quotes of type 'Lame'");
            ListAllQuotesOfType(QuoteType.Lame);
            Console.WriteLine();
            Console.WriteLine("List all quotes of type 'Cheesy'");
            ListAllQuotesOfType(QuoteType.Cheesy);
            Console.WriteLine();
            Console.WriteLine("List all 'Cheesy' quotes with samurai");
            ListAllQuotesOfType_WithSamurai(QuoteType.Cheesy);
            Console.WriteLine();
            Console.WriteLine("List all 'Lame' quotes with samurai");
            ListAllQuotesOfType_WithSamurai(QuoteType.Lame);
            Console.WriteLine();
            Console.WriteLine("List all battles from 2012-03-03 to Today and is brutal");
            ListAllBattles(Convert.ToDateTime("2012-03-03"), DateTime.Now, true);
            Console.WriteLine();
            Console.WriteLine("List all battles from 2012-03-03 to Today and is not brutal");
            ListAllBattles(Convert.ToDateTime("2012-03-03"), DateTime.Now, false);
            Console.WriteLine();
            Console.WriteLine("List all battles from 2012-03-03 to Today");
            ListAllBattles(Convert.ToDateTime("2012-03-03"), DateTime.Now.AddDays(2), null);
            Console.WriteLine();
            Console.WriteLine("List all samurai Names with their alias");
            var namesWithAlias = AllSamuariNameWithAlias();
            DisplayList(namesWithAlias);
            Console.WriteLine();
            Console.WriteLine("List all battles with it's log book and events where IsBrutal = true");
            ListAllBattles_WithLog(DateTime.MinValue, DateTime.MaxValue, true);
            Console.WriteLine();
            Console.WriteLine("List all battles with it's log book and events where IsBrutal = false");
            ListAllBattles_WithLog(DateTime.MinValue, DateTime.MaxValue, false);
            Console.WriteLine();
            //TODO: Koppla ihop samurais med respektive battles
            Console.WriteLine("Get all samurais with the battles they are participating in");
            var samuraisWithBattles = GetSamuraiInfo();

            //AddSomeSamurais();

            Console.WriteLine("\nDone!");
            Console.ReadLine();
        }

        private static List<SamuraiInfo> GetSamuraiInfo()
        {
            var samuraiInfoList = new List<SamuraiInfo>();
            SamuraiInfo samuraiInfo = new SamuraiInfo();

            using (var context = new SamuraiContext())
            {
                var battles = from s in context.Samurais
                    join sb in context.SamuraiBattles
                    on s.Id equals sb.SamuraiId
                    join b in context.Battles
                    on sb.BattleId equals b.Id
                    select new
                    {
                        samuraiName = s.Name,
                        samuraiAlias = s.Alias.RealName,
                        battles = b.Name
                    };

                foreach (var thing in battles)
                {
                    samuraiInfo.Name = thing.samuraiName;
                    samuraiInfo.RealName = thing.samuraiAlias;
                    samuraiInfo.BattleNames = thing.battles;
                    Console.WriteLine($"Name: {samuraiInfo.Name}, Alias: {samuraiInfo.RealName}, Battles: {samuraiInfo.BattleNames}");
                }


                foreach (var samurai in context.Samurais.Include(s => s.Alias))
                {
                    samuraiInfo.Name = samurai.Name;
                    samuraiInfo.RealName = samurai.Alias.RealName;



                    //TODO: Koppla ihop samurais med respektive battles
                }
                return samuraiInfoList;
            }
        }

        private static void ListAllBattles_WithLog(DateTime from, DateTime to, bool isBrutal)
        {
            using (var context = new SamuraiContext())
            {
                if (isBrutal)
                {
                    foreach (
                        var battle in
                        context.Battles.Include(b => b.BattleLog.Events)
                            .Where(b => b.Time >= from && b.Time <= to && b.IsBrutal))
                    {
                        DisplayBattle(battle);
                    }
                }
                else
                {
                    foreach (
                        var battle in
                        context.Battles.Include(b => b.BattleLog.Events)
                            .Where(b => b.Time >= from && b.Time <= to && !b.IsBrutal))
                    {
                        DisplayBattle(battle);
                    }
                }

            }
        }

        private static void DisplayBattle(Battle battle)
        {
            Console.WriteLine($"Name of battle: {battle.Name}");

            Console.WriteLine($"Log name: {battle.BattleLog.Name}");
            foreach (var happening in battle.BattleLog.Events)
            {
                Console.WriteLine($"Event: {happening.EventSummary}");
            }
            Console.WriteLine();
        }

        private static void DisplayList(List<string> stringList)
        {
            foreach (var @string in stringList)
            {
                Console.WriteLine(@string);
            }
        }

        private static List<string> AllSamuariNameWithAlias()
        {
            using (var context = new SamuraiContext())
            {
                return context.Samurais.Select(s => $"{s.Name} alias {s.Alias.RealName}").ToList();
            }
        }

        private static void ListAllBattles(DateTime from, DateTime to, bool? isBrutal)
        {
            using (var context = new SamuraiContext())
            {
                var battles = context.Battles.Where(b => b.Time >= from && b.Time <= to);

                if (isBrutal == true)
                {
                    battles = battles.Where(b => b.IsBrutal);
                    foreach (var battle in battles)
                    {
                        Console.WriteLine($"'{battle.Name}' is a brutal battle within the period {from.ToString("20yy-MM-dd")} - {to.ToString("20yy-MM-dd")}.");
                    }
                }
                else if (isBrutal == false)
                {
                    battles = battles.Where(b => !b.IsBrutal);
                    foreach (var battle in battles)
                    {
                        Console.WriteLine($"'{battle.Name}' is not a brutal battle within the period {from.ToString("20yy-MM-dd")} - {to.ToString("20yy-MM-dd")}.");
                    }
                }
                else
                {
                    foreach (var battle in battles)
                    {
                        Console.WriteLine($"'{battle.Name}' is a battle within the period {from.ToString("20yy-MM-dd")} - {to.ToString("20yy-MM-dd")}.");
                    }
                }

            }
        }

        private static void ListAllQuotesOfType_WithSamurai(QuoteType quoteType)
        {
            List<Quote> quotes = null;
            using (var context = new SamuraiContext())
            {
                quotes = context.Quotes.ToList();
            }
            using (var context = new SamuraiContext())
            {

                var samurais = context.Samurais;

                foreach (var samurai in samurais)
                {
                    var samuraiQuotes = quotes;

                    foreach (var quote in samuraiQuotes)
                    {
                        if (quote.Type == quoteType && samurai.Id == quote.SamuraiId)
                        {
                            Console.WriteLine($"'{quote.Text}' is a {quoteType} quote by {samurai.Name}.");
                        }
                    }
                }
            }
        }

        private static void ListAllQuotesOfType(QuoteType quoteType)
        {
            using (var context = new SamuraiContext())
            {
                var cheesyQuotes = context.Quotes.Where(x => x.Type == quoteType);
                foreach (var quote in cheesyQuotes)
                {
                    Console.WriteLine(quote.Text);
                }
            }
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
            var battle = new Battle { BattleLog = battleLog, Time = DateTime.Now.Subtract(new TimeSpan(10)), IsBrutal = true, Name = "Battle 1"};
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

                context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('[dbo].[BattleEvent]', RESEED, 0)");
                context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('[dbo].[Samurais]', RESEED, 0)");
                context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('[dbo].[Battles]', RESEED, 0)");
                context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('[dbo].[BattleLog]', RESEED, 0)");
                context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('[dbo].[Quotes]', RESEED, 0)");
                context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('[dbo].[SecretIdentity]', RESEED, 0)");

                context.SaveChanges();
            }
        }

        private static void AddSomeBattles()
        {
            var battlesToAdd = new List<Battle>();
            var battleOneEvents = new BattleEvent {Description = "Battle One Event", EventSummary = "Everyone dies", Time = DateTime.UtcNow};
            var battleOneLog = new BattleLog { Name = "Battle one log"};
            battleOneLog.Events.Add(battleOneEvents);
            var battleOne = new Battle { BattleLog = battleOneLog, Time = DateTime.UtcNow.Subtract(new TimeSpan(20)), IsBrutal = false, Name = "Battle 2"};
            var samuraiOne = AddOneSamurai("Daniel", 1, 2, "Daniel-san", Haircut.Western);
            var samuraiBattleOne = new SamuraiBattles {Battle = battleOne, SamuraiId = samuraiOne.Id };


            var battleTwoEvents = new BattleEvent { Description = "Battle Two Event", EventSummary = "Noone dies", Time = DateTime.UtcNow.AddDays(2) };
            var battleTwoLog = new BattleLog { Name = "Battle two log" };
            battleOneLog.Events.Add(battleTwoEvents);
            var battleTwo = new Battle { BattleLog = battleTwoLog, Time = DateTime.UtcNow.AddDays(2), IsBrutal = true, Name = "Battle 3"};
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

        private static Samurai AddOneSamurai(string name, int agility, int strength, string alias, Haircut haircut, string quote, QuoteType quoteType)
        {
            var secretIdentity = new SecretIdentity { RealName = alias };

            var samurai = new Samurai
            {
                Name = name,
                Agility = agility,
                Strength = strength,
                Alias = secretIdentity,
                Haircut = haircut
            };
            samurai.Quotes.Add(new Quote{ IsFunny = false, Text = quote, Type = quoteType});
            using (var context = new SamuraiContext())
            {
                context.Samurais.Add(samurai);
                context.SaveChanges();
            }
            return samurai;
        }
    }
}