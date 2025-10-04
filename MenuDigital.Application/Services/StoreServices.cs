// Application/Services/StoreService.cs
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using MenuDigital.Application.Interfaces;
using MenuDigital.Application.Interfaces.Store;
using MenuDigital.Domain.Entities;

namespace MenuDigital.Application.Services
{
    public class StoreService
    {
        private readonly IStoreRepository _repo;
        private readonly IUnitOfWork _uow;

        public StoreService(IStoreRepository repo, IUnitOfWork uow)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        // READ
        public async Task<ICollection<StoreModel>> GetAllAsync(string? name, string? url, CancellationToken ct = default)
        {
            var result = await _repo.GetAllAsync(ct);

            var query = result.AsEnumerable();

            if (!String.IsNullOrEmpty(name))
            {
                query = query.Where(n => n.StoreName.ToLower().Contains(name.ToLower()));
            }
            if (!String.IsNullOrEmpty(url))
            {
                query = query.Where(u => u.StoreUrl.ToLower().Contains(url.ToLower()));
            }
            return query.ToList();
        }

        public async Task<StoreModel?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _repo.GetByIdAsync(id, ct);
        }

        // CREATE
        public async Task AddAsync(StoreModel s, CancellationToken ct = default)
        {
            if (s is null) throw new ArgumentNullException(nameof(s));

            Normalize(s);
            Validate(s);

            await _repo.AddAsync(s, ct);
            await _uow.SaveChangesAsync(ct);
        }

        public async Task AddAddressAsync(AddressModel address, Guid storeId, CancellationToken ct = default)
        {
            var dbStore = await _repo.GetByIdAsync(storeId, ct);
            if (dbStore is null) throw new ArgumentNullException("StoreId not Found");
            if(dbStore.Address.Count == 0 || dbStore.Address is null)
            {
                dbStore.Address.Add(address);
                await _uow.SaveChangesAsync(ct);
            }
        }
        public async Task AddWorkScheduleAsync(WorkSchedule work, Guid storeId, CancellationToken ct = default)
        {
            var dbStore = await _repo.GetByIdAsync(storeId, ct);
            if (dbStore is null) throw new ArgumentNullException("StoreId not Found");
                dbStore.WorkSchedule.Add(work);
                await _uow.SaveChangesAsync(ct);
        }
        public async Task AddStorePaymentAsync(StorePayments addpayment, Guid storeId, CancellationToken ct = default)
        {
            var dbStore = await _repo.GetByIdAsync(storeId, ct);
            if (dbStore is null) throw new ArgumentNullException("StoreId not Found");
                dbStore.StorePayments.Add(addpayment);
                await _uow.SaveChangesAsync(ct);
        }

        // UPDATE (merge seguro + validação)
        public async Task<bool> UpdateAsync(Guid id, StoreModel incoming, CancellationToken ct = default)
        {
            if (incoming is null) throw new ArgumentNullException(nameof(incoming));

            var existing = await _repo.GetByIdAsync(id, ct);
            if (existing is null) throw new KeyNotFoundException("Store not found.");

            // Merge: só substitui se veio valor (mantém o restante)
            existing.StoreName = Coalesce(incoming.StoreName, existing.StoreName, 100);
            existing.Description = Coalesce(incoming.Description, existing.Description, 500);
            existing.ImageUrl = Coalesce(incoming.ImageUrl, existing.ImageUrl, 500);
            existing.HasImage = incoming.HasImage || existing.HasImage;
            existing.Closed = incoming.Closed;

            existing.Alert = Coalesce(incoming.Alert, existing.Alert, 500);
            existing.MinOrderPrice = incoming.MinOrderPrice ?? existing.MinOrderPrice;
            existing.StorePayments = incoming.StorePayments; // enum: será validado

            if (incoming.Colors is not null) existing.Colors = incoming.Colors;
            if (incoming.Images is not null) existing.Images = incoming.Images;
            if (incoming.SocialMedias is not null) existing.SocialMedias = incoming.SocialMedias;
            if (incoming.Contacts is not null) existing.Contacts = incoming.Contacts;


            Normalize(existing);
            Validate(existing);
            var prod = await _repo.GetByIdAsync(id);
            if (prod != null)
            {
                await _repo.UpdateAsync(prod, ct);
                await _uow.SaveChangesAsync(ct);
                return true;
            }
            return false;
        }

        // DELETE
        public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var prod = await GetByIdAsync(id);
            if (prod != null)
            {
                await _repo.DeleteAsync(prod, ct);
                await _uow.SaveChangesAsync(ct);
                return true;
            }
            return false;

        }

        // ----------------- helpers -----------------

        private static string? Coalesce(string? incoming, string? current, int maxLen)
        {
            incoming = incoming?.Trim();
            if (string.IsNullOrEmpty(incoming)) return current;
            if (incoming.Length > maxLen) incoming = incoming.Substring(0, maxLen);
            return incoming;
        }

        private static void Normalize(StoreModel s)
        {
            s.StoreName = s.StoreName?.Trim();
            s.Description = s.Description?.Trim();
            s.ImageUrl = s.ImageUrl?.Trim();
            s.Alert = s.Alert?.Trim();

            // Cores
            if (s.Colors is not null)
            {
                s.Colors.Primary = s.Colors.Primary?.Trim();
                s.Colors.Secondary = s.Colors.Secondary?.Trim();
            }

            // Imagens
            if (s.Images is not null)
            {
                s.Images.Logo = s.Images.Logo?.Trim();
                s.Images.Header = s.Images.Header?.Trim();
            }

            // Contatos: tira nulos, trim, remove vazios e duplicados
            if (s.Contacts is not null)
            {
                s.Contacts.Phones = CleanList(s.Contacts.Phones);
                s.Contacts.Whatsapps = CleanList(s.Contacts.Whatsapps);
                s.Contacts.Emails = CleanList(s.Contacts.Emails);
            }

            // Social: idem
            if (s.SocialMedias is not null)
            {
                s.SocialMedias.Facebook = CleanList(s.SocialMedias.Facebook);
                s.SocialMedias.Instagram = CleanList(s.SocialMedias.Instagram);
                s.SocialMedias.Whatsapp = CleanList(s.SocialMedias.Whatsapp);
                s.SocialMedias.X = CleanList(s.SocialMedias.X);
                s.SocialMedias.Website = CleanList(s.SocialMedias.Website);
            }



        }

        private static List<string> CleanList(IEnumerable<string>? items)
        {
            return (items ?? Enumerable.Empty<string>())
                .Select(x => x?.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        private static void Validate(StoreModel s)
        {
            var errors = new List<string>();

            // básicos
            if (string.IsNullOrWhiteSpace(s.StoreName))
                errors.Add("StoreName is required.");
            if (s.StoreName?.Length > 100)
                errors.Add("StoreName must be at most 100 characters.");
            if (s.Description?.Length > 500)
                errors.Add("Description must be at most 500 characters.");
            if (s.ImageUrl?.Length > 500)
                errors.Add("ImageUrl must be at most 500 characters.");
            if (s.Alert?.Length > 500)
                errors.Add("Alert must be at most 500 characters.");

            if (s.MinOrderPrice is < 0)
                errors.Add("MinOrderPrice cannot be negative.");


            // URLs válidas
            if (!string.IsNullOrWhiteSpace(s.ImageUrl) && !IsValidUrl(s.ImageUrl!))
                errors.Add("ImageUrl is not a valid absolute URL.");
            if (s.Images is not null)
            {
                if (!string.IsNullOrWhiteSpace(s.Images.Logo) && !IsValidUrl(s.Images.Logo!))
                    errors.Add("Images.Logo is not a valid absolute URL.");
                if (!string.IsNullOrWhiteSpace(s.Images.Header) && !IsValidUrl(s.Images.Header!))
                    errors.Add("Images.Header is not a valid absolute URL.");
            }

            // Cores HEX (#RGB ou #RRGGBB)
            if (s.Colors is not null)
            {
                if (!string.IsNullOrWhiteSpace(s.Colors.Primary) && !IsHexColor(s.Colors.Primary!))
                    errors.Add("Colors.Primary must be a hex color like #RRGGBB.");
                if (!string.IsNullOrWhiteSpace(s.Colors.Secondary) && !IsHexColor(s.Colors.Secondary!))
                    errors.Add("Colors.Secondary must be a hex color like #RRGGBB.");
            }



            if (errors.Count > 0)
                throw new ValidationException(string.Join(" | ", errors));
        }

        private static bool IsValidUrl(string url)
            => Uri.TryCreate(url, UriKind.Absolute, out var u) && (u.Scheme == Uri.UriSchemeHttp || u.Scheme == Uri.UriSchemeHttps);

        private static bool IsHexColor(string color)
            => Regex.IsMatch(color, "^#(?:[0-9a-fA-F]{3}){1,2}$");

    }

}

