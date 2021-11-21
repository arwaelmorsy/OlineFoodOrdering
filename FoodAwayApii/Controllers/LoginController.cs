using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FoodAwayApii.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodAwayApii.Controllers
{
    public class LoginController : ApiController
    {

        public Object GetToken(string Mail, string Pass)
        {
            string key = "my_secret_key_12345"; //Secret key which will be used later during validation
            var issuer = "http://mysite.com"; //normally this will be your site URL
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //Create a List of Claims, Keep claims name short
            var permClaims = new List<Claim>();
            //permClaims.Add(new Claim("valid", "1"));
            //permClaims.Add(new Claim("userid", "1"));
            //permClaims.Add(new Claim("name", "bilal"));
            permClaims.Add(new Claim("Email", Mail));
            permClaims.Add(new Claim("Password", Pass));
            //Create Security Token object by giving required parameters
            var token = new JwtSecurityToken(issuer, //Issure
            issuer, //Audience
            permClaims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return new { data = jwt_token };
        }



        FoodAway db =new FoodAway();

        // POST api/Account/Login

        [HttpPost]
        [Route("api/Login/{Mail}/{Pass}")]
        public IHttpActionResult Login(string Mail, string Pass)
        {
            if (Mail == null || Pass == null)
                return BadRequest();
            var obj = db.Customers.Where(a => a.Email == Mail && a.Password == Pass).FirstOrDefault();
            // var token = GetToken(Mail, Pass);
            if (obj == null)
                return BadRequest();

            return Ok(obj);
        }



    //[HttpPost]
    //[Route("api/Login/{Mail}/{Pass}")]
    //public IHttpActionResult GetName1(string Mail, string Pass)
    //{
    //    if (User.Identity.IsAuthenticated)
    //    {

    //        var identity = User.Identity as ClaimsIdentity;
    //        var obj = db.Customers.Where(a => a.Email == Mail && a.Password == Pass).FirstOrDefault();
    //        if (identity != null && obj != null)
    //        {
    //            IEnumerable<Claim> claims = identity.Claims;
    //        }
    //        return Ok(obj);
    //    }
    //    else
    //    {
    //        return BadRequest();
    //    }
    //}
    //[HttpPost]
    //[Route("api/Login/{Mail}/{Pass}")]
    //public  IHttpActionResult GetName1(string Mail,string Pass,[FromBody]string all)
    //{
    //    HttpRequestMessage request = this.ActionContext.Request;
    //    if (request.Headers.Authorization == null ||
    //         string.IsNullOrEmpty(request.Headers.Authorization.Parameter))
    //    {
    //        return BadRequest();
    //    }

    //    string Token = request.Headers.Authorization.Parameter;
    //    //    var client = new HttpClient();

    //    //if (Mail == null || Pass == null)
    //    //    return BadRequest();
    //    //var obj = db.Customers.Where(a => a.Email == Mail && a.Password == Pass).FirstOrDefault();
    //    ////GetToken(Mail, Pass);
    //    // var token = GetToken(Mail, Pass);
    //    //var tokenn = client.DefaultRequestHeaders.Authorization;
    //    //if (obj == null)
    //    //    return BadRequest();

    //    return Ok(Token);

    //}

    //static async Task Login()
    //{
    //    using (var client = new HttpClient())
    //    {
    //        client.BaseAddress = new Uri("https://api.xxxxxx.com/auth/");
    //        var content = new FormUrlEncodedContent(new[]
    //        {
    //        new KeyValuePair<string, string>("email", "xxx@localhost.com"),
    //        new KeyValuePair<string, string>("password", "hello123")
    //    });

    //        var result = await client.PostAsync("sign_in", content);
    //        string resultContent = await result.Content.ReadAsStringAsync();
    //        Console.WriteLine(resultContent);
    //    }
    //}
    //var result = await client.PostAsync("sign_in", content);
    //var token = result.Headers.GetValues("access-token").FirstOrDefault();



}
}
