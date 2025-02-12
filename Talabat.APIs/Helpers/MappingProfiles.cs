using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Talabat.APIs.DTOs;
using Talabat.Core.Entites;

namespace Talabat.APIs.Helpers;

public class MappingProfiles  : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductToRetuenDto>()
            .ForMember(d => d.ProductType , O => O.MapFrom(S => S.ProductType.Name))
            .ForMember(d => d.ProductBrand , O => O.MapFrom(S => S.ProductBrand.Name))
            .ForMember(d => d.PictureUrl , O => O.MapFrom<ProductPictureUrlResolver>());


        /// Convert From Product To ProductToRetuenDto
        /// Convert From ProductType and ProductBrand (Objects) =>  its Name only
        /// Add Configuations for Function => Resolver
    }

}
