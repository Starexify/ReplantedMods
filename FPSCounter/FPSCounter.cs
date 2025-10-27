using MelonLoader;
using UnityEngine;
using Object = UnityEngine.Object;

[assembly: MelonInfo(typeof(FPSCounter.SimpleFPSCounter), "Simple FPS Counter", "1.0.0", "ImVeryBad")]
[assembly: MelonGame("PopCap Games", "PvZ Replanted")]

#nullable disable
namespace FPSCounter;

public class SimpleFPSCounter : MelonMod
{
    private static bool showFps = true;
    
    private static Texture2D backgroundTexture;

    // FPS tracking variables
    private static float expSmoothingFactor = 0.9f;
    private static float refreshFrequency = 0.4f;
    private static float timeSinceUpdate = 0f;
    private static float averageFps = 1f;

    public override void OnInitializeMelon()
    {
        CreateBackgroundTexture();
    }
    
    private void CreateBackgroundTexture()
    {
        backgroundTexture = new Texture2D(1, 1);
        backgroundTexture.SetPixel(0, 0, new Color(0.1f, 0.1f, 0.1f, 0.7f));
        backgroundTexture.Apply();
        
        // Don't destroy on scene load
        Object.DontDestroyOnLoad(backgroundTexture);
    }
    
    public override void OnGUI()
    {
        if (!showFps) return;
        
        if (backgroundTexture == null)
        {
            CreateBackgroundTexture();
        }
        
        var style = new GUIStyle
        {
            fontSize = 16,
            normal =
            {
                textColor = Color.white,
                background = backgroundTexture
            },
            padding =
            {
                left = 5,
                right = 5,
                top = 5,
                bottom = 5
            }
        };

        GUI.Label(new Rect(10, 10, 90, 30), $"FPS: {Mathf.RoundToInt(averageFps)}", style);
    }

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F3)) showFps = !showFps;
        
        averageFps = expSmoothingFactor * averageFps + (1f - expSmoothingFactor) * 1f / Time.unscaledDeltaTime;

        if (timeSinceUpdate < refreshFrequency)
        {
            timeSinceUpdate += Time.deltaTime;
            return;
        }

        timeSinceUpdate = 0f;
    }
}