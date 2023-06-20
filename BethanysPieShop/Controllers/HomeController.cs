using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepository _pieRepository;

        // This is an example of constructor injection since we have already registered both of these reposities in Program.cs at the start of the program.
        // Therefore the instance of these reposities will get injected into the constructor (automatically) and we can use these values to set the private fields in this controller.
        public HomeController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new(_pieRepository.PiesOfTheWeek); // passes in the PiesOfTheWeek in the HomeViewModel constructor
            return View(homeViewModel);
        }
    }
}
