using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.WebAPI.Controllers
{
     [Route("api/[controller]")]
     [ApiController]
    public class EventoController: ControllerBase
    {
        private readonly IProAgilRepository repo;

        public EventoController(IProAgilRepository repo)
        {
            this.repo = repo;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await this.repo.GetAllEventoAsync(true); 

                return Ok(results); 
            }
            catch (System.Exception)
            { 
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar o Banco de Dados"); 
            }
            
        }

        [HttpGet("{EventoId}")]
        public async Task<IActionResult> Get(int eventoId)
        {
            try
            {
                var results = await this.repo.GetEventoAsyncId(eventoId, true); 

                return Ok(results); 
            }
            catch (System.Exception)
            { 
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar o Banco de Dados"); 
            }
            
        }

        [HttpGet("getByTema/{Tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                var results = await this.repo.GetAllEventoAsyncByTema(tema, true); 

                return Ok(results); 
            }
            catch (System.Exception)
            { 
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar o Banco de Dados"); 
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento evento)
        {
            try
            {
                this.repo.Add(evento); 

                if(await this.repo.SaveChangesAsync())
                {
                    return Created($"api/evento/{evento.Id}",evento); 
                }
            }
            catch (System.Exception)
            { 
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar o Banco de Dados"); 
            }

            return BadRequest();  
        }

        [HttpPut]
        public async Task<IActionResult> Put(int eventoId, Evento evento)
        {
            try
            {
                //Verificar se o evento foi encontrado. 
                var loEvento = await this.repo.GetEventoAsyncId(eventoId,false); 
                if(loEvento == null)
                    return  NotFound(); 

                this.repo.Update(evento); 

                if(await this.repo.SaveChangesAsync())
                {
                    return Created($"api/evento/{evento.Id}",evento); 
                }
            }
            catch (System.Exception)
            { 
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar o Banco de Dados"); 
            }

            return BadRequest();  
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int eventoId)
        {
            try
            {
                //Verificar se o evento foi encontrado. 
                var evento = await this.repo.GetEventoAsyncId(eventoId,false); 
                if(evento == null)
                    return  NotFound(); 

                this.repo.Delete(evento); 

                if(await this.repo.SaveChangesAsync())
                {
                    return Ok(); 
                }
            }
            catch (System.Exception)
            { 
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar o Banco de Dados"); 
            }

            return BadRequest();  
        }
    }
}