using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TestMod.Helpers;
using static TestMod.Helpers.CurrentPlayerStatusHelper;
using static TestMod.Helpers.GalaxiaSwordStatusHelper;

namespace TestMod.Items
{
	public class GalaxiaSword : ModItem
	{
        // The Display Name and Tooltip of this item can be edited in the Localization/en-US_Mods.TestMod.hjson file.
		public override void SetDefaults()
		{
			Item.damage = 15;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6f;
			Item.value = 10000;
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.Item15; // Use Phasesaber sound - Placeholder
			Item.autoReuse = true;

			Item.shoot = ProjectileID.None; // Default to 0 - No projectile
			Item.shootSpeed = 16f; // Shoot speed for projectile
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.GoldBar, 25);
			recipe.AddIngredient(ItemID.MeteoriteBar, 10);
			recipe.AddIngredient(ItemID.Ruby, 20);
			recipe.AddIngredient(ItemID.FallenStar, 30);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}

        public override void UseAnimation(Player player)
        {
            base.UseAnimation(player);
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
			PlayerHealthStatus playerHealthStatus = playerCurrentHealthStatus(player.statLifeMax);
			PlayerManaStatus playerManaStatus = playerCurrentManaStatus(player.statManaMax);
			int buffEffectID = determineGalaxiaWeaponBuffStatus(playerHealthStatus, playerManaStatus);

			determineGalaxiaBuffEffectAndItemProperties(buffEffectID, Item, target);
			determineGalaxiaDustEffect(buffEffectID, target.Hitbox);
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
			PlayerHealthStatus playerHealthStatus = playerCurrentHealthStatus(player.statLifeMax);
			PlayerManaStatus playerManaStatus = playerCurrentManaStatus(player.statManaMax);
			int buffEffectID = determineGalaxiaWeaponBuffStatus(playerHealthStatus, playerManaStatus);
			
			determineGalaxiaDustEffect(buffEffectID, hitbox);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
			PlayerHealthStatus playerHealthStatus = playerCurrentHealthStatus(player.statLifeMax);
			PlayerManaStatus playerManaStatus = playerCurrentManaStatus(player.statManaMax);
            
			if (galaxiaProjectileEnabled(playerHealthStatus, playerManaStatus)) // Fire projectile when at full health and max hearts
			{
				Item.shoot = ProjectileID.Bullet; // 14 = bullet (Placeholder)
			}
        }
    }
}