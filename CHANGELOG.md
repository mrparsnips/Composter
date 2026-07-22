# Changelog

## 0.3.2 — 2026-07-21

- Verified on **Vintage Story 1.22.5** (L0–L3 smoke against dedicated server)
- Docs/metadata updated for 1.22.5; minimum game dependency remains **1.22.3**

### Install

Download `composterrepack_0.3.2.zip`. Remove older `composterrepack_*.zip` first.

## 0.3.1 — 2026-07-17

- Optional [ConfigLib](https://mods.vintagestory.at/configlib) support: edit `PerishRate` / `QuantitySlots` in the in-game config GUI (same `ModConfig/Composter.json`)
- Live-apply perish rate to loaded composters when config changes; slot count still affects **new placements** only
- Soft dependency only — ConfigLib is not required
- ConfigLib slider range for `PerishRate` is **0.1–1000** (was 0.01–100000)

### Install

Download `composterrepack_0.3.1.zip`. Remove older `composterrepack_*.zip` first.

## 0.3.0 — 2026-07-16

- Config: `QuantitySlots` (default **16**, clamp 1–64) in `ModConfig/Composter.json` — applies to **newly placed** composters only; existing ones keep their saved inventory size
- Config: default `PerishRate` is now **100** (was 500); clamp to a minimum of `0.01` and log when values are adjusted
- Document both settings in the README

### Install

Download `composterrepack_0.3.0.zip`. Remove older `composterrepack_*.zip` first.

## 0.2.1 — 2026-07-13

- Apply compost perish multiplier via `TransitionableSpeedMulByType` (vanilla path) so spoilage and the "Stored food perish speed" tooltip both reflect config (default **500×** climate/room rate, e.g. ~165× outdoors)
- Sync `perishRate` to clients; fix cases where the old inventory hook never attached because `InitInventory` ran before `Api` was set

### Install

Download `composterrepack_0.2.1.zip`. Remove older `composterrepack_*.zip` first.

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
