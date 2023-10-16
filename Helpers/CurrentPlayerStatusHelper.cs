namespace TestMod.Helpers
{
    //Helper class to help determine current player status
    public static class CurrentPlayerStatusHelper
    {
        public static PlayerHealthStatus playerCurrentHealthStatus(int currentHealth)
        {
            if (currentHealth >= 400 && currentHealth <= 500)
            {
                return PlayerHealthStatus.MAX_HEALTH;
            }
            else if (currentHealth >= 500)
            {
                return PlayerHealthStatus.MAX_LIFE_FRUIT;
            }
            return PlayerHealthStatus.DEFAULT_HEALTH;
        }

        public static PlayerManaStatus playerCurrentManaStatus(int currentMana)
        {
            if (currentMana >= 400)
            {
                return PlayerManaStatus.MAX_MANA;
            }
            return PlayerManaStatus.DEFAULT_MANA;
        }
    }
}