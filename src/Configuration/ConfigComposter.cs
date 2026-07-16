using System;

namespace Composter.Configuration;

public class ConfigComposter : IModConfig
{
    public const float DefaultPerishRate = 100.0f;
    public const float MinPerishRate = 0.01f;

    public const int DefaultQuantitySlots = 16;
    public const int MinQuantitySlots = 1;
    public const int MaxQuantitySlots = 64;

    public float PerishRate { get; set; } = DefaultPerishRate;
    public int QuantitySlots { get; set; } = DefaultQuantitySlots;

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

        PerishRate = Math.Max(MinPerishRate, PerishRate);
        QuantitySlots = Math.Clamp(QuantitySlots, MinQuantitySlots, MaxQuantitySlots);

        if (api != null && (peri != PerishRate || slots != QuantitySlots))
        {
            api.Logger.Warning(
                "[composterrepack] Clamped config: PerishRate {0} -> {1}, QuantitySlots {2} -> {3}",
                peri, PerishRate, slots, QuantitySlots);
        }
    }
}
