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
                IsActived = dbProduct.IsActived,
                InactivedDate = dbProduct.InactivedDate,
                Observations = dbProduct.Observations,
                PreviewPrice = dbProduct.PreviewPrice
            };
        }

        public static bool ProductUpdateDto(this ProductModel dbProduct, ProductUpdateDto productDto)
        {
            if (productDto == null || dbProduct == null)
                return false;

            // UPDATE PRODUCT FIELDS
            if (!string.IsNullOrEmpty(productDto.Name)) dbProduct.Name = productDto.Name;
            if (!string.IsNullOrEmpty(productDto.Description)) dbProduct.Description = productDto.Description;
            if (!string.IsNullOrEmpty(productDto.Category)) dbProduct.Category = productDto.Category;
            if (!string.IsNullOrEmpty(productDto.ImgUrl)) dbProduct.ImgUrl = productDto.ImgUrl;

            if (productDto.InactivedDate.HasValue) dbProduct.InactivedDate = productDto.InactivedDate;
            if (productDto.IsActived.HasValue) dbProduct.IsActived = productDto.IsActived;

            dbProduct.Price = productDto.Price ?? dbProduct.Price;
            dbProduct.PreviewPrice = productDto.PreviewPrice ?? dbProduct.PreviewPrice;

            // UPDATE ADDITIONAL
            if (productDto.Additional != null && productDto.Additional.Count > 0)
            {
                if (dbProduct.Additional == null || dbProduct.Additional.Count == 0)
                    return false; 

                foreach (var dtoItem in productDto.Additional)
                {
                    var existing = dbProduct.Additional.FirstOrDefault(a => a.Id == dtoItem.Id);

                    // if ID doesn't match any additional, return false
                    if (existing == null)
                        return false;

                    // update existing additional
                    existing.Name = dtoItem.Name ?? existing.Name;
                    existing.Category = dtoItem.Category ?? existing.Category;
                    existing.Max = dtoItem.Max ?? existing.Max;
                    existing.Min = dtoItem.Min ?? existing.Min;
                    existing.Size = dtoItem.Size ?? existing.Size;

                    var parsedProductIdList = Additional.ToStringArray(dtoItem.ProductIdList);
                    foreach (var item in parsedProductIdList)
                    {
                        if (parsedProductIdList != null)
                            parsedProductIdList[parsedProductIdList.Length - 1] = item;
                    }
                    
                        existing.ProductIdList = parsedProductIdList;
                }
            }

            return true;
        }


        public static ProductModel Create(ProductCreate product, CancellationToken ct)
        {
            var additional = new List<Additional>();
            if (product.Additional != null && product.Additional.Count != 0)
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
                        ProductIdList = Additional.ToStringArray(item.ProductIdList)
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
                InactivedDate = DateOnly.Parse(product.InactivedDate),
                Observations = product.Observations,
                PreviewPrice = product.PreviewPrice,
                Price = product.Price,
                Additional = additional,

            };

            return dbProduct;
        }
    }
}
