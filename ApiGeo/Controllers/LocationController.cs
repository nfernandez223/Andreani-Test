using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGeo.Context;
using ApiGeo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using RestSharp;
using Microsoft.EntityFrameworkCore;

namespace ApiGeo.Controllers
{
    [Route("api/")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly DataContext context;
        private static readonly HttpClient client = new HttpClient();

        public LocationController(DataContext context)
        {
            this.context = context;
        }

        public IEnumerable<Location> Get() //DEVOLVER TODOS LOS ELEMENTOS
        {
            return context.Location.ToList();
        }

        [HttpGet("geocodificar")]
        public ActionResult Get([FromQuery] int id) //DEVOLVER UN ELEMENTO POR ID
        {
            var location = context.Location.FirstOrDefault(x => x.id == id);

            if(location != null)
            {

                var estado = location.latitud != 0 && location.latitud != 0 ? "TERMINADO" : "EN PROCESO";

                return new JsonResult(new { id = id, latitud = location.latitud, longitud = location.longitud, estado = estado });

            } else
            {
                return new JsonResult(new { msj = "NO SE ENCONTRO REGISTRO CON ID: " + id });
            }

        }

        [HttpPost("geolocalizar")]
        public ActionResult Post([FromBody] Location location) //CARGA UN LOCATION
        {
            try
            {

              context.Location.Add(location);
              context.SaveChanges();
              int id = location.id;

                //ENVIAR A GEOLOCALIZAR
                var client = new RestClient("https://localhost:44355/api/message"); //DIRECCION LOCAL
                var request = new RestRequest();
                request.AddJsonBody(location);
                request.AddHeader("header", "value");
                var response = client.Post(request);
                //var content = response.Content; // Raw content as string
                var response2 = client.Post<Location>(request);
                var latitud = response2.Data.latitud;
                var longitud = response2.Data.longitud;

                if (latitud != 0 && longitud != 0)
                {
                    var location1 = context.Location.FirstOrDefault(x => x.id == response2.Data.id);
                    location1.estado = true;
                    location1.latitud = latitud;
                    location1.longitud = longitud;
                    //context.Entry(location1).State = EntityState.Modified;
                    context.SaveChanges();
                }

                return new JsonResult(new { id = id });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}