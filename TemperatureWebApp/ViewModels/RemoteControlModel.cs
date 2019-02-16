using System.ComponentModel;

namespace TemperatureWebApp.ViewModels
{
    /// <summary>
    /// View model for remote control controller 
    /// </summary>
    public class RemoteControlModel
    {
        /// <summary>
        /// time interval in seconds
        /// </summary>
        [DisplayName("Enter interval in seconds")]
        public int Interval { get; set; } = 5;
    }
}