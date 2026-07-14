# Changelog

## 0.2.0 — 2026-07-13

- Fix release packaging: zip entries now use POSIX `/` paths so Linux dedicated servers unpack `assets/` correctly (1.2.3 used Windows `\` paths; DLL loaded but blocks never registered)
- Rebuild and republish as **0.2.0** (Mod DB already consumed file id / version slot for 1.2.3)

### Install

Download `composterrepack_0.2.0.zip` from [Releases](https://github.com/mrparsnips/Composter/releases) or [Mod DB](https://mods.vintagestory.at/compostthis) and place it in your `Mods` folder. Remove any older `composterrepack_1.2.3.zip`.

## 1.2.3 — 2026-07-13

First maintained release for **Vintage Story 1.22.3** (`net10.0`).

- Port of [Craluminum2413's Composter](https://github.com/Craluminum-Mods/Composter) v1.2.2
- Rebuilt for game **1.22.3** with updated `modinfo.json`
- Mod id is `composterrepack` (the original `composter` id is taken on the Mod DB); asset domain stays `composter`, so placed composters carry over from the original mod
- **Known issue:** package used Windows zip path separators; broken on Linux dedicated servers. Fixed in 0.2.0.

### Install

Download `composterrepack_1.2.3.zip` from [Releases](https://github.com/mrparsnips/Composter/releases) and place it in your `Mods` folder.

### Configuration

After first world load, edit `ModConfig/Composter.json`. Default `PerishRate` is `500`.
