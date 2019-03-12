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
            string substr = resource.Name.Substring(begin, length);
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
            resource.Name = resourceName;
            return Ok();
        }

        [HttpPut]
        public void EditResourceBeginningInsert(int id, string beginInsert)
        {
            Resource resource = resources.FirstOrDefault((p) => p.Id == id);
            resource.Name = resource.Name.Insert(0, beginInsert);
        }

        [HttpPut]
        public void EditResourceEndingInsert(int id, string endInsert)
        {
            Resource resource = resources.FirstOrDefault((p) => p.Id == id);
            resource.Name = resource.Name.Insert(resource.Name.Length, endInsert);
        }

        [HttpPut]
        public void EditResourceIndexPosInsert(int id, int index, string anyInsert)
        {
            Resource resource = resources.FirstOrDefault((p) => p.Id == id);
            resource.Name = resource.Name.Insert(index, anyInsert);
        }

        [HttpPut]
        public void EditResourceRemoveSubsrt(int id, int index, int length)
        {
            Resource resource = resources.FirstOrDefault((p) => p.Id == id);
            resource.Name = resource.Name.Remove(index, length);
        }

        [HttpPut]
        public void EditResourceReplaceSubsrt(int id, string oldStr, string newStr)
        {
            Resource resource = resources.FirstOrDefault((p) => p.Id == id);
            resource.Name = resource.Name.Replace(oldStr, newStr);
        }
        // DELETE api/values/5
        public void Delete(int id)
        {
            resources.RemoveAt(id);
        }
    }
}
