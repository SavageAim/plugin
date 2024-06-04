using Dalamud.Logging;
using SavageAimPlugin.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SavageAimPlugin;

public class SavageAimClient
{
    public static async Task<List<BISList>> GetBisLists(String apiKey, uint charId)
    {
        try
        {
            using HttpClient client = new HttpClient();
            // TODO - Replace with a proper API Key impl when it exists
            client.DefaultRequestHeaders.Add("Cookie", $"sessionid={apiKey}");
            var response = await client.GetAsync($"https://savageaim.com/backend/api/character/{charId}/bis_lists/");
            response.EnsureSuccessStatusCode();
            var bisListList = await JsonSerializer.DeserializeAsync<List<BISList>>(response.Content.ReadAsStream());
            return bisListList ?? new();
        }
        catch (HttpRequestException ex)
        {
            PluginLog.Error("Error Occurred when fetching BIS Lists", ex.Message);
        }
        return new();
    }

    public static async Task<List<SACharacter>> GetCharacters(String apiKey)
    {
        try
        {
            using HttpClient client = new HttpClient();
            // TODO - Replace with a proper API Key impl when it exists
            client.DefaultRequestHeaders.Add("Cookie", $"sessionid={apiKey}");
            var response = await client.GetAsync("https://savageaim.com/backend/api/character/");
            response.EnsureSuccessStatusCode();
            var charList = await JsonSerializer.DeserializeAsync<List<SACharacter>>(response.Content.ReadAsStream());
            return charList ?? new();
        }
        catch (HttpRequestException ex)
        {
            PluginLog.Error("Error Occurred when fetching Characters", ex.Message);
        }
        return new();
    }

    public static async Task<List<Gear>> GetGear(String apiKey)
    {
        try
        {
            using HttpClient client = new HttpClient();
            // TODO - Replace with a proper API Key impl when it exists
            client.DefaultRequestHeaders.Add("Cookie", $"sessionid={apiKey}");
            var response = await client.GetAsync("https://savageaim.com/backend/api/gear/");
            response.EnsureSuccessStatusCode();
            var gearList = await JsonSerializer.DeserializeAsync<List<Gear>>(response.Content.ReadAsStream());
            return gearList ?? new();
        }
        catch (HttpRequestException ex)
        {
            PluginLog.Error("Error Occurred when fetching Gear", ex.Message);
        }
        return new();
    }

    public static async Task<bool> TestApiKey(String apiKey)
    {
        try
        {
            using HttpClient client = new HttpClient();
            // TODO - Replace with a proper API Key impl when it exists
            client.DefaultRequestHeaders.Add("Cookie", $"sessionid={apiKey}");
            var response = await client.GetAsync("https://savageaim.com/backend/api/me/");
            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (HttpRequestException ex)
        {
            PluginLog.Error("Error Occurred when testing API Key", ex.Message);
        }
        return false;
    }
}
