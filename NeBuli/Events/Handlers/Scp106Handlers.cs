﻿// -----------------------------------------------------------------------
// <copyright file=Scp106Handlers.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.Events.EventArguments.SCPs.Scp106;

namespace Nebuli.Events.Handlers
{
    public static class Scp106Handlers
    {
        /// <summary>
        ///     Triggered when SCP-106 is attacking a player.
        /// </summary>
        public static event EventManager.CustomEventHandler<Scp106AttackingEvent> Attacking;

        internal static void OnAttacking(Scp106AttackingEvent ev)
        {
            Attacking.CallEvent(ev);
        }
    }
}