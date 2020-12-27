using AlienTorpedoAPI.Models;
using AlienTorpedoAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
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
       
        // POST: api/Sorteio/SortearTodos
        [HttpPost]
        public IActionResult SortearTodos([FromBody] Grupo grupo)
        {
            SorteioRepository sorteio = new SorteioRepository(_configuration);
            int resultado = sorteio.GeraSorteioTodos(grupo, _dbcontext);

            if (resultado == 0)
                return Json(new { cdretorno = 1, mensagem = "Eventos não foram sorteados!" });

            return Json(new { cdretorno = 0, mensagem = "Sucesso!" });
        }

        // POST: api/Sorteio/Sortear
        [HttpPost]
        public IActionResult Sortear()
        {
            SorteioRepository sorteio = new SorteioRepository(_configuration);
            int resultado = sorteio.GeraSorteio(_dbcontext);

            if (resultado == 0)
                return Json(new { cdretorno = 1, mensagem = "Eventos não foram sorteados!" });


            return Json(new { cdretorno = 0, mensagem = "Sucesso!"});
        }
    }
}
