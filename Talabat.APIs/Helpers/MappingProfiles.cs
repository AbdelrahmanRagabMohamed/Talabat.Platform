using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Identity;

namespace Talabat.APIs.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductToReturnDto>()
            .ForMember(d => d.ProductType, O => O.MapFrom(S => S.ProductType.Name))
            .ForMember(d => d.ProductBrand, O => O.MapFrom(S => S.ProductBrand.Name))
            .ForMember(d => d.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());

        CreateMap<Address, AddressDto>().ReverseMap();

        CreateMap<CustomerBasketDto, CustomerBasket>();

        CreateMap<BasketItemDto, BasketItem>();


        /// Convert From Product To ProductToRetuenDto
        /// Convert From ProductType and ProductBrand (Objects) =>  its Name only
        /// Add Configuations for Function => Resolver
        /// Convert From Address To AddressDto and ReverseMap
        /// Convert From Address To CustomerBasket

    }

}
