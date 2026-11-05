using CarDealerShip.Services;
using CarDealerShip.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarDealerShip.Controllers
{
    [Authorize(Roles = "Boss")]
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        public async Task<IActionResult> Index([Bind("Brand,Model,Status,BranchId,Page")] CarListViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            CarListViewModel returnVM = await _carService.GetCarListAsync(vm);
            return View(returnVM);
        }
    }
}
