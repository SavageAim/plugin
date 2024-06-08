using Dalamud.Logging;
using SavageAimPlugin.Data;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SavageAimPlugin;

public class SavageAimClient
{
    private static HttpClient GetClient(string apiKey)
    {
        var client = new HttpClient();
        // TODO - Replace with a proper API Key impl when it exists
        client.DefaultRequestHeaders.Add("Cookie", $"sessionid={apiKey}");
        return client;
    }

    public static async Task GetBisLists(string apiKey, uint charId)
    {
        try
        {
            using var client = GetClient(apiKey);
            var response = await client.GetAsync($"https://savageaim.com/backend/api/character/{charId}/");
            response.EnsureSuccessStatusCode();
            var characterDetails = await JsonSerializer.DeserializeAsync<SACharacterDetails>(response.Content.ReadAsStream());
            Service.BISListDataManager.SetData(characterDetails?.BISLists ?? []);
        }
        catch (HttpRequestException ex)
        {
            Service.PluginLog.Error($"Error Occurred when fetching BIS Lists: {ex.Message}");
        }
    }

    public static async Task GetCharacters(string apiKey)
    {
        try
        {
            using var client = GetClient(apiKey);
            var response = await client.GetAsync("https://savageaim.com/backend/api/character/");
            response.EnsureSuccessStatusCode();
            var charList = await JsonSerializer.DeserializeAsync<List<SACharacter>>(response.Content.ReadAsStream());
            Service.CharacterDataManager.SetData(charList ?? new());
        }
        catch (HttpRequestException ex)
        {
            Service.PluginLog.Error("Error Occurred when fetching Characters", ex.Message);
        }
    }

    public static async Task TestApiKey(string apiKey)
    {
        if (apiKey == "")
        {
            Service.APIKeyManager.SetKeyIsValid(false);
            return;
        }

        try
        {
            using var client = GetClient(apiKey);
            var response = await client.GetAsync("https://savageaim.com/backend/api/me/");
            response.EnsureSuccessStatusCode();
            // /me returns a valid response if you're not logged in. Token is valid if id is not null
            var userData = await JsonSerializer.DeserializeAsync<SAUser>(response.Content.ReadAsStream());
            Service.APIKeyManager.SetKeyIsValid(userData != null && userData.ID != null);
        }
        catch (HttpRequestException ex)
        {
            Service.PluginLog.Error("Error Occurred when testing API Key", ex.Message);
            Service.APIKeyManager.SetKeyIsValid(false);
        }
    }
}
