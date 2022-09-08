
namespace Explorus.Models
{
    internal interface ICollection
    {
        Sprites[,] map { get; }
        int total { get; }
        int acquired { get; }
        Sprites sprite { get; }
        Bars barName { get; }
        bool defaultFull { get; }
    }
}
