using System.Collections.Generic;

namespace EnterTheGuncave.Content
{
    public struct Level
    {
        public int[] data;
        public List<mapPoint> points;

        public Level(int[] data, List<mapPoint> points)
        {
            this.points = points;
            this.data = data;
        }
    }

    public struct mapPoint
    {
        public int x;
        public int y;
        public int objectId;

        public mapPoint(int x, int y, int objectId)
        {
            this.x = x;
            this.y = y;
            this.objectId = objectId;
        }
    }
}