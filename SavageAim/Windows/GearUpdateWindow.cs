using System;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using SavageAimPlugin;
using SavageAimPlugin.Data;

namespace SavageAim.Windows;

public class GearUpdateWindow : Window, IDisposable
{
    private SavageAim plugin;
    public BISList? toUpdate;

    // We give this window a hidden ID using ##
    // So that the user will see "My Amazing Window" as window title,
    // but for ImGui the ID is "My Amazing Window##With a hidden ID"
    public GearUpdateWindow(SavageAim plugin)
        : base("Savage Aim - Update Gear###Update Window")
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(375, 330),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };

        this.plugin = plugin;
    }

    public void Dispose() { }

    public void UpdateBis(BISList bis)
    {
        this.toUpdate = bis;
        this.WindowName = $"Savage Aim - Update {bis.DisplayName} ({bis.Job.ID})###Update Window";
        if (!this.IsOpen) this.Toggle();
    }

    public void DrawUpdateTable()
    {
        ImGui.BeginTable($"gearUpdateTable", 4, ImGuiTableFlags.BordersH | ImGuiTableFlags.BordersV);
        // Headers Row
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Slot");
        ImGui.TableNextColumn();
        ImGui.TableHeader("Current");
        ImGui.TableNextColumn();
        ImGui.TableHeader("");
        ImGui.TableNextColumn();
        ImGui.TableHeader("Updated");


        // Mainhand
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Mainhand");
        ImGui.TableNextColumn();
        ImGui.Text(this.toUpdate!.CurrentMainhand.Name);
        ImGui.TableNextColumn();
        var newName = Service.GearImportManager.Data!.Mainhand?.Name ?? this.toUpdate.CurrentMainhand.Name;
        if (this.toUpdate.CurrentMainhand.Name != newName) ImGui.Text("=>");
        ImGui.TableNextColumn();
        ImGui.Text(newName);

        // Offhand
        if (toUpdate.Job.ID == "PLD")
        {
            ImGui.TableNextRow();
            ImGui.TableNextColumn();
            ImGui.TableHeader("Offhand");
            ImGui.TableNextColumn();
            ImGui.Text(toUpdate.CurrentOffhand.Name);
            ImGui.TableNextColumn();
            newName = Service.GearImportManager.Data!.Offhand?.Name ?? this.toUpdate.CurrentOffhand.Name;
            if (this.toUpdate.CurrentOffhand.Name != newName) ImGui.Text("=>");
            ImGui.TableNextColumn();
            ImGui.Text(newName);
        }

        // Head
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Head");
        ImGui.TableNextColumn();
        ImGui.Text(toUpdate.CurrentHead.Name);
        ImGui.TableNextColumn();
        newName = Service.GearImportManager.Data!.Head?.Name ?? this.toUpdate.CurrentHead.Name;
        if (this.toUpdate.CurrentHead.Name != newName) ImGui.Text("=>");
        ImGui.TableNextColumn();
        ImGui.Text(newName);

        // Body
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Body");
        ImGui.TableNextColumn();
        ImGui.Text(toUpdate.CurrentBody.Name);
        ImGui.TableNextColumn();
        newName = Service.GearImportManager.Data!.Body?.Name ?? this.toUpdate.CurrentBody.Name;
        if (this.toUpdate.CurrentBody.Name != newName) ImGui.Text("=>");
        ImGui.TableNextColumn();
        ImGui.Text(newName);

        // Hands
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Hands");
        ImGui.TableNextColumn();
        ImGui.Text(toUpdate.CurrentHands.Name);
        ImGui.TableNextColumn();
        newName = Service.GearImportManager.Data!.Hands?.Name ?? this.toUpdate.CurrentHands.Name;
        if (this.toUpdate.CurrentHands.Name != newName) ImGui.Text("=>");
        ImGui.TableNextColumn();
        ImGui.Text(newName);

        // Legs
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Legs");
        ImGui.TableNextColumn();
        ImGui.Text(toUpdate.CurrentLegs.Name);
        ImGui.TableNextColumn();
        newName = Service.GearImportManager.Data!.Legs?.Name ?? this.toUpdate.CurrentLegs.Name;
        if (this.toUpdate.CurrentLegs.Name != newName) ImGui.Text("=>");
        ImGui.TableNextColumn();
        ImGui.Text(newName);

        // Feet
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Feet");
        ImGui.TableNextColumn();
        ImGui.Text(toUpdate.CurrentFeet.Name);
        ImGui.TableNextColumn();
        newName = Service.GearImportManager.Data!.Feet?.Name ?? this.toUpdate.CurrentFeet.Name;
        if (this.toUpdate.CurrentFeet.Name != newName) ImGui.Text("=>");
        ImGui.TableNextColumn();
        ImGui.Text(newName);

        // Earrings
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Earrings");
        ImGui.TableNextColumn();
        ImGui.Text(toUpdate.CurrentEarrings.Name);
        ImGui.TableNextColumn();
        newName = Service.GearImportManager.Data!.Earrings?.Name ?? this.toUpdate.CurrentEarrings.Name;
        if (this.toUpdate.CurrentEarrings.Name != newName) ImGui.Text("=>");
        ImGui.TableNextColumn();
        ImGui.Text(newName);

        // Necklace
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Necklace");
        ImGui.TableNextColumn();
        ImGui.Text(toUpdate.CurrentNecklace.Name);
        ImGui.TableNextColumn();
        newName = Service.GearImportManager.Data!.Necklace?.Name ?? this.toUpdate.CurrentNecklace.Name;
        if (this.toUpdate.CurrentNecklace.Name != newName) ImGui.Text("=>");
        ImGui.TableNextColumn();
        ImGui.Text(newName);

        // Bracelet
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Bracelet");
        ImGui.TableNextColumn();
        ImGui.Text(toUpdate.CurrentBracelet.Name);
        ImGui.TableNextColumn();
        newName = Service.GearImportManager.Data!.Bracelet?.Name ?? this.toUpdate.CurrentBracelet.Name;
        if (this.toUpdate.CurrentBracelet.Name != newName) ImGui.Text("=>");
        ImGui.TableNextColumn();
        ImGui.Text(newName);

        // Right Ring
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Right Ring");
        ImGui.TableNextColumn();
        ImGui.Text(toUpdate.CurrentRightRing.Name);
        ImGui.TableNextColumn();
        newName = Service.GearImportManager.Data!.RightRing?.Name ?? this.toUpdate.CurrentRightRing.Name;
        if (this.toUpdate.CurrentRightRing.Name != newName) ImGui.Text("=>");
        ImGui.TableNextColumn();
        ImGui.Text(newName);

        // Left Ring
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Left Ring");
        ImGui.TableNextColumn();
        ImGui.Text(toUpdate.CurrentLeftRing.Name);
        ImGui.TableNextColumn();
        newName = Service.GearImportManager.Data!.LeftRing?.Name ?? this.toUpdate.CurrentLeftRing.Name;
        if (this.toUpdate.CurrentLeftRing.Name != newName) ImGui.Text("=>");
        ImGui.TableNextColumn();
        ImGui.Text(newName);

        ImGui.EndTable();
    }

    public override void Draw()
    {
        if (toUpdate == null)
        {
            ImGui.Text("Not sure how you opened this without having a BIS set...");
            return;
        }
        // Hit the endpoint to turn current equipped gear names into SA Gear data
        if (Service.GearImportManager is { IsDataReady: false, IsDataLoading: false, HasFailed: false })
        {
            Service.GearImportManager.FetchData(Service.Configuration.apiKey);
        }
        if (Service.GearImportManager is { IsDataReady: false, IsDataLoading: true })
        {
            ImGui.Text("Loading data...");
            return;
        }
        if (Service.GearImportManager is { IsDataLoading: false, HasFailed: true })
        {
            ImGui.Text("Failed to fetch SA Data");
            if (ImGui.Button("Try Again?"))
            {
                Service.GearImportManager.HasFailed = false;
            }
            return;
        }

        if (Service.GearImportManager.Data == null)
        {
            ImGui.Text("Failed to import data. Close this window and try again!");
            return;
        }
        //Service.PluginLog.Info(this.toUpdate.ToString());
        //Service.PluginLog.Info(Service.GearImportManager.Data.ToString());
        this.DrawUpdateTable();
        ImGui.Spacing();
        ImGui.Text("If you are happy with these changes, click the Save button below, and the BIS List will be updated on the site!");
        ImGui.Text("If not, close this window, make any changes you need and click the Save Current Data button when ready!");
        ImGui.Text("Please note, gear not at level cap is not tracked in the system!");
        if (ImGui.Button("Save These Changes"))
        {
            Service.GearImportManager.SaveBis(Service.Configuration.apiKey, this.toUpdate, Service.GearImportManager.Data);
        }
        ImGui.SameLine();
        if (Service.GearImportManager is { IsSaving: true })
        {
            ImGui.Text("Saving...");
            return;
        }
        if (Service.GearImportManager is { HasFailedSaving: true })
        {
            ImGui.Text("Error occurred while saving!");
            return;
        }
        if (Service.GearImportManager is { HasSaved: true, IsSaving: false, HasFailedSaving: false })
        {
            Service.BISListDataManager.Reset();
            this.Toggle();
        }
    }
}
