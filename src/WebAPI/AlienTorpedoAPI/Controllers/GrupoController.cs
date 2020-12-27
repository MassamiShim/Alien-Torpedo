using AlienTorpedoAPI.Models;
using AlienTorpedoAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace AlienTorpedoAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    public class GrupoController : Controller
    {
        private readonly dbAlienContext _dbcontext;
        private readonly IConfiguration _configuration;

        public GrupoController(dbAlienContext dbContext, IConfiguration configuration)
        {
            _dbcontext = dbContext;
            _configuration = configuration;
        }

        // GET api/Grupo/ListaGrupo
        [HttpGet]
        public IActionResult ListaGrupo()
        {
            var lstGrupos = _dbcontext.Grupo.ToList();
            return Ok(lstGrupos);
        }

        // POST api/Grupo/CadastraGrupo
        [HttpPost]
        public IActionResult CadastraGrupo([FromBody]Grupo group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dbcontext.Add(group);
            _dbcontext.SaveChanges();

            return Ok("Grupo cadastrado com sucesso!");
        }

        // POST api/Grupo/AtrelarGrupoEvento
        [HttpPost]
        public JsonResult AtrelarGrupoEvento([FromBody]GrupoEvento group)
        {
            if (!ModelState.IsValid)            
                return Json(new { cdretorno = 1, mensagem = "Chamada fora do padrão, favor verificar!" });

            EventoRepository.AtrelarEventoAGrupo(group, _configuration);

            return Json(new { cdretorno = 0, mensagem = "Vinculação realizada com sucesso!" });            
        }
    }
}