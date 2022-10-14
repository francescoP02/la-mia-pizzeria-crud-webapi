using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;

namespace la_mia_pizzeria_static.Controllers.Api
{
    [Route("api/pizzas")]
    [ApiController]
    public class PizzasController : Controller
    {
        private PizzaContext _db;

        public PizzasController()
        {
            _db = new PizzaContext();
        }

        [HttpGet]
        public IActionResult GetPizzas()
        {
            IQueryable<Pizza> pizzas = _db.pizzasList;
            return Ok(pizzas.ToList());
        }
    }
}
