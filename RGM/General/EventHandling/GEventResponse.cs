using RGM.General.ContentHandling.Rooms;

namespace RGM.General.EventHandling
{
    public static class GEventResponse
    {

        public static void subscribeGeneralMethods()
        {
            GEventHandler.subscribe(enemyKillResponse, dEvents.enemyKilled);
            GEventHandler.subscribe(roomClearResponse, dEvents.roomClear);
        }
        
        private static void enemyKillResponse(dEvents e)
        {
            RGM.enemiesInRoom--;

            if (RGM.enemiesInRoom <= 0)
            {
                GEventHandler.queueFiringEvent(dEvents.roomClear);
            }
        }
        
        private static void roomClearResponse(dEvents e)
        {
            RoomLoader.clearedRooms[RGM.Player.mapPosition.X, RGM.Player.mapPosition.Y] = 1;
        }


        
    }
}