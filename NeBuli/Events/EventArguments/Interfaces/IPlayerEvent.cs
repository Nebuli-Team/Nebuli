﻿// -----------------------------------------------------------------------
// <copyright file=IPlayerEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

namespace Nebuli.Events.EventArguments.Interfaces
{
    public interface IPlayerEvent
    {
        /// <summary>
        ///     The player triggering the event.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}