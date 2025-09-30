using MenuDigital.Application.DTOs.Products.Request.Update;
using MenuDigital.Application.DTOs.Products.Response;
using MenuDigital.Application.DTOs.Products.Response.CategoryResponse;
using MenuDigital.Application.DTOs.Products.Response.ProductMenu;
using MenuDigital.Domain.Entities;
using MenuDigital.Domain.Entities.MenuModels;


namespace MenuDigital.Application.DTOs.Transformers.Product
{
    public class ProductTransformer
    {
        static public ProductGetAllReponseDto GetAll(ProductModel dbProduct)
        {
            var productsDtoModel = new LinkedList<ProductGetAllReponseDto>();
            var combined = dbProduct.CombinedProducts;
            var combinedList = new List<CombinedProductGetAllResponseDto>();

            foreach (var item in combined)
            {
                combinedList.Add(
                     new CombinedProductGetAllResponseDto
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
                     }
                    );
            }

            var category = dbProduct.Category;
            var categoryList = new List<ProductCategoryResponse>();

            foreach (var item in category)
            {
                categoryList.Add(new ProductCategoryResponse
                {
                    Name = item.Name,
                });
            }

            var productDto = new ProductGetAllReponseDto
            {
                ProductId = dbProduct.ProductId,
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
            return productDto;
        }

        static public ProductModel ProductUpdateDto( ProductMenuRequestUpdateDto productDto, ProductModel dbProduct)
        {
            var combinedProductDtoList = new List<CombinedProduct>();
            foreach (var item in productDto.CombinedProducts) {
                combinedProductDtoList.Add
                    (
                        new CombinedProduct
                        {
                            Id = dbProduct.CombinedProducts.First().Id,
                            Name = item.Name,
                            Size = item.Size,
                            Category = item.Category,
                            MainMenu = item.MainMenu ?? false,
                            Max = item.Max ?? 0,
                            Min = item.Min ?? 0,
                            Prices = item.Prices,
                            ProductId = dbProduct.ProductId,
                            Type = item.Type,
                        }
                    );
            }
            var productUpdated = new ProductModel
            {
                Name = productDto.Name ?? dbProduct.Name,
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
            return productUpdated;
        }
    }
}
