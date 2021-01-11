using AlienTorpedoAPI.Models;
using AlienTorpedoAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace AlienTorpedoAPI.Controllers
{
    //[Produces("application/json")]
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
        public IActionResult Sortear([FromBody] Grupo grupo)
        {
            SorteioRepository sorteio = new SorteioRepository(_configuration);
            int resultado = sorteio.GeraSorteio(grupo, _dbcontext, _configuration);

            if (resultado == 0)
                return Json(new { cdretorno = 1, mensagem = "Ocorreu um erro ao tentar sortear os eventos!" });

            return Json(new { cdretorno = 0, mensagem = "Eventos sorteados com sucesso!" });
        }

        [HttpGet]
        public IActionResult ListarEventosSorteados()
        {
            SorteioRepository sorteio = new SorteioRepository(_configuration);
            List<dynamic> eventos = sorteio.ListarEventosSorteados(_configuration);

            return Json(eventos);
        }
    }
}
