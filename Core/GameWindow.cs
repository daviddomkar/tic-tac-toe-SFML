using System;

using SFML;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Piskvorky.Core
{
    abstract class GameWindow
    {
        protected RenderWindow window;
        private View view;

        public GameWindow()
        {
            PreInit();

            Log.Debug("Creating window...");

            ContextSettings contextSettings = new ContextSettings
            {
                DepthBits = 32,
                AntialiasingLevel = 8
            };

            window = new RenderWindow(new VideoMode(720, 720), "Piškvorky", Styles.Default, contextSettings);
            window.SetActive();

            window.Closed += OnClosed;
            window.KeyPressed += OnKeyPressed;
            window.MouseButtonPressed += OnMouseButtonDown;
            //window.Resized += new EventHandler<SizeEventArgs>(OnResized);

            Log.Debug("Creating view...");

            window.SetView(new View(new Vector2f(500, 500), new Vector2f(1000, 1000)));

            Init();

            while (window.IsOpen)
            {
                window.DispatchEvents();

                Update();

                window.Clear(Color.Cyan);

                Render();
                
                window.Display();
            }         
        }

        protected abstract void PreInit();
        protected abstract void Init();
        protected abstract void Update();
        protected abstract void Render();

        protected virtual void OnMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        protected virtual void OnKeyPressed(object sender, KeyEventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            if (e.Code == Keyboard.Key.Escape)
                window.Close();
            else if(e.Code == Keyboard.Key.N)
            {
                NewGame();
            }
            else if (e.Code == Keyboard.Key.S)
            {
                Save();
            }
            else if (e.Code == Keyboard.Key.L)
            {
                Load();
            }
        }

        protected virtual void OnClosed(object sender, System.EventArgs e)
        {
            Window window = (Window)sender;
            window.Close();
        }

        protected abstract void NewGame();
        protected abstract void Save();
        protected abstract void Load();
    }
}
