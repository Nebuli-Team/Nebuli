// -----------------------------------------------------------------------
// <copyright file=Scp173AttackEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.Events.EventArguments.Interfaces;
using System;
using Nebuli.API.Features;

namespace Nebuli.Events.EventArguments.SCPs.Scp173;

/// <summary>
/// Triggered when SCP-173 attacks a player.
/// </summary>
public class Scp173AttackEvent : EventArgs, IDamageEvent, ICancellableEvent
{
    public Scp173AttackEvent(ReferenceHub player, ReferenceHub target)
    {
        Attacker = API.Features.Player.Get(player);
        Target = API.Features.Player.Get(target);
        IsCancelled = false;
    }

    /// <summary>
    /// Gets SCP-173.
    /// </summary>
    public API.Features.Player Attacker { get; }

    /// <summary>
    /// Gets the target being attacked.
    /// </summary>
    public API.Features.Player Target { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}