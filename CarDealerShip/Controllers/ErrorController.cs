using CarDealerShip.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarDealerShip.Controllers
{
    //非開發環境發生例外時由 UseExceptionHandler 轉到這裡
    public class ErrorController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            var vm = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier//對照伺服器日誌用
            };
            return View("Error", vm);
        }
    }
}
