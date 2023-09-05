﻿using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerEnteringPocketDimensionEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerEnteringPocketDimensionEvent(API.Features.Player.Player player, API.Features.Player.Player target)
    {
        Player = player;
        Target = target;
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player teleporting the target, or SCP-106.
    /// </summary>
    public API.Features.Player.Player Player { get; }

    /// <summary>
    /// Gets the player being teleported.
    /// </summary>
    public API.Features.Player.Player Target { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
