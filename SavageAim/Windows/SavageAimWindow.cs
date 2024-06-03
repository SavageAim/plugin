using System;
using System.Collections.Generic;
using System.Numerics;
using Dalamud.Interface.Windowing;
using Dalamud.Logging;
using ImGuiNET;
using SavageAimPlugin;
using SavageAimPlugin.Data;

namespace SavageAim.Windows;

public class SavageAimWindow : Window, IDisposable
{
    private SavageAim plugin;
    private List<Gear> gearList = new();

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
        this.GetGearData();
    }

    public void Dispose() { }

    private void GetGearData()
    {
        PluginLog.Information("Fetching Gear Data");
        var gearTask = SavageAimClient.GetGear(this.plugin.Configuration.apiKey);
        gearTask.Wait();
        this.gearList = gearTask.Result;
    }

    private void DrawCurrentGearTab()
    {
        var data = InGameCharacterData.Instance();
        data.Draw();
    }

    private void DrawBisListsTab()
    {
        foreach (var gear in this.gearList)
        {
            ImGui.Text($"{gear.ID}: {gear.Name} ({gear.ItemLevel})");
        }
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
