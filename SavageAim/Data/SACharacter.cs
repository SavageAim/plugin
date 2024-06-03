using System.Text.Json.Serialization;

namespace SavageAimPlugin.Data;

public record class BISSummary (
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("id")] uint ID
);

public record class SACharacter(
    [property: JsonPropertyName("bis_lists")] BISSummary[] BISSummaries,
    [property: JsonPropertyName("id")] uint ID,
    [property: JsonPropertyName("lodestone_id")] string LodestoneID,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("world")] string World
);
