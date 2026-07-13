# Composter (1.22.3)

A Vintage Story mod that adds a wooden composter container. Food placed inside perishes much faster than normal storage.

Maintained by **mrparsnips** and **TheSchlomo** for **Vintage Story 1.22.3** (`net10.0`), based on the original [Composter](https://github.com/Craluminum-Mods/Composter) by [Craluminum2413](https://mods.vintagestory.at/composter) (Unlicense).

## Build

Requires `$env:VINTAGE_STORY` pointing at a 1.22.3 install (folder containing `VintagestoryAPI.dll`).

```powershell
dotnet build -c Release
```

Output: `bin/Release/Mods/mod/`

## Install (release)

1. Download **`composter_1.2.3.zip`** from [GitHub Releases](https://github.com/mrparsnips/Composter/releases).
2. Copy the zip into your Vintage Story **`Mods`** folder (do not unzip).
3. Launch the game — requires **Vintage Story 1.22.3**.

## Verify (VSMods harness)

From the parent [VSMods](https://github.com/mrparsnips/VSMods) workspace:

```powershell
.\tools\Invoke-ModVerify.ps1 -ModPath .\Composter
```

## Configuration

After first load, edit `ModConfig/Composter.json` in your world data folder. Default `PerishRate` is `500`.

## Optional compatibility

`assets/composter/patches/carrycapacity.json` adds CarryOn support when the `carryon` mod is installed.

## License

Unlicense — see [UNLICENSE](UNLICENSE). Original work by Craluminum2413.
