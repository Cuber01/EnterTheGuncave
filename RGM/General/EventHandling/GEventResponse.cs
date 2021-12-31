namespace RGM.General.EventHandling
{
    public static class GEventResponse
    {

        public static void subscribeGeneralMethods()
        {
            GEventHandler.subscribe(enemyKillResponse, dEvents.enemyKilled);
        }
        
        private static void enemyKillResponse(dEvents e)
        {
            RGM.enemiesInRoom--;

            if (RGM.enemiesInRoom <= 0)
            {
                GEventHandler.queueFiringEvent(dEvents.roomClear);
            }
        }


        
    }
}