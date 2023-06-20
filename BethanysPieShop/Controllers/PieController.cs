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
            // This is an example of constructor injection since we have already registered both of these reposities in Program.cs at the start of the program.
            // Therefore the instance of these reposities will get injected into the constructor (automatically) and we can use these values to set the private fields in this controller.
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }

        /*
         * IActionResult is the overarching interface for all different types of results implement
         */
        public IActionResult List()
        {
            PieListViewModel pieListViewModel = new(_pieRepository.AllPies, "All Yummy Pies");
            return View(pieListViewModel);
        }

        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie == null)
                return NotFound();
            return View(pie);
        }
    }
}
