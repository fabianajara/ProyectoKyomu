using BackEnd.DTO;
using BackEnd.Services.Implementations;
using BackEnd.Services.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallePedidoController : ControllerBase
    {
        IDetalleService _detalleService;
        public DetallePedidoController(IDetalleService detalleService)
        {
            _detalleService = detalleService;
        }

        // GET: api/<DetallePedidoController>
        [HttpGet]
        public IEnumerable<DetallePedidoDTO> Get()
        {
            return _detalleService.GetDetalles();
        }

        // GET api/<DetallePedidoController>/5
        [HttpGet("{id}")]
        public DetallePedidoDTO Get(int id)
        {
            return _detalleService.GetDetalleById(id);
        }

        // POST api/<DetallePedidoController>
        [HttpPost]
        public void Post([FromBody]DetallePedidoDTO detalle)
        {
            _detalleService.Add(detalle);
        }

        // PUT api/<DetallePedidoController>/5
        [HttpPut("{id}")]
        public ActionResult<DetallePedidoDTO> Put([FromBody] DetallePedidoDTO detalle)
        {
            _detalleService.Update(detalle);
            //Devolver el objeto actualizado que solicita el front//
            return Ok(detalle);
        }

        // DELETE api/<DetallePedidoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _detalleService.Delete(id);
        }
    }
}
