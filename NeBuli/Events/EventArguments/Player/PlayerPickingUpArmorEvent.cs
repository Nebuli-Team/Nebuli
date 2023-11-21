﻿// -----------------------------------------------------------------------
// <copyright file=PlayerPickingUpArmorEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using InventorySystem.Items.Pickups;
using Nebuli.API.Features.Items.Pickups;
using Nebuli.Events.EventArguments.Interfaces;
using System;
using Nebuli.API.Features;

namespace Nebuli.Events.EventArguments.Player;

/// <summary>
/// Triggered when a player picks up armor.
/// </summary>
public class PlayerPickingUpArmorEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerPickingUpArmorEvent(ReferenceHub player, ItemPickupBase armor)
    {
        Player = API.Features.Player.Get(player);
        Armor = (ArmorPickup)Pickup.Get(armor);
        IsCancelled = false;
    }

    public API.Features.Player Player { get; }

    /// <summary>
    /// The <see cref="ArmorPickup"/> being picked up.
    /// </summary>
    public ArmorPickup Armor { get; }

    public bool IsCancelled { get; set; }
}