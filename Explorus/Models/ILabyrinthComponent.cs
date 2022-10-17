using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;

namespace Explorus.Models
{
    internal interface ILabyrinthComponent: IRenderableModel
    {
        Guid id { get; }
        bool isSolid { get; }

        Rectangle hitbox { get; }
        [MethodImpl(MethodImplOptions.Synchronized)]
        bool Collide(ILabyrinthComponent comp);

        bool IsValidDestination(Slime playerCharacter);
    }
}
