using System;

namespace ConstructionLine.CodingChallenge
{
    public class Shirt
    {
        public Guid Id { get; }

        public string Name { get; }

        public Size Size { get; }

        public Colour Colour { get; }

        public Shirt(Guid id, string name, Size size, Colour colour)
        {
            Id = id;
            Name = name;
            Size = size;
            Colour = colour;
        }
    }
}