using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using TemperatureRPiApp.DirectMethodHandler;
using TemperatureRPiApp.Models;
using TemperatureRPiApp.Thermometer;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TemperatureRPiApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        private readonly TemperatureAPIService _service;

        /// <summary>
        /// Constructor for page
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            var thermometerDht22 = new ThermometerDht22(10);
            _service = new TemperatureAPIService(new Uri("https://temperatureapiservice.azurewebsites.net"));
            thermometerDht22.OnTimerTick += ShowData;
            thermometerDht22.InitializeThermometer();

            var relay = new RelayModule.RelayModule();
            relay.InitializeRelay();

            var client = new MyDeviceClient(relay, thermometerDht22);
            client.OnMessageReceived += ShowMessageStatus;
        }


        private void ShowData(double temp, double hum)
        {
            TbTemp.Text = $"Temperature: {temp}";
            TbHum.Text = $"Humidity: {hum}";

            _service.Add(new TemperatureRestData(DateTime.Now, temp, hum, 0));
        }

        private async void ShowMessageStatus(string message)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => TbMessage.Text = message);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //nop
        }
    }
}
