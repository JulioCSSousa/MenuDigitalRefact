using MenuDigital.Domain.Entities;

namespace MenuDigitalApi.DTOs.Store
{
    public static class StoreUpdateTransformer
    {
        public static void ApplyUpdate(this StoreModel entity, StoreUpdateDto dto)
        {
            if (dto == null || entity == null) return;

            // Strings
            if (!string.IsNullOrEmpty(dto.StoreName))
                entity.StoreName = dto.StoreName;

            if (!string.IsNullOrEmpty(dto.Description))
                entity.Description = dto.Description;

            if (!string.IsNullOrEmpty(dto.ImageUrl))
                entity.ImageUrl = dto.ImageUrl;

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
                    Description = ct.Description
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
