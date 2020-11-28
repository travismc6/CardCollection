using CardCollection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardCollection.Helpers;

namespace CardCollection.Data
{
    public interface ICardsRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;

        Task<Card> GetCardById(int id);

        Task<ICollection<Card>> GetCards(CardParams userParams);

        Task<ICollection<CollectionCard>> GetCollectionCards(int collectionId, CardParams userParams);

        Task<Collection> GetCollection(int collectionId);

        Task<CollectionCard> GetCollectionCardById(int collectionId, int id);
        Task<Photo> GetPhoto(int id);


        Task<bool> SaveAll();
    }
}
