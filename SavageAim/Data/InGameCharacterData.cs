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

public class InGameCharacterData
{
    public string Name { get; init; }
    public string World { get; init; }
    public Job Job { get; init; }
    public string Mainhand { get; init; }
    public string Offhand { get; init; }
    public string Head { get; init; }
    public string Body { get; init; }
    public string Hands { get; init; }
    public string Legs { get; init; }
    public string Feet { get; init; }
    public string Earrings { get; init; }
    public string Necklace { get; init; }
    public string Bracelet { get; init; }
    public string RightRing { get; init; }
    public string LeftRing { get; init; }

    public unsafe InGameCharacterData()
    {
        Name = Player.Name;
        World = Player.HomeWorld;
        Job = Player.Job;
        Mainhand = GetGearName(GearSlots.MAINHAND);
        Head = GetGearName(GearSlots.HEAD);
        Body = GetGearName(GearSlots.BODY);
        Hands = GetGearName(GearSlots.HANDS);
        Legs = GetGearName(GearSlots.LEGS);
        Feet = GetGearName(GearSlots.FEET);
        Earrings = GetGearName(GearSlots.EARRINGS);
        Necklace = GetGearName(GearSlots.NECKLACE);
        Bracelet = GetGearName(GearSlots.BRACELET);
        RightRing = GetGearName(GearSlots.RIGHT_RING);
        LeftRing = GetGearName(GearSlots.LEFT_RING);

        if (this.Job == Job.PLD)
        {
            Offhand = GetGearName(GearSlots.OFFHAND);
        }
        else
        {
            Offhand = Mainhand;
        }
    }

    private static unsafe string GetGearName(GearSlots slot)
    {
        var manager = InventoryManager.Instance();
        var item = manager->GetInventorySlot(InventoryType.EquippedItems, (int)slot);
        var i = ExcelItemHelper.Get(item->ItemID);
        // var itemLevel = i.LevelItem.Value.RowId;
        var name = i.GetName();
        return name;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine($"{Name}@{World}");
        builder.AppendLine($"{Job}");
        builder.AppendLine($"Mainhand: {Mainhand}");
        if (this.Job == Job.PLD) builder.AppendLine($"Offhand: {Offhand}");
        builder.AppendLine($"Head: {Head}");
        builder.AppendLine($"Body: {Body}");
        builder.AppendLine($"Hands: {Hands}");
        builder.AppendLine($"Legs: {Legs}");
        builder.AppendLine($"Feet: {Feet}");
        builder.AppendLine($"Earrings: {Earrings}");
        builder.AppendLine($"Necklace: {Necklace}");
        builder.AppendLine($"Bracelet: {Bracelet}");
        builder.AppendLine($"Right Ring: {RightRing}");
        builder.AppendLine($"Left Ring: {LeftRing}");
        return builder.ToString();
    }
}
