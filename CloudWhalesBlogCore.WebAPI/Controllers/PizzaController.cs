using CloudWhalesBlogCore.WebAPI.Model;
using CloudWhalesBlogCore.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CloudWhalesBlogCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        // GET: api/<PizzaController>
        [HttpGet]
        public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll();


        // GET api/<PizzaController>/5
        [HttpGet("{id}")]
        [Route("api/pizza/{id}",Name ="GetPizzaById")]
        public ActionResult<Pizza> Get(int id)
        {
            var pizza = PizzaService.Get(id);
            if (pizza == null)
                return NotFound();
            return pizza;
        }

        // POST api/<PizzaController>
        [HttpPost]
        public IActionResult Create(Pizza pizza)
        {
            PizzaService.Add(pizza);
            return CreatedAtAction(nameof(Create), new { id = pizza.Id }, pizza);
        }

        // PUT api/<PizzaController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza pizza)
        {
            if (id != pizza.Id)
                return BadRequest();
            var existingPizza = PizzaService.Get(id);
            if (existingPizza is null)
                return NotFound();
            PizzaService.Update(pizza);
            return NoContent();
        }

        // DELETE api/<PizzaController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pizza = PizzaService.Get(id);
            if (pizza is null)
            {
                return NotFound();
            }
            PizzaService.Delete(id);
            return NoContent();
        }
    }
}
