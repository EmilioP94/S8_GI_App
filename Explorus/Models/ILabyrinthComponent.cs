using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;

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
        [MethodImpl(MethodImplOptions.Synchronized)]
        bool Collide(ILabyrinthComponent comp);

        bool IsValidDestination(Slime playerCharacter);
    }
}
