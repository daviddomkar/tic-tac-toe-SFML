using System;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Piskvorky.Shapes;

namespace Piskvorky.Core
{
    class Loader
    {
        private string path;

        public Loader()
        {
            path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\DexitovyPiskvorky.txt";
        }

        public void Save(GameSave gameSave)
        {
            using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8))
                {
                    streamWriter.WriteLine(JsonConvert.SerializeObject(gameSave));
                }
            }
        }

        public GameSave Load()
        {
            using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
            {
                using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
                {
                    return JsonConvert.DeserializeObject<GameSave>(streamReader.ReadToEnd());
                }
            }
        }
    }

    class GameSave
    {
        private SlotSave[,] slots;

        private Player player1;
        private Player player2;

        private bool gameDone;

        public SlotSave[,] Slots
        {
            get => slots;
            set => slots = value;
        }

        public Player Player1
        {
            get => player1;
            set => player1 = value;
        }

        public Player Player2
        {
            get => player2;
            set => player2 = value;
        }

        public bool GameDone
        {
            get => gameDone;
            set => gameDone = value;
        }
    }

    class SlotSave
    {
        private bool empty;
        private ShapeType shapeType;

        public bool Empty
        {
            get => empty;
            set => empty = value;
        }

        public ShapeType ShapeType
        {
            get => shapeType;
            set => shapeType = value;
        }
    }
}