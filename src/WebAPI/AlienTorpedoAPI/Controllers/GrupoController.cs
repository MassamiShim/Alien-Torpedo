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

        // POST api/Grupo/AtrelarGrupoEvento
        [HttpPost]
        public JsonResult AtrelarGrupoEvento([FromBody]GrupoEvento group)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { cdretorno = 1, mensagem = "Chamada fora do padrão, favor verificar!" });
                //return BadRequest();
            }

            _dbcontext.Add(group);
            _dbcontext.SaveChanges();

            //return Ok("Vinculação realizada com sucesso!");
            return Json(new { cdretorno = 0, mensagem = "Vinculação realizada com sucesso!" });            
        }
    }
}