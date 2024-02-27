﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Ian Lucas. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using CounterStrikeSharp.API.Core;

namespace InventorySimulator;

public partial class InventorySimulator
{
    public PlayerInventory GetPlayerInventory(CCSPlayerController player)
    {
        if (g_PlayerInventory.TryGetValue(player.SteamID, out var inventory))
        {
            return inventory;
        }
        return new PlayerInventory(null);
    }
}

public class PlayerInventory
{
    public Dictionary<string, object>? Inventory { get; set; }

    public PlayerInventory(Dictionary<string, object>? inventory)
    {
        Inventory = inventory ?? new Dictionary<string, object>();
    }

    public bool HasProperty(string prefix, byte team)
    {
        if (Inventory == null) return false;
        return Inventory.ContainsKey($"{prefix}_{team}");
    }

    public bool HasProperty(string prefix, byte team, ushort itemDef)
    {
        if (Inventory == null) return false;
        return Inventory.ContainsKey($"{prefix}_{team}_{itemDef}");
    }

    public bool HasProperty(string prefix)
    {
        if (Inventory == null) return false;
        return Inventory.ContainsKey(prefix);
    }

    public ushort GetUShort(string prefix, byte team)
    {
        if (Inventory == null) return 0;
        var key = $"{prefix}_{team}";
        return Convert.ToUInt16((long)Inventory[key]);
    }

    public ushort GetUShort(string prefix)
    {
        if (Inventory == null || !Inventory.ContainsKey(prefix)) return 0;
        return Convert.ToUInt16((long)Inventory[prefix]);
    }

    public int GetInt(string prefix, byte team, ushort itemDef, int defaultValue)
    {
        var key = $"{prefix}_{team}_{itemDef}";
        if (Inventory == null || !Inventory.ContainsKey(key)) return defaultValue;
        return Convert.ToInt32((long)Inventory[key]);
    }

    public float GetFloat(string prefix, byte team, ushort itemDef, float defaultValue)
    {
        var key = $"{prefix}_{team}_{itemDef}";
        if (Inventory == null || !Inventory.ContainsKey(key)) return defaultValue;
        return Inventory[key] switch
        {
            double d => (float)d,
            int i => i,
            _ => defaultValue
        };
    }

    public string GetString(string prefix, byte team, ushort itemDef, string defaultValue)
    {
        var key = $"{prefix}_{team}_{itemDef}";
        if (Inventory == null || !Inventory.ContainsKey(key)) return defaultValue;
        return (string)Inventory[key];
    }
}
