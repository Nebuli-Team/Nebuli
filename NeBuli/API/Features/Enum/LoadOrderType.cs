﻿namespace Nebuli.API.Features.Enum;

/// <summary>
/// Represents the load order types for plugins.
/// </summary>
public enum LoadOrderType
{
    /// <summary>
    /// Specifies that the plugin should load before all others.
    /// </summary>
    PreLoad,

    /// <summary>
    /// Specifies that the plugin should load early in the load order.
    /// </summary>
    EarlyLoad,

    /// <summary>
    /// Specifies that the plugin should load at the default load order.
    /// </summary>
    NormalLoad,

    /// <summary>
    /// Specifies that the plugin should load late in the load order.
    /// </summary>
    LateLoad,

    /// <summary>
    /// Specifies that the plugin should load after all others.
    /// </summary>
    PostLoad
}