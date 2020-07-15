using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AlienTorpedoAPI.Classes;
using AlienTorpedoAPI.Models;

namespace AlienTorpedoAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[Action]")]
    public class SorteioController : Controller
    {

        private readonly dbAlienContext _dbcontext;
        public SorteioController(dbAlienContext dbContext)
        {
            _dbcontext = dbContext;
        }

        // POST: api/Sorteio/Sortear
        [HttpPost]
        public IActionResult Sortear([FromBody]GrupoEvento grupoEvento)
        {
            Sorteio sorteio = new Sorteio();
            int cdEvento = sorteio.GeraSorteio(grupoEvento, _dbcontext);
            var result = sorteio.BuscaSorteio(_dbcontext, grupoEvento);

            return Ok(result);
        }
    }
}
