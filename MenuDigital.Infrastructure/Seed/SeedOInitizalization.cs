using MenuDigital.Domain.Entities;
using MenuDigital.Domain.Entities.MenuModels;
using MenuDigital.Domain.Entities.ValuesObjects;
using MenuDigital.Domain.Entities.ValuesObjects.Enum;
using MenuDigital.Domain.Models.Entities;
using MenuDigital.Infrastructure.Persistence.MySQLContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuDigital.Infrastructure.Seed
{
    public class SeedOInitizalization
    {
        public static async Task SeedAsync(AppDbContext context)
        {

            // Verifica se já existe alguma loja
            if (context.StoreModels.Any())
                return;

            // Cria uma loja inicial
            var store1 = new StoreModel
            {
                StoreId = Guid.NewGuid(),
                StoreName = "Valdisney ta na Disney",
                Description = "A melhor pizzaria artesanal da cidade com ingredientes selecionados.",
                StoreUrl = "https://pizzariacalabresa.com",
                HasImage = true,
                Closed = false,
                Colors = new Colors
                {
                    Primary = "",
                    Secondary = ""
                },
                Contacts = new Contact
                {
                    Phones = ["phone1 phone2"],
                    Emails = ["email1", "email2"],
                    Whatsapps = [""]
                },
                Images = new Images
                {
                    Header = "",
                    Logo = ""
                },
                SocialMedias = new List<SocialMedia>
                {
                    new SocialMedia
                    {
                        Name = "Instagram",
                        Url = ""
                    },
                    new SocialMedia
                    {
                        Name = "Facebook",
                        Url = ""
                    }
                },
                Alert = "Entrega grátis em pedidos acima de R$50!",
                MinOrderPrice = 30.0,
                Category = new List<Category>
                {
                    new Category { Name = "Pizzas", Icon = "🍕" },
                    new Category { Name = "Bebidas", Icon = "🥤" }
                },
                Address = new List<AddressModel>
                {
                    new AddressModel
                    {
                        ZipCode = "13338-000",
                        Street = "Rua das Pizzas",
                        Number = "123",
                        Neighborhood = "Centro",
                        City = "Indaiatuba",
                        Complement = "Próximo à praça central"
                    }
                },
                WorkSchedule = new List<WorkSchedule>
                {
                    new WorkSchedule
                    {
                        Day = new DateTime(2025, 10, 5),
                        IsSelected = true,
                        Start = new TimeSpan(18, 0, 0),
                        End = new TimeSpan(23, 30, 0)
                    },
                    new WorkSchedule
                    {
                        Day = new DateTime(2025, 10, 6),
                        IsSelected = true,
                        Start = new TimeSpan(18, 0, 0),
                        End = new TimeSpan(23, 30, 0)
                    }
                },
                StorePayments = new List<StorePayments>
                {
                    new StorePayments { PaymentsCount = (PaymentForm?)3 }
                }
            };
            var store2 = new StoreModel
            {
                StoreId = Guid.NewGuid(),
                StoreName = "Coala do Sertão",
                Description = "A melhor pizzaria artesanal da cidade dos Coalas ingredientes selecionados.",
                StoreUrl = "https://coaladosertao.com.br",
                HasImage = true,
                Closed = false,
                Alert = "Entrega grátis em pedidos acima de R$50!",
                MinOrderPrice = 30.0,
                Category = new List<Category>
                {
                    new Category { Name = "Pizzas", Icon = "🍕" },
                    new Category { Name = "Bebidas", Icon = "🥤" }
                },
                Address = new List<AddressModel>
                {
                    new AddressModel
                    {
                        ZipCode = "13338-000",
                        Street = "Rua das Pizzas",
                        Number = "123",
                        Neighborhood = "Centro",
                        City = "Indaiatuba",
                        Complement = "Próximo à praça central"
                    }
                },
                WorkSchedule = new List<WorkSchedule>
                {
                    new WorkSchedule
                    {
                        Day = new DateTime(2025, 10, 5),
                        IsSelected = true,
                        Start = new TimeSpan(18, 0, 0),
                        End = new TimeSpan(23, 30, 0)
                    },
                    new WorkSchedule
                    {
                        Day = new DateTime(2025, 10, 6),
                        IsSelected = true,
                        Start = new TimeSpan(18, 0, 0),
                        End = new TimeSpan(23, 30, 0)
                    }
                },
                StorePayments = new List<StorePayments>
                {
                    new StorePayments { PaymentsCount = (PaymentForm?)3 }
                }
            };
            StoreModel[] models = { store1, store2 };
            foreach (var item in models)
                await context.StoreModels.AddAsync(item);
            await context.SaveChangesAsync();


            // Evita duplicar
            if (context.Products.Any())
                return;

            // 🔹 Criação dos produtos
            var cocaId = Guid.NewGuid();
            var guaranaId = Guid.NewGuid();
            var calabresaId = Guid.NewGuid();
            var mussarelaId = Guid.NewGuid();
            var portuguesaId = Guid.NewGuid();
            var frangoCatupiryId = Guid.NewGuid();
            var margueritaId = Guid.NewGuid();
            var quatroQueijosId = Guid.NewGuid();
            var pepperoniId = Guid.NewGuid();
            var doceId = Guid.NewGuid();

            var storeId = store1.StoreId;

            var products = new List<ProductModel>
            {
                new()
                {
                    ProductId = calabresaId,
                    StoreId = storeId,
                    Name = "Pizza Calabresa",
                    Category = "Pizzas",
                    Description = "Pizza clássica com calabresa fatiada, cebola roxa e orégano.",
                    InactivedDate = null,
                    ImgUrl = "https://i.imgur.com/5UrFvV2.png",
                    Observations = "Contém glúten e lactose.",
                    Price = 39.90m,
                    PreviewPrice = 42.90m,
                    Additional = new List<Additional>
                    {
                        new Additional
                        {
                            Id = Guid.NewGuid(),
                            Category = "Bebidas",
                            ProductId = calabresaId,
                            Name = "Acompanhamentos",
                            Size = "500ml",
                            Min = 0,
                            Max = 2,
                            ProductIdList = new string[] { cocaId.ToString(), guaranaId.ToString() }
                        }
                    }
                },
                new ProductModel
                {
                    ProductId = mussarelaId,
                    StoreId = storeId,
                    Name = "Pizza Mussarela",
                    Category = "Pizzas",
                    Description = "Tradicional pizza com molho de tomate e mussarela derretida.",
                    ImgUrl = "https://i.imgur.com/fPg3KzE.png",
                    Observations = "Pizza simples e deliciosa.",
                    Price = 37.50m,
                    PreviewPrice = 40.00m,
                    Additional = new List<Additional>
                    {
                        new Additional
                        {
                            Id = Guid.NewGuid(),
                            Category = "Bebidas",
                            ProductId = mussarelaId,
                            Name = "Acompanhamentos",
                            Size = "500ml",
                            Min = 0,
                            Max = 2,
                            ProductIdList = new string[] { cocaId.ToString(), guaranaId.ToString() }
                        }
                    }
                },
                new ProductModel
                {
                    ProductId = portuguesaId,
                    StoreId = storeId,
                    Name = "Pizza Portuguesa",
                    Category = "Pizzas",
                    Description = "Presunto, cebola, ovo cozido, azeitona e mussarela.",
                    ImgUrl = "https://i.imgur.com/5A5ZQJm.png",
                    Observations = "Sabor tradicional e bem servido.",
                    Price = 42.90m,
                    PreviewPrice = 45.90m,
                    Additional = new List<Additional>
                    {
                        new Additional
                        {
                            Id = Guid.NewGuid(),
                            Category = "Bebidas",
                            ProductId = portuguesaId,
                            Name = "Bebidas",
                            Size = "Lata",
                            Min = 0,
                            Max = 2,
                            ProductIdList = new string[] { cocaId.ToString(), guaranaId.ToString() }
                        }
                    }
                },
                new ProductModel
                {
                    ProductId = frangoCatupiryId,
                    StoreId = storeId,
                    Name = "Pizza Frango com Catupiry",
                    Category = "Pizzas",
                    Description = "Frango desfiado com requeijão cremoso e orégano.",
                    ImgUrl = "https://i.imgur.com/epZLHYy.png",
                    Observations = "Recheio generoso e sabor marcante.",
                    Price = 43.90m,
                    PreviewPrice = 46.90m,
                    Additional = new List<Additional>
                    {
                        new Additional
                        {
                            Id = Guid.NewGuid(),
                            Category = "Bebidas",
                            ProductId = frangoCatupiryId,
                            Name = "Acompanhamentos",
                            Size = "600ml",
                            Min = 0,
                            Max = 2,
                            ProductIdList = new string[] { cocaId.ToString(), guaranaId.ToString() }
                        }
                    }
                },
                new ProductModel
                {
                    ProductId = margueritaId,
                    StoreId = storeId,
                    Name = "Pizza Marguerita",
                    Category = "Pizzas",
                    Description = "Mussarela, tomate e manjericão fresco.",
                    ImgUrl = "https://i.imgur.com/lP3rK2Q.png",
                    Observations = "Sabor leve e refrescante.",
                    Price = 41.90m,
                    PreviewPrice = 44.00m,
                    Additional = new List<Additional>
                    {
                        new Additional
                        {
                            Id = Guid.NewGuid(),
                            Category = "Bebidas",
                            ProductId = margueritaId,
                            Name = "Bebidas",
                            Size = "Lata",
                            Min = 0,
                            Max = 2,
                            ProductIdList = new string[] { cocaId.ToString(), guaranaId.ToString() }
                        }
                    }
                },
                new ProductModel
                {
                    ProductId = quatroQueijosId,
                    StoreId = storeId,
                    Name = "Pizza Quatro Queijos",
                    Category = "Pizzas",
                    Description = "Mussarela, provolone, parmesão e gorgonzola.",
                    ImgUrl = "https://i.imgur.com/fzCwMZz.png",
                    Observations = "Perfeita para os amantes de queijo.",
                    Price = 46.90m,
                    PreviewPrice = 49.90m,
                    Additional = new List<Additional>
                    {
                        new Additional
                        {
                            Id = Guid.NewGuid(),
                            Category = "Bebidas",
                            ProductId = quatroQueijosId,
                            Name = "Acompanhamentos",
                            Size = "600ml",
                            Min = 0,
                            Max = 2,
                            ProductIdList = new string[] { cocaId.ToString(), guaranaId.ToString() }
                        }
                    }
                },
                new ProductModel
                {
                    ProductId = pepperoniId,
                    StoreId = storeId,
                    Name = "Pizza Pepperoni",
                    Category = "Pizzas",
                    Description = "Molho especial, pepperoni fatiado e mussarela.",
                    ImgUrl = "https://i.imgur.com/wUZDd1D.png",
                    Observations = "Levemente picante e muito saborosa.",
                    Price = 44.90m,
                    PreviewPrice = 47.90m,
                    Additional = new List<Additional>
                    {
                        new Additional
                        {
                            Id = Guid.NewGuid(),
                            Category = "Bebidas",
                            ProductId = pepperoniId,
                            Name = "Bebidas",
                            Size = "Lata",
                            Min = 0,
                            Max = 2,
                            ProductIdList = new string[] { cocaId.ToString(), guaranaId.ToString() }
                        }
                    }
                },
                new ProductModel
                {
                    ProductId = doceId,
                    StoreId = storeId,
                    Name = "Pizza Doce Chocolate",
                    Category = "Pizzas Doces",
                    Description = "Coberta com chocolate ao leite e morangos frescos.",
                    ImgUrl = "https://i.imgur.com/gdHkJfW.png",
                    Observations = "Deliciosa opção de sobremesa.",
                    Price = 38.90m,
                    PreviewPrice = 40.00m,
                    Additional = new List<Additional>
                    {
                        new Additional
                        {
                            Id = Guid.NewGuid(),
                            Category = "Bebidas",
                            ProductId = doceId,
                            Name = "Acompanhamentos",
                            Size = "350ml",
                            Min = 0,
                            Max = 1,
                            ProductIdList = new string[] { cocaId.ToString(), guaranaId.ToString() }
                        }
                    }
                },
                new ProductModel
                {
                    ProductId = cocaId,
                    StoreId = storeId,
                    Name = "Coca-Cola 2L",
                    Category = "Bebidas",
                    Description = "Refrigerante sabor cola, 2 litros.",
                    ImgUrl = "https://i.imgur.com/jhKUd5p.png",
                    Observations = "Servido bem gelado.",
                    Price = 12.90m,
                    PreviewPrice = 14.90m
                },
                new ProductModel
                {
                    ProductId = guaranaId,
                    StoreId = storeId,
                    Name = "Guaraná Antarctica 2L",
                    Category = "Bebidas",
                    Description = "Refrigerante tradicional brasileiro, 2 litros.",
                    ImgUrl = "https://i.imgur.com/kAN9mBd.png",
                    Observations = "Perfeito para acompanhar pizzas.",
                    Price = 11.90m,
                    PreviewPrice = 13.50m
                }
            };

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();

            Console.WriteLine("✅ Product seed completed successfully.");
        }
    }
}
