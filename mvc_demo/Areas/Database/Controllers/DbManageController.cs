using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc_demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace mvc_demo.Areas.Database.Controllers
{
    [Area("database")]
    public class DbManageController : Controller
    {
        private readonly AppDbContext _dbContext;

        public DbManageController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("admin/quanlydb")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("admin/xoadb")]
        public IActionResult DeleteDatabase()
        {
            return View();
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpPost("admin/xoadb")]
        public async Task<IActionResult> DeleteDatabaseAsync()
        {
            bool succes = await _dbContext.Database.EnsureDeletedAsync();

            if (succes)
                StatusMessage = $@"Xóa database
                    {_dbContext.Database.GetDbConnection().Database} 
                    thành công";
            else
                StatusMessage = $@"Xóa database
                    { _dbContext.Database.GetDbConnection().Database}
                        thất bại";

            return RedirectToAction("index");
        }

        [HttpPost("admin/taodb")]
        public async Task<IActionResult> MigrateAsync()
        {
            await _dbContext.Database.MigrateAsync();

            
            StatusMessage = "Cập nhật database thành công !";

            return RedirectToAction("index");
        }
    }
}
