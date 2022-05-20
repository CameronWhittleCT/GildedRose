using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GildedRose.Console
{
    public class Program
    {
        internal IList<Item> Items;
        private int qualityModifier;

        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program()
            {
                Items = new List<Item>
                {
                    new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
                    new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 },
                    new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
                    new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
                    new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 },
                    new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
                }
            };

            app.UpdateQuality();

            System.Console.ReadKey();

        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                item.SellIn--;

                switch (item.Name)
                {
                    case "Sulfuras, Hand of Ragnaros":
                        item.SellIn++;
                        continue;

                    case "Aged Brie":
                        qualityModifier = 1;
                        break;

                    case "Backstage passes to a TAFKAL80ETC concert":

                        if (item.SellIn > -1)
                            qualityModifier = Math.Max(1, 3 - (item.SellIn / 5)); //the old max(min, calc) to enforce a min value trick
                        else
                            qualityModifier = -99999; //big number
                        break;

                    case "Conjured Mana Cake":
                        qualityModifier = -2;
                        break;

                    default:
                        qualityModifier = -1;
                        break;
                }

                //if we have passes the sell by date items degrate or appreciate twice as fast
                if (item.SellIn < 0)
                    qualityModifier *= 2;

                //adjust quality and ensure it does not drop below 0 or above 50
                item.Quality += qualityModifier;

                if (item.Quality < 0)
                    item.Quality = 0;

                if (item.Quality > 50)
                    item.Quality = 50;
            }
        }

    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

}
