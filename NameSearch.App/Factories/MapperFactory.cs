using AutoMapper;

namespace NameSearch.App.Factories
{
    /// <summary>
    /// Mapper Factory
    /// </summary>
    public static class MapperFactory
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public static IMapper Get()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.Domain.Api.Response.Person, Models.Entities.Person>()
                    .ForMember(x => x.Id, opt => opt.Ignore())
                    .ForMember(x => x.PersonSearchResultId, opt => opt.Ignore())
                    .ForMember(x => x.IsActive, opt => opt.Ignore())
                    .ForMember(x => x.CreatedDateTime, opt => opt.Ignore())
                    .ForMember(x => x.ModifiedDateTime, opt => opt.Ignore())
                    .ForMember(x => x.Alias, opt => opt.Ignore())
                    .ForMember(x => x.AgeRange, m => m.MapFrom(a => a.AgeRange))
                    .ForMember(x => x.AgeRange, m => m.MapFrom(a => a.AgeRange))
                    .ForMember(x => x.Addresses, m => m.MapFrom(a => a.CurrentAddresses))
                    .ForMember(x => x.Associates, m => m.MapFrom(a => a.AssociatedPeople))
                    .ForMember(x => x.Phones, m => m.MapFrom(a => a.Phones))
                    .ReverseMap();
                cfg.CreateMap<Models.Domain.Api.Response.Address, Models.Entities.Address>()
                    .ForMember(x => x.Id, opt => opt.Ignore())
                    .ForMember(x => x.PersonId, opt => opt.Ignore())
                    .ForMember(x => x.IsActive, opt => opt.Ignore())
                    .ForMember(x => x.CreatedDateTime, opt => opt.Ignore())
                    .ForMember(x => x.ModifiedDateTime, opt => opt.Ignore())
                    .ForMember(x => x.Address1, m => m.MapFrom(a => a.StreetLine1))
                    .ForMember(x => x.Address2, m => m.MapFrom(a => a.StreetLine2))
                    .ForMember(x => x.City, m => m.MapFrom(a => a.City))
                    .ForMember(x => x.State, m => m.MapFrom(a => a.StateCode))
                    .ForMember(x => x.Zip, m => m.MapFrom(a => a.PostalCode))
                    .ForMember(x => x.Plus4, m => m.MapFrom(a => a.Zip4))
                    .ForMember(x => x.Country, m => m.MapFrom(a => a.CountryCode))
                    .ReverseMap();
                cfg.CreateMap<Models.Domain.Api.Response.Associate, Models.Entities.Associate>()
                    .ForMember(x => x.Id, opt => opt.Ignore())
                    .ForMember(x => x.PersonId, opt => opt.Ignore())
                    .ForMember(x => x.IsActive, opt => opt.Ignore())
                    .ForMember(x => x.CreatedDateTime, opt => opt.Ignore())
                    .ForMember(x => x.ModifiedDateTime, opt => opt.Ignore())
                    .ForMember(x => x.Name, m => m.MapFrom(a => a.Name))
                    .ForMember(x => x.Relation, m => m.MapFrom(a => a.Relation))
                    .ReverseMap();
                cfg.CreateMap<Models.Domain.Api.Response.Phone, Models.Entities.Phone>()
                    .ForMember(x => x.Id, opt => opt.Ignore())
                    .ForMember(x => x.PersonId, opt => opt.Ignore())
                    .ForMember(x => x.IsActive, opt => opt.Ignore())
                    .ForMember(x => x.CreatedDateTime, opt => opt.Ignore())
                    .ForMember(x => x.ModifiedDateTime, opt => opt.Ignore())
                    .ForMember(x => x.PhoneNumber, m => m.MapFrom(a => a.PhoneNumber))
                    .ReverseMap();
            });

            var mapper = new Mapper(config);

            return mapper;
        }
    }
}
