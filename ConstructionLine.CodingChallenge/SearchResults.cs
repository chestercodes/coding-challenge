using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge
{
    public class SearchResults
    {
        public List<Shirt> Shirts { get; set; } = new List<Shirt>();


        public List<SizeCount> SizeCounts { get; set; } = new List<SizeCount>();


        public List<ColourCount> ColourCounts { get; set; } = new List<ColourCount>();
    }


    public class SizeCount
    {
        public SizeCount(Size size, int count)
        {
            Size = size;
            Count = count;
        }

        public Size Size { get; }

        public int Count { get; }
    }


    public class ColourCount
    {
        public ColourCount(Colour colour, int count)
        {
            Colour = colour;
            Count = count;
        }

        public Colour Colour { get; }

        public int Count { get; }
    }
}