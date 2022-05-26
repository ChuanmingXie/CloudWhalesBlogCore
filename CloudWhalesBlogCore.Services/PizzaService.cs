/*****************************************************************************
*项目名称:CloudWhalesBlogCore.WebAPI.Services
*项目描述:
*类 名 称:Class
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/11 11:37:09
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using CloudWhalesBlogCore.Model.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace CloudWhalesBlogCore.WebAPI.Services
{
    public class PizzaService
    {
        static List<Pizza> Pizzas { get; }

        static int nextId = 3;

        static PizzaService()
        {
            Pizzas = new List<Pizza>
            {
                new Pizza{Id=1,Name="Classic Itelian", IsGlutenFree=false},
                new Pizza{Id=2,Name="Veggie", IsGlutenFree=true}
            };
        }

        public static List<Pizza> GetAll() => Pizzas;
# nullable enable
        public static Pizza? Get(int id) => Pizzas.FirstOrDefault(p => p.Id == id);

        public static void Add(Pizza pizza)
        {
            pizza.Id = nextId++;
            Pizzas.Add(pizza);
        }

        public static void Delete (int Id)
        {
            var pizza = Get(Id);
            if (pizza is null)
                return;
            Pizzas.Remove(pizza);
        }

        public static void Update(Pizza pizza)
        {
            var index = Pizzas.FindIndex(p => p.Id == pizza.Id);
            if (index == -1)
                return;

            Pizzas[index] = pizza;
        }
    }
}
