using System;
using System.Linq;
using System.Net;
using Piskvorky.Shapes;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Piskvorky.Core
{
    class Game : GameWindow
    {
        private Board board;

        private Player player1;
        private Player player2;

        private Player lastPlayer;

        private Loader loader;

        protected override void PreInit()
        {
            Log.SetTag("PIŠKVORKY");
            Log.Debug("Initializing...");

            TextureManager.CreateAtlas();
        }
    
        protected override void Init()
        {
            loader = new Loader();

            board = new Board(new Vector2i(10, 10));

            player1 = new Player("Player 1", ShapeType.Circle, board);
            player2 = new Player("Player 2", ShapeType.Cross, board);
        }

        protected override void Update()
        {
            if(player1.IsPlaying || player2.IsPlaying)
                return;

            if (lastPlayer == player1)
            {
                player2.Play();
                lastPlayer = player2;
            }
            else
            {
                player1.Play();
                lastPlayer = player1;
            }
        }

        protected override void Render()
        {
            window.Draw(board);
        }

        protected override void OnMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseButtonDown(sender, e);

            if (e.Button == Mouse.Button.Left)
            {
                Vector2f mousePosition = window.MapPixelToCoords(new Vector2i(e.X, e.Y));
                lastPlayer.PlaceShape(mousePosition.X, mousePosition.Y);
            }
        }

        protected override void NewGame()
        {
            player1.IsPlaying = false;
            player2.IsPlaying = false;

            lastPlayer = null;

            board.GameDone = false;
            board.Clear();

            Log.Debug("========================NEW GAME========================");
        }

        protected override void Save()
        {
            GameSave gameSave = new GameSave()
            {
                GameDone = board.GameDone,
                Player1 = player1,
                Player2 = player2
            };

            SlotSave[,] slotSaves = new SlotSave[10, 10];

            for (int i = 0; i < board.Slots.GetLength(0); i++)
            {
                for (int j = 0; j < board.Slots.GetLength(1); j++)
                {
                    slotSaves[i, j] = new SlotSave();

                    if (board.Slots[i, j].Shape != null)
                        slotSaves[i, j].ShapeType = board.Slots[i, j].Shape.ShapeType;
                    else
                        slotSaves[i, j].Empty = true;
                }
            }

            gameSave.Slots = slotSaves;

            loader.Save(gameSave);
            Log.Debug("========================GAME SAVED========================");
        }

        protected override void Load()
        {
            GameSave gameSave = loader.Load();

            player1.IsPlaying = gameSave.Player1.IsPlaying;
            player2.IsPlaying = gameSave.Player2.IsPlaying;
            player1.Name = gameSave.Player1.Name;
            player2.Name = gameSave.Player2.Name;

            board.GameDone = gameSave.GameDone;
            board.Load(gameSave.Slots);

            if (player2.IsPlaying)
                lastPlayer = player2;
            else
                lastPlayer = player1;

            Log.Debug("========================GAME LOADED========================");
            if(board.GameDone)
                Log.Debug(lastPlayer.Name + " won the game");
            else
                Log.Debug(lastPlayer.Name + " is playing...");
        }
    }
}
