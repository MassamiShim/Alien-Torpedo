using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlienTorpedoAPI.Models;

namespace AlienTorpedoAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    public class GrupoController : Controller
    {
        private readonly dbAlienContext _dbcontext;

        public GrupoController(dbAlienContext dbContext)
        {
            _dbcontext = dbContext;
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

        // POST api/Grupo/CadastraGrupoEvento
        [HttpPost]
        public IActionResult CadastraGrupoEvento([FromBody]GrupoEvento group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dbcontext.Add(group);
            _dbcontext.SaveChanges();

            return Ok("Grupo Evento cadastrado com sucesso!");
        }
    }
}