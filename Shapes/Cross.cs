using System;

using Piskvorky.Core;
using SFML.Graphics;
using SFML.System;

namespace Piskvorky.Shapes
{
    class Cross : Shape
    {
        private RectangleShape line1;
        private RectangleShape line2;

        public Cross(float x, float y) : base(ShapeType.Cross, x, y)
        {
            line1 = new RectangleShape(new Vector2f(100, 10));
            line1.Origin = new Vector2f(50, 5);
            line1.Position = new Vector2f(x, y);
            line1.FillColor = Color.Red;
            line1.Rotation = 45;

            line2 = new RectangleShape(line1);
            line2.Rotation = -45;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(line1);
            target.Draw(line2);
        }
    }
}
