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
    private BISList? toUpdate;

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
        this.WindowName = $"Savage Aim - Update {bis.DisplayName}###Update Window";
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
        ImGui.Text(this.toUpdate.CurrentMainhand.Name);
        ImGui.TableNextColumn();
        if (this.toUpdate.CurrentMainhand.Name != Service.GearImportManager.Data.Mainhand.Name) ImGui.Text("->");
        ImGui.TableNextColumn();
        ImGui.Text(Service.GearImportManager.Data.Mainhand.Name);

        // Offhand
        if (toUpdate.Job.ID == "PLD")
        {
            ImGui.TableNextRow();
            ImGui.TableNextColumn();
            ImGui.TableHeader("Offhand");
            ImGui.TableNextColumn();
            ImGui.Text(toUpdate.CurrentOffhand.Name);
            ImGui.TableNextColumn();
            if (this.toUpdate.CurrentOffhand.Name != Service.GearImportManager.Data.Offhand.Name) ImGui.Text("->");
            ImGui.TableNextColumn();
            ImGui.Text(Service.GearImportManager.Data.Offhand.Name);
        }

        // Head
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Head");
        ImGui.TableNextColumn();
        ImGui.Text(toUpdate.CurrentHead.Name);
        ImGui.TableNextColumn();
        if (this.toUpdate.CurrentHead.Name != Service.GearImportManager.Data.Head.Name) ImGui.Text("->");
        ImGui.TableNextColumn();
        ImGui.Text(Service.GearImportManager.Data.Head.Name);

        // Body
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Body");
        ImGui.TableNextColumn();
        ImGui.Text(toUpdate.CurrentBody.Name);
        ImGui.TableNextColumn();
        if (this.toUpdate.CurrentBody.Name != Service.GearImportManager.Data.Body.Name) ImGui.Text("->");
        ImGui.TableNextColumn();
        ImGui.Text(Service.GearImportManager.Data.Body.Name);

        // Hands
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Hands");
        ImGui.TableNextColumn();
        ImGui.Text(toUpdate.CurrentHands.Name);
        ImGui.TableNextColumn();
        if (this.toUpdate.CurrentHands.Name != Service.GearImportManager.Data.Hands.Name) ImGui.Text("->");
        ImGui.TableNextColumn();
        ImGui.Text(Service.GearImportManager.Data.Hands.Name);

        // Legs
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Legs");
        ImGui.TableNextColumn();
        ImGui.Text(toUpdate.CurrentLegs.Name);
        ImGui.TableNextColumn();
        if (this.toUpdate.CurrentLegs.Name != Service.GearImportManager.Data.Legs.Name) ImGui.Text("->");
        ImGui.TableNextColumn();
        ImGui.Text(Service.GearImportManager.Data.Legs.Name);

        // Feet
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Feet");
        ImGui.TableNextColumn();
        ImGui.Text(toUpdate.CurrentFeet.Name);
        ImGui.TableNextColumn();
        if (this.toUpdate.CurrentFeet.Name != Service.GearImportManager.Data.Feet.Name) ImGui.Text("->");
        ImGui.TableNextColumn();
        ImGui.Text(Service.GearImportManager.Data.Feet.Name);

        // Earrings
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Earrings");
        ImGui.TableNextColumn();
        ImGui.Text(toUpdate.CurrentEarrings.Name);
        ImGui.TableNextColumn();
        if (this.toUpdate.CurrentEarrings.Name != Service.GearImportManager.Data.Earrings.Name) ImGui.Text("->");
        ImGui.TableNextColumn();
        ImGui.Text(Service.GearImportManager.Data.Earrings.Name);

        // Necklace
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Necklace");
        ImGui.TableNextColumn();
        ImGui.Text(toUpdate.CurrentNecklace.Name);
        ImGui.TableNextColumn();
        if (this.toUpdate.CurrentNecklace.Name != Service.GearImportManager.Data.Necklace.Name) ImGui.Text("->");
        ImGui.TableNextColumn();
        ImGui.Text(Service.GearImportManager.Data.Necklace.Name);

        // Bracelet
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Bracelet");
        ImGui.TableNextColumn();
        ImGui.Text(toUpdate.CurrentBracelet.Name);
        ImGui.TableNextColumn();
        if (this.toUpdate.CurrentBracelet.Name != Service.GearImportManager.Data.Bracelet.Name) ImGui.Text("->");
        ImGui.TableNextColumn();
        ImGui.Text(Service.GearImportManager.Data.Bracelet.Name);

        // Right Ring
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Right Ring");
        ImGui.TableNextColumn();
        ImGui.Text(toUpdate.CurrentRightRing.Name);
        ImGui.TableNextColumn();
        if (this.toUpdate.CurrentRightRing.Name != Service.GearImportManager.Data.RightRing.Name) ImGui.Text("->");
        ImGui.TableNextColumn();
        ImGui.Text(Service.GearImportManager.Data.RightRing.Name);

        // Left Ring
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader("Left Ring");
        ImGui.TableNextColumn();
        ImGui.Text(toUpdate.CurrentLeftRing.Name);
        ImGui.TableNextColumn();
        if (this.toUpdate.CurrentLeftRing.Name != Service.GearImportManager.Data.LeftRing.Name) ImGui.Text("->");
        ImGui.TableNextColumn();
        ImGui.Text(Service.GearImportManager.Data.LeftRing.Name);

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
        this.DrawUpdateTable();
    }
}
