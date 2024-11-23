using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.Json;
using Dalamud.Interface.Windowing;
using Dalamud.Logging;
using Dalamud.Utility;
using ImGuiNET;
using SavageAimPlugin;
using SavageAimPlugin.Data;
using static System.Net.WebRequestMethods;

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

    private void CheckBisTabRequirements()
    {
        // Ensure our API Key is working before we use the System
        if (Service.APIKeyManager is { IsDataLoading: false, IsDataReady: false, HasFailed: false })
        {
            Service.APIKeyManager.ValidateAPIKey();
        }
        if (Service.APIKeyManager is { IsDataReady: false, IsDataLoading: true })
        {
            ImGui.Text("Validating API Key...");
            return;
        }

        if (Service.APIKeyManager is { IsDataLoading: false, HasFailed: true })
        {
            ImGui.Text("Failed to validate API Key");
            if (ImGui.Button("Try Again?"))
            {
                Service.APIKeyManager.HasFailed = false;
            }
            return;
        }

        if (Service.APIKeyManager is { IsDataReady: true, APIKeyIsValid: false })
        {
            ImGui.Text("Please provide a valid API Key on the Settings Tab.");
            return;
        }

        // Set the Character Data to Load if it hasn't been already.
        if (Service.APIKeyManager is { IsDataReady: true, APIKeyIsValid: true })
        {
            if (Service.CharacterDataManager is { IsDataLoading: false, IsDataReady: false, HasFailed: false })
            {
                Service.CharacterDataManager.FetchData(Service.Configuration.apiKey);
            }
        }
        if (Service.CharacterDataManager is { IsDataReady: false, IsDataLoading: true })
        {
            ImGui.Text("Fetching Characters from Savage Aim...");
        }
        else if (Service.CharacterDataManager is { IsDataLoading: false, HasFailed: true })
        {
            ImGui.Text("Failed to fetch SA Character Data");
            if (ImGui.Button("Try Again?"))
            {
                Service.CharacterDataManager.HasFailed = false;
            }
        }
    }

    private void DrawBisListsTab()
    {
        // If the current Character isn't in the list, display an error message
        // Take only the first word of the world from SA since SA world contains DC as well
        var currentIGChar = Service.CharacterDataManager.InGameCharacter;
        if (currentIGChar == null) return;
        var currentSAChar = Service.CharacterDataManager.GetCurrentCharacterInSA();
        if (currentSAChar == null)
        {
            ImGui.Text("Your Current Character was not found in your Savage Aim Account.");
            if (ImGui.Button("Add New Character"))
            {
                Util.OpenLink("https://savageaim.com/characters/new/");
            }
            return;
        }

        if (Service.BISListDataManager is { IsDataLoading: false, IsDataReady: false, HasFailed: false })
        {
            Service.BISListDataManager.FetchData(Service.Configuration.apiKey, currentSAChar.ID);
        }

        if (Service.BISListDataManager is { IsDataReady: false, IsDataLoading: true })
        {
            ImGui.Text("Fetching BIS List Data...");
            return;
        }

        if (Service.BISListDataManager is { IsDataLoading: false, HasFailed: true })
        {
            ImGui.Text("Failed to fetch BIS List Data");
            if (ImGui.Button("Try Again?"))
            {
                Service.BISListDataManager.HasFailed = false;
            }
            return;
        }

        // Draw a List Box down the side, and selecting one displays the stuff on the right
        // TODO - Change from tabs to above idea
        ImGui.BeginTabBar("bisListTabs", ImGuiTabBarFlags.NoCloseWithMiddleMouseButton | ImGuiTabBarFlags.FittingPolicyScroll);
        foreach (var bis in Service.BISListDataManager.Data)
        {
            var header = $"{bis.DisplayName} ({bis.Job.ID})";
            if (ImGui.BeginTabItem(header))
            {
                this.DrawBisListDetails(bis);
                if (ImGui.Button("Reload Data"))
                {
                    Service.BISListDataManager.Reset();
                }
                if (currentIGChar.Job.ToString() == bis.Job.ID)
                {
                    ImGui.SameLine();
                    if (ImGui.Button("Save Current Gear"))
                    {
                        Service.GearImportManager.Reset();
                        this.plugin.UpdateWindow.UpdateBis(bis);
                    }
                    if (Service.GearImportManager is { HasSaved: true, IsSaving: false, HasFailedSaving: false })
                    {
                        if (this.plugin.UpdateWindow.toUpdate != null && this.plugin.UpdateWindow.toUpdate.ID == bis.ID)
                        {
                            ImGui.SameLine();
                            ImGui.Text("Saved!");
                        }
                    }
                }
                if (ImGui.Button("Open on savageaim.com"))
                {
                    Util.OpenLink($"https://savageaim.com/characters/{currentSAChar.ID}/bis_list/{bis.ID}");
                }
                if (bis.URL != null)
                {
                    ImGui.SameLine();
                    var domain = bis.URL.Replace("http://", "").Replace("https://", "").Split("/")[0];
                    if (ImGui.Button($"Open on {domain}")) {
                        Util.OpenLink(bis.URL);
                    }
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
        String apiKey = Service.Configuration.apiKey;
        if (ImGui.InputText("API Key", ref apiKey, 128))
        {
            // Test the API Key before saving it.
            Service.Configuration.apiKey = apiKey;
            Service.APIKeyManager.NeedsRevalidation = true;
        }
        if (Service.APIKeyManager is { NeedsRevalidation: true })
        {
            if (ImGui.Button("Test and Save"))
            {
                Service.APIKeyManager.ValidateAPIKey();
            }
        }

        // Show coloured Text alongside the button
        if (Service.APIKeyManager.IsDataReady)
        {
            if (Service.APIKeyManager.APIKeyIsValid)
            {
                ImGui.Text("Valid Key Saved!");
            }
            else if (Service.Configuration.apiKey != "" && Service.APIKeyManager is { NeedsRevalidation: false })
            {
                ImGui.Text("API Key Invalid. Please double check your API Key and try again!");
            }
        }
        else if (Service.APIKeyManager.HasFailed)
        {
            ImGui.Text("An error occurred trying to validated your API Key, please try again.");
        }

        if (ImGui.Button("Open Savage Aim Settings"))
        {
            Util.OpenLink("https://savageaim.com/settings/");

        }
    }

    public override void Draw()
    {
        if (Service.CharacterDataManager.InGameCharacter == null)
        {
            ImGui.Text("Please log in as a Character to use the Plugin!");
            return;
        }

        ImGui.BeginTabBar("saMainMenu", ImGuiTabBarFlags.NoCloseWithMiddleMouseButton);
        if (ImGui.BeginTabItem("BIS Lists"))
        {
            this.CheckBisTabRequirements();
            if (Service.CharacterDataManager is { IsDataReady: true, IsDataLoading: false, HasFailed: false }) this.DrawBisListsTab();
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
