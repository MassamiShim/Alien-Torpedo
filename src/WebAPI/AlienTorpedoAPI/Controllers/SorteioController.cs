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

        // POST: api/Sorteio/Sortiar
        [HttpPost]
        public string Sortiar([FromBody]GrupoEvento grupoEvento)
        {
            int cdEvento = 0;
            string result;
            Sorteio sorteio = new Sorteio();
            cdEvento = sorteio.GeraSorteio(grupoEvento, _dbcontext);
            result = sorteio.BuscaSorteio(_dbcontext, grupoEvento);

            return result;
        }
    }
}
