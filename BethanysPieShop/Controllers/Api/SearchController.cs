using BethanysPieShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers.Api
{
   //[controller] is evaluated to the name of the controller class, in this case, "Search"
   //[Route] ensures that we get actions to this controller class
   //[ApiController] is optional, don't need this to make the API controller work. It just adds a few API-specific behaviours
   [Route("api/[controller]")]
   [ApiController]
   public class SearchController : ControllerBase //Controllers for Web API inherits from ControllerBase. This excludes View functionality which we don't need in Web API
   {
      private readonly IPieRepository _pieRepository;

      public SearchController(IPieRepository pieRepository)
      {
         _pieRepository = pieRepository;
      }

      // GET requests to api/Search will go to this method
      [HttpGet]
      public IActionResult GetAll()
      {
         var allPies = _pieRepository.AllPies;
         return Ok(allPies);
         
      }

      // GET requests with id parameter will go this method
      [HttpGet("{id}")]
      public IActionResult GetById(int id)
      {
         if (!_pieRepository.AllPies.Any(p => p.PieId == id))
            return NotFound();

         return Ok(_pieRepository.AllPies.Where(p => p.PieId == id));
      }
   }
}
