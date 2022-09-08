
namespace Explorus.Models
{
    internal interface IObserver<T>
    {
        void OnNext(T value);
    }
}
