using RGM.General.ContentHandling.Rooms;

namespace RGM.General.EventHandling
{
    public static class GEventResponse
    {

        // Sound event reponse is in SoundManager.cs
        public static void subscribeGeneralMethods()
        {
            GEventHandler.subscribe(enemyKilledResponse, dEvents.enemyKilled);
            GEventHandler.subscribe(roomClearResponse, dEvents.roomClear);
            GEventHandler.subscribe(playerKilledResponse, dEvents.playerKilled);
        }
        
        private static void enemyKilledResponse(dEvents e)
        {
            RGM.enemiesInRoom--;

            if (RGM.enemiesInRoom <= 0)
            {
                GEventHandler.queueFiringEvent(dEvents.roomClear);
            }
        }
        
        private static void roomClearResponse(dEvents e)
        {

            //DungeonGenerator.DungeonGenerator.debugPrintDungeon();
            
            RoomLoader.clearedRooms[RGM.Player.mapPosition.X, RGM.Player.mapPosition.Y] = 1;
            // Util.prettyPrint2DArray(RoomLoader.clearedRooms);
        }

        private static void playerKilledResponse(dEvents e)
        {
            RGM.gameOver = true;
        }



    }
}