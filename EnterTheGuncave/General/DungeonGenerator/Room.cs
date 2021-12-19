using EnterTheGuncave.General.ContentHandling.Rooms;

namespace EnterTheGuncave.General.DungeonGenerator
{
    public struct RoomInfo
    {
        public RoomInfo(bool[] doors, int roomIndex, dRoomType roomType)
        {
            this.doors = doors;
            this.roomIndex = roomIndex;
            this.roomType = roomType;
        }
        
        // 0 up, 1 down, 2 right, 3 left
        public bool[] doors;
        public int roomIndex;
        public dRoomType roomType;
    }

    public enum dRoomType
    {
        start,
        normal,
        treasure,
        boss
    }
}