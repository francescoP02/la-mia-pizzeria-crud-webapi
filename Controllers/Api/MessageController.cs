using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessageController : Controller
    {

        private PizzaContext _db;

        public MessageController()
        {
            _db = new PizzaContext();
        }

        [HttpPost]
        public IActionResult Send([FromBody] Message message)
        {
            _db.messages.Add(message);
            _db.SaveChanges();
            return Ok();
        }
    }
}
