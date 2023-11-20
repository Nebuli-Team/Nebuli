// -----------------------------------------------------------------------
// <copyright file=Scp173BlinkEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nebuli.Events.EventArguments.SCPs.Scp173;

/// <summary>
/// Triggered when SCP-173 performs a blink.
/// </summary>
public class Scp173BlinkEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp173BlinkEvent(ReferenceHub player, Vector3 position, List<NebuliPlayer> blinkers)
    {
        Player = NebuliPlayer.Get(player);
        Position = position;
        IsCancelled = false;
        Blinkers = blinkers;
    }

    /// <summary>
    /// The player triggering the event.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// The position 173 will be teleported to.
    /// </summary>
    public Vector3 Position { get; set; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets the players blinking.
    /// </summary>
    public List<NebuliPlayer> Blinkers { get; }
}