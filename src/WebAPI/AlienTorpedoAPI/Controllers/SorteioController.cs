using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AlienTorpedoAPI.Classes;
using AlienTorpedoAPI.Models;
using Microsoft.Extensions.Logging;

namespace AlienTorpedoAPI.Controllers
{
    [ApiController]
    //[Produces("application/json")]
    [Route("api/[controller]")]
    public class SorteioController : ControllerBase
    {

        private readonly dbAlienContext _dbcontext;

        public SorteioController(dbAlienContext dbContext)
        {
            _dbcontext = dbContext;
        }

        // GET: api/Sorteio
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Sorteio/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Sorteio/Post
        [HttpPost]
        public string Post([FromBody]GrupoEvento grupoEvento)
        {
            int cdEvento = 0;
            string result;
            Sorteio sorteio = new Sorteio();
            cdEvento = sorteio.GeraSorteio(grupoEvento, _dbcontext);
            result = sorteio.BuscaSorteio(_dbcontext, grupoEvento);
            return result;
        }

        //[HttpPost]
        //public void Post([FromBody]int id)
        //{
        //    Sorteio sorteio = new Sorteio();
        //    sorteio.GeraSorteio(id, _dbcontext);
        //}

        // PUT: api/Sorteio/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
