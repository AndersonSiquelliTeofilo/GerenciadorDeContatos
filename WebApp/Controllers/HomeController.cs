using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data.Interfaces;
using WebApp.Models;

namespace GerenciadorDeContatos.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContactRepository _contactRepository;
        private readonly IErrorLogRepository _errorLogRepository;

        public HomeController(IContactRepository contactRepository, IErrorLogRepository errorLogRepository)
        {
            _contactRepository = contactRepository;
            _errorLogRepository = errorLogRepository;
        }

        public async Task<IActionResult> Index()
        {
            DashboardViewModel model = new DashboardViewModel();

            try
            {
                model.Year = await _contactRepository.GetCountYearAsync();
                model.Mouth = await _contactRepository.GetCountMouthAsync();
                model.Today = await _contactRepository.GetCountTodayAsync();
            }
            catch (Exception e)
            {
                await _errorLogRepository.SaveExceptionAsync("HomeController", "Index", e);
            }

            return View(model);
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
