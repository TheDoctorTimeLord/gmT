using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GameThief.GameModel
{
    public static class PointExtensions
    {
        public static IEnumerable<Point> FindNeighbours(this Point point)
        {
            return PossibleDirections
                .Select(size => new Size(point + size))
                .Select(size => new Point(size));
        }

        private static readonly Size[] PossibleDirections =
        {
            new Size(0, 1),
            new Size(1, 1),
            new Size(1, 0),
            new Size(1, -1),
            new Size(0, -1),
            new Size(-1, -1),
            new Size(-1, 0),
            new Size(-1, 1)
        };
    }
}
