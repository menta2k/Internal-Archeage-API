using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAPI.BotClasses
{
    public enum ItemGrade
    {
        DONOTUSE,
        All,
        Grand,
        Rare,
        Arcane,
        Heroic,
        Unique,
        Celestial,
        Divine,
        Epic,
        Legendary,
        Mythic,
        Eternal,
    }

    public enum AuctionCategory : byte
    {
        // Token: 0x04000ADA RID: 2778
        Off,
        Weapon,
        Armor,
        Accessories,
        Instrument,
        Costume,
        Consumables,
        Crafting,
        Machining,
        Companions,
        Other,
        Lunagem,
        Lunastone,
        OneHandWeapon,
        TwoHandWeapon,
        RangedWeapon,
        Shield,
        ClothArmor,
        LeatherArmor,
        PlateArmor,
        Cloak,
        Earrings,
        Necklace,
        Ring,
        Flute,
        Lute,
        InstrumentCostume,
        Potion,
        Food,
        Drink,
        Tool,
        Explosive,
        Design,
        Talisman,
        Archeum,
        RegalOre,
        Materials,
        Animals,
        Plants,
        IndoorDecor,
        Books,

    }

    public enum AuctionSortType : byte
    {
        // Token: 0x04000ACB RID: 2763
        Off,
        // Token: 0x04000ACC RID: 2764
        Bet,
        // Token: 0x04000ACD RID: 2765
        Name,
        // Token: 0x04000ACE RID: 2766
        Level,
        // Token: 0x04000ACF RID: 2767
        Time,
        // Token: 0x04000AD0 RID: 2768
        BidPrice,
        // Token: 0x04000AD1 RID: 2769
        BuyNowPrice
    }

    public enum SortOrder : byte
    {
        // Token: 0x04000AD7 RID: 2775
        Asc,
        // Token: 0x04000AD8 RID: 2776
        Desc
    }

    public class Auction
    {
        public void SearchAuction(string itemName, bool isExactNameMatch = false, int page = 1, ItemGrade itemGrade = ItemGrade.All, AuctionCategory category = AuctionCategory.Off, int minPriceInCopper = 0, int maxPriceInCopper = 0, int minLevel = 0, int maxLevel = 0)
        {
            string searchQuery = string.Format("X2Auction:SearchAuctionArticle({0}, {1}, {2}, {3}, {4}, {5}, \"{6}\", \"{7}\", \"{8}\")", page, minLevel, maxLevel, (int)itemGrade, (int)category, isExactNameMatch.ToString().ToLower(), itemName, minPriceInCopper, maxPriceInCopper);
            System.Windows.Forms.MessageBox.Show(searchQuery);
            ArcheAPI.Instance.Lua.Execute(new Action(() =>
            {
                ArcheAPI.Instance.Lua.ExecuteLuaScript(ArcheAPI.Instance.Lua.auctionLuaState, searchQuery, (uint)Encoding.UTF8.GetByteCount(searchQuery), "SearchQuery");
            }));
        }
    }
}
