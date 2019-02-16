using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using TemperatureDatabase;
using TemperatureLibrary;

namespace TemperatureAPI.Controllers
{
    public class TemperatureReceiverController : ApiController
    {

        // POST api/values
        /// <summary>
        /// Api method for receiving data
        /// </summary>
        /// <param name="value">sensor data</param>
        /// <returns>Http response 200 - ok, 400 - bad</returns>
        [SwaggerOperation("Add")]
        [SwaggerResponse(HttpStatusCode.Created)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        public HttpResponseMessage Post(TemperatureRestData value)
        {
            if (SaveData(value))
                return Request.CreateResponse(HttpStatusCode.Created);
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }


        private bool SaveData(TemperatureRestData value)
        {

            try
            {
                using (var db = new TemperatureContext())
                {
                    var data = new TemperatureData()
                    {
                        Date = value.Date,
                        Humidity = value.Humidity,
                        Location = value.Location,
                        Temperature = value.Temperature
                    };
                    db.TemperatureData.Add(data);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


    }
}
