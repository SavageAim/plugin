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

    public static async Task<List<SACharacter>> GetCharacters(string apiKey)
    {
        try
        {
            using var client = GetClient(apiKey);
            var response = await client.GetAsync("https://savageaim.com/backend/api/character/");
            response.EnsureSuccessStatusCode();
            var charList = await JsonSerializer.DeserializeAsync<List<SACharacter>>(response.Content.ReadAsStream());
            return charList ?? new();
        }
        catch (HttpRequestException ex)
        {
            Service.PluginLog.Error("Error Occurred when fetching Characters", ex.Message);
        }
        return new();
    }

    public static async Task<List<Gear>> GetGear(string apiKey)
    {
        try
        {
            using var client = GetClient(apiKey);
            var response = await client.GetAsync("https://savageaim.com/backend/api/gear/");
            response.EnsureSuccessStatusCode();
            var gearList = await JsonSerializer.DeserializeAsync<List<Gear>>(response.Content.ReadAsStream());
            return gearList ?? new();
        }
        catch (HttpRequestException ex)
        {
            Service.PluginLog.Error("Error Occurred when fetching Gear", ex.Message);
        }
        return new();
    }

    public static async Task<bool> TestApiKey(string apiKey)
    {
        Service.PluginLog.Information($"Testing API Key: {apiKey}");
        try
        {
            using var client = GetClient(apiKey);
            var response = await client.GetAsync("https://savageaim.com/backend/api/me/");
            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (HttpRequestException ex)
        {
            Service.PluginLog.Error("Error Occurred when testing API Key", ex.Message);
        }
        return false;
    }
}
