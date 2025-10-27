using MelonLoader;

[assembly: MelonInfo(typeof(ReplantMod.ReplantedMod), "ReplantedMod", "1.0.0", "ImVeryBad")]
[assembly: MelonGame("PopCap Games", "PvZ Replanted")]

#nullable disable
namespace ReplantMod;

public abstract class ReplantedMod : MelonMod
{
}