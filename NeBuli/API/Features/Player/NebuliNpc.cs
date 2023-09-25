﻿using MEC;
using Mirror;
using Nebuli.API.Extensions;
using Nebuli.API.Internal;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using PlayerStatsSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Nebuli.API.Features.Player;

/// <summary>
/// Class for handling <see cref="NebuliNpc"/> easily in-game.
/// </summary>
public class NebuliNpc : NebuliPlayer
{
    /// <summary>
    /// Creates a new <see cref="NebuliNpc"/> with a specified <see cref="ReferenceHub"/>.
    /// </summary>
    /// <param name="hub">The <see cref="ReferenceHub"/> to use to create the NPC.</param>
    internal NebuliNpc(ReferenceHub hub) : base(hub)
    { }

    /// <summary>
    /// Creates a new <see cref="NebuliNpc"/> with a specified <see cref="GameObject"/>.
    /// </summary>
    /// <param name="gameObject">The <see cref="GameObject"/> to use to create the NPC.</param>
    internal NebuliNpc(GameObject gameObject) : base(gameObject)
    { }

    /// <summary>
    /// Gets a list of all the <see cref="NebuliNpc"/> instances.
    /// </summary>
    public static new List<NebuliNpc> List => 
        NebuliPlayer.List.Where(player => player.IsNPC)
        .Select(player => player as NebuliNpc)
        .ToList();

    /// <summary>
    /// Creates a new NPC with the specified parameters.
    /// </summary>
    /// <param name="name">The name of the NPC.</param>
    /// <param name="role">The <see cref="RoleTypeId"/> of the NPC.</param>
    /// <param name="ID">The ID of the NPC.</param>
    /// <param name="UserId">The UserID of the NPC.</param>
    /// <returns>A newly created <see cref="NebuliNpc"/>.</returns>
    public static NebuliNpc CreateNPC(string name, RoleTypeId role, int ID, string UserId = null)
    {
        try
        {
            GameObject newPlayer = Object.Instantiate(NetworkManager.singleton.playerPrefab);
            NebuliNpc newNPC = new(newPlayer);
            try
            {
                newNPC.ReferenceHub.roleManager.InitializeNewRole(RoleTypeId.None, RoleChangeReason.None);
            }
            catch (Exception e)
            {
                Log.Debug("Safe to ignore, error caused by setting NPC role --->\n" + e);
            }
            FakeConnection fakeConnection = new(ID);
            NetworkServer.AddPlayerForConnection(fakeConnection, newPlayer);
            try
            {
                newNPC.ReferenceHub.characterClassManager.UserId = UserId is not null ? UserId : null;
            }
            catch (Exception e)
            {
                Log.Debug("Safe to ignore, error caused by setting NPC UserID --->\n" + e);
            }
            newNPC.ReferenceHub.nicknameSync.Network_myNickSync = name;

            Timing.CallDelayed(0.4f, () =>
            {
                newNPC.SetRole(role);
            });

            newNPC.IsNPC = true;

            return newNPC;
        }
        catch (Exception e)
        {
            Log.Error("Error while creating a NPC! Full error -->\n" + e);
            return null;
        }
    }

    /// <summary>
    /// Disconnects and destroys the NPC.
    /// </summary>
    public void DestroyNPC()
    {
        NetworkServer.Destroy(GameObject);
        CustomNetworkManager.TypedSingleton.OnServerDisconnect(ReferenceHub.connectionToClient);
        ReferenceHub.OnDestroy();
        Dictionary.Remove(ReferenceHub);
    }

    /// <summary>
    /// Makes the NPC look at the specified position.
    /// </summary>
    /// <param name="position">The position to look at.</param>
    public void LookAt(Vector3 position)
    {
        Vector3 direction = position - Position;
        Quaternion quat = Quaternion.LookRotation(direction, Vector3.up);
        FpcMouseLook mouseLook = ((IFpcRole)ReferenceHub.roleManager.CurrentRole).FpcModule.MouseLook;
        (ushort horizontal, ushort vertical) = quat.ToClientUShorts();
        mouseLook.ApplySyncValues(horizontal, vertical);
    }
}