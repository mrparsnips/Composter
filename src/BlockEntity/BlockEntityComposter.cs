using System.Collections.Generic;
using Composter.Configuration;
using Vintagestory.API.Datastructures;

namespace Composter;

public class BlockEntityComposter : BlockEntityGenericTypedContainer
{
    /// <summary>
    /// Multiplier applied on top of the climate/room perish rate.
    /// Synced to clients so the "Stored food perish speed" tooltip is accurate.
    /// </summary>
    float perishRate = 500f;

    public override void Initialize(ICoreAPI api)
    {
        base.Initialize(api);

        // Config lives in the world ModConfig folder (server / SP host).
        if (api.Side.IsServer())
        {
            ConfigComposter config = Core.GetInstance(api)?.Config;
            if (config != null)
            {
                perishRate = config.PerishRate;
            }
        }

        ApplyPerishSpeed();
    }

    protected override void InitInventory(Block block)
    {
        base.InitInventory(block);
        ApplyPerishSpeed();
    }

    public override void FromTreeAttributes(ITreeAttribute tree, IWorldAccessor worldForResolving)
    {
        perishRate = tree.GetFloat("perishRate", perishRate);
        base.FromTreeAttributes(tree, worldForResolving);
        ApplyPerishSpeed();
    }

    public override void ToTreeAttributes(ITreeAttribute tree)
    {
        base.ToTreeAttributes(tree);
        tree.SetFloat("perishRate", perishRate);
    }

    void ApplyPerishSpeed()
    {
        if (Inventory is not InventoryGeneric inv)
        {
            return;
        }

        // Vanilla BEContainer.GetBlockInfo multiplies GetPerishRate() by this value for the tooltip,
        // and InventoryGeneric.GetTransitionSpeedMul applies it before climate delegates — so this
        // both accelerates spoilage and shows the real combined rate (e.g. 0.33 × 500 ≈ 165×).
        inv.TransitionableSpeedMulByType ??= new Dictionary<EnumTransitionType, float>();
        inv.TransitionableSpeedMulByType[EnumTransitionType.Perish] = perishRate;
    }
}
