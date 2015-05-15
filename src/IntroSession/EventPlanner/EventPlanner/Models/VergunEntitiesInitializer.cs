using System;
using System.Collections.Generic;
using System.Linq;
using EventPlanner.Code.Infrastructure.EntityFramework;

namespace EventPlanner.Models
{
    public class VergunEntitiesInitializer : MyDropCreateDatabaseAlways<EvenementEntities>
    {
        private static Random _rnd;

        public override void InitializeDatabase(EvenementEntities context)
        {
            context.Database.ExecuteSqlCommand(
                string.Format("ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE",
                    context.Database.Connection.Database));

            base.InitializeDatabase(context);
        }

        protected override void Seed(EvenementEntities context)
        {
            _rnd = new Random();

            var straten = new List<Straat>();
            for (int i = 0; i < 100; i++)
            {
                Straat straat = CreateStraat(i);
                straten.Add(straat);
                context.Straten.Add(straat);
            }

            context.SaveChanges();

            for (int i = 0; i < 50; i++)
            {
                context.Evenementen.Add(CreateReservatie(i, straten));
            }

            context.SaveChanges();

            base.Seed(context);
        }

        private Straat CreateStraat(int straatId)
        {
            return new Straat
            {
                Postcode = "8200",
                Gemeente = LoremIpsum(1, 1),
                Id = straatId,
                Straatnaam = LoremIpsum(1, 2)
            };
        }

        private static Evenement CreateReservatie(int reservatieId, List<Straat> straten)
        {
            DateTime datumBegin = DateTime.Now.AddDays(reservatieId%10);
            return new Evenement
            {
                DatumBeslissing = datumBegin.AddMonths(-5),
                Organisatorid = 1,
                Id = reservatieId,
                Eigenaar = "GV",
                MuziekVergunning = true,
                Omschrijving = LoremIpsum(3, 10),
                Optie = true,
                Periodes = new List<Periode>
                {
                    CreatePeriode(reservatieId, straten),
                    CreatePeriode(reservatieId, straten)
                },
                Titel = LoremIpsum(2, 4)
            };
        }

        private static Periode CreatePeriode(int reservatieId, List<Straat> straten)
        {
            int number = _rnd.Next(0, 10);
            DateTime date = DateTime.Now.AddDays(- number + reservatieId);
            var periode = new Periode
            {
                BeginPeriode = date,
                EindePeriode = date.AddDays(number),
                Dagen = new List<Dag>
                {
                    new Dag
                    {
                        Beginuur = new TimeSpan(9, 0, 0),
                        Einduur = new TimeSpan(16, 0, 0),
                        Datum = date,
                        Opmerking = LoremIpsum(1, 3)
                    },
                    new Dag
                    {
                        Beginuur = new TimeSpan(9, 0, 0),
                        Einduur = new TimeSpan(16, 0, 0),
                        Datum = date.AddDays(2),
                        Opmerking = LoremIpsum(1, 3)
                    },
                },
                Opmerking = LoremIpsum(1, 3),
                Straten = new List<Straat>
                {
                    straten.ElementAt(_rnd.Next(0, straten.Count - 1)),
                    straten.ElementAt(_rnd.Next(0, straten.Count - 1)),
                    straten.ElementAt(_rnd.Next(0, straten.Count - 1))
                }
            };
            return periode;
        }

        private static string LoremIpsum(int minWords, int maxWords,
            int minSentences = 1, int maxSentences = 1)
        {
            var words = new[]
            {
                "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
                "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
                "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"
            };

            int numSentences = _rnd.Next(maxSentences - minSentences)
                               + minSentences + 1;
            int numWords = _rnd.Next(maxWords - minWords) + minWords + 1;

            string result = string.Empty;

            for (int s = 0; s < numSentences; s++)
            {
                for (int w = 0; w < numWords; w++)
                {
                    if (w > 0)
                    {
                        result += " ";
                    }
                    result += words[_rnd.Next(words.Length)];
                }
                result += " ";
            }

            return result;
        }
    }
}