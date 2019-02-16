using System.Collections.Generic;
using Newtonsoft.Json;

namespace TemperatureWebApp.ViewModels
{
    /// <summary>
    /// View model for chart controller
    /// </summary>
    public class ChartDataModel
    {
        /// <summary>
        /// Time interval of data in graph
        /// </summary>
        public TimeIntervalModel TimeInterval { get; set; }

        /// <summary>
        /// List of graph points of temperature
        /// </summary>
        public List<DataPointModel> Temps { get; set; }
        /// <summary>
        /// List of graph points of humidity
        /// </summary>
        public List<DataPointModel> Hums { get; set; }

        /// <summary>
        /// List of graph points of temperature converted to Json
        /// </summary>
        public string JsonTemps => JsonConvert.SerializeObject(Temps);
        /// <summary>
        /// List of graph points of humidity converted to Json
        /// </summary>
        public string JsonHums => JsonConvert.SerializeObject(Hums);

        /// <summary>
        /// Constructor
        /// </summary>
        public ChartDataModel()
        {
            TimeInterval = new TimeIntervalModel();
        }
    }
}