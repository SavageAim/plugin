using Dalamud.Configuration;
using System;

namespace SavageAim;

[Serializable]
public class Configuration : IPluginConfiguration
{
    public int Version { get; set; } = 0;

    public string apiKey { get; set; } = "";

    public void Save()
    {
        SavageAim.PluginInterface!.SavePluginConfig(this);
    }
}
