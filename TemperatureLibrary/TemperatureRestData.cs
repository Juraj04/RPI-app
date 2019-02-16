using System;

namespace TemperatureLibrary
{
    /// <summary>
    /// Rest data sent from rpi to api
    /// </summary>
    public class TemperatureRestData
    {
        /// <summary>
        /// Scanned time
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// scanned temperature
        /// </summary>
        public double Temperature { get; set; }
        /// <summary>
        /// scanned humidity
        /// </summary>
        public double Humidity { get; set; }
        /// <summary>
        /// scanned location
        /// </summary>
        public Location Location { get; set; }
    }
}
