using Microsoft.Xna.Framework;
using Terraria;

namespace TestMod.Helpers
{
    public static class DustEffectsHelper
    {
        public static void applyDustEffect(Rectangle hitbox, short dustID)
        {
            	Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), // Position to spawn
				hitbox.Width, // Width and 
				hitbox.Height, // Height
				dustID, // Dust type
				0, 0, // Velocity X and Velocity Y of the dust, I set to 0 to prevent dust from moving
				75); // Dust transparency, 0 - full visibility, 255 - full transparency
        } 
    }
}