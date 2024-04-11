using System.Diagnostics.CodeAnalysis;
using BepInEx;
using BepInEx.IL2CPP;
using BepInEx.Logging;
using HarmonyLib;

namespace SoDTemplate;

/// <summary>
///     Main plugin class for SoDTemplate.
/// </summary>
[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
[SuppressMessage("Class Declaration", "BepInEx002:Classes with BepInPlugin attribute must inherit from BaseUnityPlugin")]
public class Plugin : BasePlugin
{
    private bool _isPatched;
    private Harmony? Harmony { get; set; }
    internal static ManualLogSource? Logger { get; private set; }

    /// <summary>
    ///     Singleton instance of the plugin.
    /// </summary>
    public static Plugin? Instance { get; private set; }

    /// <summary>
    ///     Loads the plugin.
    /// </summary>
    public override void Load()
    {
        // Set instance
        Instance = this;

        // Init logger
        Logger = Log;

        // Patch using Harmony
        PatchAll();

        // Report plugin loaded
        Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
    }

    private void PatchAll()
    {
        if (_isPatched)
        {
            Logger?.LogWarning("Already patched!");
            return;
        }

        Logger?.LogDebug("Patching...");

        Harmony ??= new Harmony(PluginInfo.PLUGIN_GUID);

        Harmony.PatchAll();
        _isPatched = true;

        Logger?.LogDebug("Patched!");
    }
}