using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TestMod.Helpers;

namespace TestMod.Items
{
	public class PoisonSword : ModItem
	{
        // The Display Name and Tooltip of this item can be edited in the Localization/en-US_Mods.TestMod.hjson file.

		public override void SetDefaults()
		{
			Item.damage = 5;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6f;
			Item.value = 10000;
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}

        public override void UseAnimation(Player player)
        {
            base.UseAnimation(player);
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
			if (Main.rand.NextBool(4)) // 1/4 chance, or 25% in other words
            {
                target.AddBuff(BuffID.Poisoned, // Adding Poisoned to target
                    300); // for 5 seconds (60 ticks = 1 second)
            }
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
			if (Main.rand.NextBool(3)) // With 1/3 chance per tick (60 ticks = 1 second)...
			{ 
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), // Position to spawn
				hitbox.Width, // Width and Height
				hitbox.Height, // Dust type
				DustID.Poisoned, 
				0, 0, // Velocity X and Velocity Y of the dust, I set to 0 to prevent dust from moving
				75); // Dust transparency, 0 - full visibility, 255 - full transparency
			}
        }
    }
}