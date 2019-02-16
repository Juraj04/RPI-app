using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TemperatureWebApp.ViewModels
{
    /// <summary>
    /// View model for chart model, time interval of data in graph
    /// </summary>
    public class TimeIntervalModel
    {
        //Constructor, set default dates
        public TimeIntervalModel()
        {
            From = new DateTime(2018, 1, 1);
            To = new DateTime(2020, 1, 1);
        }

        /// <summary>
        /// Date from
        /// </summary>
        [DisplayName("Enter date from")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime From { get; set; }

        /// <summary>
        /// Date to
        /// </summary>
        [DisplayName("Enter date to")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime To { get; set; }
    }
}