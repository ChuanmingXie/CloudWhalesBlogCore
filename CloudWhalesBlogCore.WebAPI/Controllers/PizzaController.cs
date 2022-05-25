using CloudWhalesBlogCore.WebAPI.Model;
using CloudWhalesBlogCore.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CloudWhalesBlogCore.WebAPI.Controllers
{
    /// <summary>
    /// pizza控制器接口
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PizzaController : ControllerBase
    {
        /// <summary>
        /// 获取pizza列表
        /// </summary>
        /// <returns></returns>
        // GET: api/<PizzaController>
        [HttpGet]
        public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll();

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<PizzaController>/5
        [HttpGet,Route("{id}")]
        //[Route("/pizza/{id}",Name ="GetPizzaById")]
        public ActionResult<Pizza> Get(int id)
        {
            var pizza = PizzaService.Get(id);
            if (pizza == null)
                return NotFound();
            return pizza;
        }

       /// <summary>
       /// 创建pizza
       /// </summary>
       /// <param name="pizza"></param>
       /// <returns></returns>
        // POST api/<PizzaController>
        [HttpPost]
        public IActionResult Create(Pizza pizza)
        {
            PizzaService.Add(pizza);
            return CreatedAtAction(nameof(Create), new { id = pizza.Id }, pizza);
        }

        /// <summary>
        /// 更新pizza
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pizza"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 删除pizza
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
