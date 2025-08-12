using System.Text.Json.Serialization;

namespace SavageAimPlugin.Data;

public record class BISSummary (
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("id")] uint ID
);

public record class BISList (
    [property: JsonPropertyName("bis_body")] Gear BisBody,
    [property: JsonPropertyName("bis_bracelet")] Gear BisBracelet,
    [property: JsonPropertyName("bis_earrings")] Gear BisEarrings,
    [property: JsonPropertyName("bis_feet")] Gear BisFeet,
    [property: JsonPropertyName("bis_hands")] Gear BisHands,
    [property: JsonPropertyName("bis_head")] Gear BisHead,
    [property: JsonPropertyName("bis_left_ring")] Gear BisLeftRing,
    [property: JsonPropertyName("bis_legs")] Gear BisLegs,
    [property: JsonPropertyName("bis_mainhand")] Gear BisMainhand,
    [property: JsonPropertyName("bis_necklace")] Gear BisNecklace,
    [property: JsonPropertyName("bis_offhand")] Gear BisOffhand,
    [property: JsonPropertyName("bis_right_ring")] Gear BisRightRing,

    [property: JsonPropertyName("current_body")] Gear CurrentBody,
    [property: JsonPropertyName("current_bracelet")] Gear CurrentBracelet,
    [property: JsonPropertyName("current_earrings")] Gear CurrentEarrings,
    [property: JsonPropertyName("current_feet")] Gear CurrentFeet,
    [property: JsonPropertyName("current_hands")] Gear CurrentHands,
    [property: JsonPropertyName("current_head")] Gear CurrentHead,
    [property: JsonPropertyName("current_left_ring")] Gear CurrentLeftRing,
    [property: JsonPropertyName("current_legs")] Gear CurrentLegs,
    [property: JsonPropertyName("current_mainhand")] Gear CurrentMainhand,
    [property: JsonPropertyName("current_necklace")] Gear CurrentNecklace,
    [property: JsonPropertyName("current_offhand")] Gear CurrentOffhand,
    [property: JsonPropertyName("current_right_ring")] Gear CurrentRightRing,

    [property: JsonPropertyName("display_name")] string DisplayName,
    [property: JsonPropertyName("external_link")] string URL,
    [property: JsonPropertyName("id")] uint ID,
    [property: JsonPropertyName("item_level")] uint ItemLevel,
    [property: JsonPropertyName("job")] SAJob Job,
    [property: JsonPropertyName("name")] string Name
);
