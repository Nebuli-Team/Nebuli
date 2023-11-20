﻿// -----------------------------------------------------------------------
// <copyright file=VerificationCompleted.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using CentralAuth;
using HarmonyLib;
using Nebuli.Events.EventArguments.Player;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using System.Collections.Generic;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.Player;

[HarmonyPatch(typeof(PlayerAuthenticationManager), nameof(PlayerAuthenticationManager.FinalizeAuthentication))]
internal class VerificationCompleted
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnJoining(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<VerificationCompleted>(142, instructions);

        Label retLabel = generator.DefineLabel();
        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Stloc_0) - 7;

        newInstructions.InsertRange(index, new[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new(OpCodes.Callvirt, PropertyGetter(typeof(ServerRoles), nameof(ServerRoles.isLocalPlayer))),
            new(OpCodes.Brtrue_S, retLabel),
            new(OpCodes.Ldarg_0),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerJoinEvent))[0]),
            new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnJoin))),
        });

        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}