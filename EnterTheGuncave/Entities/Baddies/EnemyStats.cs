namespace EnterTheGuncave.Entities.Baddies
{
    public struct EnemyStats
    {
        public EnemyStats(int speed, int heartDamage, int hitpoints)
        {
            this.speed = speed;
            this.heartDamage = heartDamage;
            this.hitpoints = hitpoints;
        }

        public int speed;
        public int heartDamage;
        public int hitpoints;
    }
}