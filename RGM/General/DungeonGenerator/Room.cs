namespace RGM.General.DungeonGenerator
{
    public struct RoomInfo
    {
        public RoomInfo(int roomIndex, dRoomType roomType)
        {
            this.roomIndex = roomIndex;
            this.roomType = roomType;
        }
        
        public int roomIndex;
        public dRoomType roomType;
    }

    public enum dRoomType
    {
        empty,
        start,
        normal,
        treasure,
        boss
    }
}