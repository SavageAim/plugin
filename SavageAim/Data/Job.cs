using System.Text.Json.Serialization;

namespace SavageAimPlugin.Data;

public record class SAJob (
    [property: JsonPropertyName("id")] string ID
);
