using GenericApp.Web.Data;
using GenericApp.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericApp.Web.Data
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
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new CountryEntity
                {
                    Name = "Argentina",
                    Departments = new List<DepartmentEntity>
                {
                    new DepartmentEntity
                    {
                        Name = "Córdoba",
                        Cities = new List<CityEntity>
                        {
                            new CityEntity { Name = "Córdoba" },
                            new CityEntity { Name = "Río Cuarto" },
                            new CityEntity { Name = "Villa María" }
                        }
                    },
                    new DepartmentEntity
                    {
                        Name = "Buenos Aires",
                        Cities = new List<CityEntity>
                        {
                            new CityEntity { Name = "La Plata" },
                            new CityEntity { Name = "Mar del  Plata" },
                            new CityEntity { Name = "Tandil" }
                        }
                    },
                    new DepartmentEntity
                    {
                        Name = "Santa Fe",
                        Cities = new List<CityEntity>
                        {
                            new CityEntity { Name = "Santa Fe" },
                            new CityEntity { Name = "Rosario" },
                            new CityEntity { Name = "Venado Tuerto" }
                        }
                    }
                },
                    Teams = new List<TeamEntity>
                {
                    new TeamEntity
                    {
                        Name = "Talleres",
                    },
                    new TeamEntity
                    {
                        Name = "Belgrano",
                    },
                    new TeamEntity
                    {
                        Name = "River Plate",
                    },
                    new TeamEntity
                    {
                        Name = "Boca Juniors",
                    },
                    }
                });

                _context.Countries.Add(new CountryEntity
                {
                    Name = "USA",
                    Departments = new List<DepartmentEntity>
                {
                    new DepartmentEntity
                    {
                        Name = "California",
                        Cities = new List<CityEntity>
                        {
                            new CityEntity { Name = "Los Angeles" },
                            new CityEntity { Name = "San Diego" },
                            new CityEntity { Name = "San Francisco" }
                        }
                    },
                    new DepartmentEntity
                    {
                        Name = "Illinois",
                        Cities = new List<CityEntity>
                        {
                            new CityEntity { Name = "Chicago" },
                            new CityEntity { Name = "Springfield" }
                        }
                    },
                    new DepartmentEntity
                    {
                        Name = "Florida",
                        Cities = new List<CityEntity>
                        {
                            new CityEntity { Name = "Miami" },
                            new CityEntity{ Name = "Orlando" }
                        }
                    }
                },
                    Teams = new List<TeamEntity>
                {
                    new TeamEntity
                    {
                        Name = "San Antonio Spurs",
                    },
                    new TeamEntity
                    {
                        Name = "Los Angeles Lakers",
                    },
                    new TeamEntity
                    {
                        Name = "Miami Heats",
                    },
                    new TeamEntity
                    {
                        Name = "New York Knicks",
                    },
                    }
                });

            };
            await _context.SaveChangesAsync();
        }
    }
}