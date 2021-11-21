using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FoodAwayApii.Models;

namespace TalbatApi.Controllers
{
    public class FilterController : ApiController
    {
        FoodAway db = new FoodAway();
        // GET api/values


        [Route("api/values/tab1")]
        public IEnumerable<Restaurant> Gettab1(string city)
        {
            List<Restaurant> res=new List<Restaurant>();
            List<List<Restaurant>> list = db.Addresses.Where(a => a.city == city).Select(a=>a.Restaurants).ToList();
            for(int i=0;i<list.Count;i++)
            {
                for(int j=0;j<list[i].Count;j++)
                {
                    res.Add(list[i][j]);
                }
            }
             
            return res; 
        }
        [Route("api/values/tab4")]
        public List<Restaurant> Gettab2(string city)
        {
            List<RestaurantCustomer> rc = db.RestaurantCustomers.ToList();
            for(int i=0;i<rc.Count;i++)
            {
                for(int j=0;j<rc.Count;j++)
                {
                    if(i!=j)
                    {
                        if(rc[i].RestaurantId==rc[j].RestaurantId)
                        {
                            rc[i].Rate += rc[j].Rate;
                            rc.RemoveAt(j);
                            break;
                        }
                    }
                }
            }

            List<List<Restaurant>> queryable = db.Addresses.Where(b => b.city == city).Select(e => e.Restaurants).ToList();
            for (int o = 0; o < rc.Count; o++)
            {
                for (int j = 0; j < queryable.Count; j++)
                {
                    for (int y = 0; y < queryable[j].Count; y++)
                    {
                        if (rc[o].RestaurantId != queryable[j][y].RestaurantId)
                        {
                            rc.RemoveAt(o);
                        }
                    }
                }

            }

            if (queryable.Count != 0)
                return rc.OrderByDescending(a => a.Rate).Select(a => a.Restaurant).ToList();
            else
                return new List<Restaurant>();


            
        }
        [Route("api/values/tab3")]
        public IEnumerable<Restaurant> Gettab3()
        {
            return null;
        }
        [Route("api/values/tab2")]
        public IEnumerable<Restaurant> Gettab4(string city)
        {
            List<Restaurant> res = db.Restaurants.OrderBy(a => a.RestaurantName).ToList();

            List<List<Restaurant>> queryable = db.Addresses.Where(b => b.city == city).Select(e => e.Restaurants).ToList();
            for (int o = 0; o < res.Count; o++)
            {
                for (int j = 0; j < queryable.Count; j++)
                {
                    for (int y = 0; y < queryable[j].Count; y++)
                    {
                        if (res[o].RestaurantId != queryable[j][y].RestaurantId)
                        {
                            res.RemoveAt(o);
                        }
                    }
                }

            }

            if (queryable.Count != 0)
                return res;
            else
                return new List<Restaurant>();
        }


        [Route("api/values/search")]
        public List<Restaurant> Get(string character,string city)
        {
            List<Restaurant> res = db.Restaurants.Where(a => a.RestaurantName.Contains(character)).Select(a => a).ToList();

            List<List<Restaurant>> queryable = db.Addresses.Where(b => b.city == city).Select(e => e.Restaurants).ToList();
            for (int o = 0; o < res.Count; o++)
            {
                for (int j = 0; j < queryable.Count; j++)
                {
                    for (int y = 0; y < queryable[j].Count; y++)
                    {
                        if (res[o].RestaurantId != queryable[j][y].RestaurantId)
                        {
                            res.RemoveAt(o);
                        }
                    }
                }

            }

            if (queryable.Count != 0)
                return res;
            else
                return new List<Restaurant>();
           
        }

        [Route("api/values/GetAllCusins")]
        public IEnumerable<Cuisine> GetAllCusins()=>db.Cuisines;


        [Route("api/values/Cusins")]

        public IEnumerable<Restaurant> GetAllCusinsFilter(string filter,string city)
        {
            List<Restaurant>res= db.RestaurantCusines.Where(a=>a.Cuisine.CuisineName== filter).Select(a=>a.Restaurant).ToList();
            List<List<Restaurant>> queryable = db.Addresses.Where(b => b.city == city).Select(e => e.Restaurants).ToList();
            for (int o = 0; o < res.Count; o++)
            {
                for (int j = 0; j < queryable.Count; j++)
                {
                    for (int y = 0; y < queryable[j].Count; y++)
                    {
                        if (res[o].RestaurantId != queryable[j][y].RestaurantId)
                        {
                            res.RemoveAt(o);
                        }
                    }
                }

            }

            if (queryable.Count != 0)
                return res;
            else
                return new List<Restaurant>();
        }


        // POST api/values
       
    }
}
