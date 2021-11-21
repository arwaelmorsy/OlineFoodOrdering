using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FoodAwayApii.Models;

namespace TalbatApi.Controllers
{
    public class CustomerController : ApiController
    {
        private FoodAway db = new FoodAway();

        // GET: api/Customer
        public IQueryable<Customer> GetCustomers()
        {
            return db.Customers;
        }

        // GET: api/Customer/5
        [ResponseType(typeof(Customer))]
        [Route("api/cust/{id}")]
        public IHttpActionResult GetCustomerById(int? id)
        {
            if (id == null)
                return BadRequest();
            Customer customer = db.Customers.Find(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        // PUT: api/Customer/5
        [ResponseType(typeof(void))]
        [Route("api/editAcc/{id}")]
        public IHttpActionResult PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("api/custOrders/{id}")]
        public IHttpActionResult getCustomerOrders(int? id)
        {
            if (id == null)
                return BadRequest();

            //List<Order> CustOrders = db.Orders.Where(o => o.CustomerId == id).ToList();

            //if (CustOrders == null)
            //    return NotFound();
            //return Ok(CustOrders);

            var MealsOrder = (from C in db.Customers.Where(c=>c.CustomerId==id)
                              join O in db.Orders on C.CustomerId equals O.CustomerId
                              join MO in db.MealOrders on O.OrderId equals MO.OrderId
                              join M in db.Meals on MO.MealId equals M.MealId
                              select M).ToList();
            if (MealsOrder == null)
                return NotFound();
            return Ok(MealsOrder);
        }





        // POST: api/Customer
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customers.Add(customer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customer.CustomerId }, customer);
        }

        // DELETE: api/Customer/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult DeleteCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.Customers.Remove(customer);
            db.SaveChanges();

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return db.Customers.Count(e => e.CustomerId == id) > 0;
        }
    }
}