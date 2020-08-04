using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AlienTorpedoAPI.Classes;
using AlienTorpedoAPI.Models;
using Microsoft.Extensions.Configuration;

namespace AlienTorpedoAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[Action]")]
    public class SorteioController : Controller
    {

        private readonly dbAlienContext _dbcontext;
        private readonly IConfiguration _configuration;
        public SorteioController(dbAlienContext dbContext, IConfiguration configuration)
        {
            _dbcontext = dbContext;
            _configuration = configuration;
        }

        // POST: api/Sorteio/Sortear
        [HttpPost]
        public IActionResult Sortear([FromBody]GrupoEvento grupoEvento)
        {
            Sorteio sorteio = new Sorteio(_configuration);
            int cdEvento = sorteio.GeraSorteio(grupoEvento, _dbcontext);
            var result = new List<GrupoEventoViewModel>();

            if (cdEvento == 0)           
                return Json(new { cdretorno = 1, mensagem = "O evento selecionado n�o est� atrelado a este grupo!", data = result });            

            result = sorteio.BuscaSorteio(_dbcontext, grupoEvento);

            return Json(new { cdretorno = 0, mensagem = "Sucesso!", data = result });
        }
    }
}
