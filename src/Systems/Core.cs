global using Vintagestory.API.Common;
global using Vintagestory.API.Config;
global using Vintagestory.API.MathTools;
global using Vintagestory.GameContent;
using System;
using Composter.Configuration;
using Newtonsoft.Json.Linq;
using Vintagestory.API.Server;

namespace Composter;

public class Core : ModSystem
{
    public ConfigComposter Config { get; private set; }

    public static Core GetInstance(ICoreAPI api) => api.ModLoader.GetModSystem<Core>();

    public override void StartPre(ICoreAPI api)
    {
        if (api.Side.IsServer())
        {
            Config = ModConfig.ReadConfig<ConfigComposter>(api, ConfigLibCompat.ConfigFile);
            Config?.Clamp(api);
        }
        else
        {
            // Defaults until ConfigLib syncs from the server (or SP host file).
            Config = new ConfigComposter(api);
        }

        if (api.ModLoader.IsModEnabled("configlib") && Config != null)
        {
            ConfigLibCompat.Register(api, Config, () => OnConfigApplied(api));
        }
    }

    public override void Start(ICoreAPI api)
    {
        api.RegisterBlockClass("Composter.BlockComposter", typeof(BlockComposter));
        api.RegisterBlockEntityClass("Composter.BlockEntityComposter", typeof(BlockEntityComposter));
        Mod.Logger.Event("started '{0}' mod", Mod.Info.Name);
    }

    /// <summary>
    /// Patch quantitySlots on registered composter blocks so newly placed BEs
    /// pick up the config value in InitInventory. Existing saves keep their qslots.
    /// </summary>
    public override void AssetsFinalize(ICoreAPI api)
    {
        if (!api.Side.IsServer() || Config == null)
        {
            return;
        }

        ApplyQuantitySlots(api);
    }

    void OnConfigApplied(ICoreAPI api)
    {
        Config?.Clamp(api);

        if (!api.Side.IsServer() || Config == null)
        {
            return;
        }

        ApplyQuantitySlots(api);
        RefreshPlacedComposterPerishRates(api);
    }

    void ApplyQuantitySlots(ICoreAPI api)
    {
        int slots = Config.QuantitySlots;
        int patched = 0;

        foreach (Block block in api.World.Blocks)
        {
            if (block?.Code == null || block.Code.Domain != "composter")
            {
                continue;
            }

            if (!block.Code.Path.StartsWith("composter", StringComparison.Ordinal))
            {
                continue;
            }

            if (block.Attributes == null || !block.Attributes.KeyExists("quantitySlots"))
            {
                continue;
            }

            if (block.Attributes["quantitySlots"].Token is not JObject quantitySlots)
            {
                continue;
            }

            foreach (JProperty prop in quantitySlots.Properties())
            {
                prop.Value = slots;
            }

            patched++;
        }

        api.Logger.Event(
            "[composterrepack] QuantitySlots={0} applied to {1} block variant(s) (new placements only)",
            slots, patched);
    }

    void RefreshPlacedComposterPerishRates(ICoreAPI api)
    {
        if (api is not ICoreServerAPI sapi)
        {
            return;
        }

        int updated = 0;
        foreach (IWorldChunk chunk in sapi.WorldManager.AllLoadedChunks.Values)
        {
            if (chunk?.BlockEntities == null)
            {
                continue;
            }

            foreach (BlockEntity be in chunk.BlockEntities.Values)
            {
                if (be is not BlockEntityComposter compost)
                {
                    continue;
                }

                compost.ApplyConfigPerishRate(Config.PerishRate);
                updated++;
            }
        }

        if (updated > 0)
        {
            sapi.Logger.Event(
                "[composterrepack] PerishRate={0} applied to {1} loaded composter(s)",
                Config.PerishRate, updated);
        }
    }
}
