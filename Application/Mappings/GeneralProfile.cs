using Application.Features.AppTroopers.SecurityTips.Commands.CreateSecurityTipCategory;
using Application.Features.AppTroopers.SecurityTips.GetAllSecurityTipCategories;
using Application.Features.AppTroopers.SecurityTips.Queries.GetAllSecurityTipCategories;
using Application.Features.Location;
using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Queries.GetAllProducts;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.AppTroopers.SecurityTips;
using Domain.Entities.LocationEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Product, GetAllProductsViewModel>().ReverseMap();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<GetAllProductsQuery, GetAllProductsParameter>();

            //Location
            //District
            CreateMap<CreateDistrictCommand, Town>();
            CreateMap<Town, GetAllDistrictsViewModel>().ReverseMap();
            CreateMap<GetAllDistrictsQuery, GetAllDistrictsParameter>();
            //LGA
            CreateMap<GetDistrictsinLGAQuery, GetDistrictsinLGAParameter>();
            //State
            CreateMap<GetLGAsinStateQuery, GetLGAsinStateParameter>();

            //AppTroopers --------------------------------------------
            //Security Tip Category
            CreateMap<CreateSecurityTipCategoryCommand, SecurityTipCategory>();
            CreateMap<SecurityTipCategory, GetAllSecurityTipCategoriesViewModel>().ReverseMap();
        }
    }
}