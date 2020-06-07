using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProAgil.Repository;
using ProAgil.WebAPI.Model;

namespace ProAgil.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ProAgilContext Context; 

        public WeatherForecastController(ProAgilContext proAgilContext)
        {
            this.Context = proAgilContext; 
        }

        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await this.Context.Eventos.ToListAsync(); 

                return Ok(results); 
            }
            catch (System.Exception)
            { 
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar o Banco de Dados"); 
            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await this.Context.Eventos.FirstOrDefaultAsync(e => e.Id == id); 
                return Ok(result); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar o Banco de Dados"); 
            }
        } 
    }
}
