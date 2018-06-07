using System;

using SFML.Graphics;

namespace Piskvorky.Shapes
{
    class Shape : Drawable
    {
        private ShapeType shapeType;

        protected float x;
        protected float y;

        public ShapeType ShapeType => shapeType;
        protected Shape(ShapeType shapeType, float x, float y)
        {
            this.shapeType = shapeType;
            this.x = x;
            this.y = y;
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {

        } 
    }

    public enum ShapeType
    {
        Circle,
        Cross
    }
}
