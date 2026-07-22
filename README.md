# Composter (1.22.3+)

A Vintage Story mod that adds a wooden composter container. Food placed inside perishes much faster than normal storage.

Maintained by **mrparsnips** and **TheSchlomo** for **Vintage Story 1.22.3+** (`net10.0`, verified on **1.22.5**), based on the original [Composter](https://github.com/Craluminum-Mods/Composter) by [Craluminum2413](https://mods.vintagestory.at/composter) (Unlicense).

## Build

Requires `$env:VINTAGE_STORY` pointing at a 1.22.3+ install (folder containing `VintagestoryAPI.dll`).

```powershell
dotnet build -c Release
```

Output: `bin/Release/Mods/mod/`

## Install (release)

1. Download **`composterrepack_0.3.2.zip`** from [GitHub Releases](https://github.com/mrparsnips/Composter/releases) or [Mod DB](https://mods.vintagestory.at/compostthis).
2. Copy the zip into your Vintage Story **`Mods`** folder (do not unzip).
3. Launch the game — requires **Vintage Story 1.22.3+** (verified on 1.22.5).

## Verify (VSMods harness)

From the parent [VSMods](https://github.com/mrparsnips/VSMods) workspace:

```powershell
.\tools\Invoke-ModVerify.ps1 -ModPath .\Composter
```

## Configuration

Edit `ModConfig/Composter.json` (under the world `--dataPath` / dedicated `data` folder), or use **ConfigLib** in-game if that mod is installed:

```json
{
  "PerishRate": 100.0,
  "QuantitySlots": 16
}
```

| Key | Default | Notes |
|-----|--------:|-------|
| `PerishRate` | `100` | Multiplier on the climate/room perish rate (tooltip shows climate × this). Clamped to `0.1`–`1000`. Live-updates loaded composters when changed via ConfigLib. |
| `QuantitySlots` | `16` | Inventory slots for **newly placed** composters only (1–64). Already-placed composters keep their saved size until broken and replaced. |

Without ConfigLib, restart the server/world after editing the JSON file so quantity-slot patches re-apply.

## Optional compatibility

- **ConfigLib** (optional): in-game GUI for the settings above. Not listed as a hard dependency.
- `assets/composter/patches/carrycapacity.json` adds CarryOn support when the `carryon` mod is installed.

## License

Unlicense — see [UNLICENSE](UNLICENSE). Original work by Craluminum2413.
