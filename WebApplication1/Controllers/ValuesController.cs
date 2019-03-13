using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using System.Threading.Tasks;
using System.Web.Http.Cors;


namespace WebApplication1.Controllers
{
    [EnableCors(origins: "http://localhost:57358", headers: "*", methods: "*")]
    public class ValuesController : ApiController
    {
        public static List<Resource> resources = new List<Resource>
        {
            new Resource { Id = 1, Name = "juice"},
            new Resource { Id = 2, Name = "soup"},
            new Resource { Id = 3, Name = "cream"},
            new Resource { Id = 4, Name = "ice-cream"}
        };


        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<Resource>> Get()
        {
            return await Task.Run(() => resources);
        }

        // GET api/values/5
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            Resource resource = await GetResourceAsync(id);
            if (resource == null)
            {
                return await Task.FromResult(NotFound());
            }
            return await Task.FromResult(Ok(resources.Last().Name));
        }

        private async Task<Resource> GetResourceAsync(int id)
        {
            return await Task.Run(() => resources.FirstOrDefault((p) => p.Id == id));
        }


        // GET api/values/5
        [HttpGet]
        public async Task<IHttpActionResult> GetSubstring(int id, int begin, int length)
        {
            Resource resource = await GetResourceAsync(id);
            if (resource == null)
            {
                return await Task.FromResult(NotFound());
            }            
            string substr = resource.Name.Substring(begin, length);
            return await Task.FromResult(Ok(substr));
        }

        // POST api/values
        [HttpPost]
        public async Task<IHttpActionResult> CreateResource(string newRes)
        {
            int numberOfRessources = resources.Last().Id;
            Resource resource = new Resource();
            resource.Id = numberOfRessources + 1;
            resource.Name = newRes;
            await Task.Run(() => resources.Add(resource));
            return await Task.FromResult(Ok(resources));
        }

        // PUT api/values/5
        [HttpPut]
        public async Task<IHttpActionResult> EditResource(int id, string resourceName)
        {
            Resource resource = await GetResourceAsync(id);
            if (resource == null)
            {
                return await Task.FromResult(NotFound());
            }
            resource.Name = resourceName;
            return await Task.FromResult(Ok(resource.Name));
        }

        [HttpPut]
        public async Task<IHttpActionResult> EditResourceBeginningInsert(int id, string beginInsert)
        {
            Resource resource = await GetResourceAsync(id);
            if (resource == null)
            {
                return await Task.FromResult(NotFound());
            }
            resource.Name = resource.Name.Insert(0, beginInsert);
            return await Task.FromResult(Ok(resource.Name));
        }

        [HttpPut]
        public async Task<IHttpActionResult> EditResourceEndingInsert(int id, string endInsert)
        {
            Resource resource = await GetResourceAsync(id);
            if (resource == null)
            {
                return await Task.FromResult(NotFound());
            }
            resource.Name = resource.Name.Insert(resource.Name.Length, endInsert);
            return await Task.FromResult(Ok(resource.Name));
        }

        [HttpPut]
        public async Task<IHttpActionResult> EditResourceIndexPosInsert(int id, int index, string anyInsert)
        {
            Resource resource = await GetResourceAsync(id);
            if (resource == null)
            {
                return await Task.FromResult(NotFound());
            }
            resource.Name = resource.Name.Insert(index, anyInsert);
            return await Task.FromResult(Ok(resource.Name));
        }

        [HttpPut]
        public async Task<IHttpActionResult> EditResourceRemoveSubsrt(int id, int index, int length)
        {
            Resource resource = await GetResourceAsync(id);
            if (resource == null)
            {
                return await Task.FromResult(NotFound());
            }
            resource.Name = resource.Name.Remove(index, length);
            return await Task.FromResult(Ok(resource.Name));
        }

        [HttpPut]
        public async Task<IHttpActionResult> EditResourceReplaceSubsrt(int id, string oldStr, string newStr)
        {
            Resource resource = await GetResourceAsync(id);
            if (resource == null)
            {
                return await Task.FromResult(NotFound());
            }
            resource.Name = resource.Name.Replace(oldStr, newStr);
            return await Task.FromResult(Ok(resource.Name));
        }
        // DELETE api/values/5
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            Resource resource = await GetResourceAsync(id);
            await Task.Run(() => resources.Remove(resource));
            return await Task.FromResult(Ok(resources.Count.ToString()));
        }
    }
}
