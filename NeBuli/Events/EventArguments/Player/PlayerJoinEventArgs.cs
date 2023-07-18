﻿using System;
using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerJoinEventArgs : EventArgs, IPlayerEvent
{
    public PlayerJoinEventArgs(ServerRoles serverRoles)
    {
        Player = new NebuliPlayer(serverRoles._hub);
    }
    
    /// <summary>
    /// The player calling the event.
    /// </summary>
    public NebuliPlayer Player { get; }
}