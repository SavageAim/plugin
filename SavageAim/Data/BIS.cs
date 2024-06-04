using System.Text.Json.Serialization;

namespace SavageAim.Data;

public record class BISSummary (
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("id")] uint ID
);

public record class BISList 
{
    [property: JsonPropertyName("bis_body")] 
    public Gear BisBody { get; set; }
    [property: JsonPropertyName("bis_bracelet")] 
    public Gear BisBracelet { get; set; }
    [property: JsonPropertyName("bis_earrings")]
    public Gear BisEarrings { get; set; }
    [property: JsonPropertyName("bis_feet")]
    public Gear BisFeet { get; set; }
    [property: JsonPropertyName("bis_hands")] 
    public Gear BisHands { get; set; }
    [property: JsonPropertyName("bis_head")]
    public Gear BisHead { get; set; }
    [property: JsonPropertyName("bis_left_ring")] 
    public Gear BisLeftRing { get; set; }
    [property: JsonPropertyName("bis_legs")] 
    public Gear BisLegs { get; set; }
    [property: JsonPropertyName("bis_mainhand")] 
    public Gear BisMainhand { get; set; }
    [property: JsonPropertyName("bis_necklace")] 
    public Gear BisNecklace { get; set; }
    [property: JsonPropertyName("bis_offhand")] 
    public Gear BisOffhand { get; set; }
    [property: JsonPropertyName("bis_right_ring")] 
    public Gear BisRightRing { get; set; }

    [property: JsonPropertyName("current_body")]
    public Gear CurrentBody { get; set; }
    [property: JsonPropertyName("current_bracelet")] 
    public Gear CurrentBracelet { get; set; }
    [property: JsonPropertyName("current_earrings")]
    public Gear CurrentEarrings { get; set; }
    [property: JsonPropertyName("current_feet")]
    public Gear CurrentFeet { get; set; }
    [property: JsonPropertyName("current_hands")]
    public Gear CurrentHands { get; set; }
    [property: JsonPropertyName("current_head")]
    public Gear CurrentHead { get; set; }
    [property: JsonPropertyName("current_left_ring")]
    public Gear CurrentLeftRing { get; set; }
    [property: JsonPropertyName("current_legs")]
    public Gear CurrentLegs { get; set; }
    [property: JsonPropertyName("current_mainhand")]
    public Gear CurrentMainhand { get; set; }
    [property: JsonPropertyName("current_necklace")]
    public Gear CurrentNecklace { get; set; }
    [property: JsonPropertyName("current_offhand")]
    public Gear CurrentOffhand { get; set; }
    [property: JsonPropertyName("current_right_ring")]
    public Gear CurrentRightRing { get; set; }

    [property: JsonPropertyName("display_name")]
    public string DisplayName { get; set; }
    [property: JsonPropertyName("id")]
    public uint ID { get; set; }
    [property: JsonPropertyName("item_level")]
    public uint ItemLevel { get; set; }
    [property: JsonPropertyName("job")]
    public Job job { get; set; }
    [property: JsonPropertyName("name")]
    public string Name { get; set; }

    public void Draw() 
    {
        ImGui.BeginTable($"bisTable-{this.ID}", 3, ImGuiTableFlags.BordersH | ImGuiTableFlags.BordersV);
        // Headers Row
        ImGui.TableHeadersRow();
        ImGui.TableNextColumn();
        ImGui.Text("Slot");
        ImGui.TableNextColumn();
        ImGui.Text("BIS");
        ImGui.TableNextColumn();
        ImGui.Text("Current");

        // Mainhand
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader();
        ImGui.Text("Mainhand");
        ImGui.TableNextColumn();
        ImGui.Text(this.BisMainhand.Name);
        ImGui.TableNextColumn();
        ImGui.Text(this.CurrentMainhand.Name);

        // Offhand
        if (job == Job.PLD)
        {
            ImGui.TableNextRow();
            ImGui.TableNextColumn();
            ImGui.TableHeader();
            ImGui.Text("Offhand");
            ImGui.TableNextColumn();
            ImGui.Text(this.BisOffhand.Name);
            ImGui.TableNextColumn();
            ImGui.Text(this.CurrentOffhand.Name);
        }

        // Head
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader();
        ImGui.Text("Head");
        ImGui.TableNextColumn();
        ImGui.Text(this.BisHead.Name);
        ImGui.TableNextColumn();
        ImGui.Text(this.CurrentHead.Name);

        // Body
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader();
        ImGui.Text("Body");
        ImGui.TableNextColumn();
        ImGui.Text(this.BisBody.Name);
        ImGui.TableNextColumn();
        ImGui.Text(this.CurrentBody.Name);

        // Hands
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader();
        ImGui.Text("Hands");
        ImGui.TableNextColumn();
        ImGui.Text(this.BisHands.Name);
        ImGui.TableNextColumn();
        ImGui.Text(this.CurrentHands.Name);

        // Legs
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader();
        ImGui.Text("Legs");
        ImGui.TableNextColumn();
        ImGui.Text(this.BisLegs.Name);
        ImGui.TableNextColumn();
        ImGui.Text(this.CurrentLegs.Name);

        // Feet
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader();
        ImGui.Text("Feet");
        ImGui.TableNextColumn();
        ImGui.Text(this.BisFeet.Name);
        ImGui.TableNextColumn();
        ImGui.Text(this.CurrentFeet.Name);

        // Earrings
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader();
        ImGui.Text("Earrings");
        ImGui.TableNextColumn();
        ImGui.Text(this.BisEarrings.Name);
        ImGui.TableNextColumn();
        ImGui.Text(this.CurrentEarrings.Name);

        // Necklace
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader();
        ImGui.Text("Necklace");
        ImGui.TableNextColumn();
        ImGui.Text(this.BisNecklace.Name);
        ImGui.TableNextColumn();
        ImGui.Text(this.CurrentNecklace.Name);

        // Bracelet
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader();
        ImGui.Text("Bracelet");
        ImGui.TableNextColumn();
        ImGui.Text(this.BisBracelet.Name);
        ImGui.TableNextColumn();
        ImGui.Text(this.CurrentBracelet.Name);

        // Right Ring
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader();
        ImGui.Text("Right Ring");
        ImGui.TableNextColumn();
        ImGui.Text(this.BisRightRing.Name);
        ImGui.TableNextColumn();
        ImGui.Text(this.CurrentRightRing.Name);

        // Left Ring
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        ImGui.TableHeader();
        ImGui.Text("Left Ring");
        ImGui.TableNextColumn();
        ImGui.Text(this.BisLeftRing.Name);
        ImGui.TableNextColumn();
        ImGui.Text(this.CurrentLeftRing.Name);

        ImGui.EndTable();
    }
    }
}