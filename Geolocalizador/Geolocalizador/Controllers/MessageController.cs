using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geolocalizador.Model;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Geolocalizador.Context;

namespace Geolocalizador.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : Controller
    {
        private readonly IBusControl _bus;
       

        public MessageController(IBusControl bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(Message msg)
        {
            StreetMap ApiMap = new StreetMap(); 
             Uri uri = new Uri("rabbitmq://localhost/msg_queue");

            try
            {
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(msg);

                msg = ApiMap.Buscar(msg);

            } catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }


            return Ok(msg);
        }
    }
}