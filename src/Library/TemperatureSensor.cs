using System.Threading;
using System;
using System.Collections.Generic;

namespace Observer
{
    public class TemperatureSensor : ISubject
    {
        // An array of sample data to mimic a temperature device.
        public Nullable<Decimal>[] SampleData = { 14.6m, 14.65m, 14.7m, 14.9m, 14.9m, 15.2m, 15.25m, 15.2m, 15.4m, 15.45m };

        public const int Delay = 1000;

        private List<IObserver> observers = new List<IObserver>();
        public List<IObserver> Observers 
        {
            get
            {
                return this.observers;
            }
        }
        public Temperature Current { get; private set; }

        public void AddObserver(IObserver observer)
        {
            if (! observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }

        public void RemoveObserver(IObserver observer)
        {
            if (observers.Contains(observer))
            {
                this.observers.Remove(observer);
            }
        }

        public void GetTemperature()
        {
            // Store the previous temperature, so notification is only sent after at least .1 change.
            Nullable<Decimal> previous = null;
            bool start = true;

            foreach (var temp in this.SampleData)
            {
                System.Threading.Thread.Sleep(Delay);
                if (temp.HasValue)
                {
                    if (start || (Math.Abs(temp.Value - previous.Value) >= 0.1m))
                    {
                        this.Current = new Temperature(temp.Value, DateTime.Now);
                        ReportToObservers();
                        previous = temp;
                        if (start)
                        {
                            start = false;
                        }
                    }
                }
            }
        }
        public void ReportToObservers()
        {
            foreach (IObserver observer in observers)
                {
                    observer.CheckSubject(this);
                }
        }
    }
}
