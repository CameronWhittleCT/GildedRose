using Xunit;
using GildedRose.Console;
using System.Collections.Generic;
using System.Linq;

namespace GildedRose.Tests
{
    public class GildededRoseShould
    {
        private List<Item> TestItems = new List<Item>
            {
                new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15,Quality = 20},
                new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
            };


        [Fact]
        public void UpdateDexterityVest()
        {
            var sut = new Program() {Items = TestItems};

            sut.UpdateQuality();

            Assert.Equal(9, sut.Items[0].SellIn);
            Assert.Equal(19, sut.Items[0].Quality);
        }

        [Fact]
        public void UpdateAgedBrie()
        {
            var sut = new Program() { Items = TestItems };

            sut.UpdateQuality();

            Assert.Equal(1, sut.Items[1].SellIn);
            Assert.Equal(1, sut.Items[1].Quality);
        }

        [Fact]
        public void UpdateElixirOfTheMongoose()
        {
            var sut = new Program() { Items = TestItems };

            sut.UpdateQuality();

            Assert.Equal(4, sut.Items[2].SellIn);
            Assert.Equal(6, sut.Items[2].Quality);
        }

        [Fact]
        public void UpdateSulfurasHandOfRagnaros()
        {
            var sut = new Program() { Items = TestItems };

            sut.UpdateQuality();

            Assert.Equal(0, sut.Items[3].SellIn);
            Assert.Equal(80, sut.Items[3].Quality);
        }

        [Fact]
        public void UpdateBackstagePasses()
        {
            var sut = new Program() { Items = TestItems };

            sut.UpdateQuality();

            Assert.Equal(14, sut.Items[4].SellIn);
            Assert.Equal(21, sut.Items[4].Quality);
        }

        [Fact]
        public void UpdateConjuredManaCake()
        {
            var sut = new Program() { Items = TestItems };

            sut.UpdateQuality();

            Assert.Equal(2, sut.Items[5].SellIn);
            Assert.Equal(4, sut.Items[5].Quality);
        }

        [Fact]
        public void NeverHaveNegativeQuality()
        {
            var sut = new Program() { Items = TestItems };

            foreach (var x in Enumerable.Range(0, 100))
            {
                sut.UpdateQuality();

                foreach (var item in sut.Items)
                {
                    Assert.True(item.Quality > -1);
                }
            }
        }

        [Fact]
        public void DegradeQualiltyFastAfterSellByDate()
        {
            var sut = new Program() { Items = TestItems };

            sut.UpdateQuality();
            sut.UpdateQuality();
            sut.UpdateQuality();
            sut.UpdateQuality();
            sut.UpdateQuality();
            sut.UpdateQuality();

            Assert.Equal(-1, sut.Items[2].SellIn);
            Assert.Equal(0, sut.Items[2].Quality);

        }

        [Fact]
        public void IncreaseBrieQualiltyFastAfterSellByDate()
        {
            var sut = new Program() { Items = TestItems };

            sut.UpdateQuality();
            sut.UpdateQuality();
            sut.UpdateQuality();

            Assert.Equal(-1, sut.Items[1].SellIn);
            Assert.Equal(4, sut.Items[1].Quality);

        }

        [Fact]
        public void IncreaseBackStagePassesApprochingDate()
        {
            var sut = new Program() { Items = TestItems };

            sut.UpdateQuality();
            sut.UpdateQuality();
            sut.UpdateQuality();
            sut.UpdateQuality();
            sut.UpdateQuality();

            Assert.Equal(10, sut.Items[4].SellIn);
            Assert.Equal(25, sut.Items[4].Quality);

            sut.UpdateQuality();

            Assert.Equal(9, sut.Items[4].SellIn);
            Assert.Equal(27, sut.Items[4].Quality);

            sut.UpdateQuality();
            sut.UpdateQuality();
            sut.UpdateQuality();
            sut.UpdateQuality();

            Assert.Equal(5, sut.Items[4].SellIn);
            Assert.Equal(35, sut.Items[4].Quality);

            sut.UpdateQuality();

            Assert.Equal(4, sut.Items[4].SellIn);
            Assert.Equal(38, sut.Items[4].Quality);

            sut.UpdateQuality();
            sut.UpdateQuality();
            sut.UpdateQuality();
            sut.UpdateQuality();

            Assert.Equal(0, sut.Items[4].SellIn);
            Assert.Equal(50, sut.Items[4].Quality);

            sut.UpdateQuality();

            Assert.Equal(-1, sut.Items[4].SellIn);
            Assert.Equal(0, sut.Items[4].Quality);
        }

    }
}