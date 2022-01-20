using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LeagueOfLegends_API.Data;
using LeagueOfLegends_API.HATEOAS;
using LeagueOfLegends_API.Models;
using LeagueOfLegends_API.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeagueOfLegends_API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "Admin")]

    public class PersonagemController : ControllerBase
    {
        private readonly ApplicationDbContext _database;
        private HATEOAS.HATEOAS _HATEOAS;

        public PersonagemController(ApplicationDbContext database)
        {
            _database = database;
            _HATEOAS = new HATEOAS.HATEOAS("localhost:5001/api/v1/Personagem");
            _HATEOAS.AddAction("GET_INFO", "GET");
            _HATEOAS.AddAction("DELETE_PRODUCT", "DELETE");
            _HATEOAS.AddAction("EDIT_PRODUCT", "PATCH");
        }

        [HttpGet]
        public IActionResult Get()
        {
            var personagens = _database.Personagens.Include(x => x.Habilidades).ToList();
            List<PersonagemContainer> PersonagensHATEOAS = new List<PersonagemContainer>();
            foreach (var personagem in personagens)
            {
                PersonagemContainer personagemHATEOAS = new PersonagemContainer();
                personagemHATEOAS.Personagem = personagem;
                personagemHATEOAS.Links = _HATEOAS.GetActions(personagem.Id.ToString());
                PersonagensHATEOAS.Add(personagemHATEOAS);
            }

            return Ok(PersonagensHATEOAS);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var personagem = _database.Personagens.Include(x => x.Habilidades).First(x => x.Id == id);
                return Ok(personagem);
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult("");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] PersonagemView personagem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var personagemBd = new Personagem();
                    personagemBd.Nome = personagem.Nome;
                    personagemBd.Descricao = personagem.Descricao;
                    personagemBd.Classe = personagem.Classe;
                    personagemBd.Status = true;

                    foreach (var item in personagem.Habilidades)
                    {
                        var hab = new Habilidade();

                        hab.Cooldown = item.Cooldown;
                        hab.Dano = item.Dano;
                        hab.Descricao = item.Descricao;
                        hab.Nome = item.Nome;
                        hab.TipoDano = item.TipoDano;

                        personagemBd.Habilidades.Add(hab);
                    }

                    _database.Personagens.Add(personagemBd);
                    _database.SaveChanges();

                    Response.StatusCode = 201;
                    return new ObjectResult("");
                }
                catch (System.Exception)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "Verifique o personagem informado" });
                }

            }
            else
            {
                Response.StatusCode = 400;
                return new ObjectResult(new { msg = "Verifique o personagem informado" });
            }
        }

        [HttpPatch]
        public IActionResult Patch([FromBody] Personagem personagem)
        {
            if (personagem.Id > 0)
            {
                try
                {
                    var personagemDataBase = _database.Personagens.First(x => x.Id == personagem.Id && x.Status == true);
                    if (personagemDataBase != null)
                    {
                        //Edição
                        personagemDataBase.Nome = personagem.Nome;
                        personagemDataBase.Descricao = personagem.Descricao;
                        personagemDataBase.Classe = personagem.Classe;
                        personagemDataBase.Habilidades = personagem.Habilidades;
                        _database.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new { msg = "Id inválido." });
                    }
                }
                catch (Exception)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "Id inválido." });
                }
            }
            else
            {
                Response.StatusCode = 400;
                return new ObjectResult(new { msg = "Id inválido." });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var personagem = _database.Personagens.Include(x => x.Habilidades).First(x => x.Id == id);
                personagem.Status = false;
                foreach (var item in personagem.Habilidades)
                {
                    item.Status = false;
                }
                _database.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult("");
            }
        }



        public class PersonagemView
        {
            [Required]
            [MaxLength(60, ErrorMessage = "Este campo precisa ter entre 3 a 40 caracteres")]
            [MinLength(3, ErrorMessage = "Este campo precisa ter entre 3 a 40 caracteres")]
            public string Nome { get; set; }

            [Required]
            public double Dano { get; set; }

            [Required]
            [MinLength(20, ErrorMessage = "Este campo precisa ter entre 20 a 400 caracteres")]
            public string Descricao { get; set; }

            [Required]
            public Classe Classe { get; set; }

            [Required]
            public ICollection<HabilidadeView> Habilidades { get; set; }

            [Required]
            public int TipoDano { get; set; } // 0 - Fisico | 1 - Mágico

            [Required]
            public double Cooldown { get; set; }
            public bool Status { get; set; }
        }

        public class PersonagemContainer
        {
            public Personagem Personagem { get; set; }
            public Link[] Links { get; set; }
        }
        public class HabilidadeView
        {
            [Required]
            [MaxLength(60, ErrorMessage = "Este campo precisa ter entre 3 a 40 caracteres")]
            [MinLength(3, ErrorMessage = "Este campo precisa ter entre 3 a 40 caracteres")]
            public string Nome { get; set; }

            [Required]
            public double Dano { get; set; }

            [Required]
            [MinLength(20, ErrorMessage = "Este campo precisa ter entre 20 a 400 caracteres")]
            public string Descricao { get; set; }

            [Required]
            public int TipoDano { get; set; } // 0 - Fisico | 1 - Mágico

            [Required]
            public double Cooldown { get; set; }
            public bool Status { get; set; }
        }
    }
}