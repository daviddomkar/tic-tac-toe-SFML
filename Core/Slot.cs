using System;
using Newtonsoft.Json;
using Piskvorky.Shapes;
using SFML.Graphics;
using SFML.System;
using Shape = Piskvorky.Shapes.Shape;

namespace Piskvorky.Core
{
    class Slot : Drawable
    {
        private Shape shape;

        private float x;
        private float y;

        public Shape Shape => shape;

        public Slot(int x, int y, VertexArray array)
        {
            this.x = x * 100 + 50;
            this.y = y * 100 + 50;

            int tuLighter = 300;
            int tvLighter = 0;
            int tuDarker = 200;
            int tvDarker = 0;

            Vertex[] vertices = new Vertex[4];

            vertices[0] = new Vertex();
            vertices[1] = new Vertex();
            vertices[2] = new Vertex();
            vertices[3] = new Vertex();

            vertices[0].Position = new Vector2f(this.x - 50, this.y - 50);
            vertices[1].Position = new Vector2f(this.x + 50, this.y - 50);
            vertices[2].Position = new Vector2f(this.x + 50, this.y + 50);
            vertices[3].Position = new Vector2f(this.x - 50, this.y + 50);

            vertices[0].Color = Color.White;
            vertices[1].Color = Color.White;
            vertices[2].Color = Color.White;
            vertices[3].Color = Color.White;

            if ((x % 2 == 0 && y % 2 == 0) || (x % 2 != 0 && y % 2 != 0))
            {
                vertices[0].TexCoords = new Vector2f(tuLighter, tvLighter);
                vertices[1].TexCoords = new Vector2f(tuLighter + 100, tvLighter);
                vertices[2].TexCoords = new Vector2f(tuLighter + 100, tvLighter + 100);
                vertices[3].TexCoords = new Vector2f(tuLighter, tvLighter + 100);
            }
            else
            {
                vertices[0].TexCoords = new Vector2f(tuDarker, tvDarker);
                vertices[1].TexCoords = new Vector2f(tuDarker + 100, tvDarker);
                vertices[2].TexCoords = new Vector2f(tuDarker + 100, tvDarker + 100);
                vertices[3].TexCoords = new Vector2f(tuDarker, tvDarker + 100);
            }

            array.Append(vertices[0]);
            array.Append(vertices[1]);
            array.Append(vertices[2]);
            array.Append(vertices[3]);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if(shape != null) target.Draw(shape);
        }

        public void AsignShape(ShapeType type)
        {
            switch (type)
            {
                case ShapeType.Circle:
                    shape = new Circle(x, y);
                    break;
                case ShapeType.Cross:
                    shape = new Cross(x, y);
                    break;
            }
        }

        public void DetachShape()
        {
            shape = null;
        }
    }
}
