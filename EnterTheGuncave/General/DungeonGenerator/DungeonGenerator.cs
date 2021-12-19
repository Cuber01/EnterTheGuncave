using Microsoft.Xna.Framework;

namespace EnterTheGuncave.General.DungeonGenerator
{
    
    public static class DungeonGenerator
    {
        public static RoomInfo[,] floorMap = new RoomInfo[maxFloorWidth, maxFloorHeight];
        
        private static int roomCount;
        private static int maxRooms;
        
        private const int maxFloorWidth = 25;
        private const int maxFloorHeight = 25;
        private static float doorChance;


        public static void generate()
        {
            Point currentPos = new Point(maxFloorWidth / 2, maxFloorHeight / 2);

            floorMap[currentPos.X, currentPos.Y] = new RoomInfo(
                new bool[] {Util.randomBool(doorChance), Util.randomBool(doorChance), Util.randomBool(doorChance), Util.randomBool(doorChance)}, 
                1,
                dRoomType.start
            );


        }
    }
}