
using Explorus.Models.Slimes;

namespace Explorus.Models
{
    internal static class LabyrinthComponentFactory
    {

        static public ILabyrinthComponent GetLabyrinthComponent(Sprites component, int x, int y)
        {
            switch (component)
            {
                case Sprites.wall:
                    return new Wall(x, y, SpriteFactory.GetInstance().GetSprite(Sprites.wall));
                case Sprites.door:
                    return new Door(x, y, SpriteFactory.GetInstance().GetSprite(Sprites.door));
                case Sprites.slimusDownLarge:
                    return new Slimus(x, y, false);
                case Sprites.player2DownLarge:
                    return new Slimus(x, y, true);
                case Sprites.toxicSlimeFollow:
                    return new ToxicSlimeFollow(x, y);
                case Sprites.toxicSlimeParallel:
                    return new ToxicSlimeParallel(x, y);
                case Sprites.toxicSlimeRunAndFollow:
                    return new ToxicSlimeRunAndFollow(x, y);
                case Sprites.miniSlime:
                    return new MiniSlime(x, y, SpriteFactory.GetInstance().GetSprite(Sprites.miniSlime));
                case Sprites.gem:
                    return new Gems(x, y, SpriteFactory.GetInstance().GetSprite(component));
                default:
                    return new LabyrinthComponent(x, y, SpriteFactory.GetInstance().GetSprite(component));
            }
        }
    }
}
