using MenuDigital.Domain.Entities;
using MenuDigitalApi.DTOs.Store;
using System.Net;

namespace MenuDigitalApi.DTOs.Transformers.Store
{
    public class StoreCreateTransform
    {
        static public StoreModel ToDbModel(StoreCreateDto dto)
        {
            var workschedule = new List<WorkSchedule>();
            if (dto.WorkSchedule != null) {
                foreach (var item in dto.WorkSchedule)
                {
                    workschedule.Add(new WorkSchedule
                    {
                        Day = item.Day,
                        End = item.End,
                        IsSelected = item.IsSelected,
                        Start = item.Start,
                    });
                }
            }
            else
            {
                workschedule = new List<WorkSchedule>();
            }

            var category = new List<Category>();
            if (dto.Category != null) 
            {
                foreach (var item in dto.Category)
                {
                    category.Add( new Category
                    {
                        Name = item.Name,
                        Description = item.Description,
                    });
                }
            }

            var address = new List<AddressModel>();
            if (dto.Address != null)
            {
                foreach (var item in dto.Address)
                {
                    address.Add(new AddressModel
                    {
                        Street = item.Street,
                        neighborhood = item.neighborhood,
                        City = item.City,
                        Complement = item.Complement,
                        Number = item.Number,
                        ZipCode = item.ZipCode,
                    });
                }
            }
            var dbStore = new StoreModel
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
                StorePayments = dto.StorePayments,
                WorkSchedule = workschedule,
                Category = category,
                Address = address
            };
            return dbStore;
        }
    }
}
