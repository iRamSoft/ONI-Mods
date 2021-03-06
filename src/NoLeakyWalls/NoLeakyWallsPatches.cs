﻿using Harmony;

namespace NoLeakyWalls
{
	public class NoLeakyWallsPatches
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

		[HarmonyPatch(typeof(ExteriorWallConfig))]
		[HarmonyPatch("CreateBuildingDef")]
		public static class ExteriorWallConfig_CreateBuildingDef_Patch
		{
			public static void Postfix(ref BuildingDef __result)
			{
				__result.BuildLocationRule = BuildLocationRule.Anywhere;
			}
		}

		[HarmonyPatch(typeof(ThermalBlockConfig))]
		[HarmonyPatch("CreateBuildingDef")]
		public static class ThermalBlockConfig_CreateBuildingDef_Patch
		{
			public static void Postfix(ref BuildingDef __result)
			{
				__result.BuildLocationRule = BuildLocationRule.Anywhere;
			}
		}
	}
}
