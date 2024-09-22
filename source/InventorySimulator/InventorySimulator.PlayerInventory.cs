﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Ian Lucas. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using CounterStrikeSharp.API.Modules.Utils;
using System.Text.Json.Serialization;

namespace InventorySimulator;

public class StickerItem
{
    [JsonPropertyName("def")]
    public uint Def { get; set; }

    [JsonPropertyName("slot")]
    public ushort Slot { get; set; }

    [JsonPropertyName("wear")]
    public float Wear { get; set; }
}

public class BaseEconItem
{
    [JsonPropertyName("def")]
    public ushort Def { get; set; }

    [JsonPropertyName("paint")]
    public int Paint { get; set; }

    [JsonPropertyName("seed")]
    public int Seed { get; set; }

    [JsonPropertyName("wear")]
    public float Wear { get; set; }

    public int? FadeSeed;
}

public class WeaponEconItem : BaseEconItem
{
    [JsonPropertyName("legacy")]
    public bool Legacy { get; set; }

    [JsonPropertyName("nametag")]
    public required string Nametag { get; set; }

    [JsonPropertyName("stattrak")]
    public required int Stattrak { get; set; }

    [JsonPropertyName("stickers")]
    public required List<StickerItem> Stickers { get; set; }

    [JsonPropertyName("uid")]
    public required int Uid { get; set; }
}

public class AgentItem
{
    [JsonPropertyName("model")]
    public required string Model { get; set; }

    [JsonPropertyName("patches")]
    public required List<uint> Patches { get; set; }

    [JsonPropertyName("vofallback")]
    public required bool VoFallback { get; set; }

    [JsonPropertyName("vofemale")]
    public required bool VoFemale { get; set; }

    [JsonPropertyName("voprefix")]
    public required string VoPrefix { get; set; }
}

public class MusicKitItem
{
    [JsonPropertyName("def")]
    public int Def { get; set; }

    [JsonPropertyName("stattrak")]
    public required int Stattrak { get; set; }

    [JsonPropertyName("uid")]
    public required int Uid { get; set; }
}

public class GraffitiItem
{
    [JsonPropertyName("def")]
    public required int Def { get; set; }

    [JsonPropertyName("tint")]
    public required int Tint { get; set; }
}

[method: JsonConstructor]
public class PlayerInventory(
    Dictionary<byte, WeaponEconItem>? knives = null,
    Dictionary<byte, BaseEconItem>? gloves = null,
    Dictionary<ushort, WeaponEconItem>? tWeapons = null,
    Dictionary<ushort, WeaponEconItem>? ctWeapons = null,
    Dictionary<byte, AgentItem>? agents = null,
    uint? pin = null,
    MusicKitItem? musicKit = null,
    GraffitiItem? graffiti = null)
{
    [JsonPropertyName("knives")]
    public Dictionary<byte, WeaponEconItem> Knives { get; set; } = knives ?? [];

    [JsonPropertyName("gloves")]
    public Dictionary<byte, BaseEconItem> Gloves { get; set; } = gloves ?? [];

    [JsonPropertyName("tWeapons")]
    public Dictionary<ushort, WeaponEconItem> TWeapons { get; set; } = tWeapons ?? [];

    [JsonPropertyName("ctWeapons")]
    public Dictionary<ushort, WeaponEconItem> CTWeapons { get; set; } = ctWeapons ?? [];

    [JsonPropertyName("agents")]
    public Dictionary<byte, AgentItem> Agents { get; set; } = agents ?? [];

    [JsonPropertyName("pin")]
    public uint? Pin { get; set; } = pin;

    [JsonPropertyName("musicKit")]
    public MusicKitItem? MusicKit { get; set; } = musicKit;

    [JsonPropertyName("graffiti")]
    public GraffitiItem? Graffiti { get; set; } = graffiti;

    public WeaponEconItem? GetKnife(byte team)
    {
        if (Knives.TryGetValue(team, out var knife))
        {
            return knife;
        }
        return null;
    }

    public WeaponEconItem? GetWeapon(CsTeam team, ushort def)
    {
        if ((team == CsTeam.Terrorist ? TWeapons : CTWeapons).TryGetValue(def, out var weapon))
        {
            return weapon;
        }
        return null;
    }
}