using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Dalamud.Interface.Windowing;
using Dalamud.Logging;
using ImGuiNET;
using SavageAimPlugin;
using SavageAimPlugin.Data;

namespace SavageAim.Windows;

public class SavageAimWindow : Window, IDisposable
{
    private List<SACharacter> charList = new();
    private List<Gear> gearList = new();
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
        this.LoadData();
    }

    public void Dispose() { }

    private void LoadData()
    {
        var gearTask = SavageAimClient.GetGear(this.plugin.Configuration.apiKey);
        gearTask.Wait();
        this.gearList = gearTask.Result;

        var charTask = SavageAimClient.GetCharacters(this.plugin.Configuration.apiKey);
        charTask.Wait();
        this.charList = charTask.Result;
    }

    private void DrawCurrentGearTab()
    {
        var data = InGameCharacterData.Instance();
        data.Draw();
    }

    private void DrawBisListsTab()
    {
        var inGameChar = InGameCharacterData.Instance();
        // If the current Character isn't in the list, display an error message
        // Take only the first word of the world from SA since SA world contains DC as well
        var saChar = this.charList.Find(sa => sa.Name == inGameChar.name && sa.World.Split(" ")[0] == inGameChar.world);
        if (saChar == null)
        {
            ImGui.Text("Your Current Character was not found in your Savage Aim Account.");
            ImGui.Text("Visit https://savageaim.com/characters/new/ to add a new one!");
            return;
        }

        // Draw a List Box down the side, and selecting one displays the stuff on the right
        // TODO - Change from tabs to above idea
        ImGui.BeginTabBar("bisListTabs");
        foreach (var bis in saChar.BISSummaries)
        {
            if (ImGui.BeginTabItem(bis.Name))
            {
                ImGui.Text($"{bis.ID} - {bis.Name}");
                ImGui.EndTabItem();
            }
        }
        ImGui.EndTabBar();
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
