using Newtonsoft.Json;
using Piskvorky.Shapes;

namespace Piskvorky.Core
{
    class Player
    {
        private ShapeType shapeType;

        private bool isPlaying;

        private Board board;

        private string name;

        public string Name { get => name; set => name = value; }

        [JsonIgnore]
        public ShapeType ShapeType => shapeType;

        public bool IsPlaying
        {
            get => isPlaying;
            set => isPlaying = value;
        }

        public Player(string name, ShapeType shapeType, Board board)
        {
            this.name = name;
            this.shapeType = shapeType;
            this.board = board;
            this.isPlaying = false;
        }

        public void Play()
        {
            isPlaying = true;
            Log.Debug(name + " is playing...");
        }

        public void PlaceShape(float x, float y)
        {
            PlaceStatus status = board.PlaceShape(shapeType, x, y);

            if (status == PlaceStatus.CONNECTED)
            {
                board.GameDone = true;
                Log.Debug(name + " won the game");
            }
            else if (status == PlaceStatus.PLACED)
                isPlaying = false;
        }
    }
}
