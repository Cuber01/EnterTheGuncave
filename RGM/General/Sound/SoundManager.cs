using RGM.General.ContentHandling.Assets;
using RGM.General.EventHandling;

namespace RGM.General.Sound
{
    public static class SoundManager
    {
        public static void init()
        {
            GEventHandler.subscribe(enemyHurt, dEvents.enemyHurt);
            GEventHandler.subscribe(enemyKilled, dEvents.enemyKilled);
            GEventHandler.subscribe(playerHurt, dEvents.playerHurt);
            GEventHandler.subscribe(playerKilled, dEvents.playerKilled);
            GEventHandler.subscribe(shoot, dEvents.shoot);
        }

        private static void enemyHurt(dEvents e)
        {
            
            if (Util.randomBool(0.5f))
            {
                AssetLoader.sfx[dSoundKeys.enemy_hurt].Play();
            }
            else
            {
                AssetLoader.sfx[dSoundKeys.enemy_hurt2].Play();
            }
            
        }

        private static void shoot(dEvents e)
        {
            AssetLoader.sfx[dSoundKeys.shoot].Play();
        }

        private static void enemyKilled(dEvents e)
        {
            AssetLoader.sfx[dSoundKeys.enemy_die].Play();
        }
        
        private static void playerHurt(dEvents e)
        {
            AssetLoader.sfx[dSoundKeys.player_hit].Play();
        }
        
        private static void playerKilled(dEvents e)
        {
            AssetLoader.sfx[dSoundKeys.player_death].Play();
        }
        
        
    }
}