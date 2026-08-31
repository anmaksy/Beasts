using System.Collections.Generic;
using System.Linq;

namespace Beasts.Data;

public static class BeastsDatabase
{
    private class BeastDefinition
    {
        public string DisplayName;
        public string[] Paths;
        public string[] Crafts;
    }

    // Harvest beasts appear under two metadata paths (the regular spawn and the Memory Line
    // variant), so crafts are declared once per beast and shared by all of its paths.
    private static readonly List<BeastDefinition> Definitions = new()
    {
        new BeastDefinition
        {
            DisplayName = "Vivid Watcher",
            Paths =
            [
                "Metadata/Monsters/LeagueHarvest/Green/HarvestSquidT3MemoryLine_",
                "Metadata/Monsters/LeagueHarvest/Green/HarvestSquidT3_",
            ],
            Crafts = ["Sacrifice an Exceptional Support Gem: for 3 high Level, high Quality Support Gems - Resultant gems are not Exceptional"]
        },
        new BeastDefinition
        {
            DisplayName = "Vivid Vulture",
            Paths =
            [
                "Metadata/Monsters/LeagueHarvest/Green/HarvestVultureParasiteT3MemoryLine",
                "Metadata/Monsters/LeagueHarvest/Green/HarvestVultureParasiteT3",
            ],
            Crafts = ["Transform an Item: Reroll a Synthesis Implicit Modifier on a Unique Item"]
        },
        new BeastDefinition
        {
            DisplayName = "Vivid Abberarach",
            Paths =
            [
                "Metadata/Monsters/LeagueHarvest/Green/HarvestPlatedScorpionT3MemoryLine",
                "Metadata/Monsters/LeagueHarvest/Green/HarvestPlatedScorpionT3",
            ],
            Crafts = ["Create an Item: Shaper Guardian, Elder Guardian or Conqueror Map"]
        },
        new BeastDefinition
        {
            DisplayName = "Wild Brambleback",
            Paths =
            [
                "Metadata/Monsters/LeagueHarvest/Red/HarvestBrambleHulkT3MemoryLine",
                "Metadata/Monsters/LeagueHarvest/Red/HarvestBrambleHulkT3",
            ],
            Crafts = ["Transform an Item: Adds 500m Experience to an Exceptional Support Gem"]
        },
        new BeastDefinition
        {
            DisplayName = "Wild Hellion Alpha",
            Paths =
            [
                "Metadata/Monsters/LeagueHarvest/Red/HarvestHellionT3MemoryLine",
                "Metadata/Monsters/LeagueHarvest/Red/HarvestHellionT3",
            ],
            Crafts = ["Modify Mods on an Item: Reroll a Watcher's Eye Modifier - Cannot reroll Maximum Life, Mana or Energy Shield modifiers"]
        },
        new BeastDefinition
        {
            DisplayName = "Wild Bristle Matron",
            Paths =
            [
                "Metadata/Monsters/LeagueHarvest/Red/HarvestBeastT3MemoryLine_",
                "Metadata/Monsters/LeagueHarvest/Red/HarvestBeastT3",
            ],
            Crafts = ["Modify Mods on an Item: Add a crafted Meta-modifier to a non-Unique Item"]
        },
        new BeastDefinition
        {
            DisplayName = "Primal Crushclaw",
            Paths =
            [
                "Metadata/Monsters/LeagueHarvest/Blue/HarvestNessaCrabT3MemoryLine_",
                "Metadata/Monsters/LeagueHarvest/Blue/HarvestNessaCrabT3",
            ],
            Crafts = ["Create an Item: A valuable Scarab"]
        },
        new BeastDefinition
        {
            DisplayName = "Primal Cystcaller",
            Paths =
            [
                "Metadata/Monsters/LeagueHarvest/Blue/HarvestGoatmanT3MemoryLine",
                "Metadata/Monsters/LeagueHarvest/Blue/HarvestGoatmanT3",
            ],
            Crafts = ["Create an Item: Nightmare Map"]
        },
        new BeastDefinition
        {
            DisplayName = "Primal Rhex Matriarch",
            Paths =
            [
                "Metadata/Monsters/LeagueHarvest/Blue/HarvestRhexT3MemoryLine",
                "Metadata/Monsters/LeagueHarvest/Blue/HarvestRhexT3",
            ],
            Crafts = ["Create an Item: Synthesis Unique Map"]
        },
        new BeastDefinition
        {
            DisplayName = "Black Mórrigan",
            Paths = ["Metadata/Monsters/LeagueAzmeri/GullGoliathBestiary_"],
            Crafts = ["BEST BEAST TO CATCH"]
        },
        new BeastDefinition
        {
            DisplayName = "Craicic Croaker",
            Paths = ["Metadata/Monsters/LeagueBestiary/GemFrogBestiary"],
            Crafts =
            [
                "Apply a Hinekora's Lock: To a Magic Item",
                "Create an Imprint: Of a Magic Item",
            ]
        },
        new BeastDefinition
        {
            DisplayName = "Fenumus, First of the Night",
            Paths = ["Metadata/Monsters/LeagueBestiary/SpiderPlatedBestiarySpiritBoss"],
            Crafts = ["Craft an Aspect Skill onto an Item: Aspect of the Spider skill"]
        },
        new BeastDefinition
        {
            DisplayName = "Farrul, First of the Plains",
            Paths = ["Metadata/Monsters/LeagueBestiary/TigerBestiarySpiritBoss"],
            Crafts = ["Craft an Aspect Skill onto an Item: Aspect of the Cat skill"]
        },
        new BeastDefinition
        {
            DisplayName = "Saqawal, First of the Sky",
            Paths = ["Metadata/Monsters/LeagueBestiary/MarakethBirdSpiritBoss"],
            Crafts = ["Craft an Aspect Skill onto an Item: Aspect of the Avian skill"]
        },
        new BeastDefinition
        {
            DisplayName = "Farric Frost Hellion Alpha",
            Paths = ["Metadata/Monsters/LeagueBestiary/HellionBestiary2"],
            Crafts =
            [
                "Create Currency Items: A Stack of 3 Orbs of Unmaking",
                "Fracture a Modifier: On a Rare Talisman with at least 4 modifiers - Does not work on Influenced or Fractured items",
            ]
        },
        new BeastDefinition
        {
            DisplayName = "Craiceann, First of the Deep",
            Paths = ["Metadata/Monsters/LeagueBestiary/NessaCrabBestiarySpiritBoss"],
            Crafts = ["Craft an Aspect Skill onto an Item: Aspect of the Crab skill"]
        },
        new BeastDefinition
        {
            DisplayName = "Farric Tiger Alpha",
            Paths = ["Metadata/Monsters/LeagueBestiary/TigerBestiary"],
            Crafts = ["Open a Portal: to Farrul's Den"]
        },
        new BeastDefinition
        {
            DisplayName = "Fenumal Plagued Arachnid",
            Paths = ["Metadata/Monsters/LeagueBestiary/SpiderPlagueBestiary"],
            Crafts =
            [
                "Create an Imprint: Of a Rare Talisman",
                "Fracture two Modifiers: On a Rare Talisman with at least 6 modifiers - Does not work on Influenced or Fractured items",
            ]
        },
        new BeastDefinition
        {
            DisplayName = "Fenumal Hybrid Arachnid",
            Paths = ["Metadata/Monsters/LeagueBestiary/SpiderPlatedBestiary"],
            Crafts = ["Open a Portal: to Fenumus' Lair"]
        },
        new BeastDefinition
        {
            DisplayName = "Saqawine Rhex",
            Paths = ["Metadata/Monsters/LeagueBestiary/Avians/MarakethBirdBestiary"],
            Crafts = ["Open a Portal: to Saqawal's Roost"]
        },
        new BeastDefinition
        {
            DisplayName = "Farric Wolf Alpha",
            Paths = ["Metadata/Monsters/LeagueBestiary/WolfBestiary"],
            Crafts = ["Modify Mods on an Item: Add a Prefix, Remove a Random Suffix - Only works on rare items"]
        },
        new BeastDefinition
        {
            DisplayName = "Craicic Spider Crab",
            Paths = ["Metadata/Monsters/LeagueBestiary/CrabSpiderBestiary"],
            Crafts = ["Open a Portal: to Craiceann's Cove"]
        },
        new BeastDefinition
        {
            DisplayName = "Craicic Vassal",
            Paths = ["Metadata/Monsters/LeagueBestiary/ParasiticSquidBestiary"],
            Crafts =
            [
                "Corrupt a Map: To have an Implicit Modifier",
                "Corrupt a Map: Twice",
            ]
        },
        new BeastDefinition
        {
            DisplayName = "Farric Lynx Alpha",
            Paths = ["Metadata/Monsters/LeagueBestiary/LynxBestiary"],
            Crafts = ["Modify Mods on an Item: Add a Suffix, Remove a Random Prefix - Only works on rare items"]
        },
        new BeastDefinition
        {
            DisplayName = "Craicic Maw",
            Paths = ["Metadata/Monsters/LeagueBestiary/FrogBestiary"],
            Crafts =
            [
                "Modify Mods on an Item: Add a Mod to a Shaper Item",
                "Modify Mods on an Item: Add a Mod to an Elder Item",
                "Modify Mods on an Item: Add a Mod to a Redeemer Item",
                "Modify Mods on an Item: Add a Mod to a Hunter Item",
                "Modify Mods on an Item: Add a Mod to a Crusader Item",
                "Modify Mods on an Item: Add a Mod to a Warlord Item",
            ]
        },
        new BeastDefinition
        {
            DisplayName = "Farric Ape",
            Paths = ["Metadata/Monsters/LeagueBestiary/MonkeyBloodBestiary"],
            Crafts = ["Create a Unique: Belt"]
        },
        new BeastDefinition
        {
            DisplayName = "Craicic Savage Crab",
            Paths = ["Metadata/Monsters/LeagueBestiary/CrabParasiteLargeBestiary_"],
            Crafts =
            [
                "Create a Unique: Item",
                "Modify Mods on an Item: Add a Mod to a Rare Map - Only works on rare maps",
            ]
        },
        new BeastDefinition
        {
            DisplayName = "Saqawine Vulture",
            Paths = ["Metadata/Monsters/LeagueBestiary/VultureBestiary"],
            Crafts = ["Create an Item: Fully-linked Six-socket Rare"]
        },
        new BeastDefinition
        {
            DisplayName = "Saqawine Cobra",
            Paths = ["Metadata/Monsters/LeagueBestiary/SnakeBestiary1"],
            Crafts =
            [
                "Create a Unique: Mace or Sceptre",
                "Modify Mods on an Item: Add a Mod to a Rare Map - Only works on rare maps",
            ]
        },
        new BeastDefinition
        {
            DisplayName = "Farric Pit Hound",
            Paths = ["Metadata/Monsters/LeagueBestiary/PitbullBestiary"],
            Crafts = ["Create an Item: Level 21 Corrupted Gem"]
        },
        new BeastDefinition
        {
            DisplayName = "Farric Ursa",
            Paths = ["Metadata/Monsters/LeagueBestiary/DropBearBestiary"],
            Crafts = ["Create a Unique: Body Armour"]
        },
        new BeastDefinition
        {
            DisplayName = "Fenumal Queen",
            Paths = ["Metadata/Monsters/LeagueBestiary/InsectSpawnerBestiary"],
            Crafts =
            [
                "Create a Unique: Staff",
                "Modify Mods on an Item: Add a Mod to a Redeemer Item",
            ]
        },
        new BeastDefinition
        {
            DisplayName = "Farric Goliath",
            Paths = ["Metadata/Monsters/LeagueBestiary/BestiarySpiker"],
            Crafts =
            [
                "Create a Unique: Bow",
                "Modify Mods on an Item: Add a Mod to a Crusader Item",
            ]
        },
        new BeastDefinition
        {
            DisplayName = "Fenumal Devourer",
            Paths = ["Metadata/Monsters/LeagueBestiary/RootSpiderBestiary_"],
            Crafts =
            [
                "Create a Unique: Shield or Quiver",
                "Modify Mods on an Item: Add a Mod to a Shaper Item",
            ]
        },
        new BeastDefinition
        {
            DisplayName = "Craicic Watcher",
            Paths = ["Metadata/Monsters/LeagueBestiary/SquidBestiary"],
            Crafts =
            [
                "Create a Unique: Claw or Dagger",
                "Modify Mods on an Item: Add a Mod to a Hunter Item",
            ]
        },
        new BeastDefinition
        {
            DisplayName = "Saqawine Chimeral",
            Paths = ["Metadata/Monsters/LeagueBestiary/IguanaBestiary"],
            Crafts = ["Create Currency Items: A Stack of 10 Random Currency"]
        },
        new BeastDefinition
        {
            DisplayName = "Farric Magma Hound",
            Paths = ["Metadata/Monsters/LeagueBestiary/HoundBestiary"],
            Crafts = ["Create an Item: 23% Quality Corrupted Gem"]
        },
        new BeastDefinition
        {
            DisplayName = "Farric Taurus",
            Paths = ["Metadata/Monsters/LeagueBestiary/BestiaryBull"],
            Crafts = ["Create a Unique: Map"]
        },
        new BeastDefinition
        {
            DisplayName = "Craicic Squid",
            Paths = ["Metadata/Monsters/LeagueBestiary/SeaWitchSpawnBestiary"],
            Crafts = ["Transform an Item: Reroll a Talisman Base Type - Retains Implicit and Explicit Modifiers"]
        },
        new BeastDefinition
        {
            DisplayName = "Craicic Shield Crab",
            Paths = ["Metadata/Monsters/LeagueBestiary/ShieldCrabBestiary"],
            Crafts =
            [
                "Create Currency Items: A Stack of 4 Jeweller's Orbs",
                "Create Currency Items: A Stack of 2 Orbs of Binding",
                "Modify an Item: to Have Maximum Possible Number of Sockets",
            ]
        },
        new BeastDefinition
        {
            DisplayName = "Craicic Sand Spitter",
            Paths = ["Metadata/Monsters/LeagueBestiary/SandSpitterBestiary"],
            Crafts =
            [
                "Create Currency Items: Orb of Fusing",
                "Create Currency Items: A Stack of 2 Orbs of Binding",
                "Modify an Item: to Have Maximum Possible Links",
            ]
        },
        new BeastDefinition
        {
            DisplayName = "Fenumal Widow",
            Paths = ["Metadata/Monsters/LeagueBestiary/Spider5Bestiary"],
            Crafts = ["Create a Unique: Gloves"]
        },
        new BeastDefinition
        {
            DisplayName = "Saqawine Rhoa",
            Paths = ["Metadata/Monsters/LeagueBestiary/RhoaBestiary"],
            Crafts = ["Create a Rare: Four-Linked Helmet"]
        },
        new BeastDefinition
        {
            DisplayName = "Fenumal Scorpion",
            Paths = ["Metadata/Monsters/LeagueBestiary/BlackScorpionBestiary"],
            Crafts =
            [
                "Convert this Unique Item: Into Another Random Unique Item",
                "Corrupt a Map: Twice",
            ]
        },
        new BeastDefinition
        {
            DisplayName = "Farric Flame Hellion Alpha",
            Paths = ["Metadata/Monsters/LeagueBestiary/HellionBestiary"],
            Crafts = ["Create a Unique: Ring"]
        },
        new BeastDefinition
        {
            DisplayName = "Farric Chieftain",
            Paths = ["Metadata/Monsters/LeagueBestiary/BestiaryMonkeyChiefBlood"],
            Crafts = ["Create a Unique: Amulet"]
        },
        new BeastDefinition
        {
            DisplayName = "Saqawine Retch",
            Paths = ["Metadata/Monsters/LeagueBestiary/KiwethBestiary"],
            Crafts = ["Create a Unique: Boots"]
        },
        new BeastDefinition
        {
            DisplayName = "Farric Goatman",
            Paths = ["Metadata/Monsters/LeagueBestiary/GoatmanLeapSlamBestiary"],
            Crafts = ["Create a Unique: Flask"]
        },
        new BeastDefinition
        {
            DisplayName = "Saqawine Blood Viper",
            Paths = ["Metadata/Monsters/LeagueBestiary/SnakeBestiary2"],
            Crafts =
            [
                "Create a Unique: Sword or Axe",
                "Modify Mods on an Item: Add a Mod to an Elder Item",
            ]
        },
        new BeastDefinition
        {
            DisplayName = "Fenumal Scrabbler",
            Paths = ["Metadata/Monsters/LeagueBestiary/SandLeaperBestiary"],
            Crafts =
            [
                "Create a Unique: Wand",
                "Modify Mods on an Item: Add a Mod to a Warlord Item",
            ]
        },
        new BeastDefinition
        {
            DisplayName = "Farric Gargantuan",
            Paths = ["Metadata/Monsters/LeagueBestiary/BeastCaveBestiary"],
            Crafts =
            [
                "Create a Unique: Helmet",
                "Create Currency Items: A Stack of 3 Orbs of Unmaking",
            ]
        }
    };

    public static readonly List<Beast> AllBeasts = Definitions
        .SelectMany(definition => definition.Paths.Select(path => new Beast
        {
            DisplayName = definition.DisplayName,
            Path = path,
            Crafts = definition.Crafts
        }))
        .ToList();

    public static readonly Dictionary<string, Beast> ByPath = AllBeasts.ToDictionary(beast => beast.Path);
}
