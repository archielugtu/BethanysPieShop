using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            // This is an example of constructor injection since we have already registered both of these reposities in Program.cs at the start of your program.
            // Therefore the instance of these reposities will get injected in the constructor and we can set these values to the private fields in this controller.
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }

        /*
         * IActionResult is the overarching interface for all different types of results implement
         */
        public IActionResult List()
        {
            PieListViewModel pieListViewModel = new(_pieRepository.AllPies, "Cheese cakes");
            return View(pieListViewModel);
        }
    }
}
