global using Vintagestory.API.Common;
global using Vintagestory.API.Config;
global using Vintagestory.API.MathTools;
global using Vintagestory.GameContent;
using System;
using Composter.Configuration;
using Newtonsoft.Json.Linq;
using Vintagestory.API.Datastructures;

namespace Composter;

public class Core : ModSystem
{
    public ConfigComposter Config { get; private set; }

    public static Core GetInstance(ICoreAPI api) => api.ModLoader.GetModSystem<Core>();

    public override void StartPre(ICoreAPI api)
    {
        if (!api.Side.IsServer())
        {
            return;
        }

        Config = ModConfig.ReadConfig<ConfigComposter>(api, "Composter.json");
        Config?.Clamp(api);
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
}
