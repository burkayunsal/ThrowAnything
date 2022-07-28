public static class Configs
{
    public static class Player
    {
        public static float speed = 10f;
        public static float distanceBetweenMobs = 2f;

        public static class StandartMobSettings
        {
            public static float detectorRange = 20f;
            public static float maxHP = 80f;
            public static float damage = 50f;
            public static float shootInterval = 1.2f;
        }
        
        public static class BigBoiMobSettings
        {
            public static float detectorRange = 10f;
            public static float maxHP = 150f;
            public static float damage = 100f;
            public static float shootInterval = 2f;
        }
    }

    public static class PathConfigs
    {
        public const float respawnPoint = .92f;
    }


    public static class Enemy
    {
        public static class BarbarianEnemySettings
        {
            public static float range = 20f;
            public static float damage = 50f;
            public static float attackSpeed = 1.2f;
            public static float maxHP = 100f;
        }
        
        public static class TrollEnemySettings
        {
            public static float range = 10f;
            public static float damage = 50f;
            public static float attackSpeed = 1.2f;
            public static float maxHP = 250f;

        }
        
        public static float speed = 3f;
    }
       

    public static class UI
    {
        public static float FadeOutTime = .2f;
    }
}
