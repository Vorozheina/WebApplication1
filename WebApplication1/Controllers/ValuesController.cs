using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ValuesController : ApiController
    {
        List<Resource> resources = new List<Resource>
        {
            new Resource { Id = 1, Name = "juice"},
            new Resource { Id = 2, Name = "soup"},
            new Resource { Id = 3, Name = "cream"},
            new Resource { Id = 4, Name = "ice-cream"}
        };


        // GET api/values
        public IEnumerable<Resource> Get()
        {
            return resources;
        }

        // GET api/values/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            Resource resource = resources.FirstOrDefault((p) => p.Id == id);
            if (resource == null)
            {
                return Content(HttpStatusCode.NotFound, "Указанный ресурс не существует!");
            }
            return Ok(resource);
        }


        // GET api/values/5
        [HttpGet]
        public IHttpActionResult GetSubstring(int id, int begin, int length)
        {
            Resource resource = resources.FirstOrDefault((p) => p.Id == id);
            if (resource == null)
            {
                return Content(HttpStatusCode.NotFound, "Указанный ресурс не существует!");
            }
            int resourceLength = resource.Name.Length;
            if (begin > resourceLength || begin < 0 || length > resourceLength || length <=0 || (begin > 0 && length > resourceLength)
                || (begin == length && length ==0))
            {
                return Content(HttpStatusCode.BadRequest, "Параметры подстроки указаны неверно");
            }
            string substr = resources[id].Name.Substring(begin, length);
            return Ok(substr);
        }

        // POST api/values
        [HttpPost]
        public void CreateResource([FromBody]Resource resource)
        {
            resources.Add(resource);
        }

        // PUT api/values/5
        [HttpPut]
        public IHttpActionResult EditResource(int id, string resourceName)
        {
            Resource resource = resources.FirstOrDefault((p) => p.Id == id);
            if (resource == null)
            {
                return Content(HttpStatusCode.NotFound, "Указанный ресурс не существует!");
            }
            resources[id].Name = resourceName;
            return Ok();
        }

        [HttpPut]
        public void EditResourceBeginningInsert(int id, string beginInsert)
        {
            resources[id].Name = String.Concat(beginInsert, resources[id].Name);
        }

        [HttpPut]
        public void EditResourceEndingInsert(int id, string endInsert)
        {
            resources[id].Name = String.Concat(resources[id].Name, endInsert);
        }

        [HttpPut]
        public void EditResourceIndexPosInsert(int id, int index, string anyInsert)
        {
            string resPart1 = resources[id].Name;
            if (index == 0)
                EditResourceBeginningInsert(id, anyInsert);
            if (index == resPart1.Length)
                EditResourceEndingInsert(id, anyInsert);
            resPart1 = resources[id].Name.Substring(0, index + 1);
            string resPart2 = resources[id].Name.Substring(index + 1, resources[id].Name.Length - resPart1.Length);
            resources[id].Name = String.Concat(resPart1, anyInsert, resPart2);
        }


        // DELETE api/values/5
        public void Delete(int id)
        {
            resources.RemoveAt(id);
        }
    }
}
