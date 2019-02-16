using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using TemperatureRPiApp.Thermometer;

namespace TemperatureRPiApp.DirectMethodHandler
{
    public class MyDeviceClient
    {
        private RelayModule.RelayModule _relayModule;
        private ThermometerDht22 _thermometer;
        //connection string for rpi
        private readonly string _connectionString = "HostName=iot-hub-george.azure-devices.net;DeviceId=MyNodeDevice;SharedAccessKey=4bd0hwMOHvEUwacPEIm135zvq4JRbEc6pvx3P/b/68c=";

        /// <summary>
        /// delegate for event with string parameter
        /// </summary>
        /// <param name="message">message</param>
        public delegate void DirectMessageReceived(string message);
        /// <summary>
        /// Event that occurs when direct method call is received
        /// </summary>
        public event DirectMessageReceived OnMessageReceived;

        /// <summary>
        /// Class for receiving direct methods calls from back-end application
        /// </summary>
        /// <param name="relayModule">relay module</param>
        /// <param name="thermometer">thermometer</param>
        public MyDeviceClient(RelayModule.RelayModule relayModule, ThermometerDht22 thermometer)
        {
            _relayModule = relayModule;
            _thermometer = thermometer;

            var deviceClient = DeviceClient.CreateFromConnectionString(_connectionString, TransportType.Mqtt);
            deviceClient.SetMethodHandlerAsync("SwitchRelay", SwitchRelay, null).Wait();
            //_deviceClient.SetMethodHandlerAsync("GetCurrentThermometerData", GetCurrentThermometerData, null).Wait(); in next release
            deviceClient.SetMethodHandlerAsync("SetSensorInterval", SetSensorInterval, null).Wait();
        }


        // Handle the direct method call
        private Task<MethodResponse> SwitchRelay(MethodRequest methodRequest, object userContext)
        {
            OnMessageReceived?.Invoke("Executed direct method: " + methodRequest.Name);
            _relayModule.ChangeState();
           
            string result = "{\"result\":\"Relay state: " + _relayModule.RelayState + "\"}";
            return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(result), 200));
        }

        /*private Task<MethodResponse> GetCurrentThermometerData(MethodRequest methodRequest, object userContext)
        {
            OnMessageReceived?.Invoke("Executed direct method: " + methodRequest.Name);
            //_thermometer.CreateRandomData(out var t, out var h);
            //this would be useful if thermometer sensor was running...
            return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes("OK"), 200));
        }*/

        private Task<MethodResponse> SetSensorInterval(MethodRequest methodRequest, object userContext)
        {
            var data = Encoding.UTF8.GetString(methodRequest.Data);
            if (Int32.TryParse(data, out var value))
            {
                
                var old = _thermometer.TimerInSeconds;
                _thermometer.TimerInSeconds = value;
                OnMessageReceived?.Invoke($"Old interval: {old}s set to {_thermometer.TimerInSeconds}s");
                string result = "{\"result\":\"Old interval: " + old + "s set to: " + _thermometer.TimerInSeconds + "s" + "\"}";
                return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(result), 200));
            }
            else
            {
                OnMessageReceived?.Invoke($"Could not set interval, parameter: {data}");
                string result = "{\"result\":\"Invalid parameter\"}";
                return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(result), 400));
            }
            
        }
    }
}

