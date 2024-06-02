using System;
using System.Text;
using ECommons.ExcelServices;
using ECommons.GameHelpers;
using FFXIVClientStructs.FFXIV.Client.Game;

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
        InventoryItem* chest = manager->GetInventorySlot(InventoryType.EquippedItems, (int) slot);
        return ExcelItemHelper.GetName(chest->GetItemId());
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
}
