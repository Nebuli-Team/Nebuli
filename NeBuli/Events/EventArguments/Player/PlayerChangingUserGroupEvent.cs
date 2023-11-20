﻿// -----------------------------------------------------------------------
// <copyright file=PlayerChangingUserGroupEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using System;

namespace Nebuli.Events.EventArguments.Player;

/// <summary>
/// Triggered when a player changes their user group.
/// </summary>
public class PlayerChangingUserGroupEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerChangingUserGroupEvent(ReferenceHub ply, UserGroup group)
    {
        Player = NebuliPlayer.Get(ply);
        Group = group;
    }

    /// <summary>
    /// Gets the player triggering the event.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets the <see cref="UserGroup"/> thats being changed to.
    /// </summary>
    public UserGroup Group { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}