using System.Drawing;
using System.Drawing.Imaging;

namespace Explorus.Models
{
    internal interface ILabyrinthComponent
    {
        int x { get; }
        int y { get; }
        Image2D image { get; }
        ImageAttributes attributes { get; }

        bool isSolid { get; }

        Rectangle hitbox { get; }

        bool Collide(ILabyrinthComponent comp);

        bool IsValidDestination(Slimus playerCharacter);
    }
}
