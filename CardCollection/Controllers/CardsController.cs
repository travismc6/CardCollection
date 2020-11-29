using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CardCollection.Domain.Models;
using CardCollection.Dtos;
using CardCollection.Helpers;
using CardCollection.Persistence.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Persistence.Data;

namespace CardCollection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardsRepository _repo;
        private readonly IMapper _mapper;


        public CardsController(ICardsRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetCards([FromQuery] CardParams cardParams)
        {
            var cards = await _repo.GetCards(cardParams);
            var cardToReturn = _mapper.Map<ICollection<CardsForListDto>>(cards);

            return Ok(cardToReturn);
        }

        [HttpGet("setchecklists/{collectionId}")]
        public async Task<IActionResult> GetSetChecklists(int? collectionId, [FromQuery] CardParams cardParams)
        {
            // DEBUG ONLY!
            if (collectionId == null)
            {
                collectionId = 1;
            }

            CardsForChecklistDto dto = new CardsForChecklistDto { CollectionId = collectionId.Value };

            var cards = await _repo.GetCards(cardParams);

            var sets = cards.GroupBy(r => r.CardSet);

            foreach (var s in sets)
            {
                var setDto = new SetForChecklistDto
                {
                    Brand = s.Key.Brand,
                    Description = s.Key.Description,
                    Year = s.Key.Year,
                    Name = s.Key.Name,
                    Cards = new List<CardForChecklistDto>(),
                    Id = s.Key.Id
                };

                var mycards = await _repo.GetCollectionCards(collectionId.Value, cardParams);

                foreach (var c in s.OrderBy(r => r.Number, new NumberComparer()))
                {
                    CardForChecklistDto cardDto = new CardForChecklistDto()
                    {
                        Brand = c.CardSet.Brand,
                        PlayerName = c.Name,
                        Id = c.Id,
                        Notes = c.Notes,
                        Year = c.CardSet.Year,
                        SetName = c.CardSet.Name,
                        Number = c.Number,
                        HasCard = mycards.Any(x => x.CardId == c.Id)
                    };

                    setDto.Cards.Add(cardDto);
                }

                dto.Sets.Add(setDto);
            }

            return Ok(dto);
        }

        [HttpGet("/collection/{collectionId}/cardsforcollection")]
        public async Task<IActionResult> GetCardsForCollection(int collectionId, [FromQuery] CardParams cardParams)
        {
            var cards = await _repo.GetCollectionCards(collectionId, cardParams);

            var cardsToReturn = _mapper.Map<ICollection<CardsForListDto>>(cards);

            return Ok(cardsToReturn.OrderBy(r => r.Year).ThenBy(r => r.Brand).ThenBy(r => r.SetName).ThenBy(r => r.Number, new NumberComparer()));

            // OrderBy(r=> r.Number, new NumberComparer())
        }

        [HttpPut("{cardId}/collection/{collectionId}")]
        public async Task<IActionResult> AddOrRemoveCollectionCard(int collectionId, int cardId)
        {
            var collection = await _repo.GetCollection(collectionId);

            if (collection == null)
                return NotFound();

            var collectionCards = collection.CollectionCards.Where(r => r.CollectionId == collectionId && r.CardId == cardId);

            if (collectionCards.Any())
            {
                foreach (var c in collectionCards.ToList())
                {
                    collection.CollectionCards.Remove(c);
                }
            }

            else
            {
                var cmodel = await _repo.GetCardById(cardId);

                var model = new CollectionCard
                {
                    CardId = cmodel.Id,
                    CollectionId = collectionId
                };

                _repo.Add(model);
            }

            await _repo.SaveAll();

            return Ok();
        }

        [HttpGet("collection/{collectionId}/card/{id}")]
        public async Task<IActionResult> GetCollectionCard(int collectionId, int id)
        {
            var card = await _repo.GetCollectionCardById(collectionId, id);

            if (card == null)
                return NotFound();
            else
            {
                var cardToReturn = _mapper.Map<CardsForListDto>(card);

                return Ok(cardToReturn);
            }
        }

        [HttpPost("collection/{collectionId}/card/{id}")]
        public async Task<IActionResult> UpdateCollectionCard(int collectionId, int id, CardsForListDto userForUpdateDto)
        {
            var userFromRepo = await _repo.GetCollectionCardById(collectionId, id);

            _mapper.Map(userForUpdateDto, userFromRepo);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Updating user {id} failed on save");

        }
    }
}
