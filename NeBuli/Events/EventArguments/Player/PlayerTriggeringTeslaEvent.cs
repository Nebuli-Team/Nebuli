﻿// -----------------------------------------------------------------------
// <copyright file=PlayerTriggeringTeslaEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebuli.Events.EventArguments.Interfaces;
using NebuliTeslaGate = Nebuli.API.Features.Map.TeslaGate;

namespace Nebuli.Events.EventArguments.Player
{
    /// <summary>
    ///     Triggered when a player triggers a Tesla gate.
    /// </summary>
    public class PlayerTriggeringTeslaEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public PlayerTriggeringTeslaEvent(API.Features.Player player, TeslaGate teslaGate)
        {
            Player = player;
            TeslaGate = NebuliTeslaGate.Get(teslaGate);
            IsCancelled = false;
            IsInIdleRange =
                true; // This is true because we already check if the player is in idle range when calling the event.
            IsTriggerable = TeslaGate.IsPlayerInHurtingRange(Player);
        }

        /// <summary>
        ///     Gets the <see cref="NebuliTeslaGate" /> being triggered.
        /// </summary>
        public NebuliTeslaGate TeslaGate { get; }

        /// <summary>
        ///     Gets or sets if the player is in idle range.
        /// </summary>
        public bool IsInIdleRange { get; set; }

        /// <summary>
        ///     Gets or sets if the player is in trigger range.
        /// </summary>
        public bool IsTriggerable { get; set; }

        /// <summary>
        ///     Gets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     Gets the player triggering the tesla gate.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}