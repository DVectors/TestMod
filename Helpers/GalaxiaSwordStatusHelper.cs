using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

using static TestMod.Helpers.DustEffectsHelper;

namespace TestMod.Helpers
{
    // Helper class to help determine Galaxia Sword buff statuses
    public static class GalaxiaSwordStatusHelper
    {
        public static int determineGalaxiaWeaponBuffStatus(
            PlayerHealthStatus playerHealthStatus,
            PlayerManaStatus  playerManaStatus)
        {   
            switch (playerHealthStatus, playerManaStatus)
            {
                case (PlayerHealthStatus.DEFAULT_HEALTH,
                        PlayerManaStatus.MAX_MANA):
                    return BuffID.Electrified;
                case (PlayerHealthStatus.MAX_HEALTH,
                        PlayerManaStatus.MAX_MANA):
                    return BuffID.OnFire;
                case (PlayerHealthStatus.MAX_LIFE_FRUIT,
                        PlayerManaStatus.MAX_MANA):
                    return BuffID.OnFire3;
                default:
                    return 0; // 0 = No Buff
            }       
        }

        public static void determineGalaxiaBuffEffectAndItemProperties(int buffEffectID, Item item, NPC target)
        {
            switch (buffEffectID)
            {
                case BuffID.Electrified:
                    item.damage = 20;
				    target.AddBuff(buffEffectID, 300); // Electrify for 300 ticks if player has all hearts
                    break;
                case BuffID.OnFire:
                    item.damage = 20;
				    target.AddBuff(buffEffectID, 300); // Electrify for 300 ticks if player has all hearts
                    break;
                case BuffID.OnFire3:
                    item.damage = 20;
				    target.AddBuff(buffEffectID, 300); // Electrify for 300 ticks if player has all hearts
                    break;
                default:
                    item.damage = 15;
                    break;	
            }
        }

        public static void determineGalaxiaDustEffect(int buffEffectID, Rectangle hitbox)
        {
            switch (buffEffectID)
            {
                case BuffID.Electrified:
                    applyDustEffect(hitbox, DustID.Electric);
                    break;
                case BuffID.OnFire:
                    applyDustEffect(hitbox, DustID.FlameBurst);
                    break;
                case BuffID.OnFire3:
                    applyDustEffect(hitbox, DustID.GoldFlame);
                    break;
                default:
                    break;
            }
        }

        public static bool galaxiaProjectileEnabled(
            PlayerHealthStatus playerHealthStatus, 
            PlayerManaStatus playerManaStatus)
        {
            if (playerHealthStatus == PlayerHealthStatus.MAX_LIFE_FRUIT 
            && playerManaStatus == PlayerManaStatus.MAX_MANA)
            {
                return true;
            }
            return false;
        }
    }
}