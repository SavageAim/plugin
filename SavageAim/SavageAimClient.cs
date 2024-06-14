using SavageAimPlugin.Data;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SavageAimPlugin;

public class SavageAimClient
{
    private static readonly string BaseUrl = "https://savageaim.com";

    private static HttpClient GetClient(string apiKey)
    {
        var client = new HttpClient();
        // TODO - Replace with a proper API Key impl when it exists
        client.DefaultRequestHeaders.Add("Cookie", $"sessionid=xx4gvk8g4blsp27hblfjlcq6cx51bklm");
        return client;
    }

    public static async Task GetBisLists(string apiKey, uint charId)
    {
        using var client = GetClient(apiKey);
        var response = await client.GetAsync($"{BaseUrl}/backend/api/character/{charId}/");
        response.EnsureSuccessStatusCode();
        var characterDetails = await JsonSerializer.DeserializeAsync<SACharacterDetails>(response.Content.ReadAsStream());
        Service.BISListDataManager.SetData(characterDetails?.BISLists ?? []);
    }

    public static async Task GetCharacters(string apiKey)
    {
        using var client = GetClient(apiKey);
        var response = await client.GetAsync($"{BaseUrl}/backend/api/character/");
        response.EnsureSuccessStatusCode();
        var charList = await JsonSerializer.DeserializeAsync<List<SACharacter>>(response.Content.ReadAsStream());
        Service.CharacterDataManager.SetData(charList ?? new());
    }

    public static async Task ImportCurrentGear(string apiKey)
    {
        var currentCharacter = Service.CharacterDataManager.InGameCharacter;
        if (currentCharacter == null)
        {
            Service.GearImportManager.SetData(null);
            return;
        }

        using var client = GetClient(apiKey);

        // Prepare request body
        string json = JsonSerializer.Serialize(currentCharacter);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync($"{BaseUrl}/backend/api/import/plugin/", content);
        response.EnsureSuccessStatusCode();
        var importResponse = await JsonSerializer.DeserializeAsync<ImportResponse> (response.Content.ReadAsStream());
        Service.GearImportManager.SetData(importResponse);
    }

    public static async Task TestApiKey(string apiKey)
    {
        if (apiKey == "")
        {
            Service.APIKeyManager.SetKeyIsValid(false);
            return;
        }

        using var client = GetClient(apiKey);
        var response = await client.GetAsync($"{BaseUrl}/backend/api/me/");
        response.EnsureSuccessStatusCode();
        // /me returns a valid response if you're not logged in. Token is valid if id is not null
        var userData = await JsonSerializer.DeserializeAsync<SAUser>(response.Content.ReadAsStream());
        Service.APIKeyManager.SetKeyIsValid(userData != null && userData.ID != null);
    }

    public static async Task UpdateBis(string apiKey, BISListModify data)
    {
        var currentCharacter = Service.CharacterDataManager.GetCurrentCharacterInSA();
        if (currentCharacter == null)
        {
            return;
        }
        using var client = GetClient(apiKey);

        // Prepare request body
        string json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PutAsync($"{BaseUrl}/backend/api/character/{currentCharacter.ID}/bis_lists/{data.ID}/", content);
        Service.PluginLog.Info(await response.Content.ReadAsStringAsync());
        response.EnsureSuccessStatusCode();
        Service.GearImportManager.FinishedSaving();
    }
}
