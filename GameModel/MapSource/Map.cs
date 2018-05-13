namespace GameThief.GameModel.MapSource
{
    public class Map<T>
    {
        public T[,] Cells;

        public Map(int width, int height)
        {
            Cells = new T[width, height];
            Wigth = width;
            Height = height;
        }

        public int Wigth { get; set; }
        public int Height { get; set; }

        public T this[int i, int j]
        {
            get => Cells[i, j];
            set => Cells[i, j] = value;
        }
    }
}