﻿using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using System;

namespace Nebuli.Events.EventArguments.Server;

/// <summary>
/// Triggered before the warhead stops its detonation sequence.
/// </summary>
public class WarheadStoppingEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public WarheadStoppingEvent(ReferenceHub player)
    {
        Player = player == null ? API.Features.Server.NebuliHost : NebuliPlayer.Get(player);
        IsCancelled = false;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool IsCancelled { get; set; }
}