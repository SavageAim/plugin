using System.Text;
using ECommons.ExcelServices;
using ECommons.GameHelpers;
using FFXIVClientStructs.FFXIV.Client.Game;
using ImGuiNET;
using SavageAim;

namespace SavageAimPlugin.Data;

public enum GearSlots
{
	MAINHAND = 0,
    OFFHAND = 1,
	HEAD = 2,
    BODY = 3,
    HANDS = 4,
    LEGS = 6,
    FEET = 7,
    EARRINGS = 8,
    NECKLACE = 9,
    BRACELET = 10,
    RIGHT_RING = 11,
    LEFT_RING = 12,
}

public struct InGameCharacterData
{
    public string name;
    public string world;
    public Job job;
    public string mainhand;
    public string offhand;
    public string head;
    public string body;
    public string hands;
    public string legs;
    public string feet;
    public string earrings;
    public string necklace;
    public string bracelet;
    public string rightRing;
    public string leftRing;

    private static unsafe string GetGearName(GearSlots slot)
    {
        var manager = InventoryManager.Instance();
        var item = manager->GetInventorySlot(InventoryType.EquippedItems, (int)slot);
        var i = ExcelItemHelper.Get(item->ItemID);
        // var itemLevel = i.LevelItem.Value.RowId;
        var name = i.GetName();
        return name;
    }

    public static unsafe InGameCharacterData Instance()
    {
        var data = new InGameCharacterData
        {
            name = Player.Name,
            world = Player.HomeWorld,
            job = Player.Job,
            mainhand = GetGearName(GearSlots.MAINHAND),
            head = GetGearName(GearSlots.HEAD),
            body = GetGearName(GearSlots.BODY),
            hands = GetGearName(GearSlots.HANDS),
            legs = GetGearName(GearSlots.LEGS),
            feet = GetGearName(GearSlots.FEET),
            earrings = GetGearName(GearSlots.EARRINGS),
            necklace = GetGearName(GearSlots.NECKLACE),
            bracelet = GetGearName(GearSlots.BRACELET),
            rightRing = GetGearName(GearSlots.RIGHT_RING),
            leftRing = GetGearName(GearSlots.LEFT_RING),
        };

        if (data.job == Job.PLD) data.offhand = GetGearName(GearSlots.OFFHAND);
        return data;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine($"{name}@{world}");
        builder.AppendLine($"{job}");
        builder.AppendLine($"Mainhand: {mainhand}");
        if (job == Job.PLD) builder.AppendLine($"Offhand: {offhand}");
        builder.AppendLine($"Head: {head}");
        builder.AppendLine($"Body: {body}");
        builder.AppendLine($"Hands: {hands}");
        builder.AppendLine($"Legs: {legs}");
        builder.AppendLine($"Feet: {feet}");
        builder.AppendLine($"Earrings: {earrings}");
        builder.AppendLine($"Necklace: {necklace}");
        builder.AppendLine($"Bracelet: {bracelet}");
        builder.AppendLine($"Right Ring: {rightRing}");
        builder.AppendLine($"Left Ring: {leftRing}");
        return builder.ToString();
    }

    public void Draw()
    {
        ImGui.BeginTable($"currGear-{job}", 4, ImGuiTableFlags.BordersH | ImGuiTableFlags.BordersV);
        // Mainhand | Offhand (if required)
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader();
        ImGui.Text("Mainhand");
        ImGui.TableNextColumn();
        ImGui.Text(mainhand);
        if (job == Job.PLD)
        {
            ImGui.TableNextColumn();
            ImGui.TableHeader();
            ImGui.Text("Offhand");
            ImGui.TableNextColumn();
            ImGui.Text(offhand);
        }

        // Head | Earrings
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader();
        ImGui.Text("Head");
        ImGui.TableNextColumn();
        ImGui.Text(head);
        ImGui.TableNextColumn();
        ImGui.TableHeader();
        ImGui.Text("Earrings");
        ImGui.TableNextColumn();
        ImGui.Text(earrings);

        // Body | Necklace
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader();
        ImGui.Text("Body");
        ImGui.TableNextColumn();
        ImGui.Text(body);
        ImGui.TableNextColumn();
        ImGui.TableHeader();
        ImGui.Text("Necklace");
        ImGui.TableNextColumn();
        ImGui.Text(necklace);

        // Hands | Bracelet
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader();
        ImGui.Text("Hands");
        ImGui.TableNextColumn();
        ImGui.Text(hands);
        ImGui.TableNextColumn();
        ImGui.TableHeader();
        ImGui.Text("Bracelet");
        ImGui.TableNextColumn();
        ImGui.Text(bracelet);

        // Legs | Right Ring
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader();
        ImGui.Text("Legs");
        ImGui.TableNextColumn();
        ImGui.Text(legs);
        ImGui.TableNextColumn();
        ImGui.TableHeader();
        ImGui.Text("Right Ring");
        ImGui.TableNextColumn();
        ImGui.Text(rightRing);

        // Feet | Left Ring
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader();
        ImGui.Text("Feet");
        ImGui.TableNextColumn();
        ImGui.Text(feet);
        ImGui.TableNextColumn();
        ImGui.TableHeader();
        ImGui.Text("Left Ring");
        ImGui.TableNextColumn();
        ImGui.Text(leftRing);

        ImGui.EndTable();
    }
}
