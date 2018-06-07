using System;
using System.Data.SqlTypes;
using Piskvorky.Shapes;
using SFML.Graphics;
using SFML.System;

namespace Piskvorky.Core
{
    class Board : Transformable, Drawable
    {
        private Slot[,] slots;

        private VertexArray drawingArray;

        private Vector2i size;

        private bool gameDone;

        public Slot[,] Slots
        {
            get => slots;
        }

        public bool GameDone
        {
            get => gameDone;
            set => gameDone = value;
        }

        public Board(Vector2i size)
        {
            this.size = size;

            slots = new Slot[size.X, size.Y];

            drawingArray = new VertexArray
            {
                PrimitiveType = PrimitiveType.Quads
            };
            drawingArray.Resize((uint)slots.Length);

            for (int y = 0; y < slots.GetLength(0); y++)
            {
                for (int x = 0; x < slots.GetLength(1); x++)
                {
                    slots[y, x] = new Slot(x, y, drawingArray);
                }
            }
        }

        public PlaceStatus PlaceShape(ShapeType shapeType, float x, float y)
        {
            if (gameDone)
                return PlaceStatus.CONNECTED;

            int xPos = (int) ((x - 0) / (1000 - 0) * (size.X - 0) + 0);
            int yPos = (int) ((y - 0) / (1000 - 0) * (size.Y - 0) + 0);

            if (slots[yPos, xPos].Shape != null)
            {
                Log.Debug($"This place is occupied X: {xPos}, Y: {yPos}");
                return PlaceStatus.OCCUPIED;
            }

            slots[yPos, xPos].AsignShape(shapeType);

            Log.Debug(string.Format((shapeType == ShapeType.Circle ? "Circle" : "Cross") + " was placed at position: X: {0}, Y: {1}", xPos, yPos));

            return IsConnected(yPos, xPos) ? PlaceStatus.CONNECTED : PlaceStatus.PLACED;
        }

        private bool IsConnected(int y, int x)
        {
            Slot currentSlot = slots[y, x];

            //From Down Left to Top Right
            int count = 0;

            for (int i = 1; count <= 4; i++)
                if (CheckSlot(currentSlot.Shape.ShapeType, x + i, y - i))
                    count++;
                else
                    break;

            for (int i = 1; count <= 4; i++)
                if (CheckSlot(currentSlot.Shape.ShapeType, x - i, y + i))
                    count++;
                else
                    break;

            if (count >= 4) return true;

            //From Down Right to Top Left
            count = 0;

            for (int i = 1; count <= 4; i++)
                if (CheckSlot(currentSlot.Shape.ShapeType, x - i, y - i))
                    count++;
                else
                    break;

            for (int i = 1; count <= 4; i++)
                if (CheckSlot(currentSlot.Shape.ShapeType, x + i, y + i))
                    count++;
                else
                    break;

            if (count >= 4) return true;

            //From Down to Top
            count = 0;

            for (int i = 1; count <= 4; i++)
                if (CheckSlot(currentSlot.Shape.ShapeType, x, y - i))
                    count++;
                else
                    break;

            for (int i = 1; count <= 4; i++)
                if (CheckSlot(currentSlot.Shape.ShapeType, x, y + i))
                    count++;
                else
                    break;

            if (count >= 4) return true;

            //From Left to Right
            count = 0;

            for (int i = 1; count <= 4; i++)
                if (CheckSlot(currentSlot.Shape.ShapeType, x + i, y))
                    count++;
                else
                    break;

            for (int i = 1; count <= 4; i++)
                if (CheckSlot(currentSlot.Shape.ShapeType, x - i, y))
                    count++;
                else
                    break;

            if (count >= 4) return true;

            return false;
        }

        private bool CheckSlot(ShapeType type, int x, int y)
        {
            if (x >= 0 && y >= 0 && x < slots.GetLength(1) && y < slots.GetLength(0))
                if (slots[y, x].Shape != null && slots[y, x].Shape.ShapeType == type)
                    return true;

            return false;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;

            states.Texture = TextureManager.Texture.Texture;

            target.Draw(drawingArray, states);

            for (int y = 0; y < slots.GetLength(0); y++)
            {
                for (int x = 0; x < slots.GetLength(1); x++)
                {
                    target.Draw(slots[y, x]);
                }
            }
        }

        public void Load(SlotSave[,] slots)
        {
            for (int y = 0; y < slots.GetLength(0); y++)
            {
                for (int x = 0; x < slots.GetLength(1); x++)
                {
                    if(slots[y, x].Empty)
                        this.slots[y, x].DetachShape();
                    else
                        this.slots[y, x].AsignShape(slots[y, x].ShapeType);
                }
            }
        }

        public void Clear()
        {
            for (int y = 0; y < slots.GetLength(0); y++)
            {
                for (int x = 0; x < slots.GetLength(1); x++)
                {
                    slots[y, x].DetachShape();
                }
            }
        }
    }

    public enum PlaceStatus
    {
        PLACED,
        CONNECTED,
        OCCUPIED
    }
}
