using System.Collections.Generic;
using OdeToFood.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext db;

        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            this.db = db;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetRestaurantById(id);

            if (restaurant != null)
            {
                db.Restaurants.Remove(restaurant);
            }

            return restaurant;
        }

        public Restaurant GetRestaurantById(int id)
        {
            return db.Restaurants.Find(id);
        }

        public int GetRestaurantCount()
        {
            return db.Restaurants.Count();
        }

        public IEnumerable<Restaurant> GetRestaurants(string name)
        {
            var query = from r in db.Restaurants
                        where string.IsNullOrEmpty(name) || r.Name.Contains(name, System.StringComparison.OrdinalIgnoreCase)
                        orderby r.Name
                        select r;

            return query;
        }

        public Restaurant Insert(Restaurant newRestaurant)
        {
            db.Add(newRestaurant);

            return newRestaurant;
        }

        public Restaurant Update(Restaurant newRestaurant)
        {
            var entity = db.Restaurants.Attach(newRestaurant);
            entity.State = EntityState.Modified;

            return newRestaurant;
        }
    }
}
