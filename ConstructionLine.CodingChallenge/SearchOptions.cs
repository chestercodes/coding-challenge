using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge
{
    public class SearchOptions
    {
        public List<Size> Sizes { get; set; } = new List<Size>();

        public List<Colour> Colours { get; set; } = new List<Colour>();
    }
}