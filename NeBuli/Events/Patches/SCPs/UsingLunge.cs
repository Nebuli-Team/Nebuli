using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebuli.Events.EventArguments.SCPs.Scp939;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp939;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.SCPs;

[HarmonyPatch(typeof(Scp939LungeAbility), nameof(Scp939LungeAbility.TriggerLunge))]
public class UsingLunge
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnUsingLunge(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<UsingLunge>(8, instructions);
        
        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new(OpCodes.Ldarg_0),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp939LungeAbility), nameof(Scp939LungeAbility.Owner))),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp939UseLunge))[0]),
            new(OpCodes.Call, Method(typeof(Scp939Handlers), nameof(Scp939Handlers.OnUseLunge))),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}