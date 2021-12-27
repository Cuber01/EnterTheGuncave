namespace RGM.Entities.Projectiles
{
    public struct ShooterStats
    {
        public ShooterStats(int reloadTime, int accuracy, BulletStats bulletStats)
        {
            this.reloadTime = reloadTime;
            this.accuracy = accuracy;
            this.bulletStats = bulletStats;
        }
        
        public BulletStats bulletStats;
        
        public readonly int reloadTime;
        public int accuracy;
    }
}