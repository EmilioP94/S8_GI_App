﻿using System.Drawing;


namespace Explorus.Models
{
    internal class Wall : LabyrinthComponent
    {
        public Wall(int x, int y, Image2D image) : base(x, y, image)
        {
            isSolid = true;
            hitbox = new Rectangle(x, y, Constants.unit * 2, Constants.unit * 2);
        }
        public override bool IsValidDestination(Slime playerCharacter)
        {
            return false;
        }
    }
}
