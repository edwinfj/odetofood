using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant { Id = 1, Name = "May Flower", Cuisine = CuisineType.Chinese, Location = "New York"},
                new Restaurant { Id = 2, Name = "Kong", Cuisine = CuisineType.Japanese, Location = "San Jose"},
                new Restaurant { Id = 3, Name = "Jang Sujang", Cuisine = CuisineType.Korean, Location = "Seattle"}
            };
        }
        public IEnumerable<Restaurant> GetAll()
        {
            return from r in restaurants
                   orderby r.Id
                   select r;
        }
    }
}
