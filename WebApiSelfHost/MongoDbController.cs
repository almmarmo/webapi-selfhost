using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace WebApiSelfHost
{
    [RoutePrefix("api/mongo")]
    public class MongoDbController : ApiController
    {
        [HttpGet]
        [Route("getAllDocuments")]
        public async Task<JsonResult<IEnumerable<ErroLog>>> GetAllDocuments()
        {
            MongoRepository rep = new MongoRepository("dblogs");
            var result = await rep.GetAll("logerros");

            return Json(result);
        }
    }
}
