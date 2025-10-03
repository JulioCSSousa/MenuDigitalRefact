using MenuDigital.Domain.Entities;
using MenuDigitalApi.DTOs.Store;
using System.Linq;

namespace MenuDigitalApi.DTOs.Transformers.Store
{
    public static class StoreCreateTransform
    {
        public static StoreModel ToDbModel(StoreCreateDto dto)
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
                ImageUrl = dto.ImageUrl,
                MinOrderPrice = dto.MinOrderPrice,
                SocialMedias = dto.SocialMedias,
                StoreName = dto.StoreName,

                WorkSchedule = dto.WorkSchedule?
                    .Select(item => new WorkSchedule
                    {
                        Day = item.Day,
                        IsSelected = item.IsSelected,
                        Start = item.Start,
                        End = item.End
                    })
                    .ToList() ?? new List<WorkSchedule>(),

                Category = dto.Category?
                    .Select(item => new Category
                    {
                        Name = item.Name,
                        Description = item.Description
                    })
                    .ToList() ?? new List<Category>(),

                Address = dto.Address?
                    .Select(item => new AddressModel
                    {
                        Street = item.Street,
                        neighborhood = item.neighborhood,
                        City = item.City,
                        Complement = item.Complement,
                        Number = item.Number,
                        ZipCode = item.ZipCode
                    })
                    .ToList() ?? new List<AddressModel>(),

                StorePayments = dto.StorePayments?
                    .Select(item => new StorePayments
                    {
                        PaymentsCount = item.PaymentsCount
                    })
                    .ToList() ?? new List<StorePayments>()
            };
        }
    }
}
