using System;

using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Piskvorky.Shapes
{
    class Circle : Shape
    {
        private CircleShape circleShape;

        public Circle(float x, float y) : base(ShapeType.Circle, x, y)
        {
            circleShape = new CircleShape(35);
            circleShape.OutlineThickness = 10;
            circleShape.OutlineColor = Color.Green;
            circleShape.Origin = new Vector2f(35, 35);
            circleShape.Position = new Vector2f(x, y);
            circleShape.FillColor = Color.Transparent;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {          
            target.Draw(circleShape);
        }
    }
}
