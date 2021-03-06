﻿using System.Collections.Generic;
using Harmony;
using TUNING;

namespace MosaicTile
{
	public static class MosaicTilePatches
	{
		[HarmonyPatch(typeof(SplashMessageScreen))]
		[HarmonyPatch("OnPrefabInit")]
		public static class SplashMessageScreen_OnPrefabInit_Patch
		{
			public static void Postfix()
			{
				CaiLib.ModCounter.ModCounter.Hit(ModInfo.Name, ModInfo.Version);
				CaiLib.Logger.LogInit(ModInfo.Name, ModInfo.Version);
			}
		}

		[HarmonyPatch(typeof(GeneratedBuildings))]
		[HarmonyPatch("LoadGeneratedBuildings")]
		public static class GeneratedBuildings_LoadGeneratedBuildings_Patch
		{
			public static void Prefix()
			{
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{MosaicTileConfig.Id.ToUpperInvariant()}.NAME", MosaicTileConfig.DisplayName);
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{MosaicTileConfig.Id.ToUpperInvariant()}.DESC", MosaicTileConfig.Description);
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{MosaicTileConfig.Id.ToUpperInvariant()}.EFFECT", MosaicTileConfig.Effect);

				AddBuildingToPlanScreen("Base", MosaicTileConfig.Id);
			}
		}

		[HarmonyPatch(typeof(Db))]
		[HarmonyPatch("Initialize")]
		public static class Db_Initialize_Patch
		{
			public static void Prefix()
			{
				var luxuryTech = new List<string>(Database.Techs.TECH_GROUPING["Luxury"]) { MosaicTileConfig.Id };
				Database.Techs.TECH_GROUPING["Luxury"] = luxuryTech.ToArray();
			}
		}

		private static void AddBuildingToPlanScreen(HashedString category, string buildingId)
		{
			var index = BUILDINGS.PLANORDER.FindIndex(x => x.category == category);

			if (index == -1)
				return;

			(BUILDINGS.PLANORDER[index].data as IList<string>)?.Add(buildingId);
		}
	}
}
