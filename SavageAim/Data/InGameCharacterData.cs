using System.Text;
using System.Text.Json.Serialization;
using FFXIVClientStructs.FFXIV.Client.Game;
using Lumina.Excel.GeneratedSheets;

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

public record class InGameGear(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("item_level")] uint ItemLevel
);

public class InGameCharacterData
{
    [JsonIgnore]
    public string Name { get; init; }
    
    [JsonIgnore]
    public string World { get; init; }
    
    [JsonIgnore]
    public string Job { get; init; }
    
    [JsonPropertyName("mainhand")]
    public InGameGear Mainhand { get; init; }
    
    [JsonPropertyName("offhand")]
    public InGameGear Offhand { get; init; }
    
    [JsonPropertyName("head")]
    public InGameGear Head { get; init; }
    
    [JsonPropertyName("body")]
    public InGameGear Body { get; init; }
    
    [JsonPropertyName("hands")]
    public InGameGear Hands { get; init; }
    
    [JsonPropertyName("legs")]
    public InGameGear Legs { get; init; }
    
    [JsonPropertyName("feet")]
    public InGameGear Feet { get; init; }
    
    [JsonPropertyName("earrings")]
    public InGameGear Earrings { get; init; }
    
    [JsonPropertyName("necklace")]
    public InGameGear Necklace { get; init; }
    
    [JsonPropertyName("bracelet")]
    public InGameGear Bracelet { get; init; }
    
    [JsonPropertyName("right_ring")]
    public InGameGear RightRing { get; init; }
    
    [JsonPropertyName("left_ring")]
    public InGameGear LeftRing { get; init; }

    public unsafe InGameCharacterData()
    {
        Name = Service.ClientState.LocalPlayer?.Name.ToString() ?? "";
        World = Service.ClientState.LocalPlayer?.HomeWorld.GameData?.Name.ToString() ?? "";
        Job = Service.ClientState.LocalPlayer?.ClassJob.GameData?.Abbreviation.ToString() ?? "";
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

        if (this.Job == "PLD")
        {
            Offhand = GetGearName(GearSlots.OFFHAND);
        }
        else
        {
            Offhand = Mainhand;
        }
    }

    private static unsafe InGameGear GetGearName(GearSlots slot)
    {
        var manager = InventoryManager.Instance();
        var item = manager->GetInventorySlot(InventoryType.EquippedItems, (int)slot);
        var id = item->ItemId;
        var data = Service.DataManager.GetExcelSheet<Item>().GetRow(id);
        if (data == null) return new InGameGear($"#{id}", id);
        return new InGameGear(data.Name, data.LevelItem.Value.RowId);
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine($"{Name}@{World}");
        builder.AppendLine($"{Job}");
        builder.AppendLine($"Mainhand: {Mainhand}");
        if (this.Job == "PLD") builder.AppendLine($"Offhand: {Offhand}");
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
