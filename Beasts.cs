using System;
using System.Collections.Generic;
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

    public override void OnLoad()
    {
        Settings.FetchBeastPrices.OnPressed += async () => await FetchPrices();
        Task.Run(FetchPrices);
    }

    private async Task FetchPrices()
    {
        try
        {
            DebugWindow.LogMsg("Fetching Beast Prices from PoeNinja...");
            var prices = await PoeNinja.GetBeastsPrices();
            foreach (var beast in BeastsDatabase.AllBeasts)
            {
                Settings.BeastPrices[beast.DisplayName] = prices.TryGetValue(beast.DisplayName, out var price) ? price : -1;
            }

            Settings.LastUpdate = DateTime.Now;
        }
        catch (Exception e)
        {
            DebugWindow.LogError($"Failed to fetch Beast Prices from PoeNinja: {e.Message}");
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