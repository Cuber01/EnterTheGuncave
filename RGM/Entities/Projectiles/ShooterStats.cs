namespace RGM.Entities.Projectiles
{
    public struct ShooterStats
    {
        public ShooterStats(int reloadTime, int spread, BulletStats bulletStats)
        {
            this.reloadTime = reloadTime;
            this.spread = spread;
            this.bulletStats = bulletStats;
        }
        
        public BulletStats bulletStats;
        
        public readonly int reloadTime;
        public int spread;
    }
}