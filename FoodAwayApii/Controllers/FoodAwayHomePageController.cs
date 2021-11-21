using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FoodAwayApii.Models;

namespace TalbatApi.Controllers
{
    public class FoodAwayHomePageController : ApiController
    {


        private FoodAway db = new FoodAway();
        [HttpGet]
        [Route("api/RestrauntAddresses")]
        public IHttpActionResult RestrauntAddresses()
        {
            var restaurants = db.Restaurants.Include("Address").
                Select(c => c.Address).Distinct();
            return Ok(restaurants);

        }
        [HttpGet]
        [Route("api/GetCities")]
        public IHttpActionResult GetCities()
        {
            var restaurants = db.Restaurants.Include("Address").
                Select(c => c.Address.city).Distinct();
            return Ok(restaurants);

        }
        [Route("api/TopRatedRestaunt/{_city}")]
        [HttpGet]
        public IHttpActionResult TopRatedRestaunt(string _city)
        {
            if (_city == null)
            {
                return BadRequest();
            }
            //int restaurant_id = db.Restaurants.Include("Address").Where(a => a.Address.city == _city).Select(res => res.RestaurantId).FirstOrDefault();
            //if (restaurant_id == 0)
            //{
            //    return NotFound();
            //}
            //var restaurants = db.RestaurantCustomers.Where(c => c.RestaurantId == restaurant_id).Where(x=>x.Rate>4).Select(k=>k.Restaurant);
            //return Ok(restaurants);

            //List<Restaurant> _restraunt = db.Restaurants.Where(s => s.Address.city == _city).ToList();
            List<Restaurant> rets = (from R in db.Restaurants.Where(s => s.Address.city == _city).ToList()
                                     join C in db.RestaurantCustomers.Where(x => x.Rate > 4) on R.RestaurantId equals C.RestaurantId
                                     select R
                                   ).ToList();
            if (rets.Count() == 0)
            {
                return NotFound();
            }
            return Ok(rets);
        }



    }
}