using MenuDigital.Domain.Entities;
using MenuDigitalApi.DTOs.Store;
using System.Linq;

namespace MenuDigitalApi.DTOs.Transformers.Store
{
    public static class StoreTransform
    {
        public static StoreGetAllDto GetAll(StoreModel dto)
        {
            return new StoreGetAllDto
            {
                StoreId = dto.StoreId,
                Alert = dto.Alert,
                Closed = dto.Closed,
                Colors = dto.Colors,
                Contacts = dto.Contacts,
                Description = dto.Description,
                HasImage = dto.HasImage,
                Images = dto.Images,
                StoreUrl = dto.StoreUrl,
                MinOrderPrice = dto.MinOrderPrice,
                SocialMedias = dto.SocialMedias,
                StoreName = dto.StoreName,

                WorkSchedule = dto.WorkSchedule?
                    .Select(item => new WorkScheduleGetDto
                    {
                        Id = item.Id,
                        Day = item.Day,
                        IsSelected = item.IsSelected,
                        Start = item.Start.ToString(),
                        End = item.End.ToString(),
                    })
                    .ToList() ?? new List<WorkScheduleGetDto>(),


                Address = dto.Address?
                    .Select(item => new AddressGetDto
                    {
                        AddressId = item.AddressId,
                        Street = item.Street,
                        Neighborhood = item.Neighborhood,
                        City = item.City,
                        State = item.State,
                        Complement = item.Complement,
                        Number = item.Number,
                        ZipCode = item.ZipCode

                    })
                    .ToList() ?? new List<AddressGetDto>(),

                Category = dto.Category?
                    .Select(item => new CategoryGetAll
                    {
                        CategoryId = item.CategoryId,
                        Name = item.Name,
                        Icon = item.Icon
                    })
                    .ToList() ?? new List<CategoryGetAll>(),

                StorePayments = dto.StorePayments?
                    .Select(item => new StorePayments
                    {
                        Id= item.Id,
                        PaymentsCount = item.PaymentsCount,
                        StoreId = item.StoreId, 
                    })
                    .ToList() ?? new List<StorePayments>()
            };
        }

        public static StoreModel Create(StoreCreateDto dto)
        {
            return new StoreModel
            {
                Alert = dto.Alert,
                Closed = dto.Closed,
                Colors = dto.Colors,
                Contacts = dto.Contacts,
                Description = dto.Description,
                HasImage = dto.HasImage,
                Images = dto.Images,
                StoreUrl = dto.StoreUrl,
                MinOrderPrice = dto.MinOrderPrice,
                SocialMedias = dto.SocialMedias,
                StoreName = dto.StoreName,

                WorkSchedule = dto.WorkSchedule?
                    .Select(item => new WorkSchedule
                    {
                        Day = item.Day,
                        IsSelected = item.IsSelected,
                        Start = TimeSpan.Parse(item.Start),
                        End = TimeSpan.Parse(item.End)
                    })
                    .ToList() ?? new List<WorkSchedule>(),


                Address = dto.Address?
                    .Select(item => new AddressModel
                    {
                        Street = item.Street,
                        Neighborhood = item.Neighborhood,
                        City = item.City,
                        State = item.State,
                        Complement = item.Complement,
                        Number = item.Number,
                        ZipCode = item.ZipCode

                    })
                    .ToList() ?? new List<AddressModel>(),

                Category = dto.Category?
                    .Select(item => new Category
                    {
                        Name = item.Name,
                        Icon = item.Icon
                    })
                    .ToList() ?? new List<Category>(),

                StorePayments = dto.StorePayments?
                    .Select(item => new StorePayments
                    {
                        PaymentsCount = item.PaymentsCount
                    })
                    .ToList() ?? new List<StorePayments>()
            };
        }
        public static void Update(this StoreModel entity, StoreUpdateDto dto)
        {
            if (dto == null || entity == null) return;

            // Strings
            if (!string.IsNullOrEmpty(dto.StoreName))
                entity.StoreName = dto.StoreName;

            if (!string.IsNullOrEmpty(dto.Description))
                entity.Description = dto.Description;

            if (!string.IsNullOrEmpty(dto.StoreUrl))
                entity.StoreUrl = dto.StoreUrl;

            if (!string.IsNullOrEmpty(dto.Alert))
                entity.Alert = dto.Alert;

            // Bools
            if (dto.HasImage.HasValue)
                entity.HasImage = dto.HasImage.Value;

            if (dto.Closed.HasValue)
                entity.Closed = dto.Closed.Value;

            // Double
            if (dto.MinOrderPrice.HasValue)
                entity.MinOrderPrice = dto.MinOrderPrice.Value;

            // Objetos complexos
            if (dto.Colors != null)
                entity.Colors = dto.Colors;

            if (dto.Images != null)
                entity.Images = dto.Images;

            if (dto.SocialMedias != null)
                entity.SocialMedias = dto.SocialMedias;

            if (dto.Contacts != null)
                entity.Contacts = dto.Contacts;

            // Listas
            if (dto.Category != null && dto.Category.Any())
                entity.Category = dto.Category.Select(ct => new Category
                {
                    Name = ct.Name,
                    Icon = ct.Icon
                }).ToList();

            if (dto.Address != null && dto.Address.Any())
                entity.Address = dto.Address;

            if (dto.WorkSchedule != null && dto.WorkSchedule.Any())
            {
                entity.WorkSchedule = dto.WorkSchedule
                    .Select(ws => new WorkSchedule
                    {
                        IsSelected = true,
                        Start = TimeSpan.Parse(ws.Start),
                        End = TimeSpan.Parse(ws.End),
                        Day = ws.Day,
                    }).ToList();
            }

            if (dto.StorePayments != null && dto.StorePayments.Any())
                entity.StorePayments = dto.StorePayments;
        }
    }
}
