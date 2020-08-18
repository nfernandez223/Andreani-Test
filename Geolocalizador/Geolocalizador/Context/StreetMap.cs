using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geolocalizador.Model;
using Nominatim.API.Geocoders;
using Nominatim.API.Models;

namespace Geolocalizador.Context
{
    public class StreetMap
    {
        public Message Buscar(Message msg)
        {
            ForwardGeocodeRequest FGR = new ForwardGeocodeRequest();
            GeocodeResponse request = new GeocodeResponse();

            try
            {
                FGR.City = msg.ciudad;
                FGR.Country = msg.pais;
                FGR.StreetAddress = msg.calle + " " + msg.numero.ToString();
                FGR.County = msg.provincia;
                ForwardGeocoder FG = new ForwardGeocoder();
                Task<GeocodeResponse[]> response = FG.Geocode(FGR);
                request = response.Result[0];
                msg.latitud = request.Latitude;
                msg.longitud = request.Longitude;
            }
            catch(Exception ex)
            {
                var error = ex.Message;

            } 

            return msg;
        }
    }
}
