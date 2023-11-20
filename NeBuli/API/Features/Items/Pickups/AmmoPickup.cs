﻿// -----------------------------------------------------------------------
// <copyright file=AmmoPickup.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

namespace Nebuli.API.Features.Items.Pickups;

public class AmmoPickup : Pickup
{
    /// <summary>
    /// Gets the <see cref="InventorySystem.Items.Firearms.Ammo.AmmoPickup"/> base.
    /// </summary>
    public new InventorySystem.Items.Firearms.Ammo.AmmoPickup Base { get; }

    internal AmmoPickup(InventorySystem.Items.Firearms.Ammo.AmmoPickup pickupBase) : base(pickupBase)
    {
        Base = pickupBase;
    }

    /// <summary>
    /// Gets the max ammo of the <see cref="AmmoPickup"/>.
    /// </summary>
    public int MaxAmmo => Base.MaxAmmo;

    /// <summary>
    /// Gets or sets the current ammo of the <see cref="AmmoPickup"/>.
    /// </summary>
    public ushort CurrentAmmo
    {
        get => Base.NetworkSavedAmmo;
        set => Base.NetworkSavedAmmo = value;
    }
}