using System;
using System.Web.Mvc;
using Microsoft.Azure.Devices;

namespace TemperatureWebApp.Controllers
{
    /// <summary>
    /// Controller for remore controlling of rpi
    /// </summary>
    public class RemoteControlController : Controller
    {

        private ServiceClient _serviceClient;
        //service connection string
        private readonly string _connectionString = "HostName=iot-hub-george.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=I6WYVXHOoKF16+uRNQrusXglW8XlYqu9vicZUEmQfB8=";

        /// <summary>
        /// Constructor creating service
        /// </summary>
        public RemoteControlController()
        {
            _serviceClient = ServiceClient.CreateFromConnectionString(_connectionString);
        }

        // GET: RemoteControl
        /// <summary>
        /// Default action after loading
        /// </summary>
        /// <returns></returns>
        public ActionResult RemoteControl()
        {
            return View();
        }

        /// <summary>
        /// Invoking direct method on device to switch relay module
        /// </summary>
        /// <returns>view</returns>
        public ActionResult SwitchClientRelay()
        {
            //InvokeMethodSwitchRelay().GetAwaiter().GetResult();
            InvokeMethodSwitchRelay();

            return View("RemoteControl");
        }
        /// <summary>
        /// Invoking direct method on device to adjust time interval for scanning data
        /// </summary>
        /// <param name="interval">time in seconds</param>
        /// <returns>view</returns>
        [HttpPost]
        public ActionResult SendTimeInterval(int? interval)
        {
            if (interval != null)
                //InvokeMethodSendTimeInterval(interval.Value).GetAwaiter().GetResult();
                InvokeMethodSendTimeInterval(interval.Value);


            return View("RemoteControl");
        }


        private void InvokeMethodSwitchRelay()
        {
            var methodInvocation = new CloudToDeviceMethod("SwitchRelay") { ResponseTimeout = TimeSpan.FromSeconds(30) };
            methodInvocation.ConnectionTimeout = TimeSpan.FromSeconds(10);

            //var response = await _serviceClient.InvokeDeviceMethodAsync("MyNodeDevice", methodInvocation);
            _serviceClient.InvokeDeviceMethodAsync("MyNodeDevice", methodInvocation);
        }

        private void InvokeMethodSendTimeInterval(int time)
        {
            var methodInvocation = new CloudToDeviceMethod("SetSensorInterval") { ResponseTimeout = TimeSpan.FromSeconds(30) };
            methodInvocation.ConnectionTimeout = TimeSpan.FromSeconds(30);
            methodInvocation.SetPayloadJson(time.ToString());


            // var response = await _serviceClient.InvokeDeviceMethodAsync("MyNodeDevice", methodInvocation);
            _serviceClient.InvokeDeviceMethodAsync("MyNodeDevice", methodInvocation);
        }
    }
}