﻿// -----------------------------------------------------------------------
// <copyright file=Flashlight.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using FlashlightBase = InventorySystem.Items.ToggleableLights.Flashlight.FlashlightItem;

namespace Nebuli.API.Features.Items.LightSources
{
    public class Flashlight : Item
    {
        internal Flashlight(FlashlightBase itemBase) : base(itemBase)
        {
            Base = itemBase;
        }

        /// <summary>
        ///     Gets the <see cref="FlashlightBase" /> base.
        /// </summary>
        public new FlashlightBase Base { get; }

        /// <summary>
        ///     Gets or sets if the flashlight is emitting light.
        /// </summary>
        public bool IsEmittingLight
        {
            get => Base.IsEmittingLight;
            set => Base.IsEmittingLight = value;
        }
    }
}