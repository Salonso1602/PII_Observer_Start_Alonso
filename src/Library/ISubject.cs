using System.Collections.Generic;

namespace Observer
{
    public interface ISubject
    {
        public List<IObserver> Observers {get;}
        public void AddObserver(IObserver iobserver);
        public void RemoveObserver(IObserver iobserver);

        public void ReportToObservers();
    }
}