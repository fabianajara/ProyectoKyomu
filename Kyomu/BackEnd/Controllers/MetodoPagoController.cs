using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MetodoPagoController : ControllerBase
    {
        IMetodoPagoService _metodoPagoService;
        public MetodoPagoController(IMetodoPagoService metodoPagoService)
        {
            _metodoPagoService = metodoPagoService;
        }

        // GET: api/<MetodoPagoController>
        [HttpGet]
        public IEnumerable<MetodoPagoDTO> Get()
        {
            return _metodoPagoService.GetMetodosPago();
        }

        // GET api/<MetodoPagoController>/5
        [HttpGet("{id}")]
        public MetodoPagoDTO Get(int id)
        {
            return _metodoPagoService.GetMetodoPagoById(id);
        }

        // POST api/<MetodoPagoController>
        [HttpPost]
        public void Post([FromBody]MetodoPagoDTO metodoPago)
        {
            _metodoPagoService.Add(metodoPago);
        }

        // PUT api/<MetodoPagoController>/5
        [HttpPut("{id}")]
        public ActionResult<MetodoPagoDTO> Put([FromBody] MetodoPagoDTO metodoPago)
        {
            _metodoPagoService.Update(metodoPago);
            //Devolver el objeto actualizado que solicita el front//
            return Ok(metodoPago);
        }

        // DELETE api/<MetodoPagoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _metodoPagoService.Delete(id);
        }
    }
}
