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
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        // GET: api/<PedidoController>
        [HttpGet]
        public IEnumerable<PedidoDTO> Get()
        {
            return _pedidoService.GetPedidos();
        }

        // GET api/<PedidoController>/5
        [HttpGet("{id}")]
        public ActionResult<PedidoDTO> Get(int id)
        {
            var pedido = _pedidoService.GetPedidoById(id);
            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }

        // POST api/<PedidoController>
        [HttpPost]
        public ActionResult Post([FromBody] PedidoDTO pedido)
        {
            if (pedido == null)
            {
                return BadRequest();
            }
            _pedidoService.Add(pedido);
            return CreatedAtAction(nameof(Get), new { id = pedido.IdPedido }, pedido);
        }

        // PUT api/<PedidoController>/5
        [HttpPut("{id}")]
        public ActionResult<PedidoDTO> Put([FromBody] PedidoDTO pedido)
        {
            _pedidoService.Update(pedido);
            //Devolver el objeto actualizado que solicita el front//
            return Ok(pedido);
        }

        // DELETE api/<PedidoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _pedidoService.Delete(id);
        }
    }
}
