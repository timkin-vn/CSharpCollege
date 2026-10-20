using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FifteenGame.WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public object Post([FromBody] string value)
        {
            return new { Text = value, Length = value.Length };
        }

        [HttpPost]
        [Route("api/values/greeting")]
        public string Greeting([FromBody] string userName)
        {
            return string.IsNullOrEmpty(userName) ? "Привет, незнакомец!" : $"Приветствую, {userName}!";
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
