using System.Text.Json.Serialization;

namespace SavageAim.Data;

public record class SAJob (
    [property: JsonPropertyName("id")] string ID,
);