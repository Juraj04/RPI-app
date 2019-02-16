using Windows.Devices.Gpio;

namespace TemperatureRPiApp.RelayModule
{
    /// <summary>
    /// Class that works with relay module
    /// </summary>
    public class RelayModule
    {
        private const int RelayPin = 21;
        private GpioPin _pin;
        private GpioPinValue _pinValue;

        /// <summary>
        /// Returns string representation of relay state
        /// </summary>
        public string RelayState => _pinValue == GpioPinValue.High ? "On" : "Off";

        /// <summary>
        /// Initialize pins and relay for work
        /// </summary>
        public void InitializeRelay()
        {
            var gpio = GpioController.GetDefault();

            if (gpio == null)
            {
                _pin = null;
                return;
            }

            _pin = gpio.OpenPin(RelayPin);
            _pinValue = GpioPinValue.High;
            _pin.Write(_pinValue);
            _pin.SetDriveMode(GpioPinDriveMode.Output);

        }

        /// <summary>
        /// Changes state of relays On/Off
        /// </summary>
        public void ChangeState()
        {
            if (_pinValue == GpioPinValue.High)
            {
                _pinValue = GpioPinValue.Low;
                _pin.Write(_pinValue);

            }
            else
            {
                _pinValue = GpioPinValue.High;
                _pin.Write(_pinValue);
            }
        }
    }
}
