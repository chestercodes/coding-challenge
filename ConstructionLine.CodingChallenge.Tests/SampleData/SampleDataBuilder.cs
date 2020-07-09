﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge.Tests.SampleData
{
    public class SampleDataBuilder
    {
        private readonly int _numberOfShirts;

        private readonly Random _random = new Random();

        
        public SampleDataBuilder(int numberOfShirts)
        {
            _numberOfShirts = numberOfShirts;
        }


        public List<Shirt> CreateShirts()
        {
            return Enumerable.Range(0, _numberOfShirts)
                .Select(i => new Shirt(Guid.NewGuid(), $"Shirt {i}", GetRandomSize(), GetRandomColour()))
                .ToList();
        }

       
        private Size GetRandomSize()
        {
            
            var sizes = All.Sizes;
            var index = _random.Next(0, sizes.Count);
            return sizes.ElementAt(index);
        }


        private Colour GetRandomColour()
        {
            var colors = All.Colours;
            var index = _random.Next(0, colors.Count);
            return colors.ElementAt(index);
        }
    }
}