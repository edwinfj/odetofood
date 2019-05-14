using OdeToFood.Core;
using System.Collections.Generic;
using System.Text;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurants(string name);

        Restaurant GetRestaurantById(int id);

        Restaurant Update(Restaurant newRestaurant);

        Restaurant Insert(Restaurant newRestaurant);

        int Commit();

        Restaurant Delete(int id);
    }
}
