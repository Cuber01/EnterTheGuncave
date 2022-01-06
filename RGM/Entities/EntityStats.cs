namespace RGM.Entities.Baddies
{
    public struct EntityStats
    {
        public EntityStats(int speed, int damage, int hitpoints, int spread, int range, int reloadTime, int penetration, int bulletSpeed)
        {
            this.speed = speed;
            this.damage = damage;
            this.hitpoints = hitpoints;

            this.spread = spread;
            this.range = range;
            this.reloadTime = reloadTime;
            this.penetration = penetration;
            this.bulletSpeed = bulletSpeed;
        }

        public int speed;
        public int damage;
        public int hitpoints;
        
        public int spread;
        public int range;
        public int reloadTime;
        public int penetration;
        public int bulletSpeed;
    }
}