﻿using HarmonyLib;
using Mirror;
using Nebuli.Events.EventArguments.SCPs.Scp079;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp079.Pinging;
using RelativePositioning;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.SCPs.Scp079;

[HarmonyPatch(typeof(Scp079PingAbility), nameof(Scp079PingAbility.ServerProcessCmd))]
internal class PingingPatch
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnPing(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<PingingPatch>(53, instructions);

        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Blt_S) + 2;

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new(OpCodes.Ldarg_1),
            new(OpCodes.Call, Method(typeof(PingingPatch), nameof(HandlePing))),
            new(OpCodes.Ret),
        });

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }

    private static void HandlePing(Scp079PingAbility pingAbility, NetworkReader networkReader)
    {
        RelativePosition curRelativePos = networkReader.ReadRelativePosition();
        Vector3 syncNormal = networkReader.ReadVector3();
        Scp079PingingEvent ev = new(pingAbility.Owner, curRelativePos, pingAbility._cost, pingAbility._syncProcessorIndex, syncNormal);
        Scp079Handlers.OnScp079Pinging(ev);

        if (ev.IsCancelled)
            return;

        pingAbility._syncNormal = ev.SyncPosition;
        pingAbility._syncPos = curRelativePos;
        pingAbility.ServerSendRpc(hub => pingAbility.ServerCheckReceiver(hub, ev.Position, (int)ev.PingType));
        pingAbility.AuxManager.CurrentAux -= ev.PowerCost;
        pingAbility._rateLimiter.RegisterInput();
    }

}
