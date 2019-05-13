using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurants(string name);
        Restaurant GetRestaurantById(int id);
        Restaurant Update(Restaurant newRestaurant);
        int Commit();
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
        public IEnumerable<Restaurant> GetRestaurants(string name)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.Contains(name, StringComparison.OrdinalIgnoreCase)
                   orderby r.Id
                   select r;
        }

        public Restaurant GetRestaurantById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        public Restaurant Update(Restaurant newRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.Id == newRestaurant.Id);

            if (restaurant != null)
            {
                restaurant.Name = newRestaurant.Name;
                restaurant.Location = newRestaurant.Location;
                restaurant.Cuisine = newRestaurant.Cuisine;
            }
            else if (newRestaurant.Id == 0)
            {
                newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
                restaurants.Add(newRestaurant);
            }

            return restaurants.SingleOrDefault(r => r.Id == newRestaurant.Id);
        }

        public int Commit()
        {
            return 0;
        }
    }
}
