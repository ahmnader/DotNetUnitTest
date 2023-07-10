using DevOpsWebApp.Data;
using DevOpsWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DevOpsWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _appDbContext;

        public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> Employee()
        {
            List<Employee> employees = await _appDbContext.Employees.ToListAsync();

            return View(employees);
        }
        public async Task<ActionResult> Edit(int id, string employeeName)
        {

            _appDbContext.Employees.Update(new Employee() { Id=id, FullName=employeeName});
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("employee");
        }
        public async Task<ActionResult> Delete(Employee employee)
        {
            _appDbContext.Employees.Remove(employee);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("employee");
        }
        [HttpPost]
        public async Task<ActionResult> Create(string name)
        {
            int employeeId = (await _appDbContext.Employees.ToListAsync()).Count != 0 ? _appDbContext.Employees.Max(r => r.Id) : 0;
            _appDbContext.Employees.Add(new Employee() { Id = employeeId+1, FullName = name });
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Employee");
        }
    }
}

