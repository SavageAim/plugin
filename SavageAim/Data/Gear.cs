using System.Text.Json.Serialization;

namespace SavageAimPlugin.Data;

public record class Gear(
    [property: JsonPropertyName("has_accessories")] bool HasAccessories,
    [property: JsonPropertyName("has_armour")] bool HasArmour,
    [property: JsonPropertyName("has_weapon")] bool HasWeapon,
    [property: JsonPropertyName("id")] uint ID,
    [property: JsonPropertyName("item_level")] uint ItemLevel,
    [property: JsonPropertyName("name")] string Name
);
