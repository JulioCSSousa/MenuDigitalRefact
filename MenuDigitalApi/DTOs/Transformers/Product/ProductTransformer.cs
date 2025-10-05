using MenuDigital.Domain.Entities;
using MenuDigital.Domain.Entities.MenuModels;
using MenuDigitalApi.DTOs.Menu.Products.Request.Create;
using MenuDigitalApi.DTOs.Menu.Products.Request.Update;
using MenuDigitalApi.DTOs.Menu.Products.Response.CategoryResponse;
using MenuDigitalApi.DTOs.Menu.Products.Response.ProductMenu;
using System.Linq;

namespace MenuDigitalApi.DTOs.Transformers.Product
{
    public static class ProductTransformer
    {
        public static ProductGetAllReponseDto GetAll(ProductModel dbProduct)
        {
            var combinedList = dbProduct.CombinedProducts?.Any() == true
                ? dbProduct.CombinedProducts
                    .Select(item => new CombinedProductGetAllResponseDto
                    {
                        Id = item.Id,
                        Name = item.Name,
                        MainMenu = item.MainMenu,
                        Max = item.Max,
                        Min = item.Min,
                        Prices = item.Prices,
                        Size = item.Size,
                        Category = item.Category,
                        ProductId = item.ProductId,
                        Type = item.Type
                    })
                    .ToList()
                : new List<CombinedProductGetAllResponseDto>();

            var categoryList = dbProduct.Category?.Any() == true
                ? dbProduct.Category
                    .Select(item => new ProductCategoryResponse { Name = item.Name })
                    .ToList()
                : new List<ProductCategoryResponse>();

            return new ProductGetAllReponseDto
            {
                ProductId = dbProduct.ProductId,
                StoreId = dbProduct.StoreId,
                Name = dbProduct.Name,
                Description = dbProduct.Description,
                Category = categoryList,
                Prices = dbProduct.Prices,
                CombinedPrice = dbProduct.CombinedPrice,
                CombinedProducts = combinedList,
                ImgUrl = dbProduct.ImgUrl,
                ExtraIndex = dbProduct.ExtraIndex,
                IsSale = dbProduct.IsSale,
                Multiple = dbProduct.Multiple,
                Observations = dbProduct.Observations,
                PreviewPrices = dbProduct.PreviewPrices
            };
        }

        public static ProductModel ProductUpdateDto(ProductMenuRequestUpdateDto productDto, ProductModel dbProduct)
        {
            var combinedProductDtoList = productDto.CombinedProducts?.Any() == true
                ? productDto.CombinedProducts.Select(item =>
                    new CombinedProduct
                    {
                        Id = dbProduct.CombinedProducts?.FirstOrDefault()?.Id ?? Guid.NewGuid(),
                        Name = item.Name,
                        Size = item.Size,
                        Category = item.Category,
                        MainMenu = item.MainMenu ?? false,
                        Max = item.Max ?? 0,
                        Min = item.Min ?? 0,
                        Prices = item.Prices?.ToList() ?? new List<Price>(),
                        ProductId = dbProduct.ProductId,
                        Type = item.Type,
                    }).ToList()
                : new List<CombinedProduct>();

            return new ProductModel
            {
                Name = productDto.Name ?? dbProduct.Name,
                StoreId = dbProduct.StoreId,
                Category = productDto.Category ?? dbProduct.Category,
                CombinedPrice = productDto.CombinedPrice ?? dbProduct.CombinedPrice,
                CombinedProducts = combinedProductDtoList,
                Description = productDto.Description ?? dbProduct.Description,
                ExtraIndex = productDto.ExtraIndex ?? dbProduct.ExtraIndex,
                ImgUrl = productDto.ImgUrl ?? dbProduct.ImgUrl,
                IsSale = productDto.IsSale ?? dbProduct.IsSale,
                Multiple = productDto.Multiple ?? dbProduct.Multiple,
                Observations = productDto.Observations ?? dbProduct.Observations,
                PreviewPrices = productDto.PreviewPrices ?? dbProduct.PreviewPrices,
                Prices = productDto.Prices ?? dbProduct.Prices
            };
        }

        public static ProductModel Create(ProductMenuCreate product, CancellationToken ct)
        {
            var category = new List<Category>();
            foreach (var item in product.Category)
            {
                category.Add(new Category
                {
                    Name = item.Name,
                    Description = item.Description
                }
                );

            }
            var combined = new List<CombinedProduct>();
            if (product.CombinedProducts.Count != 0 || product.CombinedProducts != null)
            {
                foreach (var item in product.CombinedProducts)
                {
                    combined.Add(new CombinedProduct
                    {
                        Name = item.Name,
                        MainMenu = item.MainMenu,
                        Category = item.Category,
                        Max = item.Max,
                        Min = item.Min,
                        Prices = item.Prices,
                        Size = item.Size,
                        Type = item.Type,
                    }
                    );

                }
            }

            var dbProduct = new ProductModel
            {
                StoreId = product.StoreId,
                Category = category,
                Name = product.Name,
                CombinedPrice = product.CombinedPrice,
                Description = product?.Description,
                ExtraIndex = product?.ExtraIndex,
                ImgUrl = product?.ImgUrl,
                IsSale = product.IsSale,
                Multiple = product.Multiple,
                Observations = product.Observations,
                PreviewPrices = product.PreviewPrices,
                Prices = product.Prices,
                CombinedProducts = combined,

            };

            return dbProduct;
        }
    }
}
