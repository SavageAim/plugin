using System.Text.Json.Serialization;

namespace SavageAimPlugin.Data;

public class BISListModify
{
    [JsonPropertyName("job_id")]
    public string Job {  get; init; }

    [JsonPropertyName("bis_mainhand_id")]
    public uint BisMainhand { get; init; }

    [JsonPropertyName("bis_offhand_id")]
    public uint BisOffhand { get; init; }

    [JsonPropertyName("bis_head_id")]
    public uint BisHead { get; init; }

    [JsonPropertyName("bis_body_id")]
    public uint BisBody { get; init; }

    [JsonPropertyName("bis_hands_id")]
    public uint BisHands { get; init; }

    [JsonPropertyName("bis_legs_id")]
    public uint BisLegs { get; init; }

    [JsonPropertyName("bis_feet_id")]
    public uint BisFeet { get; init; }

    [JsonPropertyName("bis_earrings_id")]
    public uint BisEarrings { get; init; }

    [JsonPropertyName("bis_necklace_id")]
    public uint BisNecklace { get; init; }

    [JsonPropertyName("bis_bracelet_id")]
    public uint BisBracelet { get; init; }

    [JsonPropertyName("bis_right_ring_id")]
    public uint BisRightRing { get; init; }

    [JsonPropertyName("bis_left_ring_id")]
    public uint BisLeftRing { get; init; }

    [JsonPropertyName("current_mainhand_id")]
    public uint CurrentMainhand { get; init; }

    [JsonPropertyName("current_offhand_id")]
    public uint CurrentOffhand { get; init; }

    [JsonPropertyName("current_head_id")]
    public uint CurrentHead { get; init; }

    [JsonPropertyName("current_body_id")]
    public uint CurrentBody { get; init; }

    [JsonPropertyName("current_hands_id")]
    public uint CurrentHands { get; init; }

    [JsonPropertyName("current_legs_id")]
    public uint CurrentLegs { get; init; }

    [JsonPropertyName("current_feet_id")]
    public uint CurrentFeet { get; init; }

    [JsonPropertyName("current_earrings_id")]
    public uint CurrentEarrings { get; init; }

    [JsonPropertyName("current_necklace_id")]
    public uint CurrentNecklace { get; init; }

    [JsonPropertyName("current_bracelet_id")]
    public uint CurrentBracelet { get; init; }

    [JsonPropertyName("current_right_ring_id")]
    public uint CurrentRightRing { get; init; }

    [JsonPropertyName("current_left_ring_id")]
    public uint CurrentLeftRing { get; init; }

    [JsonPropertyName("external_link")]
    public string URL { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonIgnore]
    public uint ID { get; init; }

    public BISListModify(BISList bisData, ImportResponse currentData)
    {
        this.ID = bisData.ID;
        this.Job = bisData.Job.ID;
        this.Name = bisData.Name;
        this.URL = bisData.URL;
        this.BisMainhand = bisData.BisMainhand.ID;
        this.BisOffhand = bisData.BisOffhand.ID;
        this.BisHead = bisData.BisHead.ID;
        this.BisBody = bisData.BisBody.ID;
        this.BisHands = bisData.BisHands.ID;
        this.BisLegs = bisData.BisLegs.ID;
        this.BisFeet = bisData.BisFeet.ID;
        this.BisEarrings = bisData.BisEarrings.ID;
        this.BisNecklace = bisData.BisNecklace.ID;
        this.BisBracelet = bisData.BisBracelet.ID;
        this.BisRightRing = bisData.BisRightRing.ID;
        this.BisLeftRing = bisData.BisLeftRing.ID;

        this.CurrentMainhand = currentData.Mainhand?.ID ?? bisData.CurrentMainhand.ID;
        this.CurrentOffhand = currentData.Offhand?.ID ?? bisData.CurrentOffhand.ID;
        this.CurrentHead = currentData.Head?.ID ?? bisData.CurrentHead.ID;
        this.CurrentBody = currentData.Body?.ID ?? bisData.CurrentBody.ID;
        this.CurrentHands = currentData.Hands?.ID ?? bisData.CurrentHands.ID;
        this.CurrentLegs = currentData.Legs?.ID ?? bisData.CurrentLegs.ID;
        this.CurrentFeet = currentData.Feet?.ID ?? bisData.CurrentFeet.ID;
        this.CurrentEarrings = currentData.Earrings?.ID ?? bisData.CurrentEarrings.ID;
        this.CurrentNecklace = currentData.Necklace?.ID ?? bisData.CurrentNecklace.ID;
        this.CurrentBracelet = currentData.Bracelet?.ID ?? bisData.CurrentBracelet.ID;
        this.CurrentRightRing = currentData.RightRing?.ID ?? bisData.CurrentRightRing.ID;
        this.CurrentLeftRing = currentData.LeftRing?.ID ?? bisData.CurrentLeftRing.ID;
    }
}
