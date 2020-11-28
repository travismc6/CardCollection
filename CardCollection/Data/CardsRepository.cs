using CardCollection.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardCollection.Helpers;

namespace CardCollection.Data
{
    public class CardsRepository : ICardsRepository
    {
        private readonly DataContext _context;
        public CardsRepository(DataContext context)
        {
            _context = context;
        }


        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Card> GetCardById(int id)
        {
            return await _context.Cards.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<ICollection<Card>> GetCards(CardParams userParams)
        {
            var cards = _context.Cards.Include(x => x.CardSet).AsQueryable();

            if (userParams.Year != null)
            {
                cards = cards.Where(x => x.CardSet.Year == userParams.Year);
            }

            if (!String.IsNullOrEmpty(userParams.Brand))
            {
                cards = cards.Where(x => x.CardSet.Brand.ToLower() == userParams.Brand.ToLower());
            }

            if (!String.IsNullOrEmpty(userParams.Name))
            {
                cards = cards.Where(x => x.CardSet.Name.ToLower() == userParams.Name.ToLower());
            }

            return await cards.OrderBy(x => x.CardSet.Year)
                .ThenBy(x => x.CardSet.Brand)
                .ThenBy(x => x.Number).ToListAsync();
        }

        public async Task<Collection> GetCollection(int collectionId)
        {
            return await _context.Collections.Include(r => r.CollectionCards).FirstOrDefaultAsync(r => r.Id == collectionId);
        }

        public async Task<CollectionCard> GetCollectionCardById(int collectionId, int id)
        {
            return await _context.CollectionCards.Include(r => r.Card).Include(r => r.Card.CardSet).Include(r => r.Photos).FirstOrDefaultAsync(r => r.CollectionId == collectionId && r.Id == id);
        }

        public async Task<ICollection<CollectionCard>> GetCollectionCards(int collectionId, CardParams userParams)
        {
            var collectionCards = _context.CollectionCards.Where(r => r.CollectionId == collectionId).Include(r => r.Card).Include(r => r.Photos).Include(r => r.Card.CardSet).AsQueryable();

            if (userParams.Year != null)
            {
                collectionCards = collectionCards.Where(x => x.Card.CardSet.Year == userParams.Year);
            }

            if (!String.IsNullOrEmpty(userParams.Brand))
            {
                collectionCards = collectionCards.Where(x => x.Card.CardSet.Brand.ToLower() == userParams.Brand.ToLower());
            }

            if (!String.IsNullOrEmpty(userParams.Name))
            {
                collectionCards = collectionCards.Where(x => x.Card.CardSet.Name.ToLower() == userParams.Name.ToLower());
            }

            return await collectionCards.ToListAsync();
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);

            return photo;
        }

        public async Task<bool> SaveAll()
        {
            var result = await _context.SaveChangesAsync() > 0;

            return result;
        }
    }
}
