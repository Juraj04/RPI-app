using System;
using TemperatureLibrary;

namespace TemperatureDatabase
{
    /// <summary>
    /// Senzor data, representing one row in database table
    /// </summary>
    public class TemperatureData
    {
        /// <summary>
        /// AutoIncrement id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Date and time of data creation
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Scanned temperature
        /// </summary>
        public Double Temperature { get; set; }
        /// <summary>
        /// Scanned humidity
        /// </summary>
        public Double Humidity { get; set; }
        /// <summary>
        /// Location of sensor - inside/outside
        /// </summary>
        public Location Location { get; set; }
    }
}
