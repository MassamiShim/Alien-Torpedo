using AlienTorpedoAPI.Models;
using AlienTorpedoAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace AlienTorpedoAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    public class EventoController : Controller
    {
        private readonly dbAlienContext _dbcontext;
        private readonly IConfiguration _configuration;

        public EventoController(dbAlienContext dbContext,IConfiguration configuration )
        {
            _dbcontext = dbContext;
            _configuration = configuration;
        }

        // GET api/Evento/ListaEventos
        [HttpGet]
        public IActionResult ListaEventos()
        {
            var TipoEventos = EventoRepository.ListarEventos(_configuration);

            return Ok(TipoEventos);
        }

        // POST api/Evento/CadastraEvento
        [HttpPost]
        public IActionResult CadastraEvento([FromBody]Evento evento)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            EventoRepository.AdicionarEvento(evento, _configuration);
           
            return Ok("Evento cadastrado com sucesso!");
        }

        // POST api/Evento/CadastraTipoEventos
        [HttpPost]
        public IActionResult CadastraTipoEvento([FromBody]TipoEvento tpevento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dbcontext.Add(tpevento);
            _dbcontext.SaveChanges();

            return Ok("Tipo de evento cadastrado com sucesso!");
        }

        // GET api/Evento/ListaTipoEvento
        [HttpGet]
        public IActionResult ListaTipoEvento()
        {
            var TipoEventos = _dbcontext.TipoEvento.ToList();

            return Ok(TipoEventos);
        }
    }
}