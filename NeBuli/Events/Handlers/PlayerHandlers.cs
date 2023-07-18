﻿using Nebuli.Events.EventArguments.Player;

namespace Nebuli.Events.Handlers;

public static class PlayerHandlers
{
    public static event EventManager.CustomEventHandler<PlayerJoinEventArgs> Join;

    public static event EventManager.CustomEventHandler<PlayerLeaveEventArgs> Leave; 

    internal static void OnJoin(PlayerJoinEventArgs ev) => Join.CallEvent(ev);
    
    internal static void OnLeave(PlayerLeaveEventArgs ev) => Leave.CallEvent(ev);
}