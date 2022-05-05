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

        public HomeController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<IActionResult> Index()
        {
            DashboardViewModel model = new DashboardViewModel()
            {
                Year = await _contactRepository.GetCountYearAsync(),
                Mouth = await _contactRepository.GetCountMouthAsync(),
                Today = await _contactRepository.GetCountTodayAsync()
            };

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
