using DataAccess.Models;
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

        [HttpGet]
        public async Task<IActionResult> GetAsync(int id)
        {
            var cus = await _customerRepository.GetCustomerById(id);
            return View("GetCustomer", cus);
        }
        
        public ActionResult Insert()
        {
            return View("Insert", new Customer());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert([Bind(include: "CustomerId, Name, Age, Salary")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _customerRepository.Create(customer);
                TempData["Message"] = "Thêm dữ liệu thành công";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Customer customer = await _customerRepository.GetCustomerById(id);
            return View("Update", customer);
        }

        [HttpPut]
        public async Task<IActionResult> Update([Bind(include: "CustomerId, Name, Age, Salary")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _customerRepository.Update(customer);
                TempData["Message"] = "Cập nhật dữ liệu thành công";
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var cus = await _customerRepository.Delete(id);
            return View("ConfirmDelete", cus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var cus = await _customerRepository.GetCustomerById(id);
            if (cus == null)
                return new NotFoundResult();
            var delete = await _customerRepository.Delete(id);
            if (delete)
                TempData["Message"] = "Xóa dữ liệu thành công";
            else 
                TempData["Message"] = "Đã có lỗi xẫy ra khi xóa";
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
