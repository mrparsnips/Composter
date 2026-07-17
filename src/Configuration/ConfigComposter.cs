using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Composter.Configuration;

public class ConfigComposter : IModConfig
{
    public const float DefaultPerishRate = 100.0f;
    public const float MinPerishRate = 0.1f;
    public const float MaxPerishRate = 1000f;

    public const int DefaultQuantitySlots = 16;
    public const int MinQuantitySlots = 1;
    public const int MaxQuantitySlots = 64;

    /// <summary>Optional ConfigLib config-file version.</summary>
    public static int Version = 2;

    [Description("Multiplier on the climate/room perish rate. Tooltip shows climate × this value.")]
    [DefaultValue(DefaultPerishRate)]
    [Range(MinPerishRate, MaxPerishRate)]
    public float PerishRate { get; set; } = DefaultPerishRate;

    [Description("Inventory slots for newly placed composters only (1–64). Existing composters keep their saved size.")]
    [DefaultValue(DefaultQuantitySlots)]
    [Range(MinQuantitySlots, MaxQuantitySlots)]
    public int QuantitySlots { get; set; } = DefaultQuantitySlots;

    public ConfigComposter()
    {
    }

    public ConfigComposter(ICoreAPI api, ConfigComposter previousConfig = null)
    {
        if (previousConfig != null)
        {
            PerishRate = previousConfig.PerishRate;
            QuantitySlots = previousConfig.QuantitySlots;
        }

        Clamp(api);
    }

    public void Clamp(ICoreAPI api)
    {
        float peri = PerishRate;
        int slots = QuantitySlots;

        PerishRate = Math.Clamp(PerishRate, MinPerishRate, MaxPerishRate);
        QuantitySlots = Math.Clamp(QuantitySlots, MinQuantitySlots, MaxQuantitySlots);

        if (api != null && (peri != PerishRate || slots != QuantitySlots))
        {
            api.Logger.Warning(
                "[composterrepack] Clamped config: PerishRate {0} -> {1}, QuantitySlots {2} -> {3}",
                peri, PerishRate, slots, QuantitySlots);
        }
    }
}
