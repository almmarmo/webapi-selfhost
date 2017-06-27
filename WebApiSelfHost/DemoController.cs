using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;

namespace WebApiSelfHost
{
    [RoutePrefix("api/demo")]
    public class DemoController : ApiController
    {
        //[HttpGet]
        private IEnumerable<Client> GetAll()
        {
            Console.WriteLine("Get chamado.");
            List<Client> list = new List<Client>();
            for(int i = 1; i < 11; i++)
            {
                list.Add(new Client() {
                    Id = i,
                    BirthDate = DateTime.Now.AddMonths(i),
                    Name = "Name " + i.ToString(),
                    Surname = "Surname " + i.ToString()
                });
            }

            return list;
        }

        [HttpGet]
        [Route("get/{id}")]
        [Authorize(Roles ="Admin")]
        public JsonResult<Client> Get([FromUri]int id)
        {
            Console.WriteLine(String.Format("User: {0}", this.User.Identity.Name));
            Console.WriteLine("Get foi chamado para o id " + id.ToString());
            return Json(GetAll().FirstOrDefault(x => x.Id == id));
        }

        [HttpGet]
        [Route("getall")]
        public JsonResult<IEnumerable<Client>> Get()
        {
            return Json(GetAll());
        }
    }
}
