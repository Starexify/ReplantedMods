using BadAPI.events;
using HarmonyLib;
using Il2CppReloaded.Data;
using Il2CppReloaded.Gameplay;
using Il2CppReloaded.TreeStateActivities;
using MelonLoader;

[assembly: MelonInfo(typeof(Rebalanced.Rebalanced), "Rebalanced", "0.0.1", "ImVeryBad")]

#nullable disable
namespace Rebalanced;

public class Rebalanced : MelonMod
{
    public override void OnInitializeMelon()
    {
        // Slower fire
        PlantEvents.OnInitPost += (plant, gridX, gridY, seedType, imitaterType, controller) =>
        {
            if (seedType == SeedType.Kernelpult) plant.mLaunchRate = 475; // Original: 300
        };

        // Always hit with butter
        PlantEvents.OnShoot += (Plant plant, Zombie targetZombie, int row, ref PlantWeapon plantWeapon) =>
        {
            if (plant.mSeedType == SeedType.Kernelpult) plantWeapon = PlantWeapon.Secondary;
        };
    }

    /*[HarmonyPatch(typeof(PlantDefinition))]
    internal static class PlantDefinitionPatch
    {
        [HarmonyPatch("get_LaunchRate")]
        [HarmonyPostfix]
        internal static void LaunchRatePostfix(PlantDefinition __instance, ref int __result)
        {
            if (__instance.m_seedType == SeedType.Kernelpult) __result = 1000;
        }
    }*/

    // Patch Almanac Entry cost
    [HarmonyPatch(typeof(AlmanacEntryData), "get_EntrySunCost")]
    internal static class AlmanacEntryDataPatch
    {
        internal static void Postfix(AlmanacEntryData __instance, ref float __result)
        {
            if (__instance.m_seedType == SeedType.Kernelpult) __result = 225;
        }
    }

    // Increase plant cost and lower refresh time
    [HarmonyPatch(typeof(Plant))]
    internal static class PlantPatch
    {
        [HarmonyPatch(nameof(Plant.GetCost))]
        internal static void Postfix(GameplayActivity gLawnApp, SeedType theSeedType, SeedType theImitaterType,
            ref int __result)
        {
            if (theSeedType == SeedType.Kernelpult) __result = 225; // Original: 100
        }

        [HarmonyPatch(nameof(Plant.GetRefreshTime))]
        [HarmonyPostfix]
        internal static void RefreshPost(GameplayActivity app, SeedType theSeedType, SeedType theImitaterType,
            ref int __result)
        {
            if (theSeedType == SeedType.Kernelpult) __result = 2250; // Original: 750
        }
    }
}