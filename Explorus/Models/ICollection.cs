
namespace Explorus.Models
{
    internal interface ICollection
    {
        int total { get; }
        int acquired { get; }
        Sprites sprite { get; }
        Bars barName { get; }
    }
}
