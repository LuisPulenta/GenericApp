using GenericApp.Web.Data;
using GenericApp.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GenericApp.Web.Controllers
{
    public class CountriesController : Controller
    {
        private readonly DataContext _context;

        public CountriesController(DataContext context)
        {
            _context = context;
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            return View(await _context.Countries
                .Include(c => c.Departments)
                .Include(t => t.Teams)
                .ToListAsync());
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .Include(c => c.Departments)
                .ThenInclude(d => d.Cities)
                .Include(t => t.Teams)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CountryEntity country)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(country);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Hay un registro con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(country);
        }


        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countryEntity = await _context.Countries.FindAsync(id);
            if (countryEntity == null)
            {
                return NotFound();
            }
            return View(countryEntity);
        }

        // POST: Countries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CountryEntity country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Hay un registro con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(country);
        }

        // POST: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CountryEntity country = await _context.Countries
                .Include(c => c.Departments)
                .ThenInclude(d => d.Cities)
                .Include(t => t.Teams)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool CountryEntityExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }

        public async Task<IActionResult> AddDepartment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CountryEntity country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            DepartmentEntity model = new DepartmentEntity { IdCountry = country.Id };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDepartment(DepartmentEntity department)
        {
            if (ModelState.IsValid)
            {
                CountryEntity country = await _context.Countries
                    .Include(c => c.Departments)
                    .FirstOrDefaultAsync(c => c.Id == department.IdCountry);
                if (country == null)
                {
                    return NotFound();
                }

                try
                {
                    department.Id = 0;
                    country.Departments.Add(department);
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"{nameof(Details)}/{country.Id}");

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Hay un registro con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(department);
        }

        public async Task<IActionResult> AddTeam(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CountryEntity country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            TeamEntity model = new TeamEntity { IdCountry = country.Id };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTeam(TeamEntity team)
        {
            if (ModelState.IsValid)
            {
                CountryEntity country = await _context.Countries
                    .Include(c => c.Teams)
                    .FirstOrDefaultAsync(c => c.Id == team.IdCountry);
                if (country == null)
                {
                    return NotFound();
                }

                try
                {
                    team.Id = 0;
                    country.Teams.Add(team);
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"{nameof(Details)}/{country.Id}");

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Hay un registro con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(team);
        }

        public async Task<IActionResult> EditDepartment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DepartmentEntity department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            CountryEntity country = await _context.Countries.FirstOrDefaultAsync(c => c.Departments.FirstOrDefault(d => d.Id == department.Id) != null);
            department.IdCountry = country.Id;
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDepartment(DepartmentEntity department)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"{nameof(Details)}/{department.IdCountry}");

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Hay un registro con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(department);
        }

        public async Task<IActionResult> EditTeam(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TeamEntity team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            CountryEntity country = await _context.Countries.FirstOrDefaultAsync(c => c.Teams.FirstOrDefault(d => d.Id == team.Id) != null);
            team.IdCountry = country.Id;
            return View(team);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTeam(TeamEntity team)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"{nameof(Details)}/{team.IdCountry}");

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Hay un registro con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(team);
        }

        public async Task<IActionResult> DeleteDepartment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DepartmentEntity department = await _context.Departments
                .Include(d => d.Cities)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            CountryEntity country = await _context.Countries.FirstOrDefaultAsync(c => c.Departments.FirstOrDefault(d => d.Id == department.Id) != null);
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return RedirectToAction($"{nameof(Details)}/{country.Id}");
        }

        public async Task<IActionResult> DeleteTeam(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TeamEntity team = await _context.Teams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            CountryEntity country = await _context.Countries.FirstOrDefaultAsync(c => c.Teams.FirstOrDefault(d => d.Id == team.Id) != null);
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction($"{nameof(Details)}/{country.Id}");
        }

        public async Task<IActionResult> DetailsDepartment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DepartmentEntity department = await _context.Departments
                .Include(d => d.Cities)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            CountryEntity country = await _context.Countries.FirstOrDefaultAsync(c => c.Departments.FirstOrDefault(d => d.Id == department.Id) != null);
            department.IdCountry = country.Id;
            return View(department);
        }

        public async Task<IActionResult> AddCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DepartmentEntity department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            CityEntity model = new CityEntity { IdDepartment = department.Id };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCity(CityEntity city)
        {
            if (ModelState.IsValid)
            {
                DepartmentEntity department = await _context.Departments
                    .Include(d => d.Cities)
                    .FirstOrDefaultAsync(c => c.Id == city.IdDepartment);
                if (department == null)
                {
                    return NotFound();
                }

                try
                {
                    city.Id = 0;
                    department.Cities.Add(city);
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"{nameof(DetailsDepartment)}/{department.Id}");

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Hay un registro con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(city);
        }

        public async Task<IActionResult> EditCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CityEntity city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            DepartmentEntity department = await _context.Departments.FirstOrDefaultAsync(d => d.Cities.FirstOrDefault(c => c.Id == city.Id) != null);
            city.IdDepartment = department.Id;
            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCity(CityEntity city)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(city);
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"{nameof(DetailsDepartment)}/{city.IdDepartment}");

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Hay un registro con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(city);
        }

        public async Task<IActionResult> DeleteCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CityEntity city = await _context.Cities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            DepartmentEntity department = await _context.Departments.FirstOrDefaultAsync(d => d.Cities.FirstOrDefault(c => c.Id == city.Id) != null);
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
            return RedirectToAction($"{nameof(DetailsDepartment)}/{department.Id}");
        }


    }
}