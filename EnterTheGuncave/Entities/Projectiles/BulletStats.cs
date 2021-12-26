namespace EnterTheGuncave.Entities.Projectiles
{
    public struct BulletStats
    {
        public BulletStats(int speed, int penetration, int lifetime, int damage, dTeam team)
        {
            this.speed = speed;
            this.penetration = penetration;
            this.lifetime = lifetime;
            this.damage = damage;
            this.team = team;
        }
        
        public readonly int speed;
        public int penetration;
        public readonly int damage;
        public int lifetime;
        public dTeam team;
    }

    public enum dTeam
    {
        allies,
        baddies,
        neutrals
    }
}