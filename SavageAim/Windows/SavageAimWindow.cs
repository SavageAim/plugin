using System;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ECommons.Configuration;
using ECommons.GameHelpers;
using ImGuiNET;
using SavageAimPlugin.Data;

namespace SavageAim.Windows;

public class SavageAimWindow : Window, IDisposable
{
    private SavageAim plugin;

    // We give this window a hidden ID using ##
    // So that the user will see "My Amazing Window" as window title,
    // but for ImGui the ID is "My Amazing Window##With a hidden ID"
    public SavageAimWindow(SavageAim plugin)
        : base("Savage Aim###MainWindow")
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(375, 330),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };

        this.plugin = plugin;
    }

    public void Dispose() { }

    private void DrawCurrentGearTab()
    {
        var data = InGameCharacterData.Instance();
        data.Draw();
    }

    private void DrawBisListsTab()
    {
        ImGui.Text("BIS LISTS :D");
    }

    private void DrawSettingsTab()
    {
        String apiKey = this.plugin.Configuration.apiKey;
        if (ImGui.InputText("API Key", ref apiKey, 128))
        {
            this.plugin.Configuration.apiKey = apiKey;
            this.plugin.Configuration.Save();
        }
        ImGui.Text("Visit https://savageaim.com/settings to get your API key.");
    }

    public override void Draw()
    {
        ImGui.BeginTabBar("saMainMenu");
        if (ImGui.BeginTabItem("Current Gear"))
        {
            this.DrawCurrentGearTab();
            ImGui.EndTabItem();
        }
        

        if (ImGui.BeginTabItem("BIS Lists"))
        {
            this.DrawBisListsTab();
            ImGui.EndTabItem();
        }

        if (ImGui.BeginTabItem("Settings"))
        {
            this.DrawSettingsTab();
            ImGui.EndTabItem();
        }
        ImGui.EndTabBar();

        /*
        if (ImGui.Button("Show Settings"))
        {
            this.plugin.ToggleConfigUI();
        }

        ImGui.Spacing();
        */
    }
}
