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
    private static readonly Dictionary<SeedType, PlantBalance> PlantBalances = new()
    {
        [SeedType.Kernelpult] = new PlantBalance
        (
            cost: 225, // Original: 100
            refreshTime: 2250, // Original: 750
            launchRate: 475, // Original: 300
            forceSecondaryWeapon: true
        )
    };

    private static readonly Dictionary<SeedType, PlantAlmanacEntryData> AlmanacEntries = new()
    {
        [SeedType.Kernelpult] = new PlantAlmanacEntryData(
            cost: 225, // Original: 100
            recharge: "slow" // Original: fast
        )
    };

    public override void OnInitializeMelon()
    {
        // Slower fire
        PlantEvents.OnInitPost += (plant, gridX, gridY, seedType, imitaterType, controller) =>
        {
            var actualType = GetActualSeedType(seedType, imitaterType);
            if (PlantBalances.TryGetValue(actualType, out var balance) && balance.LaunchRate.HasValue)
            {
                plant.mLaunchRate = balance.LaunchRate.Value;
            }
        };

        // Always hit with butter
        PlantEvents.OnShoot += (Plant plant, Zombie targetZombie, int row, ref PlantWeapon plantWeapon) =>
        {
            if (PlantBalances.TryGetValue(plant.mSeedType, out var balance) && balance.ForceSecondaryWeapon)
            {
                plantWeapon = PlantWeapon.Secondary;
            }
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
    [HarmonyPatch(typeof(AlmanacEntryData))]
    internal static class AlmanacEntryDataPatch
    {
        [HarmonyPatch(nameof(AlmanacEntryData.EntrySunCost), MethodType.Getter)]
        [HarmonyPostfix]
        internal static void CostPostfix(AlmanacEntryData __instance, ref float __result)
        {
            if (AlmanacEntries.TryGetValue(__instance.m_seedType, out var entry) && entry.Cost.HasValue)
            {
                __result = entry.Cost.Value;
            }
        }

        [HarmonyPatch(nameof(AlmanacEntryData.EntryRecharge), MethodType.Getter)]
        [HarmonyPostfix]
        internal static void RechargePost(AlmanacEntryData __instance, ref string __result)
        {
            __result = "slow";
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
            var actualType = GetActualSeedType(theSeedType, theImitaterType);
            if (PlantBalances.TryGetValue(actualType, out var balance) && balance.Cost.HasValue)
            {
                __result = balance.Cost.Value;
            }
        }

        [HarmonyPatch(nameof(Plant.GetRefreshTime))]
        [HarmonyPostfix]
        internal static void RefreshPost(GameplayActivity app, SeedType theSeedType, SeedType theImitaterType,
            ref int __result)
        {
            var actualType = GetActualSeedType(theSeedType, theImitaterType);
            if (PlantBalances.TryGetValue(actualType, out var balance) && balance.RefreshTime.HasValue)
            {
                __result = balance.RefreshTime.Value;
            }
        }
    }

    private static SeedType GetActualSeedType(SeedType seedType, SeedType imitaterType)
    {
        return imitaterType != SeedType.None ? imitaterType : seedType;
    }
}

public readonly struct PlantBalance
{
    public readonly int? Cost;
    public readonly int? RefreshTime;
    public readonly int? LaunchRate;
    public readonly bool ForceSecondaryWeapon;

    public PlantBalance(
        int? cost = null,
        int? refreshTime = null,
        int? launchRate = null,
        bool forceSecondaryWeapon = false)
    {
        Cost = cost;
        RefreshTime = refreshTime;
        LaunchRate = launchRate;
        ForceSecondaryWeapon = forceSecondaryWeapon;
    }
}

public readonly struct PlantAlmanacEntryData
{
    public readonly int? Cost;
    public readonly string Recharge;

    public PlantAlmanacEntryData(
        int? cost = null,
        string recharge = null)
    {
        Cost = cost;
        Recharge = recharge;
    }
}