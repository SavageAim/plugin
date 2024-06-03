using System;
using System.Text;
using ECommons.ExcelServices;
using ECommons.GameHelpers;
using FFXIVClientStructs.FFXIV.Client.Game;
using ImGuiNET;

namespace SavageAim;
public struct CharacterData
{
    public String name;
    public String world;
    public String job;
    public String currentMainhand;
    public String currentOffhand;
    public String currentHead;
    public String currentBody;
    public String currentHands;
    public String currentLegs;
    public String currentFeet;
    public String currentEarrings;
    public String currentNecklace;
    public String currentBracelet;
    public String currentRightRing;
    public String currentLeftRing;

    private static unsafe String GetGearName(GearSlots slot)
    {
        InventoryManager* manager = InventoryManager.Instance();
        InventoryItem* item = manager->GetInventorySlot(InventoryType.EquippedItems, (int) slot);
        return ExcelItemHelper.GetName(item->ItemID).Split(" ")[0];
    }

    public static unsafe CharacterData Instance()
    {
        var data = new CharacterData
        {
            name = Player.Name,
            world = Player.HomeWorld,
            job = Player.Job.ToString(),
            currentMainhand = CharacterData.GetGearName(GearSlots.MAINHAND),
            currentHead = CharacterData.GetGearName(GearSlots.HEAD),
            currentBody = CharacterData.GetGearName(GearSlots.BODY),
            currentHands = CharacterData.GetGearName(GearSlots.HANDS),
            currentLegs = CharacterData.GetGearName(GearSlots.LEGS),
            currentFeet = CharacterData.GetGearName(GearSlots.FEET),
            currentEarrings = CharacterData.GetGearName(GearSlots.EARRINGS),
            currentNecklace = CharacterData.GetGearName(GearSlots.NECKLACE),
            currentBracelet = CharacterData.GetGearName(GearSlots.BRACELET),
            currentRightRing = CharacterData.GetGearName(GearSlots.RIGHT_RING),
            currentLeftRing = CharacterData.GetGearName(GearSlots.LEFT_RING),
        };

        if (data.job == "PLD") data.currentOffhand = CharacterData.GetGearName(GearSlots.OFFHAND);
        return data;
    }

    public override String ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine($"{this.name}@{this.world}");
        builder.AppendLine($"{this.job}");
        builder.AppendLine($"Mainhand: {this.currentMainhand}");
        if (this.job == "PLD") builder.AppendLine($"Offhand: {this.currentOffhand}");
        builder.AppendLine($"Head: {this.currentHead}");
        builder.AppendLine($"Body: {this.currentBody}");
        builder.AppendLine($"Hands: {this.currentHands}");
        builder.AppendLine($"Legs: {this.currentLegs}");
        builder.AppendLine($"Feet: {this.currentFeet}");
        builder.AppendLine($"Earrings: {this.currentEarrings}");
        builder.AppendLine($"Necklace: {this.currentNecklace}");
        builder.AppendLine($"Bracelet: {this.currentBracelet}");
        builder.AppendLine($"Right Ring: {this.currentRightRing}");
        builder.AppendLine($"Left Ring: {this.currentLeftRing}");
        return builder.ToString();
    }

    public void Draw()
    {
        ImGui.BeginTable($"currGear-{this.job}", 4, ImGuiTableFlags.BordersH | ImGuiTableFlags.BordersV);
        // Mainhand | Offhand (if required)
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.Text("Mainhand");
        ImGui.TableNextColumn();
        ImGui.Text(this.currentMainhand);
        if (this.job == "PLD")
        {
            ImGui.TableNextColumn();
            ImGui.Text("Offhand");
            ImGui.TableNextColumn();
            ImGui.Text(this.currentOffhand);
        }

        // Head | Earrings
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.Text("Head");
        ImGui.TableNextColumn();
        ImGui.Text(this.currentHead);
        ImGui.TableNextColumn();
        ImGui.Text("Earrings");
        ImGui.TableNextColumn();
        ImGui.Text(this.currentEarrings);

        // Body | Necklace
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.Text("Body");
        ImGui.TableNextColumn();
        ImGui.Text(this.currentBody);
        ImGui.TableNextColumn();
        ImGui.Text("Necklace");
        ImGui.TableNextColumn();
        ImGui.Text(this.currentNecklace);

        // Hands | Bracelet
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.Text("Hands");
        ImGui.TableNextColumn();
        ImGui.Text(this.currentHands);
        ImGui.TableNextColumn();
        ImGui.Text("Bracelet");
        ImGui.TableNextColumn();
        ImGui.Text(this.currentBracelet);

        // Legs | Right Ring
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.Text("Legs");
        ImGui.TableNextColumn();
        ImGui.Text(this.currentLegs);
        ImGui.TableNextColumn();
        ImGui.Text("Right Ring");
        ImGui.TableNextColumn();
        ImGui.Text(this.currentRightRing);

        // Feet | Left Ring
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.Text("Feet");
        ImGui.TableNextColumn();
        ImGui.Text(this.currentFeet);
        ImGui.TableNextColumn();
        ImGui.Text("Left Ring");
        ImGui.TableNextColumn();
        ImGui.Text(this.currentLeftRing);

        ImGui.EndTable();
    }
}
