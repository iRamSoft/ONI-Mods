﻿using Harmony;
using UnityEngine;

namespace ShowIndustrialMachineryTag
{
	public static class Patches
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

		[HarmonyPatch(typeof(SimpleInfoScreen))]
		[HarmonyPatch("SetPanels")]
		public static class SimpleInfoScreen_SetPanels_Patch
		{
			public static void Postfix(ref SimpleInfoScreen __instance, GameObject target)
			{
				if (target == null) return;

				var prefab = target.GetComponent<KPrefabID>();
				if (prefab == null) return;

				if (!prefab.HasTag(RoomConstraints.ConstraintTags.IndustrialMachinery)) return;

				var field = Traverse.Create(__instance).Field("descriptionContainer");
				var descriptionContainer = field?.GetValue<DescriptionContainer>();
				if (descriptionContainer == null)
					return;

				var text = "\n\n<color=\"red\"><b>This is an industrial machine.</b></color>";
			
				descriptionContainer.flavour.text += text;

				field.SetValue(descriptionContainer);
			}
		}
	}
}
