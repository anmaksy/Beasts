using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Beasts.Api;

public static class PoeNinja
{
    private const string LeaguesUrl = "https://poe.ninja/poe1/api/economy/leagues";
    private const string ItemOverviewUrl = "https://poe.ninja/poe1/api/economy/stash/current/item/overview";

    // The poe1 economy endpoint only ever lists PC leagues, but poe.ninja gives us no realm
    // field to rely on, so drop anything that looks like a console league as a safety net.
    private static readonly string[] NonPcLeagueMarkers = ["xbox", "sony", "playstation", "console"];

    private static readonly HttpClient SharedHttpClient = CreateHttpClient();

    private static HttpClient CreateHttpClient()
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.UserAgent.ParseAdd(
            "Beasts-ExileCore-Plugin/1.0 (+https://github.com/exApiTools/ExileApi-Compiled)");
        return client;
    }

    private class PoeNinjaLeague
    {
        [JsonProperty("id")] public string Id;
    }

    private class PoeNinjaLine
    {
        [JsonProperty("name")] public string Name;
        [JsonProperty("chaosValue")] public float ChaosValue;
    }

    private class PoeNinjaResponse
    {
        [JsonProperty("lines")] public List<PoeNinjaLine> Lines;
    }

    private static bool IsPcLeague(string id)
    {
        return !NonPcLeagueMarkers.Any(marker => id.Contains(marker, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// PC leagues poe.ninja currently tracks prices for, most recent league first.
    /// </summary>
    public static async Task<List<string>> GetLeagues()
    {
        var response = await SharedHttpClient.GetAsync(LeaguesUrl);
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Failed to get poe.ninja leagues (status {(int)response.StatusCode})");

        var json = await response.Content.ReadAsStringAsync();
        var leagues = JsonConvert.DeserializeObject<List<PoeNinjaLeague>>(json)
            ?.Select(l => l.Id)
            .Where(id => !string.IsNullOrEmpty(id) && IsPcLeague(id))
            .Distinct()
            .ToList();

        if (leagues is not { Count: > 0 }) throw new HttpRequestException("poe.ninja returned no active PC leagues");

        return leagues;
    }

    public static async Task<Dictionary<string, float>> GetBeastsPrices(string league)
    {
        if (string.IsNullOrWhiteSpace(league)) throw new ArgumentException("No league given", nameof(league));

        var response = await SharedHttpClient.GetAsync(
            $"{ItemOverviewUrl}?league={Uri.EscapeDataString(league)}&type=Beast");
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException(
                $"Failed to get poe.ninja beast prices for league '{league}' (status {(int)response.StatusCode})");

        var json = await response.Content.ReadAsStringAsync();
        var lines = JsonConvert.DeserializeObject<PoeNinjaResponse>(json)?.Lines;
        if (lines == null) throw new HttpRequestException("poe.ninja returned an unexpected response");

        var prices = new Dictionary<string, float>();
        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line.Name)) continue;
            prices[line.Name] = line.ChaosValue;
        }

        return prices;
    }
}
