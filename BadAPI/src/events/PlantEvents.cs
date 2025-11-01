using Il2CppReloaded.Gameplay;
using Il2CppSource.Controllers;

namespace BadAPI.events;

#nullable disable
public delegate void ShootHandler(Plant plant, Zombie targetZombie, int row, ref PlantWeapon plantWeapon);

public static class PlantEvents
{
    public static event Action<Plant, int, int, SeedType, SeedType, PlantController> OnInitPre;
    public static event Action<Plant, int, int, SeedType, SeedType, PlantController> OnInitPost;
    public static event Action<Plant> OnDeath;
    public static event ShootHandler OnShoot;
    public static event Action<Plant> OnSpecial;

    internal static void InvokeInitPre(Plant plant, int gridX, int gridY, SeedType seedType,
        SeedType imitaterType, PlantController controller) =>
        OnInitPre?.Invoke(plant, gridX, gridY, seedType, imitaterType, controller);

    internal static void InvokeInitPost(Plant plant, int gridX, int gridY, SeedType seedType,
        SeedType imitaterType, PlantController controller) =>
        OnInitPost?.Invoke(plant, gridX, gridY, seedType, imitaterType, controller);

    internal static void InvokeOnDeath(Plant plant) => OnDeath?.Invoke(plant);
    internal static void InvokeOnSpecial(Plant plant) => OnSpecial?.Invoke(plant);

    internal static void InvokeOnShoot(Plant plant, Zombie targetZombie, int row, ref PlantWeapon plantWeapon) =>
        OnShoot?.Invoke(plant, targetZombie, row, ref plantWeapon);
}