using System.Text.Json.Serialization;

namespace SavageAimPlugin.Data;

public record class ImportResponseSlotData(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("id")] uint ID
);

public record class ImportResponse(
    [property: JsonPropertyName("mainhand")] ImportResponseSlotData Mainhand,
    [property: JsonPropertyName("offhand")] ImportResponseSlotData Offhand,
    [property: JsonPropertyName("head")] ImportResponseSlotData Head,
    [property: JsonPropertyName("body")] ImportResponseSlotData Body,
    [property: JsonPropertyName("hands")] ImportResponseSlotData Hands,
    [property: JsonPropertyName("legs")] ImportResponseSlotData Legs,
    [property: JsonPropertyName("feet")] ImportResponseSlotData Feet,
    [property: JsonPropertyName("earrings")] ImportResponseSlotData Earrings,
    [property: JsonPropertyName("necklace")] ImportResponseSlotData Necklace,
    [property: JsonPropertyName("bracelet")] ImportResponseSlotData Bracelet,
    [property: JsonPropertyName("right_ring")] ImportResponseSlotData RightRing,
    [property: JsonPropertyName("left_ring")] ImportResponseSlotData LeftRing
);
