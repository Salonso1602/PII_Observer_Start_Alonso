namespace Observer
{
    public class TemperatureReporter : IObserver
    {
        private bool first = true;

        private Temperature last;

        private TemperatureSensor provider;

        public void StartReporting(TemperatureSensor provider)
        {
            this.provider = provider;
            this.first = true;
            this.provider.AddObserver(this);
        }

        public void StopReporting()
        {
            if (this.provider != null) this.provider.RemoveObserver(this);
        }

        public void CheckSubject(ISubject value)
        {
            if (value is TemperatureSensor)
            {
                TemperatureSensor Value = value as TemperatureSensor;
                System.Console.WriteLine($"The temperature is {Value.Current.Degrees}°C at {Value.Current.Date:g}");
            if (first)
            {
                last = Value.Current;
                first = false;
            }
            else
            {
                System.Console.WriteLine($"   Change: {Value.Current.Degrees - last.Degrees}° in " +
                    $"{Value.Current.Date.ToUniversalTime() - last.Date.ToUniversalTime():g}");
            }
            }
        }
    }
}