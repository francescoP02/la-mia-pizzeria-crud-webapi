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
        public IActionResult Get(string? search)
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
        public IActionResult Show(int id)
        {

            Pizza pizza = _db.pizzasList.Where(pizza => pizza.Id == id).Include("Category").Include("Ingredients").FirstOrDefault();

            if (pizza == null)
            {
                return NotFound();
            }
            return Ok(pizza);

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Pizza pizza)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
                Pizza pizzaDaModificare = _db.pizzasList.Where(p => p.Id == id).FirstOrDefault();

                if (pizzaDaModificare != null)
                {
                    pizzaDaModificare.Name = pizza.Name;
                    pizzaDaModificare.Description = pizza.Description;
                    pizzaDaModificare.Photo = pizza.Photo;
                    pizzaDaModificare.Price = pizza.Price;
                    _db.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

                Pizza pizzaToRemove = _db.pizzasList.Where(p => p.Id == id).FirstOrDefault();

                if (pizzaToRemove != null)
                {
                    _db.pizzasList.Remove(pizzaToRemove);
                    _db.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            
        }
    }
}
