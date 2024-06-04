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

const GREEN = new Vector4(82, 149, 128, 1);
const RED = new Vector4(237,18, 62, 1);


public class SavageAimWindow : Window, IDisposable
{
    private List<BISList> bisLists = new();
    private List<Gear> gearList = new();
    private SavageAim plugin;
    private SACharacter? saChar = null;

    // API Key Test Stuff
    private bool apiKeyTested = false;
    private bool apiKeyValid = false;

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
        var charList = charTask.Result;
        var data = InGameCharacterData.Instance();
        // Find the Character from SA that matches the inGame data
        this.saChar = this.charList.Find(sa => sa.Name == inGameChar.name && sa.World.Split(" ")[0] == inGameChar.world);

        // Load the BIS Lists for the Character
        var bisTask = SavageAimClient.GetBisLists(this.plugin.Configuration.apiKey, this.saChar.ID);
        bisTask.Wait();
        this.bisLists = bisTask.Result;
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
        if (this.saChar == null)
        {
            ImGui.Text("Your Current Character was not found in your Savage Aim Account.");
            ImGui.Text("Visit https://savageaim.com/characters/new/ to add a new one!");
            return;
        }

        // Draw a List Box down the side, and selecting one displays the stuff on the right
        // TODO - Change from tabs to above idea
        ImGui.BeginTabBar("bisListTabs", ImGuiTabBarFlags.NoCloseWithMiddleMouseButton | ImGuiTabBarFlags.FittingPolicyScroll);
        foreach (var bis in this.bisLists)
        {
            var header = $"{bis.Name} ({bis.Job.Name})"
            if (ImGui.BeginTabItem(header))
            {
                bis.Draw();
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
            // Test the API Key before saving it (move the Save back to a button).
            this.plugin.Configuration.apiKey = apiKey;
        }
        ImGui.Text("Visit https://savageaim.com/settings to get your API key.");
        if (ImGui.Button("Test and Save"))
        {
            this.apiKeyTested = false;
            var testTask = SavageAimClient.TestApiKey(this.plugin.Configuration.apiKey);
            testTask.Wait();
            this.apiKeyTested = true;
            this.apiKeyValid = testTask.Result;
            if (this.apiKeyValid)
            {
                this.plugin.Configuration.Save();
            }
        }

        // Show coloured Text alongside the button
        if (this.apiKeyTested)
        {
            ImGui.SameLine();
            if (this.apiKeyValid)
            {
                ImGui.TextColored(GREEN, "Valid Key Saved!");
            }
            else
            {
                ImGui.TextColored(RED, "API Key Invalid. Please double check your API Key and try again!");
            }
        }
    }

    public override void Draw()
    {
        ImGui.BeginTabBar("saMainMenu", ImGuiTabBarFlags.NoCloseWithMiddleMouseButton);
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
    }
}
