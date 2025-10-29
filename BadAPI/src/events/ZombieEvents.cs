using Il2CppReloaded.Gameplay;
using Il2CppSource.Controllers;

namespace BadAPI.events;

#nullable disable
public static class ZombieEvents
{
    public static event Action<Zombie, int, ZombieType, bool, Zombie, int, ZombieController> OnInitPre;
    public static event Action<Zombie, int, ZombieType, bool, Zombie, int, ZombieController> OnInitPost;
    public static event Action<Zombie> OnDie;
    public static event Action<Zombie, Zombie> OnEatingZombie;
    public static event Action<Zombie, Plant> OnEatingPlant;
    public static event Action<Zombie, int, DamageFlags> OnDamage;

    internal static void InvokeInitPre(Zombie zombie, int row, ZombieType zombieType, bool variant, Zombie parentZombie,
        int fromWave, ZombieController controller) =>
        OnInitPre?.Invoke(zombie, row, zombieType, variant, parentZombie, fromWave, controller);

    internal static void InvokeInitPost(Zombie zombie, int row, ZombieType zombieType, bool variant,
        Zombie parentZombie,
        int fromWave, ZombieController controller) =>
        OnInitPost?.Invoke(zombie, row, zombieType, variant, parentZombie, fromWave, controller);

    internal static void InvokeDie(Zombie zombie) => OnDie?.Invoke(zombie);
    internal static void InvokeEatingPlant(Zombie zombie, Plant plant) => OnEatingPlant?.Invoke(zombie, plant);

    internal static void InvokeEatingZombie(Zombie zombie, Zombie targetZombie) =>
        OnEatingZombie?.Invoke(zombie, targetZombie);

    internal static void InvokeDamage(Zombie zombie, int damage, DamageFlags damageFlags) =>
        OnDamage?.Invoke(zombie, damage, damageFlags); 
}