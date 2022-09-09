using System;

namespace Explorus.Models
{
    internal interface IObservable<T>
    {
        IDisposable Subscribe(IObserver<T> observer);
    }
}
