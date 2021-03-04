using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dominio;
using WebAPI.Repo;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroAppController : ControllerBase
    {
        public readonly HeroiContexto _contexto;
        public HeroAppController(HeroiContexto contexto)
        {
            _contexto = contexto;
        }

        // GET: api/<HeroAppController>
        [HttpGet("filtro/{nome}")]
        public ActionResult GetFiltro(string nome)
        {
            var listHeroi = (from heroi in _contexto.Herois
                             where heroi.Nome.Contains(nome)
                             select heroi).ToList();
            //_contexto.Herois.Where(h => h.Nome.Contains(nome)).ToList(); //Forma 2 de fazer um listHeroi
            return Ok(listHeroi);
        }

        // GET api/<HeroAppController>/5
        [HttpGet("Atualizar/{nameHero}")]
        public ActionResult GetAtualizar(string nameHero)
        {
            //var = new Heroi { Nome = nameHero };
            var heroi = _contexto.Herois
                        .Where(h => h.Id == 3)
                        .FirstOrDefault();
            heroi.Nome = "Homem Aranha";
            _contexto.SaveChanges();
            return Ok();
        }
        
        [HttpGet("AddRange")]
        public ActionResult GetAddRange()
        {
            _contexto.AddRange(
                new Heroi { Nome = "Capitão America" },
                new Heroi { Nome = "Doutor Estranho" },
                new Heroi { Nome = "Pantera Negra" },
                new Heroi { Nome = "Viuva Negra" },
                new Heroi { Nome = "Hulk" },
                new Heroi { Nome = "Gavião Arqueiro" },
                new Heroi { Nome = "Capitã Marvel" }
                );
            _contexto.SaveChanges();
            return Ok();
        }

        // POST api/<HeroAppController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HeroAppController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HeroAppController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var heroi = _contexto.Herois
                        .Where(x => x.Id == id)
                        .Single();
            _contexto.Herois.Remove(heroi);
            _contexto.SaveChanges();
        }
    }
}
