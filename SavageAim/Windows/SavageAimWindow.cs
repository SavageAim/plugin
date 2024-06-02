using System;
using System.Collections;
using System.Numerics;
using System.Runtime.InteropServices;
using Dalamud.Game.Text.SeStringHandling.Payloads;
using Dalamud.Interface.Internal;
using Dalamud.Interface.Utility;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using ImGuiNET;
using Lumina;

namespace SavageAim.Windows;

public class SavageAimWindow : Window, IDisposable
{
    private Plugin Plugin;

    // We give this window a hidden ID using ##
    // So that the user will see "My Amazing Window" as window title,
    // but for ImGui the ID is "My Amazing Window##With a hidden ID"
    public SavageAimWindow(Plugin plugin)
        : base("Savage Aim###MainWindow")
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(375, 330),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };

        Plugin = plugin;
    }

    public unsafe String CharacterName()
    {
        PlayerState* character = PlayerState.Instance();
        byte[] name = new byte[64];
        Marshal.Copy((IntPtr) character->CharacterName, name, 0, 64);
        return System.Text.Encoding.UTF8.GetString(name);
    }

    public unsafe uint LodestoneId()
    {
        PlayerState* character = PlayerState.Instance();
        return character->ObjectId;
    }

    public unsafe String CurrentChestpiece()
    {
        InventoryManager* manager = InventoryManager.Instance();
        InventoryItem* chest = manager->GetInventorySlot(InventoryType.EquippedItems, 3);
        ItemPayload payload = ItemPayload.FromRaw(chest->GetItemId());
        String? name = payload.DisplayName;
        if (name == null) return "Unavailable";
        return payload.DisplayName;
    }

    public void Dispose() { }

    public override void Draw()
    {
        ImGui.Text($"Hello {this.CharacterName()}");
        ImGui.Text($"Your ID is {this.LodestoneId()}");
        ImGui.Text($"Equipped Item is {this.CurrentChestpiece()}");

        if (ImGui.Button("Show Settings"))
        {
            Plugin.ToggleConfigUI();
        }

        ImGui.Spacing();
    }
}
