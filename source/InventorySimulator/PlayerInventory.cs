﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Ian Lucas. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text.Json;

namespace InventorySimulator;

public partial class InventorySimulator
{
    public readonly PlayerInventory g_EmptyInventory = new()
    {
        Knives = new(),
        Gloves = new(),
        TWeapons = new(),
        CTWeapons = new(),
        Agents = new()
    };

    public void LoadPlayerInventories()
    {
        var path = Path.Combine(Server.GameDirectory, g_InventoriesFilePath);
        if (!File.Exists(path))
            return;
        try
        {
            string json = File.ReadAllText(path);
            var inventories = JsonConvert.DeserializeObject<Dictionary<ulong, PlayerInventory>>(json);
            if (inventories != null)
            {
                foreach (var pair in inventories)
                {
                    g_PlayerInventoryLock.Add(pair.Key);
                    g_PlayerInventory[pair.Key] = pair.Value;
                }
            }
        }
        catch
        {
            // Ignore any error.
        }
    }

    public void PlayerInventoryCleanUp()
    {
        var connected = Utilities.GetPlayers().Select(player => player.SteamID).ToHashSet();
        var disconnected = g_PlayerInventory.Keys.Except(connected).ToList();
        foreach (var steamId in disconnected)
        {
            RemovePlayerInventory(steamId);
        }
    }

    public void RemovePlayerInventory(ulong steamId)
    {
        if (!g_PlayerInventoryLock.Contains(steamId))
        {
            g_PlayerInventory.Remove(steamId);
        }
    }

    public PlayerInventory GetPlayerInventory(CCSPlayerController player)
    {
        if (g_PlayerInventory.TryGetValue(player.SteamID, out var inventory))
        {
            return inventory;
        }
        return g_EmptyInventory;
    }

    public float ViewUintAsFloat(uint value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        return BitConverter.ToSingle(bytes, 0);
    }
}

public class StickerItem
{
    [JsonProperty("def")]
    public uint Def { get; set; }

    [JsonProperty("slot")]
    public ushort Slot { get; set; }

    [JsonProperty("wear")]
    public float Wear { get; set; }
}

public class BaseEconItem
{
    [JsonProperty("def")]
    public ushort Def { get; set; }

    [JsonProperty("paint")]
    public int Paint { get; set; }

    [JsonProperty("seed")]
    public int Seed { get; set; }

    [JsonProperty("wear")]
    public float Wear { get; set; }
}

public class WeaponEconItem : BaseEconItem
{
    [JsonProperty("legacy")]
    public bool Legacy { get; set; }

    [JsonProperty("nametag")]
    public required string Nametag { get; set; }

    [JsonProperty("stattrak")]
    public required int Stattrak { get; set; }

    [JsonProperty("stickers")]
    public required List<StickerItem> Stickers { get; set; }
}

public class AgentItem
{
    [JsonProperty("model")]
    public required string Model { get; set; }

    [JsonProperty("patches")]
    public required List<uint> Patches { get; set; }
}

public class PlayerInventory
{
    [JsonProperty("knives")]
    public required Dictionary<byte, WeaponEconItem> Knives { get; set; }

    [JsonProperty("gloves")]
    public required Dictionary<byte, BaseEconItem> Gloves { get; set; }

    [JsonProperty("tWeapons")]
    public required Dictionary<ushort, WeaponEconItem> TWeapons { get; set; }

    [JsonProperty("ctWeapons")]
    public required Dictionary<ushort, WeaponEconItem> CTWeapons { get; set; }

    [JsonProperty("agents")]
    public required Dictionary<byte, AgentItem> Agents { get; set; }

    [JsonProperty("pin")]
    public uint? Pin { get; set; }

    [JsonProperty("musicKit")]
    public ushort? MusicKit { get; set; }

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
