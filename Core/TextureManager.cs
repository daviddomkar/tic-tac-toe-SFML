using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Piskvorky.Core
{
    class TextureManager
    {
        private static RenderTexture texture;

        public static void CreateAtlas()
        {
            texture = new RenderTexture(400, 100, true);
            texture.Smooth = true;

            CircleShape circleShape = new CircleShape(35);
            circleShape.OutlineThickness = 10;
            circleShape.OutlineColor = Color.Green;
            circleShape.Origin = new Vector2f(35, 35);
            circleShape.Position = new Vector2f(50, 50);
            circleShape.FillColor = Color.Transparent;

            RectangleShape line1 = new RectangleShape(new Vector2f(100, 10));
            line1.Origin = new Vector2f(50, 5);
            line1.Position = new Vector2f(150, 50);
            line1.FillColor = Color.Red;
            line1.Rotation = 45;

            RectangleShape line2 = new RectangleShape(line1);
            line2.Rotation = -45;

            RectangleShape rectangleShape1 = new RectangleShape(new Vector2f(100, 100));
            rectangleShape1.Origin = new Vector2f(50, 50);
            rectangleShape1.Position = new Vector2f(250, 50);
            rectangleShape1.FillColor = new Color(102, 160, 255); //Darker

            RectangleShape rectangleShape2 = new RectangleShape(rectangleShape1);
            rectangleShape2.FillColor = new Color(130, 177, 255); //Lighter
            rectangleShape2.Position = new Vector2f(350, 50);

            texture.Clear(Color.Transparent);
            texture.Draw(circleShape);
            texture.Draw(line1);
            texture.Draw(line2);
            texture.Draw(rectangleShape1);
            texture.Draw(rectangleShape2);
            texture.Display();
        }

        public static RenderTexture Texture => texture;
    }
}
