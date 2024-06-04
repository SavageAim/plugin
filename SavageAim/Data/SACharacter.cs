using System.Text.Json.Serialization;

namespace SavageAimPlugin.Data;

public record class SACharacter(
    [property: JsonPropertyName("bis_lists")] BISSummary[] BISSummaries,
    [property: JsonPropertyName("id")] uint ID,
    [property: JsonPropertyName("lodestone_id")] string LodestoneID,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("world")] string World
);

public record class SACharacterDetails(
    [property: JsonPropertyName("bis_lists")] BISList[] BISLists
);
