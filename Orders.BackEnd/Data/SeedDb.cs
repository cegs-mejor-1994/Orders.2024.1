using Orders.Shared.Entities;

namespace Orders.BackEnd.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckCategoriesAsync();
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Apple" });
                _context.Categories.Add(new Category { Name = "Autos" });                
                _context.Categories.Add(new Category { Name = "Belleze" });
                _context.Categories.Add(new Category { Name = "Calzado" });
                _context.Categories.Add(new Category { Name = "Comida" });
                _context.Categories.Add(new Category { Name = "Cosmeticos" });
                _context.Categories.Add(new Category { Name = "Deportes" });
                _context.Categories.Add(new Category { Name = "Erotica" });
                _context.Categories.Add(new Category { Name = "Ferreteria" });
                _context.Categories.Add(new Category { Name = "Gamer" });
                _context.Categories.Add(new Category { Name = "Hogar" });
                _context.Categories.Add(new Category { Name = "Jardin" });
                _context.Categories.Add(new Category { Name = "Juguetes" });
                _context.Categories.Add(new Category { Name = "Lenceria" });
                _context.Categories.Add(new Category { Name = "Mascotas" });
                _context.Categories.Add(new Category { Name = "Nutricion" });
                _context.Categories.Add(new Category { Name = "Ropa" });                
                _context.Categories.Add(new Category { Name = "Tecnologia" });
            }

            await _context.SaveChangesAsync();
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    Name = "Colombia",
                    States = new List<State>()
                    {
                        new State()
                        {
                            Name = "Antioquia",
                            Cities = new List<City>()
                            {
                                new City() { Name = "Medellin" },
                                new City() { Name = "Envigado" },
                                new City() { Name = "Itagui" }
                            }
                        },
                        new State()
                        {
                            Name = "Cundinamarca",
                            Cities = new List<City>()
                            {
                                new City() { Name = "Bogota" },
                                new City() { Name = "Chia" },
                                new City() { Name = "Facatativa" }
                            }
                        },
                    }
                });
                _context.Countries.Add(new Country
                {
                    Name = "USA",
                    States = new List<State>()
                    {
                        new State()
                        {
                            Name = "California",
                            Cities = new List<City>()
                            {
                                new City() { Name = "Los Angeles" },
                                new City() { Name = "San Diego" },
                                new City() { Name = "San Francisco" }
                            }
                        },
                        new State()
                        {
                            Name = "Illinois",
                            Cities = new List<City>()
                            {
                                new City() { Name = "Chicago" },
                                new City() { Name = "Springfield" }
                            }
                        },
                    }
                });
            }
            await _context.SaveChangesAsync();
        }
    }
}
