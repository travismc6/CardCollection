using CardCollection.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CardCollection.Persistence.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CardSet>().HasData(new List<CardSet>());
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<CardSet> CardSets { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<CollectionCard> CollectionCards { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}
