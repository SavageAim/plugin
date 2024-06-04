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
    private BISList[] bisLists = [];
    private List<Gear> gearList = new();
    private SavageAim plugin;
    private SACharacter? saChar = null;
    private bool loaded = false;

    private Vector4 GREEN = new Vector4(82, 149, 128, 1);
    private Vector4 RED = new Vector4(237, 18, 62, 1);

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
        // Find the Character from SA that matches the inGame data
        var data = InGameCharacterData.Instance();
        this.saChar = charList.Find(sa => sa.Name == data.name && sa.World.Split(" ")[0] == data.world);

        if (this.saChar == null)
        {
            return;
        }

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
        var data = InGameCharacterData.Instance();
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
            var header = $"{bis.DisplayName} ({bis.Job.ID})";
            if (ImGui.BeginTabItem(header))
            {
                this.DrawBisListDetails(bis);
                ImGui.Button("Reload Data");
                if (data.job.ToString() == bis.Job.ID)
                {
                    ImGui.SameLine();
                    ImGui.Button("Save Current Gear");
                }
                ImGui.EndTabItem();
            }
        }
        ImGui.EndTabBar();
    }

    private void DrawBisListDetails(BISList bis)
    {
        ImGui.BeginTable($"bisTable-{bis.ID}", 3, ImGuiTableFlags.BordersH | ImGuiTableFlags.BordersV);
        // Headers Row
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Slot");
        ImGui.TableNextColumn();
        ImGui.TableHeader("BIS");
        ImGui.TableNextColumn();
        ImGui.TableHeader("Current");

        // Mainhand
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Mainhand");
        ImGui.TableNextColumn();
        ImGui.Text(bis.BisMainhand.Name);
        ImGui.TableNextColumn();
        ImGui.Text(bis.CurrentMainhand.Name);

        // Offhand
        if (bis.Job.ID == "PLD")
        {
            ImGui.TableNextRow();
            ImGui.TableNextColumn();
            ImGui.TableHeader("Offhand");
            ImGui.TableNextColumn();
            ImGui.Text(bis.BisOffhand.Name);
            ImGui.TableNextColumn();
            ImGui.Text(bis.CurrentOffhand.Name);
        }

        // Head
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Head");
        ImGui.TableNextColumn();
        ImGui.Text(bis.BisHead.Name);
        ImGui.TableNextColumn();
        ImGui.Text(bis.CurrentHead.Name);

        // Body
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Body");
        ImGui.TableNextColumn();
        ImGui.Text(bis.BisBody.Name);
        ImGui.TableNextColumn();
        ImGui.Text(bis.CurrentBody.Name);

        // Hands
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Hands");
        ImGui.TableNextColumn();
        ImGui.Text(bis.BisHands.Name);
        ImGui.TableNextColumn();
        ImGui.Text(bis.CurrentHands.Name);

        // Legs
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Legs");
        ImGui.TableNextColumn();
        ImGui.Text(bis.BisLegs.Name);
        ImGui.TableNextColumn();
        ImGui.Text(bis.CurrentLegs.Name);

        // Feet
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Feet");
        ImGui.TableNextColumn();
        ImGui.Text(bis.BisFeet.Name);
        ImGui.TableNextColumn();
        ImGui.Text(bis.CurrentFeet.Name);

        // Earrings
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Earrings");
        ImGui.TableNextColumn();
        ImGui.Text(bis.BisEarrings.Name);
        ImGui.TableNextColumn();
        ImGui.Text(bis.CurrentEarrings.Name);

        // Necklace
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Necklace");
        ImGui.TableNextColumn();
        ImGui.Text(bis.BisNecklace.Name);
        ImGui.TableNextColumn();
        ImGui.Text(bis.CurrentNecklace.Name);

        // Bracelet
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Bracelet");
        ImGui.TableNextColumn();
        ImGui.Text(bis.BisBracelet.Name);
        ImGui.TableNextColumn();
        ImGui.Text(bis.CurrentBracelet.Name);

        // Right Ring
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Right Ring");
        ImGui.TableNextColumn();
        ImGui.Text(bis.BisRightRing.Name);
        ImGui.TableNextColumn();
        ImGui.Text(bis.CurrentRightRing.Name);

        // Left Ring
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Left Ring");
        ImGui.TableNextColumn();
        ImGui.Text(bis.BisLeftRing.Name);
        ImGui.TableNextColumn();
        ImGui.Text(bis.CurrentLeftRing.Name);

        ImGui.EndTable();
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
        if (!this.loaded)
        {
            this.LoadData();
            this.loaded = true;
        }

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
