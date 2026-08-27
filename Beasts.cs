using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Beasts.Api;
using Beasts.Data;
using ExileCore;
using ExileCore.PoEMemory.MemoryObjects;
using ExileCore.Shared.Enums;

namespace Beasts;

public partial class Beasts : BaseSettingsPlugin<BeastsSettings>
{
    private readonly Dictionary<long, Entity> _trackedBeasts = new();

    // Switching leagues and pressing the fetch button both kick off fetches from the render
    // thread, so keep them queued behind each other instead of writing prices concurrently.
    private readonly SemaphoreSlim _fetchLock = new(1, 1);

    public override void OnLoad()
    {
        RefreshTrackedBeastsFromDatabase();

        // Selecting a league in the dropdown refetches prices for it.
        Settings.League.OnValueSelected += _ => Task.Run(FetchPrices);
        Settings.FetchBeastPrices.OnPressed += () => Task.Run(RefreshLeaguesAndPrices);
        Task.Run(RefreshLeaguesAndPrices);
    }

    /// <summary>
    /// Enabled beasts are persisted with their craft list baked into the settings file, so
    /// re-resolve them against the database on load. Without this, beasts that were already
    /// enabled keep showing the craft text they were saved with, even after it is corrected
    /// here, and beasts whose metadata path is gone would linger forever.
    /// </summary>
    private void RefreshTrackedBeastsFromDatabase()
    {
        Settings.Beasts = Settings.Beasts
            .Select(beast => BeastsDatabase.ByPath.GetValueOrDefault(beast.Path))
            .Where(beast => beast != null)
            .Distinct()
            .ToList();
    }

    private async Task RefreshLeaguesAndPrices()
    {
        // Refetching prices is left to OnValueSelected when the selected league changed,
        // so the same fetch does not run twice.
        if (!await RefreshLeagues()) await FetchPrices();
    }

    /// <summary>
    /// Repopulates the league dropdown and returns whether the selected league changed.
    /// </summary>
    private async Task<bool> RefreshLeagues()
    {
        try
        {
            var leagues = await PoeNinja.GetLeagues();
            Settings.League.Values = leagues;

            // Fall back to the current league when nothing is selected yet, or when the
            // saved league is no longer tracked by poe.ninja.
            if (leagues.Contains(Settings.League.Value)) return false;

            // The setter fires OnValueSelected, which refetches prices for the new league.
            Settings.League.Value = leagues[0];
            return true;
        }
        catch (Exception e)
        {
            DebugWindow.LogError($"Failed to fetch leagues from PoeNinja: {e.Message}");
            return false;
        }
    }

    private async Task FetchPrices()
    {
        await _fetchLock.WaitAsync();
        try
        {
            // The dropdown is empty until the league list has been fetched at least once.
            var league = Settings.League.Value;
            if (string.IsNullOrWhiteSpace(league)) league = (await PoeNinja.GetLeagues())[0];

            DebugWindow.LogMsg($"Fetching Beast Prices from PoeNinja for league '{league}'...");
            var prices = await PoeNinja.GetBeastsPrices(league);
            foreach (var beast in BeastsDatabase.AllBeasts)
            {
                Settings.BeastPrices[beast.DisplayName] = prices.TryGetValue(beast.DisplayName, out var price) ? price : -1;
            }

            Settings.LastUpdate = DateTime.Now;
            Settings.LastUpdateLeague = league;
        }
        catch (Exception e)
        {
            DebugWindow.LogError($"Failed to fetch Beast Prices from PoeNinja: {e.Message}");
        }
        finally
        {
            _fetchLock.Release();
        }
    }

    public override void AreaChange(AreaInstance area)
    {
        _trackedBeasts.Clear();
    }

    public override void EntityAdded(Entity entity)
    {
        if (entity.Rarity != MonsterRarity.Rare) return;
        if (BeastsDatabase.ByPath.ContainsKey(entity.Metadata))
        {
            _trackedBeasts[entity.Id] = entity;
        }
    }

    public override void EntityRemoved(Entity entity)
    {
        _trackedBeasts.Remove(entity.Id);
    }
}