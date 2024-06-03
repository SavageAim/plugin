using System;
using System.Text;
using ECommons.ExcelServices;
using ECommons.GameHelpers;
using FFXIVClientStructs.FFXIV.Client.Game;
using ImGuiNET;
using Lumina.Excel.GeneratedSheets;

namespace SavageAim;
public struct CharacterData
{
    public String name;
    public String world;
    public Job job;
    public String mainhand;
    public String offhand;
    public String head;
    public String body;
    public String hands;
    public String legs;
    public String feet;
    public String earrings;
    public String necklace;
    public String bracelet;
    public String rightRing;
    public String leftRing;

    private static unsafe String GetGearName(GearSlots slot)
    {
        InventoryManager* manager = InventoryManager.Instance();
        InventoryItem* item = manager->GetInventorySlot(InventoryType.EquippedItems, (int) slot);
        Item i = ExcelItemHelper.Get(item->ItemID);
        // var itemLevel = i.LevelItem.Value.RowId;
        String name = ExcelItemHelper.GetName(i);
        return name;
    }

    public static unsafe CharacterData Instance()
    {
        var data = new CharacterData
        {
            name = Player.Name,
            world = Player.HomeWorld,
            job = Player.Job,
            mainhand = CharacterData.GetGearName(GearSlots.MAINHAND),
            head = CharacterData.GetGearName(GearSlots.HEAD),
            body = CharacterData.GetGearName(GearSlots.BODY),
            hands = CharacterData.GetGearName(GearSlots.HANDS),
            legs = CharacterData.GetGearName(GearSlots.LEGS),
            feet = CharacterData.GetGearName(GearSlots.FEET),
            earrings = CharacterData.GetGearName(GearSlots.EARRINGS),
            necklace = CharacterData.GetGearName(GearSlots.NECKLACE),
            bracelet = CharacterData.GetGearName(GearSlots.BRACELET),
            rightRing = CharacterData.GetGearName(GearSlots.RIGHT_RING),
            leftRing = CharacterData.GetGearName(GearSlots.LEFT_RING),
        };

        if (data.job == Job.PLD) data.offhand = CharacterData.GetGearName(GearSlots.OFFHAND);
        return data;
    }

    public override String ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine($"{this.name}@{this.world}");
        builder.AppendLine($"{this.job}");
        builder.AppendLine($"Mainhand: {this.mainhand}");
        if (this.job == Job.PLD) builder.AppendLine($"Offhand: {this.offhand}");
        builder.AppendLine($"Head: {this.head}");
        builder.AppendLine($"Body: {this.body}");
        builder.AppendLine($"Hands: {this.hands}");
        builder.AppendLine($"Legs: {this.legs}");
        builder.AppendLine($"Feet: {this.feet}");
        builder.AppendLine($"Earrings: {this.earrings}");
        builder.AppendLine($"Necklace: {this.necklace}");
        builder.AppendLine($"Bracelet: {this.bracelet}");
        builder.AppendLine($"Right Ring: {this.rightRing}");
        builder.AppendLine($"Left Ring: {this.leftRing}");
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
        ImGui.Text(this.mainhand);
        if (this.job == Job.PLD)
        {
            ImGui.TableNextColumn();
            ImGui.Text("Offhand");
            ImGui.TableNextColumn();
            ImGui.Text(this.offhand);
        }

        // Head | Earrings
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.Text("Head");
        ImGui.TableNextColumn();
        ImGui.Text(this.head);
        ImGui.TableNextColumn();
        ImGui.Text("Earrings");
        ImGui.TableNextColumn();
        ImGui.Text(this.earrings);

        // Body | Necklace
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.Text("Body");
        ImGui.TableNextColumn();
        ImGui.Text(this.body);
        ImGui.TableNextColumn();
        ImGui.Text("Necklace");
        ImGui.TableNextColumn();
        ImGui.Text(this.necklace);

        // Hands | Bracelet
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.Text("Hands");
        ImGui.TableNextColumn();
        ImGui.Text(this.hands);
        ImGui.TableNextColumn();
        ImGui.Text("Bracelet");
        ImGui.TableNextColumn();
        ImGui.Text(this.bracelet);

        // Legs | Right Ring
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.Text("Legs");
        ImGui.TableNextColumn();
        ImGui.Text(this.legs);
        ImGui.TableNextColumn();
        ImGui.Text("Right Ring");
        ImGui.TableNextColumn();
        ImGui.Text(this.rightRing);

        // Feet | Left Ring
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.Text("Feet");
        ImGui.TableNextColumn();
        ImGui.Text(this.feet);
        ImGui.TableNextColumn();
        ImGui.Text("Left Ring");
        ImGui.TableNextColumn();
        ImGui.Text(this.leftRing);

        ImGui.EndTable();
    }
}
