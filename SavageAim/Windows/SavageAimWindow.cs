using System;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ECommons.GameHelpers;
using ImGuiNET;

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

    public unsafe uint NameId()
    {
        return Player.Character->NameID;
    }

    public void Dispose() { }

    public override void Draw()
    {
        var data = CharacterData.Instance();
        ImGui.Text(data.ToString());
        ImGui.Text($"ID: {this.NameId()}");

        /*
        if (ImGui.Button("Show Settings"))
        {
            this.plugin.ToggleConfigUI();
        }

        ImGui.Spacing();
        */
    }
}
