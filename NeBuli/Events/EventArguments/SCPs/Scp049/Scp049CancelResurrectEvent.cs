// -----------------------------------------------------------------------
// <copyright file=Scp049CancelResurrectEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebuli.Events.EventArguments.Interfaces;

namespace Nebuli.Events.EventArguments.SCPs.Scp049
{
    /// <summary>
    ///     Triggered when SCP-049 cancels the resurrection process.
    /// </summary>
    public class Scp049CancelResurrectEvent : EventArgs, IPlayerEvent
    {
        public Scp049CancelResurrectEvent(ReferenceHub player)
        {
            Player = API.Features.Player.Get(player);
        }

        /// <summary>
        ///     Gets the player cancelling the ressurect.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}