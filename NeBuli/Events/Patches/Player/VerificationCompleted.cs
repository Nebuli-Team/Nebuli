﻿using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebuli.Events.EventArguments.Player;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;

namespace Nebuli.Events.Patches.Player;

[HarmonyPatch(typeof(ServerRoles), nameof(ServerRoles.UserCode_CmdServerSignatureComplete__String__String__String__Boolean))]
public class VerificationCompleted
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnJoining(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<VerificationCompleted>(702, instructions);
        
        Label retLabel = generator.DefineLabel();
        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Pop) + 1;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new (OpCodes.Ldarg_0),
            new (OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(ServerRoles), nameof(ServerRoles.isLocalPlayer))),
            new (OpCodes.Brtrue_S, retLabel),
            new (OpCodes.Ldarg_0),
            new (OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PlayerJoinEventArgs))[0]),
            new (OpCodes.Call, AccessTools.Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnJoin))),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}