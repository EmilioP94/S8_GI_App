using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Explorus.Models
{
    internal class LabyrinthComponent : ILabyrinthComponent
    {
        public int x { get; protected set; }
        public Guid id { get; private set; }

        public int y { get; protected set; }

        public virtual Image2D image { get; protected set; }
        public ImageAttributes attributes { get; protected set; } = null;

        public bool isSolid { get; protected set; }

        public Rectangle hitbox { get; protected set; }

        protected ILabyrinthComponent initialState = null;

        public LabyrinthComponent(int x, int y, Image2D image)
        {
            this.x = x;
            this.y = y;
            this.image = image;
            id = Guid.NewGuid();
            initialState = new LabyrinthComponent(this);
        }

        //Prototype, create a copy from another object
        public LabyrinthComponent(ILabyrinthComponent component)
        {
            Copy(component);
        }

        public virtual bool Collide(ILabyrinthComponent comp)
        {
            return false;
        }

        public virtual bool IsValidDestination(Slime playerCharacter)
        {
            return true;
        }

        public virtual void Reset()
        {
            Copy(initialState);
        }

        private void Copy(ILabyrinthComponent component)
        {
            this.x = component.x;
            this.y = component.y;
            this.image = component.image;
            this.attributes = component.attributes;
            this.isSolid = component.isSolid;
            this.hitbox = component.hitbox;
            this.id = component.id;
        }
    }
}
