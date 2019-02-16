using System;
using System.Threading.Tasks;

namespace TemperatureRPiApp.Thermometer
{
    /// <summary>
    /// Should serve as result class of scanned data
    /// </summary>
    public class ThermometerResult
    {
        public bool Success { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
    }

    /// <summary>
    /// Class that works with thermometer sensor
    /// </summary>
    public class ThermometerDht22
    {
        /// <summary>
        /// Delegate for event with two double parameters
        /// </summary>
        /// <param name="temperature">temperature</param>
        /// <param name="humidity">humidity</param>
        public delegate void DataRead(double temperature, double humidity);
        /// <summary>
        /// Event that occurs when data are scanned
        /// </summary>
        public event DataRead OnTimerTick;

        //private IDht _sensor;

        /// <summary>
        /// Time interval for scanning data
        /// </summary>
        public int TimerInSeconds{get;set;}

        private Random _random;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="timerIntervalSeconds">Initial time interval in seconds</param>
        public ThermometerDht22(int timerIntervalSeconds)
        {
            _random = new Random();
            TimerInSeconds = timerIntervalSeconds;
            CollectData = true;
        }

        /// <summary>
        /// Sets, whether sensor should collect data or not
        /// </summary>
        public bool CollectData { get; set; }

        /// <summary>
        /// Initialize thermometer
        /// should set pins...
        /// </summary>
        public void InitializeThermometer()
        {
            //var pin = GpioController.GetDefault().OpenPin(20, GpioSharingMode.Exclusive);   //in next release
           // _sensor = new Dht22(pin,GpioPinDriveMode.Input);
            GatherData();
            //TimerOnTick(null,null);
        }

        private async void GatherData()
        {
            //var r = await GetData();
            //if (r.Success)
            //{
                //OnTimerTick?.Invoke(r.Temperature,r.Humidity);
            //}
            while (CollectData)
            {
                CreateRandomData(out var temp, out var hum);
                OnTimerTick?.Invoke(temp, hum);
                await Task.Delay(TimerInSeconds * 1000);
            }
        }

        
        /// <summary>
        /// Since the sensor is not working, data are simulated
        /// </summary>
        /// <param name="t"></param>
        /// <param name="h"></param>
        public void CreateRandomData(out double t, out double h)
        {
            var maximumT = 40;
            var minimumT = 5;
            t = _random.NextDouble() * (maximumT - minimumT) + minimumT;

            var maximumH = 100;
            var minimumH = 50;
           h = _random.NextDouble() * (maximumH - minimumH) + minimumH;
        }
/**
        public async Task<ThermometerResult> GetData()
        {
            var r = new ThermometerResult();
            r.Success = false;
            try
            {
                DhtReading reading = new DhtReading();
                //reading = await _sensor.GetReadingAsync();
                if (reading.IsValid) 
                {
                    r.Temperature = reading.Temperature;
                    r.Humidity = reading.Humidity;
                    r.Success = true;
                }
                else 
                {
                    Debug.WriteLine(
                        $"IsValid: {reading.IsValid}, RetryCount: {reading.RetryCount}, TimedOut: {reading.TimedOut}, Humidity: {reading.Humidity}, Temperature: {reading.Temperature}");
                    
                }

            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);
            }

            return r;
        }*/
    }
}
