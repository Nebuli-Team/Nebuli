﻿using AdminToys;
using Mirror;
using System.Collections.Generic;
using UnityEngine;

namespace Nebuli.API.Features.AdminToys;

public static class ToyUtilities
{
    private static readonly Dictionary<string, Component> primitives = new();

    private static T GetBaseObject<T>(string objectName) where T : Component
    {
        if (!primitives.TryGetValue(objectName, out Component baseObject))
        {
            foreach (GameObject gameObject in NetworkClient.prefabs.Values)
            {
                if (gameObject.name == objectName && gameObject.TryGetComponent(out T component))
                {
                    baseObject = component;
                    if(!primitives.ContainsKey(objectName)) primitives.Add(objectName, baseObject);
                    break;
                }
            }
        }

        return (T)baseObject;
    }

    /// <summary>
    /// Gets the <see cref="PrimitiveObjectToy"/> object.
    /// </summary>
    public static PrimitiveObjectToy PrimitiveBase => GetBaseObject<PrimitiveObjectToy>("PrimitiveObjectToy");

    /// <summary>
    /// Gets the <see cref="LightSourceToy"/> object.
    /// </summary>
    public static LightSourceToy LightBase => GetBaseObject<LightSourceToy>("LightSourceToy");

    /// <summary>
    /// Gets the Sport <see cref="ShootingTarget"/> object.
    /// </summary>
    public static ShootingTarget SportShootingTarget => GetBaseObject<ShootingTarget>("sportTargetPrefab");

    /// <summary>
    /// Gets the D-Class <see cref="ShootingTarget"/> object.
    /// </summary>
    public static ShootingTarget DClassShootingTarget => GetBaseObject<ShootingTarget>("dboyTargetPrefab");

    /// <summary>
    /// Gets the Binary <see cref="ShootingTarget"/> object.
    /// </summary>
    public static ShootingTarget BinaryShootingTarget => GetBaseObject<ShootingTarget>("binaryTargetPrefab");
}
