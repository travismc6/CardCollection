using AutoMapper;
using CardCollection.Domain.Models;
using CardCollection.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardCollection.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Card, CardsForListDto>()
                .ForMember(x=> x.Brand, opt => { opt.MapFrom(src => src.CardSet.Brand); })
                .ForMember(x => x.Year, opt => { opt.MapFrom(src => src.CardSet.Year); })
                .ForMember(x => x.PlayerName, opt => { opt.MapFrom(src => src.Name); })
                .ForMember(x => x.SetName, opt => { opt.MapFrom(src => src.CardSet.Name); });

            CreateMap<CollectionCard, CardsForListDto>()
                .ForMember(x => x.Brand, opt => { opt.MapFrom(src => src.Card.CardSet.Brand); })
                .ForMember(x => x.Year, opt => { opt.MapFrom(src => src.Card.CardSet.Year); })
                .ForMember(x => x.PlayerName, opt => { opt.MapFrom(src => src.Card.Name); })
                .ForMember(x => x.SetName, opt => { opt.MapFrom(src => src.Card.CardSet.Name); })
                .ForMember(x => x.SetId, opt => { opt.MapFrom(src => src.Card.CardSet.Id); })
                .ForMember(x => x.Condition, opt => { opt.MapFrom(src => src.Condition); })
                .ForMember(x => x.Image, opt => { opt.MapFrom(src => src.Photos != null && src.Photos.Any(r=> r.IsMain) ? src.Photos.First(r=> r.IsMain).Url : "https://st4.depositphotos.com/1020091/20129/v/1600/depositphotos_201293330-stock-illustration-baseball-card-icon-flat-color.jpg");  })
                .ForMember(x => x.Notes, opt => { opt.MapFrom(src => src.Notes); })
                .ForMember(x => x.Number, opt => { opt.MapFrom(src => src.Card.Number); }).ReverseMap(); ;

        }
    }
}
