﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using HarmonyLib;
using Nebuli.API.Features;
using PluginAPI.Core.Attributes;

namespace Nebuli;

#pragma warning disable CS1591
public class Loader
{
    private Harmony _harmony;

    [PluginConfig]
    public static LoaderConfiguration Configuration;
    
    [PluginEntryPoint("Nebuli Loader", "0, 0, 0", "Nebuli Plugin Framework", "Nebuli Team")]
    public void Load()
    {
        Log.Info($"Nebuli Version {NebuliInfo.NebuliVersion} loading...", consoleColor: ConsoleColor.Red);
        Log.Debug("Loading file paths...");
        Paths.LoadPaths();
        Log.Warning($"Dependency path is {Paths.DependenciesDirectory}");
        Log.Info("Loading dependencies...");
        LoadDependencies(Paths.DependenciesDirectory.GetFiles("*.dll"));
        Log.Warning($"Plugin path is {Paths.PluginsDirectory}");
        Log.Info("Loading plugins...");
        LoadPlugins(Paths.PluginsDirectory.GetFiles("*.dll"));
        _harmony = new("nebuli.patching.core");
        try
        {
            _harmony.PatchAll();
        }
        catch (Exception e)
        {
            Log.Error($"A error has occured when patching! Full error : \n{e}");
        }
    }

    [PluginUnload]
    public void UnLoad()
    {
        _harmony.UnpatchAll(_harmony.Id);
        _harmony = null;
    }
    
    private void LoadDependencies(IEnumerable<FileInfo> files)
    {
        foreach (FileInfo file in files)
        {
            try
            {
                Assembly assembly = Assembly.Load(File.ReadAllBytes(file.FullName));
                Log.Info($"Dependency {assembly.GetName().Name} loaded!");
            }
            catch (Exception e)
            {
                Log.Error($"Failed to load dependency {file.Name}. Full error : \n{e}");
            }
        }
        Log.Info("Dependencies loaded!");
    }

    private void LoadPlugins(IEnumerable<FileInfo> files)
    {
        foreach(FileInfo file in files)
        {
            try
            {

            }
            catch(Exception e)
            {
                Log.Error($"Failed to load plugin {file.Name}. Full error : \n{e}");
            }
        }
        Log.Info("Plugins loaded!");
    }
}