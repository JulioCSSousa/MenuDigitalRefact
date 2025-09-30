using AutoMapper;
using MenuDigital.Application.DTOs.Products.Request.Create;
using MenuDigital.Application.DTOs.Products.Request.Update;
using MenuDigital.Application.DTOs.Products.Request.Update.Create;
using MenuDigital.Application.DTOs.Products.Response;
using MenuDigital.Application.DTOs.Products.Response.CategoryResponse;
using MenuDigital.Application.DTOs.Products.Response.ProductMenu;
using MenuDigital.Domain.Entities;
using MenuDigital.Domain.Entities.MenuModels;

namespace MenuDigital.Application.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // POST
            CreateMap<ProductModel, ProductMenuCreate>().ReverseMap();
            CreateMap<CombinedProduct, CombinedProductCreate>();
            CreateMap<Category, CategoryCreateDto>();
            // GET
            CreateMap<ProductModel, ProductGetAllReponseDto>().ReverseMap();

            CreateMap<CombinedProduct, CombinedProductGetAllResponseDto>();
            CreateMap<Category, ProductCategoryResponse>();

            // UPDATE Product
            CreateMap<ProductMenuRequestUpdateDto, ProductModel>()
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) =>
                        srcMember != null &&
                        !(srcMember is string s && string.IsNullOrWhiteSpace(s))));

            // UPDATE CombinedProduct
            CreateMap<CombinedProductUpdateDto, CombinedProduct>()
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) =>
                        srcMember != null &&
                        !(srcMember is string s && string.IsNullOrWhiteSpace(s))));

            // UPDATE Category
            CreateMap<CategoryUpdateDto, Category>()
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) =>
                        srcMember != null &&
                        !(srcMember is string s && string.IsNullOrWhiteSpace(s))));
        }
    }
}
