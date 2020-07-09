using System;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly Dictionary<Guid, Shirt> _shirts;

        private HashSet<Guid> SmallShirtIds = new HashSet<Guid>();
        private HashSet<Guid> MediumShirtIds = new HashSet<Guid>();
        private HashSet<Guid> LargeShirtIds = new HashSet<Guid>();

        private HashSet<Guid> RedShirtIds = new HashSet<Guid>();
        private HashSet<Guid> BlueShirtIds = new HashSet<Guid>();
        private HashSet<Guid> YellowShirtIds = new HashSet<Guid>();
        private HashSet<Guid> WhiteShirtIds = new HashSet<Guid>();
        private HashSet<Guid> BlackShirtIds = new HashSet<Guid>();
        
        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts.ToDictionary(x => x.Id); // assumes unique guids, seems fair assumption

            foreach(var (id, shirt) in _shirts)
            {
                if (shirt.Colour == Colour.Red)    RedShirtIds.Add(id);
                if (shirt.Colour == Colour.Blue)   BlueShirtIds.Add(id);
                if (shirt.Colour == Colour.Yellow) YellowShirtIds.Add(id);
                if (shirt.Colour == Colour.White)  WhiteShirtIds.Add(id);
                if (shirt.Colour == Colour.Black)  BlackShirtIds.Add(id);

                if (shirt.Size == Size.Small)  SmallShirtIds.Add(id);
                if (shirt.Size == Size.Medium) MediumShirtIds.Add(id);
                if (shirt.Size == Size.Large)  LargeShirtIds.Add(id);
            }
        }


        public SearchResults Search(SearchOptions options)
        {
            var colourCounts = new List<ColourCount>();

            if (options.Colours.Count == 0)
            {
                // if no filters are selected then include all colours
                options.Colours = All.Colours;
            }

            var shirtIdsWithSelectedColours = options.Colours.Aggregate(new HashSet<Guid>(), (agg, el) =>
            {
                var hashset = GetColourHashSet(el);
                
                colourCounts.Add(new ColourCount(el, hashset.Count));
                agg.UnionWith(hashset);

                return agg;
            });

            var sizeCounts = new List<SizeCount>();
            
            if (options.Sizes.Count == 0)
            {
                // if no filters are selected then include all sizes
                options.Sizes = All.Sizes;
            }

            var shirtIdsWithSelectedSizes = options.Sizes.Aggregate(new HashSet<Guid>(), (agg, el) =>
            {
                var hashset = GetSizeHashSet(el);

                sizeCounts.Add(new SizeCount(el, hashset.Count));
                agg.UnionWith(hashset);

                return agg;
            });

            // the combination of the filtered colours and sizes are the search results.
            var shirts = shirtIdsWithSelectedColours
                    .Intersect(shirtIdsWithSelectedSizes)
                    .Select(x => _shirts[x])
                    .ToList();

            var missingSizes = All.Sizes.Where(x => sizeCounts.Select(sc => sc.Size).ToList().Contains(x) == false);
            var missingColours = All.Colours.Where(x => colourCounts.Select(sc => sc.Colour).ToList().Contains(x) == false);

            return new SearchResults
            {
                Shirts = shirts,
                
                ColourCounts = colourCounts.Concat(
                    missingColours.Select(x => new ColourCount(x, 0))
                    ).ToList(),
                
                SizeCounts = sizeCounts.Concat(
                    missingSizes.Select(x => new SizeCount(x, 0))
                    ).ToList()
            };
        }

        private HashSet<Guid> GetColourHashSet(Colour colour)
        {
            if (colour == Colour.Black) { return BlackShirtIds; }
            if (colour == Colour.Blue) { return BlueShirtIds; }
            if (colour == Colour.Red) { return RedShirtIds; }
            if (colour == Colour.White) { return WhiteShirtIds; }
            if (colour == Colour.Yellow) { return YellowShirtIds; }
            throw new NotImplementedException($"Don't know about colour {colour}");
        }

        private HashSet<Guid> GetSizeHashSet(Size size)
        {
            if (size == Size.Small) { return SmallShirtIds; }
            if (size == Size.Medium) { return MediumShirtIds; }
            if (size == Size.Large) { return LargeShirtIds; }
            throw new NotImplementedException($"Don't know about size {size}");
        }
    }
}