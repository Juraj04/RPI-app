using System;
using System.Runtime.Serialization;

namespace TemperatureWebApp.ViewModels
{
    /// <summary>
    /// Data class for showing data in graph
    /// </summary>
    [DataContract]
    public class DataPointModel
    {
        /// <summary>
        /// Returns total milliseconds to specific date and represents data on x axis
        /// </summary>
        [DataMember(Name = "x")]
        public double? X => Date.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        ).TotalMilliseconds;

        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Value, represents data on y axis
        /// </summary>
        [DataMember(Name = "y")]
        public double? Y;
    }
}