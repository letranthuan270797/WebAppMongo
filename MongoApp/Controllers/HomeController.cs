using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoApp.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MongoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ICustomerRepository _customerRepository;
        public IConfiguration Configuration { get; }
        public HomeController
        (
            ILogger<HomeController> logger,
            ICustomerRepository customerRepository
        )
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        public async Task<IActionResult> Index()
        {
            var res = await _customerRepository.GetCustomers();
            return View(res);
        }

        public IActionResult Privacy()
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
