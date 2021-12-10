namespace EnterTheGuncave.Projectile
{
    public struct BulletStats
    {
        public BulletStats(int speed, int penetration, int lifetime, int damage)
        {
            this.speed = speed;
            this.penetration = penetration;
            this.lifetime = lifetime;
            this.damage = damage;
        }
        
        public readonly int speed;
        public readonly int penetration;
        public readonly int damage;
        public int lifetime;
    }
}