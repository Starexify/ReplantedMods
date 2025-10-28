using MelonLoader;

[assembly: MelonInfo(typeof(BadAPI.BadMod), "BadMod", "0.0.1", "ImVeryBad")]

namespace BadAPI;
public class BadMod : Module
{
    public override void OnUpdate()
    {
        if (Board != null) LoggerInstance.Msg(Board);
    }
}