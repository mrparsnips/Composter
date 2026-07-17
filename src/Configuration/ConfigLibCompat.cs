using System;
using ConfigLib;
using Vintagestory.API.Common;

namespace Composter.Configuration;

/// <summary>
/// Soft ConfigLib integration (compile-time ref only; no modinfo dependency).
/// Source: maltiez2/vsmod_configlib RegisterCustomManagedConfig (v1.12.x).
/// </summary>
public static class ConfigLibCompat
{
    public const string Domain = "composterrepack";
    public const string ConfigFile = "Composter.json";

    public static void Register(ICoreAPI api, ConfigComposter config, Action onApplied)
    {
        ConfigLibModSystem configLib = api.ModLoader.GetModSystem<ConfigLibModSystem>();

        configLib.RegisterCustomManagedConfig(
            Domain,
            config,
            ConfigFile,
            onSyncedFromServer: () =>
            {
                configLib.GetConfig(Domain)?.AssignSettingsValues(config);
                onApplied();
            },
            onSettingChanged: _ => onApplied(),
            onConfigSaved: onApplied);

        // Initial file parse updates ConfigSetting values only — push into the POCO.
        configLib.ConfigsLoaded += () =>
        {
            configLib.GetConfig(Domain)?.AssignSettingsValues(config);
            onApplied();
        };
    }
}
