# Composter (1.22.3)

A Vintage Story mod that adds a wooden composter container. Food placed inside perishes much faster than normal storage.

This repository is a maintained fork for **Vintage Story 1.22.3** (`net10.0`), based on the original [Composter](https://github.com/Craluminum-Mods/Composter) by [Craluminum2413](https://mods.vintagestory.at/composter) (Unlicense).

## Build

Requires `$env:VINTAGE_STORY` pointing at a 1.22.3 install (folder containing `VintagestoryAPI.dll`).

```powershell
dotnet build -c Release
```

Output: `bin/Release/Mods/mod/`

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
