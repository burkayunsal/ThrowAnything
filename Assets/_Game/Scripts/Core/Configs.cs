public static class Configs
{
    public static class Player
    {
        public static float speed = 10f;
        public static float distanceBetweenMobs = 2f;

        
        public static class StandartMobSettings
        {
            public static float detectorRange = 15f;
            public static float maxHP = 80f;
            public static float damage = 15f;
            public static float shootInterval = 1.2f;
        }
        
        public static class BigBoiMobSettings
        {
            public static float detectorRange = 10f;
            public static float maxHP = 150f;
            public static float damage = 25f;
            public static float shootInterval = 3f;
        }
    }

    public static class PathConfigs
    {
        public const float respawnPoint = .9f;
    }


    public static class Enemy
    {
        public static class BarbarianEnemySettings
        {
            public static float range = 20f;
            public static float damage = 10f;
            public static float attackSpeed = 1.2f;
            public static float maxHP = 50f;
        }
        
        public static class TrollEnemySettings
        {
            public static float range = 10f;
            public static float damage = 15f;
            public static float attackSpeed = 3f;
            public static float maxHP = 100f;

        }
        
        public static float speed = 3f;
    }
       

    public static class UI
    {
        public static float FadeOutTime = .2f;
        public static float PopUpTimer = 2f;
    }

    public static class UpgradePlayer
    {
        public static float[] maxHPChange = {0f, 20f, 20f, 50f, 50f, 75f, 75f, 100f, 150f, 200f, 250f};
        public static float[] damageChange = {5f, 10f, 25f, 25f, 25f, 50f, 50f, 50f, 100f, 100f};
        public static float detectorRangeChange = 1f;
        public static float shootIntervalChange = 0.05f;

    }
}
