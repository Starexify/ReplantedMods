using BadAPI.events;
using HarmonyLib;
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
        PlantEvents.OnShoot += (Plant plant, Zombie targetZombie, int row, ref PlantWeapon plantWeapon) =>
        {
            if (plant.mSeedType == SeedType.Kernelpult) plantWeapon = PlantWeapon.Secondary;
        };
    }

    [HarmonyPatch(typeof(Plant))]
    internal static class PlantPatch
    {
        [HarmonyPatch(nameof(Plant.GetCost))]
        internal static void Postfix(GameplayActivity gLawnApp, SeedType theSeedType, SeedType theImitaterType,
            ref int __result)
        {
            if (theSeedType == SeedType.Kernelpult)
            {
                __result = 225;
            }
        }

        [HarmonyPatch(nameof(Plant.GetRefreshTime))]
        [HarmonyPostfix]
        internal static void RefreshPost(GameplayActivity app, SeedType theSeedType, SeedType theImitaterType,
            ref int __result)
        {
            if (theSeedType == SeedType.Kernelpult)
            {
                __result = 2250;
            }
        }
    }
}