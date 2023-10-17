using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
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
			Item.width = 42;
			Item.height = 42;
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
			Console.WriteLine("Running Galaxia OnHitNPC method.");
			PlayerHealthStatus playerHealthStatus = playerCurrentHealthStatus(player.statLifeMax);
			Console.WriteLine("Current Health: {0}",playerHealthStatus.ToString());
			PlayerManaStatus playerManaStatus = playerCurrentManaStatus(player.statManaMax);
            Console.WriteLine("Current Health: {0}",playerHealthStatus.ToString());
            int buffEffectID = determineGalaxiaWeaponBuffStatus(playerHealthStatus, playerManaStatus);
	        Console.WriteLine("Buff ID: {0}",buffEffectID);

            determineGalaxiaBuffEffectAndItemProperties(buffEffectID, target);
			determineGalaxiaDustEffect(buffEffectID, target.Hitbox);
        }

        // public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        // {
        //     PlayerHealthStatus playerHealthStatus = playerCurrentHealthStatus(player.statLifeMax);
		// 	PlayerManaStatus playerManaStatus = playerCurrentManaStatus(player.statManaMax);
		// 	int buffEffectID = determineGalaxiaWeaponBuffStatus(playerHealthStatus, playerManaStatus);

		// 	determineGalaxiaDamage(buffEffectID, damage,);
        // }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
			Console.WriteLine("Running Galaxia MeleeEffects method.");
			PlayerHealthStatus playerHealthStatus = playerCurrentHealthStatus(player.statLifeMax);
            Console.WriteLine("Current Health: {0}",playerHealthStatus.ToString());
            PlayerManaStatus playerManaStatus = playerCurrentManaStatus(player.statManaMax);
            Console.WriteLine("Current Mana: {0}",playerManaStatus.ToString());
            int buffEffectID = determineGalaxiaWeaponBuffStatus(playerHealthStatus, playerManaStatus);
            Console.WriteLine("Buff ID: {0}",buffEffectID);

            determineGalaxiaDustEffect(buffEffectID, hitbox);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            PlayerHealthStatus playerHealthStatus = playerCurrentHealthStatus(player.statLifeMax);
			PlayerManaStatus playerManaStatus = playerCurrentManaStatus(player.statManaMax);
            
			if (galaxiaProjectileEnabled(playerHealthStatus, playerManaStatus)) // Fire projectile when at full health and max hearts
			{
				type = ProjectileID.EyeBeam; // 259 = Eye Beam (Placeholder)
				Projectile.NewProjectile(source, position, velocity, type, damage, knockback);
			}

			return false;
        }
    }
}