using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineTests
    {
        static readonly List<Shirt> ShirtData = new List<Shirt>
        {
            new Shirt(Guid.NewGuid(), "Small  - Black ", Size.Small,  Colour.Black ),
            new Shirt(Guid.NewGuid(), "Small  - Blue  ", Size.Small,  Colour.Blue  ),
            new Shirt(Guid.NewGuid(), "Small  - Red   ", Size.Small,  Colour.Red   ),
            new Shirt(Guid.NewGuid(), "Small  - White ", Size.Small,  Colour.White ),
            new Shirt(Guid.NewGuid(), "Small  - Yellow", Size.Small,  Colour.Yellow),
            new Shirt(Guid.NewGuid(), "Medium - Black",  Size.Medium, Colour.Black),
            new Shirt(Guid.NewGuid(), "Medium - Blue",   Size.Medium, Colour.Blue),
            new Shirt(Guid.NewGuid(), "Medium - Red",    Size.Medium, Colour.Red),
            new Shirt(Guid.NewGuid(), "Large  - Blue",   Size.Large,  Colour.Blue),
        };

        readonly SearchEngine searchEngine = new SearchEngine(ShirtData);

        [Test]
        public void SearchEngine_FindsSmall_WithAllColours()
        {
            RunTest(
                new SearchOptions
                {
                    Colours = new List<Colour> { },
                    Sizes = new List<Size> { Size.Small }
                },
                5
                );
        }

        [Test]
        public void SearchEngine_FindsSmallAndMedium_WithRedAndBlue()
        {
            RunTest(
                new SearchOptions
                {
                    Colours = new List<Colour> { Colour.Red, Colour.Blue },
                    Sizes = new List<Size> { Size.Small, Size.Medium }
                },
                4
            );
        }

        [Test]
        public void SearchEngine_FindsSmall_WithRed()
        {
            RunTest(
                new SearchOptions
                {
                    Colours = new List<Colour> { Colour.Red },
                    Sizes = new List<Size> { Size.Small }
                },
                1
            );
        }

        [Test]
        public void SearchEngine_FindsRed_WithAllSizes()
        {
            RunTest(
                new SearchOptions
                {
                    Colours = new List<Colour> { Colour.Red },
                    Sizes = new List<Size> { }
                },
                2
            );
        }

        [Test]
        public void SearchEngine_FindsAll()
        {
            RunTest(
                new SearchOptions
                {
                    Colours = new List<Colour> { },
                    Sizes = new List<Size> { }
                },
                9
            );
        }


        void RunTest(SearchOptions searchOptions, int count)
        {
            var results = searchEngine.Search(searchOptions);

            Assert.AreEqual(count, results.Shirts.Count);
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(searchOptions, results.SizeCounts);
            AssertColorCounts(searchOptions, results.ColourCounts);
        }

        void AssertResults(List<Shirt> shirts, SearchOptions options)
        {
            Assert.That(shirts, Is.Not.Null);

            var resultingShirtIds = shirts.Select(s => s.Id).ToList();

            foreach (var shirt in ShirtData)
            {
                if (options.Sizes.Contains(shirt.Size)
                    && options.Colours.Contains(shirt.Colour)
                    && !resultingShirtIds.Contains(shirt.Id))
                {
                    Assert.Fail($"'{shirt.Name}' with Size '{shirt.Size}' and Color '{shirt.Colour}' not found in results, " +
                                $"when selected sizes where '{string.Join(",", options.Sizes.Select(s => s))}' " +
                                $"and colors '{string.Join(",", options.Colours)}'");
                }
            }
        }


        void AssertSizeCounts(SearchOptions searchOptions, List<SizeCount> sizeCounts)
        {
            Assert.That(sizeCounts, Is.Not.Null);

            foreach (var size in All.Sizes)
            {
                var sizeCount = sizeCounts.SingleOrDefault(s => s.Size == size);
                Assert.That(sizeCount, Is.Not.Null, $"Size count for '{size}' not found in results");

                var expectedSizeCount = ShirtData
                    .Count(s => s.Size == size
                                && (!searchOptions.Sizes.Any() || searchOptions.Sizes.Contains(s.Size))
                                );

                Assert.That(sizeCount.Count, Is.EqualTo(expectedSizeCount),
                    $"Size count for '{sizeCount.Size}' showing '{sizeCount.Count}' should be '{expectedSizeCount}'");
            }
        }


        void AssertColorCounts(SearchOptions searchOptions, List<ColourCount> colorCounts)
        {
            Assert.That(colorCounts, Is.Not.Null);

            foreach (var colour in All.Colours)
            {
                var colorCount = colorCounts.SingleOrDefault(s => s.Colour == colour);
                Assert.That(colorCount, Is.Not.Null, $"Color count for '{colour}' not found in results");

                var expectedColorCount = ShirtData
                    .Count(c => c.Colour == colour
                                && (!searchOptions.Colours.Any() || searchOptions.Colours.Contains(c.Colour))
                                );

                Assert.That(colorCount.Count, Is.EqualTo(expectedColorCount),
                    $"Color count for '{colorCount.Colour}' showing '{colorCount.Count}' should be '{expectedColorCount}'");
            }
        }
    }
}
