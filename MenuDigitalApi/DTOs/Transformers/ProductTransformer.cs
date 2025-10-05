using MenuDigital.Domain.Entities;
using MenuDigital.Domain.Entities.MenuModels;
using MenuDigitalApi.DTOs.Menu.Products.Request.Create;
using MenuDigitalApi.DTOs.Menu.Products.Request.Update;
using MenuDigitalApi.DTOs.Menu.Products.Response.ProductMenu;

namespace MenuDigitalApi.DTOs.Transformers
{
    public static class ProductTransformer
    {
        public static ProductGetAllDto GetAll(ProductModel dbProduct)
        {
            var additional = dbProduct.Additional?.Any() == true
                ? dbProduct.Additional
                    .Select(item => new AdditionalGetAllDto
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Max = item.Max,
                        Min = item.Min,
                        Size = item.Size,
                        Category = item.Category,
                        ProductIdList = item.ProductIdList
                    })
                    .ToList()
                : new List<AdditionalGetAllDto>();

            return new ProductGetAllDto
            {
                ProductId = dbProduct.ProductId,
                StoreId = dbProduct.StoreId,
                Name = dbProduct.Name,
                Description = dbProduct.Description,
                Category = dbProduct.Category,
                Price = dbProduct.Price,
                Additional = additional,
                ImgUrl = dbProduct.ImgUrl,
                InactivedDate = dbProduct.InactivedDate,
                Observations = dbProduct.Observations,
                PreviewPrice = dbProduct.PreviewPrice
            };
        }

        public static ProductModel ProductUpdateDto(ProductUpdateDto productDto, ProductModel dbProduct)
        {
            var additionalDtoList = productDto.Additional?.Any() == true
                ? productDto.Additional.Select(item =>
                    new Additional
                    {
                        Id = dbProduct.Additional?.FirstOrDefault()?.Id ?? Guid.NewGuid(),
                        Name = item.Name ?? dbProduct.Additional.FirstOrDefault()?.Name,
                        Size = item.Size ?? dbProduct.Additional.FirstOrDefault()?.Size,
                        Category = item.Category ?? dbProduct.Additional.FirstOrDefault()?.Category,
                        Max = item.Max ?? dbProduct.Additional.FirstOrDefault().Max,
                        Min = item.Min ?? dbProduct.Additional.FirstOrDefault().Min,
                        ProductId = dbProduct.ProductId,
                        ProductIdList = item.ProductIdList ?? dbProduct.Additional.FirstOrDefault()?.ProductIdList
                    }).ToList()
                : new List<Additional>();

            var result = new ProductModel
            {
                ProductId = dbProduct.ProductId,
                Name = productDto.Name ?? dbProduct.Name,
                StoreId = productDto.StoreId ?? dbProduct.StoreId,
                Category = productDto.Category ?? dbProduct.Category,
                Additional = additionalDtoList,
                Description = productDto.Description ?? dbProduct.Description,
                ImgUrl = productDto.ImgUrl ?? dbProduct.ImgUrl,
                InactivedDate = productDto.InactivedDate ?? dbProduct.InactivedDate,
                Observations = productDto.Observations ?? dbProduct.Observations,
                PreviewPrice = productDto.PreviewPrice ?? dbProduct.PreviewPrice,
                Price = productDto.Price ?? dbProduct.Price
            };
            return result;
        }

        public static ProductModel Create(ProductCreate product, CancellationToken ct)
        {
            var additional = new List<Additional>();
            if (product.Additional.Count != 0 || product.Additional != null)
            {
                foreach (var item in product.Additional)
                {
                    additional.Add(new Additional
                    {
                        Name = item.Name,
                        Category = item.Category,
                        Max = item.Max,
                        Min = item.Min,
                        Size = item.Size,
                        ProductIdList = item.ProductIdList
                    }
                    );

                }
            }

            var dbProduct = new ProductModel
            {
                StoreId = product.StoreId,
                Category = product.Category,
                Name = product.Name,
                Description = product?.Description,
                ImgUrl = product?.ImgUrl,
                InactivedDate = product.InactivedDate,
                Observations = product.Observations,
                PreviewPrice = product.PreviewPrice,
                Price = product.Price,
                Additional = additional,

            };

            return dbProduct;
        }
    }
}
