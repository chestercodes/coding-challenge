using System;
using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge
{
    /// <summary>
    /// Have converted these to enums to make the code nicer but also because https://stackoverflow.com/a/5558650/4854368 seems to suggest it might be quicker
    /// </summary>
    public enum Colour
    {
        Red,
        Blue,
        Yellow,
        White,
        Black
    }

    public enum Size
    {
        Small,
        Medium,
        Large
    }

    public class All
    {
        public static List<Colour> Colours = new List<Colour>()
        {
            Colour.Red,
            Colour.Blue,
            Colour.Yellow,
            Colour.White,
            Colour.Black
        };

        public static List<Size> Sizes = new List<Size>()
        {
            Size.Small,
            Size.Medium,
            Size.Large
        };
    }

}