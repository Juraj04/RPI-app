using System.Linq;
using System.Web.Mvc;
using TemperatureDatabase;
using TemperatureWebApp.ViewModels;

namespace TemperatureWebApp.Controllers
{
    /// <summary>
    /// Controler for showing data in chart
    /// </summary>
    public class ChartsController : Controller
    {
        // GET: Charts
        public ActionResult Charts(ChartDataModel data)
        {
            return View(GetData(data));
        }

        private ChartDataModel GetData(ChartDataModel data)
        {
            var from = data?.TimeInterval.From; //sets minimum
            var to = data?.TimeInterval.To; //sets maximum
            var result = new ChartDataModel();
            using (var db = new TemperatureContext())
            {
                var all = db.TemperatureData.ToList();
                result.Temps = all
                    .Where(temperatureData => temperatureData.Date >= from && temperatureData.Date <= to)
                    .OrderBy(temperatureData => temperatureData.Date)
                    .Select(temperatureData => new DataPointModel { Date = temperatureData.Date, Y = temperatureData.Temperature })
                    .ToList();
                result.Hums = all
                    .Where(temperatureData => temperatureData.Date >= from && temperatureData.Date <= to)
                    .OrderBy(temperatureData => temperatureData.Date)
                    .Select(temperatureData => new DataPointModel { Date = temperatureData.Date, Y = temperatureData.Humidity })
                    .ToList();
            }

            return result;
        }
    }
}