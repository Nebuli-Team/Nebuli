﻿// -----------------------------------------------------------------------
// <copyright file=HazardPatch.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using HarmonyLib;
using Hazards;
using Nebuli.API.Features.Hazards;

namespace Nebuli.Events.Patches.Game
{
    [HarmonyPatch(typeof(EnvironmentalHazard), nameof(EnvironmentalHazard.Start))]
    internal class HazardPatchStart
    {
        [HarmonyPostfix]
        private static void AddNew(EnvironmentalHazard __instance)
        {
            EnviormentHazard.Get(__instance);
        }
    }

    [HarmonyPatch(typeof(EnvironmentalHazard), nameof(EnvironmentalHazard.OnDestroy))]
    internal class HazardPatchDestroy
    {
        [HarmonyPostfix]
        private static void Destroy(EnvironmentalHazard __instance)
        {
            EnviormentHazard.Dictionary.Remove(__instance);
        }
    }
}