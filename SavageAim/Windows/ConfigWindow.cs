using System;
using System.Numerics;
using Dalamud.Interface.Windowing;
using Dalamud.Logging;
using ImGuiNET;

namespace SavageAim.Windows;

public class ConfigWindow : Window, IDisposable
{
    private Configuration config;

    // We give this window a constant ID using ###
    // This allows for labels being dynamic, like "{FPS Counter}fps###XYZ counter window",
    // and the window ID will always be "###XYZ counter window" for ImGui
    public ConfigWindow(SavageAim plugin) : base("Savage Aim - Settings")
    {
        Flags = ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar |
                ImGuiWindowFlags.NoScrollWithMouse;

        config = plugin.Configuration;
    }

    public void Dispose() { }

    public override void Draw()
    {
        String apiKey = config.apiKey;
        if (ImGui.InputText("API Key", ref apiKey, 128))
        {
            PluginLog.Information($"Saving API Key: {apiKey}");
            config.apiKey = apiKey;
            this.config.Save();
        }
        ImGui.Text("Visit https://savageaim.com/settings to get your API key.");
    }
}
