using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Models
{
    internal interface IObservable<T>
    {
        IDisposable Subscribe(IObserver<T> observer);
    }
}
