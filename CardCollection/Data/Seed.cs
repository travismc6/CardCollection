using CardCollection.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardCollection.Data
{
    public class Seed
    {
        private readonly DataContext _context;
        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedUsers()
        {
            if (!_context.CardSets.Any())
            {
                var json = System.IO.File.ReadAllText("Files/1951Bowman.json");
                var cardSet = JsonConvert.DeserializeObject<CardSet>(json);
                _context.CardSets.Add(cardSet);

                json = System.IO.File.ReadAllText("Files/1952Bowman.json");
                cardSet = JsonConvert.DeserializeObject<CardSet>(json);
                _context.CardSets.Add(cardSet);

                json = System.IO.File.ReadAllText("Files/1953BowmanColor.json");
                cardSet = JsonConvert.DeserializeObject<CardSet>(json);
                _context.CardSets.Add(cardSet);

                json = System.IO.File.ReadAllText("Files/1953BowmanBW.json");
                cardSet = JsonConvert.DeserializeObject<CardSet>(json);
                _context.CardSets.Add(cardSet);

                json = System.IO.File.ReadAllText("Files/1954Bowman.json");
                cardSet = JsonConvert.DeserializeObject<CardSet>(json);
                _context.CardSets.Add(cardSet);

                json = System.IO.File.ReadAllText("Files/1955Bowman.json");
                cardSet = JsonConvert.DeserializeObject<CardSet>(json);
                _context.CardSets.Add(cardSet);

                json = System.IO.File.ReadAllText("Files/1953Topps.json");
                cardSet = JsonConvert.DeserializeObject<CardSet>(json);
                _context.CardSets.Add(cardSet);

                json = System.IO.File.ReadAllText("Files/1954Topps.json");
                cardSet = JsonConvert.DeserializeObject<CardSet>(json);
                _context.CardSets.Add(cardSet);


                json = System.IO.File.ReadAllText("Files/1955Topps.json");
                cardSet = JsonConvert.DeserializeObject<CardSet>(json);
                _context.CardSets.Add(cardSet);


                json = System.IO.File.ReadAllText("Files/1956Topps.json");
                cardSet = JsonConvert.DeserializeObject<CardSet>(json);
                _context.CardSets.Add(cardSet);


                json = System.IO.File.ReadAllText("Files/1957Topps.json");
                cardSet = JsonConvert.DeserializeObject<CardSet>(json);
                _context.CardSets.Add(cardSet);


                json = System.IO.File.ReadAllText("Files/1958Topps.json");
                cardSet = JsonConvert.DeserializeObject<CardSet>(json);
                _context.CardSets.Add(cardSet);


                json = System.IO.File.ReadAllText("Files/1959Topps.json");
                cardSet = JsonConvert.DeserializeObject<CardSet>(json);
                _context.CardSets.Add(cardSet);


                json = System.IO.File.ReadAllText("Files/1964Topps.json");
                cardSet = JsonConvert.DeserializeObject<CardSet>(json);
                _context.CardSets.Add(cardSet);


                json = System.IO.File.ReadAllText("Files/1966Topps.json");
                cardSet = JsonConvert.DeserializeObject<CardSet>(json);
                _context.CardSets.Add(cardSet);

                _context.SaveChanges();
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
