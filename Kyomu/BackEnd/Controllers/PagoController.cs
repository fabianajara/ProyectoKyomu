using BackEnd.DTO;
using BackEnd.Services.Implementations;
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
    public class PagoController : ControllerBase
    {
        private readonly IPagoService _pagoService;

        public PagoController(IPagoService pagoService)
        {
            _pagoService = pagoService;
        }

        // GET: api/<PagoController>
        [HttpGet]
        public IEnumerable<PagoDTO> Get()
        {
            return _pagoService.GetPagos();
        }

        // GET api/<PagoController>/5
        [HttpGet("{id}")]
        public ActionResult<PagoDTO> Get(int id)
        {
            var pago = _pagoService.GetPagoById(id);
            if (pago == null)
            {
                return NotFound();
            }
            return Ok(pago);
        }

        // POST api/<PagoController>
        [HttpPost]
        public ActionResult Post([FromBody] PagoDTO pago)
        {
            if (pago == null)
            {
                return BadRequest();
            }
            _pagoService.Add(pago);
            return CreatedAtAction(nameof(Get), new { id = pago.IdPago }, pago);
        }

        // PUT api/<PagoController>/5
        [HttpPut("{id}")]
        public ActionResult<PagoDTO> Put([FromBody] PagoDTO pago)
        {
            _pagoService.Update(pago);
            //Devolver el objeto actualizado que solicita el front//
            return Ok(pago);
        }

        // DELETE api/<PagoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _pagoService.Delete(id);
        }
    }
}
