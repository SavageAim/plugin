using System.Text.Json.Serialization;

namespace SavageAimPlugin.Data;

public record class SAUser(
    [property: JsonPropertyName("id")] uint? ID
);
