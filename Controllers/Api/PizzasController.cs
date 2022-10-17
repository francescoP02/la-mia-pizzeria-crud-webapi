using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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
        public IActionResult GetPizzas(string? search)
        {
            List<Pizza> pizzas;

            if (search != null && search != "")
            {
                pizzas = _db.pizzasList.Where(pizza => pizza.Name.Contains(search)).ToList();
            } else
            {
                pizzas = _db.pizzasList.ToList();
            }
            return Ok(pizzas);
        }

        [HttpGet("{id}")]
        public IActionResult ShowPizza(int id)
        {

            Pizza pizza = _db.pizzasList.Where(pizza => pizza.Id == id).Include("Category").Include("Ingredients").FirstOrDefault();

            if (pizza == null)
            {
                return NotFound();
            }
            return Ok(pizza);

        }
    }
}
