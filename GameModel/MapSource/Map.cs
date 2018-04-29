namespace GameThief.GameModel.MapSource
{
    public class Map
    {
        public Cell[,] Cells;
        public int Wigth { get; set; }
        public int Height { get; set; }

        public Map(int width, int height)
        {
            Cells = new Cell[width, height];
            Wigth = width;
            Height = height;
        }

        public Cell this[int i, int j]
        {
            get { return Cells[i, j]; }
            set { Cells[i, j] = value; }
        }
    }
}
