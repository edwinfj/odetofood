using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
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

            return restaurant;
        }

        public int Commit()
        {
            return 0;
        }

        public Restaurant Insert(Restaurant newRestaurant)
        {
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            restaurants.Add(newRestaurant);

            return newRestaurant;
        }

        public Restaurant Delete(int id)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.Id == id);

            if (restaurant != null)
            {
                restaurants.Remove(restaurant);
            }

            return restaurant;
        }
    }
}
